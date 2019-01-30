using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public abstract class StatFilter : CreatureFilter
	{
		public StatFilter(string name, double minDefaultValue, double maxDefaultValue)
			: base(name)
		{
			MinDefaultValue = minDefaultValue;
			MaxDefaultValue = maxDefaultValue;
			MinValue = minDefaultValue;
			MaxValue = maxDefaultValue;
		}

		public double MinDefaultValue { get; private set; }
		public double MaxDefaultValue { get; private set; }

		private double m_MinValue;
		public double MinValue
		{
			get { return m_MinValue; }
			set
			{
				if (m_MinValue != value)
				{
					m_MinValue = value;
					OnPropertyChanged(nameof(MinValue));
				}
			}
		}

		private double m_MaxValue;
		public double MaxValue
		{
			get { return m_MaxValue; }
			set
			{
				if (m_MaxValue != value)
				{
					m_MaxValue = value;
					OnPropertyChanged(nameof(MaxValue));
				}
			}
		}
	}
}
