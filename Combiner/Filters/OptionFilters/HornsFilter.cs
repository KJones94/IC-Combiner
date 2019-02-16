namespace Combiner
{
	public class HornsFilter : OptionFilter
	{
		public HornsFilter()
			: base("Has Horns") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.HasHorns;
		}

		public override string ToString()
		{
			return nameof(HornsFilter);
		}
	}
}
