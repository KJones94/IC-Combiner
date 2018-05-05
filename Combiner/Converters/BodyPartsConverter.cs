using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Combiner
{
	public class BodyPartsConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var bodyParts = value as Dictionary<string, string>;
			if (bodyParts != null)
			{
				StringBuilder sb = new StringBuilder();

				foreach (string limb in bodyParts.Values)
				{
					sb.Append(limb);
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
