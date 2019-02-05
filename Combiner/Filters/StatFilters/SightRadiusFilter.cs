namespace Combiner.Filters.StatFilters
{
	using Combiner.Models;

	public class SightRadiusFilter : StatFilter
	{
		public SightRadiusFilter()
			: base("Sight Radius", 0, 50) { }

		public override bool Filter(Creature creature)
		{
			return creature.SightRadius >= this.MinValue
				&& creature.SightRadius <= this.MaxValue;
		}

		public override string ToString()
		{
			return nameof(SightRadiusFilter);
		}
	}
}
