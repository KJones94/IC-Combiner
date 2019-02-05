namespace Combiner.Engine
{
	using System;
	using System.IO;

	using Combiner.Models;
	using Combiner.Utility;

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
