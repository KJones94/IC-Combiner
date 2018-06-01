using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Combiner
{
	public class StockNameConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string name = (string)value;
			if (name == string.Empty)
			{
				return name;
			}

			string finalName;
			if (Utility.ProperStockNames.TryGetValue(name, out finalName))
			{
				return finalName;
			}
			return name;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
