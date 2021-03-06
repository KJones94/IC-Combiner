﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Combiner
{
	public class RangeTypeConverter : IValueConverter
	{
		public object Convert(object value)
		{
			return Convert(value, null, null, null);
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int rangeType = (int)(double)value;
			string result;
			switch (rangeType)
			{
				case 0:
					result = "None";
					break;
				case 2:
					result = "Quill";
					break;
				case 8:
					result = "Elec";
					break;
				case 16:
					result = "Sonic";
					break;
				case 1:
				case 256:
					result = "Poison";
					break;
				default:
					result = "??";
					break;
			}

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
