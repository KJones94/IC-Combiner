namespace Combiner.Converters
{
	using System;
	using System.Globalization;
	using System.Windows.Data;

	public class DoubleToStringConverter : IValueConverter
	{
		public object Convert(object value)
		{
			return this.Convert(value, null, null, null);
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((double)value > 1)
			{
				int floor = (int)Math.Floor((double)value);
				return floor.ToString();
			}

			return Math.Truncate((double)value * 100) / 100;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
