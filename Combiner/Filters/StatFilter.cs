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
			m_MinDefaultValue = minDefaultValue;
			m_MaxDefaultValue = maxDefaultValue;
			MinValue = minDefaultValue;
			MaxValue = maxDefaultValue;
		}

		private double m_MinDefaultValue;
		private double m_MaxDefaultValue;

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

		public override void ResetFilter()
		{
			MinValue = m_MinDefaultValue;
			MaxValue = m_MaxDefaultValue;
		}
	}
}
