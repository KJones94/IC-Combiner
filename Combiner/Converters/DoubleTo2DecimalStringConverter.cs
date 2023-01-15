using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Combiner
{
	public class DoubleTo2DecimalStringConverter : IValueConverter
	{
		public object Convert(object value)
		{
			return Convert(value, null, null, null);
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Math.Truncate((double)value * 100) / 100;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
