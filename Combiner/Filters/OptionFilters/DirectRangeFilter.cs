namespace Combiner
{
	public class DirectRangeFilter : OptionFilter
	{
		public DirectRangeFilter()
			: base("Direct Range") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			bool range1HasDirect = creature.RangeDamage1 > 0
					&& creature.RangeSpecial1 == 0
					&& (int)creature.RangeType1 != (int)DamageType.Sonic;
			bool range2HasDirect = creature.RangeDamage2 > 0
				&& creature.RangeSpecial2 == 0
				&& (int)creature.RangeType2 != (int)DamageType.Sonic;

			return range1HasDirect || range2HasDirect;
		}

		public override string ToString()
		{
			return nameof(DirectRangeFilter);
		}
	}
}
