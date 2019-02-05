namespace Combiner
{
	public class SightRadiusFilter : StatFilter
	{
		public SightRadiusFilter()
			: base("Sight Radius", 0, 50) { }

		public override bool Filter(Creature creature)
		{
			return creature.SightRadius >= MinValue
				&& creature.SightRadius <= MaxValue;
		}

		public override string ToString()
		{
			return nameof(SightRadiusFilter);
		}
	}
}
