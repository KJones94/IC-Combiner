namespace Combiner
{
	public class SonicRangeFilter : OptionFilter
	{
		public SonicRangeFilter()
			: base("Sonic Range") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			bool range1HasSonic = creature.RangeSpecial1 == 0
					&& (int)creature.RangeType1 == (int)DamageType.Sonic;
			bool range2HasSonic = creature.RangeSpecial2 == 0
				&& (int)creature.RangeType2 == (int)DamageType.Sonic;

			return range1HasSonic || range2HasSonic;
		}

		public override string ToString()
		{
			return nameof(SonicRangeFilter);
		}
	}
}
