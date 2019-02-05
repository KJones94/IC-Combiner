namespace Combiner.Filters.OptionFilters
{
	using Combiner.Models;

	public class SingleRangedFilter : OptionFilter
	{
		public SingleRangedFilter()
			: base("Single Ranged Attack") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return !(creature.RangeDamage2 > 0);
		}

		public override string ToString()
		{
			return nameof(SingleRangedFilter);
		}
	}
}
