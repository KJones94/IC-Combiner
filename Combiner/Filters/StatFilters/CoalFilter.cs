namespace Combiner.Filters.StatFilters
{
	using Combiner.Models;

	public class CoalFilter : StatFilter
	{
		public CoalFilter()
			: base("Coal", 0, 2000) { }

		public override bool Filter(Creature creature)
		{
			return creature.Coal >= this.MinValue
				&& creature.Coal <= this.MaxValue;
		}

		public override string ToString()
		{
			return nameof(CoalFilter);
		}
	}
}
