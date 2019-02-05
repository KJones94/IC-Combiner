namespace Combiner.Filters.StatFilters
{
	using Combiner.Models;

	public class RankFilter : StatFilter
	{
		public RankFilter()
			: base("Rank", 1, 5) { }

		public override bool Filter(Creature creature)
		{
			return creature.Rank >= this.MinValue
				&& creature.Rank <= this.MaxValue;
		}

		public override string ToString()
		{
			return nameof(RankFilter);
		}
	}
}
