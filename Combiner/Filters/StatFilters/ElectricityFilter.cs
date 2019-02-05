namespace Combiner.Filters.StatFilters
{
	using Combiner.Models;

	public class ElectricityFilter : StatFilter
	{
		public ElectricityFilter()
			: base("Electricity", 0, 2000) { }

		public override bool Filter(Creature creature)
		{
			return creature.Electricity >= this.MinValue
				&& creature.Electricity <= this.MaxValue;
		}

		public override string ToString()
		{
			return nameof(ElectricityFilter);
		}
	}
}
