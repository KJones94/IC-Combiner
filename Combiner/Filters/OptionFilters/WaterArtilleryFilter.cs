namespace Combiner.Filters.OptionFilters
{
	using Combiner.Models;

	public class WaterArtilleryFilter : OptionFilter
	{
		public WaterArtilleryFilter()
			: base("Water Artillery") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.RangeSpecial1 == 2 || creature.RangeSpecial2 == 2;
		}

		public override string ToString()
		{
			return nameof(WaterArtilleryFilter);
		}
	}
}
