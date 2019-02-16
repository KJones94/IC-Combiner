namespace Combiner
{
	public class HitpointsFilter : StatFilter
	{
		public HitpointsFilter()
			: base("Hitpoints", 0, 3000) { }

		public override bool Filter(Creature creature)
		{
			return creature.Hitpoints >= MinValue
				&& creature.Hitpoints < (MaxValue + 1);
		}

		public override string ToString()
		{
			return nameof(HitpointsFilter);
		}
	}
}
