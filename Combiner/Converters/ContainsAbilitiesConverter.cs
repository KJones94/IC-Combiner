namespace Combiner.Converters
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Text;
	using System.Windows.Data;

	using Combiner.Utility;

	public class ContainsAbilitiesConverter : IValueConverter
	{
		public object Convert(object value)
		{
			return this.Convert(value, null, null, null);
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var abilities = value as Dictionary<string, bool>;
			if (abilities != null)
			{
				StringBuilder sb = new StringBuilder();
				foreach (string ability in AbilityNames.Abilities)
				{
					string key = AbilityNames.ProperAbilityNames[ability];
					if (abilities.ContainsKey(key))
					{
						if (abilities[key])
						{
							sb.Append(key);
							sb.Append(", ");
						}
					}
				}

				// Remove ", " at end if exists
				if (sb.Length > 0)
				{
					sb.Remove(sb.Length - 2, 2);
				}

				return sb.ToString();
			}

			return string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
