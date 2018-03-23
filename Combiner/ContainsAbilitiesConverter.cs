using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Combiner
{
	public class ContainsAbilitiesConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var gameAttributes = value as Dictionary<string, double>;
			if (gameAttributes != null)
			{
				StringBuilder sb = new StringBuilder();
				foreach (string ability in Utility.Abilities)
				{
					if (gameAttributes.ContainsKey(ability))
					{
						if (gameAttributes[ability] > 0)
						{
							sb.Append(ability);
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
