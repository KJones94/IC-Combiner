namespace Combiner.Filters.OptionFilters
{
	using Combiner.Models;

	public class RockArtilleryFilter : OptionFilter
	{
		public RockArtilleryFilter()
			: base("Rock Artillery") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.RangeSpecial1 == 1 || creature.RangeSpecial2 == 1;
		}

		public override string ToString()
		{
			return nameof(RockArtilleryFilter);
		}
	}
}
