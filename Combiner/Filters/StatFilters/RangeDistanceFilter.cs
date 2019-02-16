namespace Combiner
{
	public class RangeDistanceFilter : StatFilter
	{
		public RangeDistanceFilter()
			: base("Range Distance", 0, 100) { }

		public override bool Filter(Creature creature)
		{
			bool isBothUnderMax = creature.RangeMax1 < (MaxValue + 1)
				&& creature.RangeMax2 < (MaxValue + 1);

			bool isOneOverMin = creature.RangeMax1 >= MinValue
				|| creature.RangeMax2 >= MinValue;

			return isBothUnderMax && isOneOverMin;
		}

		public override string ToString()
		{
			return nameof(RangeDistanceFilter);
		}
	}
}
