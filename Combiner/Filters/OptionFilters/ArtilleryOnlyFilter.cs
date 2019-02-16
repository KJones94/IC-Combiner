namespace Combiner
{
	public class ArtilleryOnlyFilter : OptionFilter
	{
		public ArtilleryOnlyFilter()
			: base("Artillery Only") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.RangeSpecial1 > 0 || creature.RangeSpecial2 > 0;
		}

		public override string ToString()
		{
			return nameof(ArtilleryOnlyFilter);
		}
	}
}
