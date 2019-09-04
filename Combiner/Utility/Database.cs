using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Combiner
{
	public class Database
	{
		private readonly string m_CreaturesCollectionName = "creatures";
		private readonly string m_SavedCreaturesCollectionName = "saved_creatures";

		public Database()
		{
			if (!Directory.Exists(DirectoryConstants.DatabaseDirectory))
			{
				Directory.CreateDirectory(DirectoryConstants.DatabaseDirectory);
			}
		}

		/// <summary>
		/// Gets the name of all collections in the database
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> GetCollectionNames()
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				return db.GetCollectionNames();
			}
		}

		/// <summary>
		/// Gets all creatures from the creatures collection.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Creature> GetAllCreatures()
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (!db.CollectionExists(m_CreaturesCollectionName))
				{
					return new List<Creature>();
				}

				var collection = db.GetCollection<Creature>(m_CreaturesCollectionName);
				var creatures = collection.FindAll(); // Can use Skip/Take to do paging...
				return creatures.ToList();
			}
		}

		/// <summary>
		/// Gets a list of creatures given a query.
		/// </summary>
		/// <returns></returns>
		public List<Creature> GetCreatureQuery(Query query)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (!db.CollectionExists(m_CreaturesCollectionName))
				{
					return new List<Creature>();
				}

				var collection = db.GetCollection<Creature>(m_CreaturesCollectionName);
				List<Creature> creatures = collection.Find(query).ToList();
				return creatures;
			}
		}

		/// <summary>
		/// Gets a specific creature from the creatures collection
		/// using the given stock and body parts.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="bodyParts"></param>
		/// <returns></returns>
		public Creature GetCreature(string left, string right, Dictionary<string, string> bodyParts)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (!db.CollectionExists(m_CreaturesCollectionName))
				{
					return null;
				}

				var collection = db.GetCollection<Creature>(m_CreaturesCollectionName);
				var result = collection.Find(FindCreatureQuery(left, right, bodyParts));
				return result.First();
			}
		}

		/// <summary>
		/// Gets the total number of creatures in the database
		/// </summary>
		/// <returns></returns>
		public int GetTotalCreatureCount()
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (!db.CollectionExists(m_CreaturesCollectionName))
				{
					return 0;
				}

				var collection = db.GetCollection <Creature>(m_CreaturesCollectionName);
				return collection.Count();
			}
		}

		/// <summary>
		/// Function used for testing import/export.
		/// Could continue to use in the application
		/// </summary>
		public void DeleteSavedCreatures()
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (db.CollectionExists(m_SavedCreaturesCollectionName))
				{
					db.DropCollection(m_SavedCreaturesCollectionName);
				}
			}
		}

		/// <summary>
		/// Gets the creatures from the saved creatures collection
		/// </summary>
		/// <returns></returns>
		public List<Creature> GetSavedCreatures()
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (!db.CollectionExists(m_SavedCreaturesCollectionName))
				{
					return new List<Creature>();
				}

				var collection = db.GetCollection<Creature>(m_SavedCreaturesCollectionName);
				List<Creature> savedCreatures = collection.FindAll().ToList();
				return savedCreatures;
			}
		}

		/// <summary>
		/// Adds the creatures to the saved creatures collection
		/// </summary>
		/// <param name="creatures"></param>
		public void SaveCreatures(IEnumerable<Creature> creatures)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				// Creates collection if necessary
				var collection = db.GetCollection<Creature>(m_SavedCreaturesCollectionName);
				collection.InsertBulk(creatures);
			}
		}

		/// <summary>
		/// Adds the creature to the saved creatures collection
		/// </summary>
		/// <param name="creature"></param>
		public void SaveCreature(Creature creature)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				// Creates collection if necessary
				var collection = db.GetCollection<Creature>(m_SavedCreaturesCollectionName);
				bool creatureExists = collection.Exists(FindCreatureQuery(creature.Left, creature.Right, creature.BodyParts));
				if (!creatureExists)
				{
					collection.Insert(creature);
				}
			}
		}

		/// <summary>
		/// Removes the creature from the saved creatures collection
		/// </summary>
		/// <param name="creature"></param>
		public void UnsaveCreature(Creature creature)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				// No need to unsave if there aren't any saved creatures
				if (!db.CollectionExists(m_SavedCreaturesCollectionName))
				{
					return;
				}

				var collection = db.GetCollection<Creature>(m_SavedCreaturesCollectionName);
				collection.Delete(FindCreatureQuery(creature.Left, creature.Right, creature.BodyParts));
			}
		}

		private Query FindCreatureQuery(string left, string right, Dictionary<string, string> bodyParts)
		{
			return
				Query.And(
					Query.Or(
						Query.Or(
							Query.EQ("Left", left),
							Query.EQ("Right", right)),
						Query.Or(
							Query.EQ("Left", right),
							Query.EQ("Right", left))),
					Query.Where("BodyParts", (x => HasSameBodyParts(x, bodyParts))));
		}

		private bool HasSameBodyParts(BsonValue x, Dictionary<string, string> bodyParts)
		{
			bool same = true;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
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

		public bool Exists()
		{
			if (File.Exists(DirectoryConstants.DatabaseString))
			{
				using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
				{
					var collectionExists = db.CollectionExists(m_CreaturesCollectionName);
					return collectionExists
						? db.GetCollection<Creature>(m_CreaturesCollectionName).Count() > 0
						: false;
				}
			}
			return false;
		}

		/// <summary>
		/// Creates the main creature database and saved database. This will delete whatever
		/// is currently in both databases.
		/// </summary>
		public void CreateDB()
		{
			Directory.CreateDirectory(DirectoryConstants.DatabaseDirectory);

			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (db.CollectionExists(m_CreaturesCollectionName))
				{
					db.DropCollection(m_CreaturesCollectionName);
				}
				if (db.CollectionExists(m_SavedCreaturesCollectionName))
				{
					db.DropCollection(m_SavedCreaturesCollectionName);
				}

				var collection = db.GetCollection<Creature>(m_CreaturesCollectionName);
				CreateCreatures(collection);

				// Setup indexes
				// May not need if not querying to filter
				// These caused a 3GB spike in memory usage
				//collection.EnsureIndex(x => x.Rank);
				//collection.EnsureIndex(x => x.Abilities);
			}
		}

		/// <summary>
		/// Creates a collection with the given name.
		/// </summary>
		/// <param name="name"></param>
		public void CreateCollection(string name)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				db.GetCollection<Creature>(name); // Does this create a collection if one doesn't exist?
			}
		}

		/// <summary>
		/// Function used for testing import/export.
		/// Could continue to use in the application
		/// </summary>
		public void DeleteCollection(string name)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (db.CollectionExists(name))
				{
					db.DropCollection(name);
				}
			}
		}

		private void CreateCreatures(LiteCollection<Creature> collection)
		{
			var stockNames = Directory.GetFiles(DirectoryConstants.StockDirectory)
				.Select(s => s.Replace(".lua", "")
				.Replace(DirectoryConstants.StockDirectory, ""))
				.ToList();

			CreatureCombiner creatureCombiner = new CreatureCombiner(stockNames);

			for (int i = 0; i < stockNames.Count(); i++)
			{
				for (int j = i + 1; j < stockNames.Count(); j++)
				{
					List<Creature> creatures = creatureCombiner
						.CreateAllPossibleCreatures(
							StockNames.ProperStockNames[stockNames[i]],
							StockNames.ProperStockNames[stockNames[j]]);
					collection.InsertBulk(creatures);
				}
			}
		}

	}
}
