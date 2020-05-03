using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class StockFactory
	{
		public Stock CreateStock(string animalName, LuaStockProxy lua, string stockPath)
		{
			Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
			string path = Path.Combine(stockPath, animalName + ".lua");
			return new Stock(StockNames.ProperStockNames[animalName], lua.GetLimbAttributes(path));
			//return new Stock(StockNames.ProperStockNames[animalName], lua.GetLimbAttributes(stockPath));

		}
	}
}
