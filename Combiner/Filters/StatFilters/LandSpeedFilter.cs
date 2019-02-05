namespace Combiner
{
	public class LandSpeedFilter : StatFilter
	{
		public LandSpeedFilter()
			: base("Land Speed", 0, 50) { }

		public override bool Filter(Creature creature)
		{
			return creature.LandSpeed >= MinValue
				&& creature.LandSpeed <= MaxValue;
		}

		public override string ToString()
		{
			return nameof(LandSpeedFilter);
		}
	}
}
