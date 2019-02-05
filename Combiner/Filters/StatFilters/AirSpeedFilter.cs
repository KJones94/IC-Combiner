namespace Combiner.Filters.StatFilters
{
	using Combiner.Models;

	public class AirSpeedFilter : StatFilter
	{
		public AirSpeedFilter()
			: base("Air Speed", 0, 50) { }

		public override bool Filter(Creature creature)
		{
			return creature.AirSpeed >= this.MinValue
				&& creature.AirSpeed <= this.MaxValue;
		}

		public override string ToString()
		{
			return nameof(AirSpeedFilter);
		}
	}
}
