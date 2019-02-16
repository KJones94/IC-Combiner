using System;
using System.IO;

namespace Combiner
{
	public class StockFactory
	{
		public Stock CreateStock(string animalName, LuaStockProxy lua)
		{
			Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
			string path = Path.Combine(Environment.CurrentDirectory, DirectoryConstants.StockDirectory);
			return new Stock(StockNames.ProperStockNames[animalName], lua.GetLimbAttributes(path + animalName + ".lua"));
		}
	}
}
