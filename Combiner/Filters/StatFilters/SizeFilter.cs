namespace Combiner.Filters.StatFilters
{
	using Combiner.Models;

	public class SizeFilter : StatFilter
	{
		public SizeFilter()
			: base("Size", 0, 10) { }

		public override bool Filter(Creature creature)
		{
			return creature.Size >= this.MinValue
				&& creature.Size <= this.MaxValue;
		}

		public override string ToString()
		{
			return nameof(SizeFilter);
		}
	}
}
