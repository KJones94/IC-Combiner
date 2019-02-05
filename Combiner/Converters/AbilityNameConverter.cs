namespace Combiner.Converters
{
	using System;
	using System.Globalization;
	using System.Windows.Data;

	using Combiner.Utility;

	public class AbilityNameConverter : IValueConverter
	{
		public object Convert(object value)
		{
			return this.Convert(value, null, null, null);
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string ability = (string)value;
			if (ability == string.Empty)
			{
				return ability;
			}
			return AbilityNames.ProperAbilityNames[ability];
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
