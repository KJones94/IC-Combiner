using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Combiner
{
	public static class DatabasePrototype
	{

		public static void TestDB()
		{
			using (var db = new LiteDatabase(Utility.DatabaseString))
			{
				if (db.CollectionExists("creatures"))
				{
					db.DropCollection("creatures");
				}
				var collection = db.GetCollection<Creature>("creatures");
				CreateCreatures(collection);
				//CreateCreatures(collection);

				var results = collection.FindAll();
			}
		}

		private static void CreateCreatures(LiteCollection<Creature> collection)
		{
			LuaHandler lua = new LuaHandler();
			//List<CreatureBuilder> creatures = new List<CreatureBuilder>();

			var stockNames = Directory.GetFiles(Utility.StockDirectory).
						Select(s => s.Replace(".lua", "").Replace(Utility.StockDirectory, "")).ToList();

			//Stock left = StockFactory.Instance.CreateStock(stockNames[0], lua);
			//Stock right = StockFactory.Instance.CreateStock(stockNames[1], lua);
			//creatures.AddRange(CreatureCombiner.Combine(left, right));

			using (var file = new StreamWriter("../../Database/Log.txt"))
			{


				//List<Creature> creatures;
				//Stock left;
				//Stock right;
				for (int i = 0; i < 40; i++)
				{
					for (int j = i + 1; j < stockNames.Count(); j++)
					{
						//creatures = new List<Creature>();
						//left = StockFactory.Instance.CreateStock(stockNames[i], lua);
						//right = StockFactory.Instance.CreateStock(stockNames[j], lua);
						//foreach (var creature in CreatureCombiner.Combine(left, right))
						//{
						//	lua.LoadScript(creature);
						//	creatures.Add(creature.BuildCreature());
						//}
						//collection.InsertBulk(creatures);
						InsertIntoCollection(collection, stockNames[i], stockNames[j], lua);
						file.WriteLine("" + GC.GetTotalMemory(false));
					}
				}
			}
		}

		private static void InsertIntoCollection(LiteCollection<Creature> collection, string leftName, string rightName, LuaHandler lua)
		{
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
