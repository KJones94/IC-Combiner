namespace Combiner.Filters
{
	public abstract class StatFilter : CreatureFilter
	{
		public StatFilter(string name, double minDefaultValue, double maxDefaultValue)
			: base(name)
		{
			this.m_MinDefaultValue = minDefaultValue;
			this.m_MaxDefaultValue = maxDefaultValue;
			this.MinValue = minDefaultValue;
			this.MaxValue = maxDefaultValue;
		}

		private double m_MinDefaultValue;
		private double m_MaxDefaultValue;

		private double m_MinValue;
		public double MinValue
		{
			get { return this.m_MinValue; }
			set
			{
				if (this.m_MinValue != value)
				{
					this.m_MinValue = value;
					this.OnPropertyChanged(nameof(this.MinValue));
				}
			}
		}

		private double m_MaxValue;
		public double MaxValue
		{
			get { return this.m_MaxValue; }
			set
			{
				if (this.m_MaxValue != value)
				{
					this.m_MaxValue = value;
					this.OnPropertyChanged(nameof(this.MaxValue));
				}
			}
		}

		public override void ResetFilter()
		{
			this.MinValue = this.m_MinDefaultValue;
			this.MaxValue = this.m_MaxDefaultValue;
		}
	}
}
