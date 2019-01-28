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

		private StockFactory() { }
		
		public Stock CreateStock(string animalName, LuaStockProxy lua)
		{
			Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
			string path = Path.Combine(Environment.CurrentDirectory, Utility.StockDirectory);
			return new Stock(StockNames.ProperStockNames[animalName], lua.GetLimbAttributes(path + animalName + ".lua"));
		}
	}
}
