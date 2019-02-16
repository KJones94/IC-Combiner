namespace Combiner
{
	public class PowerFilter : StatFilter
	{
		public PowerFilter()
			: base("Power", 0, 10000) { }

		public override bool Filter(Creature creature)
		{
			return creature.Power >= MinValue
				&& creature.Power < (MaxValue + 1);
		}

		public override string ToString()
		{
			return nameof(PowerFilter);
		}
	}
}
