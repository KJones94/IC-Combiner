using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Combiner
{
	// Not thread safe
	public class StockFactory
	{
		private static readonly StockFactory _instance = new StockFactory();

		public static StockFactory Instance
		{
			get
			{
				return _instance;
			}
		}

		private Dictionary<Limb, bool> CreateBodyParts(bool[] values)
		{
			Limb[] limbs = (Limb[])Enum.GetValues(typeof(Limb));
			return limbs.Zip(values, (k, v) => new { k, v })
				.ToDictionary(x => x.k, x => x.v);
		}

		private StockFactory() { }

		public Stock CreateStock(string animalName, LuaHandler lua)
		{
			Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
			string path = Path.Combine(Environment.CurrentDirectory, Utility.StockDirectory);
			return new Stock(animalName, lua.GetLimbAttributes(path + animalName + ".lua"));
		}

		public Stock CreateStockFromFile(string file, LuaHandler lua)
		{
			Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
			string path = Path.Combine(Environment.CurrentDirectory, Utility.StockDirectory);
			return new Stock(file.Remove(file.Count() - 4), lua.GetLimbAttributes(path + file));
		}
	}
}
