using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Combiner
{
	public static class Database
	{

		/// <summary>
		/// Gets all creatures from the creatures collection
		/// </summary>
		/// <returns></returns>
		public static List<Creature> GetAllCreatures()
		{
			using (var db = new LiteDatabase(Utility.DatabaseString))
			{
				if (!db.CollectionExists("creatures"))
				{
					return new List<Creature>();
				}

				var collection = db.GetCollection<Creature>("creatures");
				List<Creature> creatures = collection.FindAll().ToList();
				return creatures;
			}
		}

		/// <summary>
		/// Gets a specific creature from the creatures colleciton
		/// using the given stock and body parts
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="bodyParts"></param>
		/// <returns></returns>
		public static Creature GetCreature(string left, string right, Dictionary<string, string> bodyParts)
		{
			using (var db = new LiteDatabase(Utility.DatabaseString))
			{
				if (!db.CollectionExists("creatures"))
				{
					return null;
				}

				// TODO: Currently finding duplicates
				var collection = db.GetCollection<Creature>("creatures");
				var result = collection
					.Find(Query.And(
					Query.Or(
					Query.And(
						Query.EQ("Left", left),
						Query.EQ("Right", right)),
					Query.And(
						Query.EQ("Left", right),
						Query.EQ("Right", left))),
					Query.Where("BodyParts", (x => HasSameBodyParts(x, bodyParts)))
					));
				return result.First();
			}
		}

		/// <summary>
		/// Function used for testing import/export.
		/// Could continue to use in the application
		/// </summary>
		public static void DeleteSavedCreatures()
		{
			using (var db = new LiteDatabase(Utility.DatabaseString))
			{
				if (db.CollectionExists("saved_creatures"))
				{
					var collection = db.GetCollection<Creature>("saved_creatures");
					collection.Delete(Query.All());
				}
			}
		}

		/// <summary>
		/// Gets the creatures from the saved creatures collection
		/// </summary>
		/// <returns></returns>
		public static List<Creature> GetSavedCreatures()
		{
			using (var db = new LiteDatabase(Utility.DatabaseString))
			{
				if (!db.CollectionExists("saved_creatures"))
				{
					return new List<Creature>();
				}

				var collection = db.GetCollection<Creature>("saved_creatures");
				List<Creature> savedCreatures = collection.FindAll().ToList();
				return savedCreatures;
			}
		}

		/// <summary>
		/// Adds the creatures to the saved creatures collection
		/// </summary>
		/// <param name="creatures"></param>
		public static void SaveCreatures(IEnumerable<Creature> creatures)
		{
			using (var db = new LiteDatabase(Utility.DatabaseString))
			{
				if (!db.CollectionExists("saved_creatures"))
				{
					CreateSavedCreatures();
					//return;
				}

				var collection = db.GetCollection<Creature>("saved_creatures");
				collection.InsertBulk(creatures);
			}
		}

		/// <summary>
		/// Adds the creature to the saved creatures collection
		/// </summary>
		/// <param name="creature"></param>
		public static void SaveCreature(Creature creature)
		{
			using (var db = new LiteDatabase(Utility.DatabaseString))
			{
				if (!db.CollectionExists("saved_creatures"))
				{
					CreateSavedCreatures();
					//return;
				}

				// TODO: Don't allow duplicate creature to be saved
				var collection = db.GetCollection<Creature>("saved_creatures");
				bool exists = collection.Exists(Query.And(
					Query.Or(
					Query.And(
						Query.EQ("Left", creature.Left),
						Query.EQ("Right", creature.Right)),
					Query.And(
						Query.EQ("Left", creature.Right),
						Query.EQ("Right", creature.Left))),
					Query.Where("BodyParts", (x => HasSameBodyParts(x, creature.BodyParts)))
					));
				if (!exists)
				{
					collection.Insert(creature);
				}
			}
		}

		private static bool HasSameBodyParts(BsonValue x, Dictionary<string, string> bodyParts)
		{
			bool same = true;
			foreach(Limb limb in Enum.GetValues(typeof(Limb)))
			{
				if (limb == Limb.Nothing || limb == Limb.General)
				{
					continue;
				}
				same = x.AsDocument.Get(limb.ToString()).First().AsString
					== bodyParts[limb.ToString()];
				if (!same)
				{
					break;
				}
			}

			return same;
		}


		/// <summary>
		/// Removes the creature from the saved creatures collection
		/// </summary>
		/// <param name="creature"></param>
		public static void UnsaveCreature(Creature creature)
		{
			using (var db = new LiteDatabase(Utility.DatabaseString))
			{
				if (!db.CollectionExists("saved_creatures"))
				{
					//CreateSavedCreatures();
					return;
				}

				var collection = db.GetCollection<Creature>("saved_creatures");
				collection.Delete(Query.And(
					Query.Or(
					Query.Or(
						Query.EQ("Left", creature.Left),
						Query.EQ("Right", creature.Right)),
					Query.Or(
						Query.EQ("Left", creature.Right),
						Query.EQ("Right", creature.Left))),
					Query.Where("BodyParts", (x => HasSameBodyParts(x, creature.BodyParts)))
					));
			}
		}



		public static void CreateDB()
		{
			using (var db = new LiteDatabase(Utility.DatabaseString))
			{
				if (db.CollectionExists("creatures"))
				{
					db.DropCollection("creatures");
				}
				if (db.CollectionExists("saved_creatures"))
				{
					db.DropCollection("saved_creatures");
				}

				var collection = db.GetCollection<Creature>("creatures");
				CreateCreatures(collection);

				// Setup indexes
				// May not need if not querying to filter
				collection.EnsureIndex(x => x.Rank);
				collection.EnsureIndex(x => x.Abilities);
			}
		}

		private static void CreateCreatures(LiteCollection<Creature> collection)
		{
			var stockNames = Directory.GetFiles(Utility.StockDirectory).
						Select(s => s.Replace(".lua", "").Replace(Utility.StockDirectory, "")).ToList();

			for (int i = 0; i < stockNames.Count(); i++)
			{
				for (int j = i + 1; j < stockNames.Count(); j++)
				{
					InsertIntoCollection(collection, stockNames[i], stockNames[j]);
				}
			}
		}

		private static void InsertIntoCollection(LiteCollection<Creature> collection, string leftName, string rightName)
		{
			LuaHandler lua = new LuaHandler();
			List<Creature> creatures = new List<Creature>();
			Stock left = StockFactory.Instance.CreateStock(leftName, lua);
			Stock right = StockFactory.Instance.CreateStock(rightName, lua);
			foreach (var creature in CreatureCombiner.Combine(left, right))
			{
				lua.LoadScript(creature);
				creatures.Add(creature.BuildCreature());
			}
			collection.InsertBulk(creatures);
		}

		private static void CreateSavedCreatures()
		{
			using (var db = new LiteDatabase(Utility.DatabaseString))
			{
				if (db.CollectionExists("saved_creatures"))
				{
					return;
				}

				db.GetCollection<Creature>("saved_creatures");
			}
		}
	}
}
