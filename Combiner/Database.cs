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

		public static Creature GetCreature(string left, string right, Dictionary<string, string> bodyParts)
		{
			using (var db = new LiteDatabase(Utility.DatabaseString))
			{
				if (!db.CollectionExists("creatures"))
				{
					return null;
				}

				var collection = db.GetCollection<Creature>("creatures");
				var result = collection
					.Find(Query.And(
					Query.Or(
						Query.EQ("Left", left),
						Query.EQ("Right", right)),
					Query.Or(
						Query.EQ("Left", right),
						Query.EQ("Right", left))))
					.Where(x => x.BodyParts.Values.SequenceEqual(bodyParts.Values));
				return result.First();
			}
		}

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

		public static void SaveCreature(Creature creature)
		{
			using (var db = new LiteDatabase(Utility.DatabaseString))
			{
				if (!db.CollectionExists("saved_creatures"))
				{
					CreateSavedCreatures();
					//return;
				}

				var collection = db.GetCollection<Creature>("saved_creatures");
				collection.Insert(creature);
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
