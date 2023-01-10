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
		public readonly string m_CreaturesCollectionName = "creatures";
		private readonly string m_SavedCreaturesCollectionName = "saved_creatures";

		private readonly string m_ModTableName = "mod_table";

		private static Dictionary<string, IEnumerable<Creature>> creatureCache;

		public Database()
		{
			if (!Directory.Exists(DirectoryConstants.DatabaseDirectory))
			{
				Directory.CreateDirectory(DirectoryConstants.DatabaseDirectory);
			}
			creatureCache = new Dictionary<string, IEnumerable<Creature>>();
		}

		public bool CreateModTable()
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (db.CollectionExists(m_ModTableName))
				{
					return false;
				}

				var collection = db.GetCollection<ModCollection>(m_ModTableName);

				return true;
			}
		}

		public IEnumerable<ModCollection> GetAllMods()
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (!db.CollectionExists(m_ModTableName))
				{
					return new List<ModCollection>();
				}

				var collection = db.GetCollection<ModCollection>(m_ModTableName);
				var mods = collection.FindAll();
				return mods.ToList();
			}
		}

		public ModCollection GetMod(string name)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (!db.CollectionExists(m_ModTableName))
				{
					return null;
				}

				var collection = db.GetCollection<ModCollection>(m_ModTableName);
				var mod = collection.FindOne(x => x.ModName == name);
				return mod;
			}
		}

		public bool CreateMod(string name, string attrPath, string stockPath)
		{
			string tableName = name + "_main";
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (!db.CollectionExists(m_ModTableName))
				{
					CreateModTable();
				}
				if (db.CollectionExists(tableName))
				{
					return false;
				}

				ModCollection mod = new ModCollection()
				{
					ModName = name,
					IsMain = true,
					CollectionName = tableName,
					AttrPath = attrPath,
					StockPath = stockPath
				};

				// TODO: Probably need more checks here...
				var collection = db.GetCollection<ModCollection>(m_ModTableName);
				collection.Insert(mod);
				CreateDB(tableName, attrPath, stockPath);
				return true; 
			}
		}

		public ModCollection getMainMod(string modName)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (!db.CollectionExists(m_ModTableName))
				{
					return null;
				}

				var collection = db.GetCollection<ModCollection>(m_ModTableName);
				var mainMod = collection.FindOne(x => x.IsMain && x.ModName == modName);
				return mainMod;
			}
		}

		public IEnumerable<string> GetMainModNames()
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (!db.CollectionExists(m_ModTableName))
				{
					return new List<string>();
				}
				var collection = db.GetCollection<ModCollection>(m_ModTableName);
				var mainMods = collection.Find(x => x.IsMain);
				return mainMods.Select(x => x.ModName).ToList();
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
				var names = db.GetCollectionNames().ToList();
				names.Remove(m_ModTableName);
				return names;
			}
		}

		/// <summary>
		/// Creates a collection with the given name.
		/// The collection will not be created if the name is taken.
		/// </summary>
		/// <param name="name"></param>
		public bool CreateCollection(string collectionName, string modName)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (db.CollectionExists(collectionName)
					|| !db.CollectionExists(m_ModTableName))
				{
					return false;
				}

				
				var mainMod = getMainMod(modName);
				if (mainMod == null)
				{
					return false;
				}

				var collection = db.GetCollection<Creature>(collectionName);
				// Need to insert or ensure index for collection to be created
				collection.EnsureIndex(x => x.Rank);

				var modTable = db.GetCollection<ModCollection>(m_ModTableName);
				ModCollection modCollection = new ModCollection()
				{
					ModName = modName,
					IsMain = false,
					CollectionName = collectionName,
					StockPath = mainMod.StockPath,
					AttrPath = mainMod.AttrPath
				};
				modTable.Insert(modCollection);

				return true;
			}
		}

		/// <summary>
		/// Function used for testing import/export.
		/// Could continue to use in the application
		/// </summary>
		public void DeleteCollection(ModCollection modCollection)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (db.CollectionExists(modCollection.CollectionName))
				{
					db.DropCollection(modCollection.CollectionName);
				}
			}
		}

		public bool RenameCollection(ModCollection modCollection, string newName)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (db.CollectionExists(newName))
				{
					return false;
				}
				if (db.CollectionExists(modCollection.CollectionName))
				{
					db.RenameCollection(modCollection.CollectionName, newName);
					return true;
				}
				return false;
			}
		}

		//public int GetCount(string collectionName)
		//{
		//	using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
		//	{
		//		if (!db.CollectionExists(collectionName))
		//		{
		//			return -1;
		//		}

		//		var collection = db.GetCollection<Creature>(collectionName);
		//		return collection.Count();
		//	}
		//}

		/// <summary>
		/// Gets all creatures from the creatures collection.
		/// </summary>
		/// <returns></returns>
		//public IEnumerable<Creature> GetAllCreatures()
		//{
		//	using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
		//	{
		//		if (!db.CollectionExists(m_CreaturesCollectionName))
		//		{
		//			return new List<Creature>();
		//		}

		//		var collection = db.GetCollection<Creature>(m_CreaturesCollectionName);
		//		var creatures = collection.FindAll(); // Can use Skip/Take to do paging...
		//		return creatures.ToList();
		//	}
		//}

		/// <summary>
		/// Gets all creatures from the given creatures collection.
		/// </summary>
		/// <returns></returns>
		
		public IEnumerable<Creature> GetAllCreatures(ModCollection mod)
		{
			if (creatureCache.ContainsKey(mod.CollectionName))
			{
				return creatureCache[mod.CollectionName];
			}

			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (!db.CollectionExists(mod.CollectionName))
				{
					return new List<Creature>();
				}

				var collection = db.GetCollection<Creature>(mod.CollectionName);
				var creatures = collection.FindAll(); // Can use Skip/Take to do paging...
				var creaturesList = creatures.ToList();
				creatureCache.Add(mod.CollectionName, creaturesList);
				return creaturesList;
			}
		}

		/// <summary>
		/// Gets a list of creatures given a query.
		/// </summary>
		/// <returns></returns>
		//public List<Creature> GetCreatureQuery(Query query)
		//{
		//	using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
		//	{
		//		if (!db.CollectionExists(m_CreaturesCollectionName))
		//		{
		//			return new List<Creature>();
		//		}

		//		var collection = db.GetCollection<Creature>(m_CreaturesCollectionName);
		//		List<Creature> creatures = collection.Find(query).ToList();
		//		return creatures;
		//	}
		//}

		/// <summary>
		/// Query for a list of creates from the given collection.
		/// </summary>
		/// <returns></returns>
		public List<Creature> GetCreatureQuery(Query query, ModCollection mod)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (!db.CollectionExists(mod.CollectionName))
				{
					return new List<Creature>();
				}

				var collection = db.GetCollection<Creature>(mod.CollectionName);
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
		public Creature GetCreature(string left, string right, Dictionary<string, string> bodyParts, ModCollection modCollection)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (!db.CollectionExists(modCollection.CollectionName))
				{
					return null;
				}

				var collection = db.GetCollection<Creature>(modCollection.CollectionName);
				var result = collection.Find(FindCreatureQuery(left, right, bodyParts));
				return result.First();
			}
		}

		/// <summary>
		/// Gets the total number of creatures in the database
		/// </summary>
		/// <returns></returns>
		public int GetTotalCreatureCount(ModCollection modCollection)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (!db.CollectionExists(modCollection.CollectionName))
				{
					return 0;
				}

				var collection = db.GetCollection <Creature>(modCollection.CollectionName);
				return collection.Count();
			}
		}

		/// <summary>
		/// Function used for testing import/export.
		/// Could continue to use in the application
		/// </summary>
		//public void DeleteSavedCreatures()
		//{
		//	using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
		//	{
		//		if (db.CollectionExists(m_SavedCreaturesCollectionName))
		//		{
		//			db.DropCollection(m_SavedCreaturesCollectionName);
		//		}
		//	}
		//}

		/// <summary>
		/// Gets the creatures from the saved creatures collection
		/// </summary>
		/// <returns></returns>
		//public List<Creature> GetSavedCreatures()
		//{
		//	using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
		//	{
		//		if (!db.CollectionExists(m_SavedCreaturesCollectionName))
		//		{
		//			return new List<Creature>();
		//		}

		//		var collection = db.GetCollection<Creature>(m_SavedCreaturesCollectionName);
		//		List<Creature> savedCreatures = collection.FindAll().ToList();
		//		return savedCreatures;
		//	}
		//}

		/// <summary>
		/// Adds the creatures to the saved creatures collection
		/// </summary>
		/// <param name="creatures"></param>
		//public void SaveCreatures(IEnumerable<Creature> creatures)
		//{
		//	using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
		//	{
		//		// Creates collection if necessary
		//		var collection = db.GetCollection<Creature>(m_SavedCreaturesCollectionName);
		//		collection.InsertBulk(creatures);
		//	}
		//}

		/// <summary>
		/// Adds the creatures to the saved creatures collection
		/// </summary>
		/// <param name="creatures"></param>
		public void SaveCreatures(IEnumerable<Creature> creatures, ModCollection modCollection)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (db.CollectionExists(modCollection.CollectionName))
				{
					var collection = db.GetCollection<Creature>(modCollection.CollectionName);
					collection.InsertBulk(creatures);
				}
			}
		}

		/// <summary>
		/// Adds the creature to the saved creatures collection
		/// </summary>
		/// <param name="creature"></param>
		//public void SaveCreature(Creature creature)
		//{
		//	using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
		//	{
		//		// Creates collection if necessary
		//		var collection = db.GetCollection<Creature>(m_SavedCreaturesCollectionName);
		//		bool creatureExists = collection.Exists(FindCreatureQuery(creature.Left, creature.Right, creature.BodyParts));
		//		if (!creatureExists)
		//		{
		//			collection.Insert(creature);
		//		}
		//	}
		//}

		/// <summary>
		/// Adds the creature to the given collection
		/// </summary>
		/// <param name="creature"></param>
		/// <param name="collectionName"></param>
		public void SaveCreature(Creature creature, ModCollection modCollection)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				var collection = db.GetCollection<Creature>(modCollection.CollectionName);
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
		//public void UnsaveCreature(Creature creature)
		//{
		//	using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
		//	{
		//		// No need to unsave if there aren't any saved creatures
		//		if (!db.CollectionExists(m_SavedCreaturesCollectionName))
		//		{
		//			return;
		//		}

		//		var collection = db.GetCollection<Creature>(m_SavedCreaturesCollectionName);
		//		collection.Delete(FindCreatureQuery(creature.Left, creature.Right, creature.BodyParts));
		//	}
		//}

		/// <summary>
		/// Removes the creature from the given collection
		/// </summary>
		/// <param name="creature"></param>
		/// <param name="collectionName"></param>
		public void UnsaveCreature(Creature creature, ModCollection modCollection)
		{
			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				// No need to unsave if there aren't any saved creatures
				if (!db.CollectionExists(modCollection.CollectionName))
				{
					return;
				}

				var collection = db.GetCollection<Creature>(modCollection.CollectionName);
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
			return false;
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
		public void CreateDB(string name, string attrPath, string stockPath)
		{
			Directory.CreateDirectory(DirectoryConstants.DatabaseDirectory);

			using (var db = new LiteDatabase(DirectoryConstants.DatabaseString))
			{
				if (db.CollectionExists(name))
				{
					db.DropCollection(name);
				}

				var collection = db.GetCollection<Creature>(name);
				CreateCreatures(collection, attrPath, stockPath);

				// Setup indexes
				// May not need if not querying to filter
				// These caused a 3GB spike in memory usage
				//collection.EnsureIndex(x => x.Rank);
				//collection.EnsureIndex(x => x.Abilities);
			}
		}

		private void CreateCreatures(LiteCollection<Creature> collection, string attrPath, string stockPath)
		{
			var stockNames = Directory.GetFiles(stockPath)
				.Where(s => Path.GetFileName(s).Contains(".lua"))
				.Select(s => Path.GetFileName(s).Replace(".lua", ""))
				.ToList();

			CreatureCombiner creatureCombiner = new CreatureCombiner(stockNames, attrPath, stockPath);

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
