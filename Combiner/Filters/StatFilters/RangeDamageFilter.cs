namespace Combiner.Filters.StatFilters
{
	using Combiner.Models;

	public class RangeDamageFilter : StatFilter
	{
		public RangeDamageFilter()
			: base("Range Damage", 0, 100) { }

		public override bool Filter(Creature creature)
		{
			bool isBothUnderMax = creature.RangeDamage1 <= this.MaxValue
				&& creature.RangeDamage2 <= this.MaxValue;

			bool isOneOverMin = creature.RangeDamage1 >= this.MinValue
				|| creature.RangeDamage2 >= this.MinValue;

			return isBothUnderMax && isOneOverMin;
		}

		public override string ToString()
		{
			return nameof(RangeDamageFilter);
		}
	}
}
