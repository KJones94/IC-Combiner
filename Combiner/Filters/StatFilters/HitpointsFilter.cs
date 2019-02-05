namespace Combiner.Filters.StatFilters
{
	using Combiner.Models;

	public class HitpointsFilter : StatFilter
	{
		public HitpointsFilter()
			: base("Hitpoints", 0, 3000) { }

		public override bool Filter(Creature creature)
		{
			return creature.Hitpoints >= this.MinValue
				&& creature.Hitpoints <= this.MaxValue;
		}

		public override string ToString()
		{
			return nameof(HitpointsFilter);
		}
	}
}
