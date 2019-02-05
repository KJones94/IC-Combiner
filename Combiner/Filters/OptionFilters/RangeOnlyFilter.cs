namespace Combiner
{
	public class RangeOnlyFilter : OptionFilter
	{
		public RangeOnlyFilter()
			: base("Range Only") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.RangeDamage1 > 0
					&& creature.RangeSpecial1 == 0
					&& creature.RangeSpecial2 == 0;
		}
	}
}
