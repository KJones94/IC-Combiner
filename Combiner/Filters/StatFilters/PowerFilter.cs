namespace Combiner.Filters.StatFilters
{
	using Combiner.Models;

	public class PowerFilter : StatFilter
	{
		public PowerFilter()
			: base("Power", 0, 10000) { }

		public override bool Filter(Creature creature)
		{
			return creature.Power >= this.MinValue
				&& creature.Power <= this.MaxValue;
		}

		public override string ToString()
		{
			return nameof(PowerFilter);
		}
	}
}
