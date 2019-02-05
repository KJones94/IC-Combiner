namespace Combiner.Filters.StatFilters
{
	using Combiner.Models;

	public class EffectiveHitpointsFilter : StatFilter
	{
		public EffectiveHitpointsFilter()
			: base("E.HP", 0, 5000) { }

		public override bool Filter(Creature creature)
		{
			return creature.EffectiveHitpoints >= this.MinValue
				&& creature.EffectiveHitpoints <= this.MaxValue;
		}

		public override string ToString()
		{
			return nameof(EffectiveHitpointsFilter);
		}
	}
}
