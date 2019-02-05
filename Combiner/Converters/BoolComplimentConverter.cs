namespace Combiner.Converters
{
	using System;
	using System.Globalization;
	using System.Windows.Data;

	public class BoolComplimentConverter : IValueConverter
	{
		public object Convert(object value)
		{
			return this.Convert(value, null, null, null);
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !(bool)value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !(bool)value;
		}
	}
}
