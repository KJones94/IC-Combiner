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
					return new List<Creature>();

				var collection = db.GetCollection<Creature>("creatures");


				//var result = collection.Find(x => x.Rank == 1);
				var result = collection.FindAll();
				List<Creature> creatures = result.ToList();
				return creatures;
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
	}
}
