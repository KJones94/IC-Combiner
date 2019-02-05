namespace Combiner
{
	public class PoisonFilter : OptionFilter
	{
		public PoisonFilter()
			: base("Has Poison") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.HasPoison;
		}

		public override string ToString()
		{
			return nameof(PoisonFilter);
		}
	}
}
