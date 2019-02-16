namespace Combiner
{
	public class RankFilter : StatFilter
	{
		public RankFilter()
			: base("Rank", 1, 5) { }

		public override bool Filter(Creature creature)
		{
			return creature.Rank >= MinValue
				&& creature.Rank <= MaxValue;
		}

		public override string ToString()
		{
			return nameof(RankFilter);
		}
	}
}
