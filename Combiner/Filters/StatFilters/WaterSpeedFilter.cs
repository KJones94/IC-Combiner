namespace Combiner.Filters.StatFilters
{
	using Combiner.Models;

	public class WaterSpeedFilter : StatFilter
	{
		public WaterSpeedFilter()
			: base("Water Speed", 0, 50) { }

		public override bool Filter(Creature creature)
		{
			return creature.WaterSpeed >= this.MinValue
				&& creature.WaterSpeed <= this.MaxValue;
		}

		public override string ToString()
		{
			return nameof(WaterSpeedFilter);
		}
	}
}
