namespace Combiner.Filters.StatFilters
{
	using Combiner.Models;

	public class LandSpeedFilter : StatFilter
	{
		public LandSpeedFilter()
			: base("Land Speed", 0, 50) { }

		public override bool Filter(Creature creature)
		{
			return creature.LandSpeed >= this.MinValue
				&& creature.LandSpeed <= this.MaxValue;
		}

		public override string ToString()
		{
			return nameof(LandSpeedFilter);
		}
	}
}
