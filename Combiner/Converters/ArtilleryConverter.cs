using System;
using System.Globalization;
using System.Windows.Data;

namespace Combiner
{
	public class ArtilleryConverter : IValueConverter
	{
		public object Convert(object value)
		{
			return Convert(value, null, null, null);
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((double)value > 0)
			{
				return "yes";
			}
			return "no";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
