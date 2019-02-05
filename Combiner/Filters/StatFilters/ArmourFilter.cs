namespace Combiner.Filters.StatFilters
{
	using Combiner.Models;

	public class ArmourFilter : StatFilter
	{
		public ArmourFilter()
			: base("Armour", 0, 1.0) { }

		public override bool Filter(Creature creature)
		{
			return creature.Armour >= this.MinValue
				&& creature.Armour <= this.MaxValue;
		}

		public override string ToString()
		{
			return nameof(ArmourFilter);
		}
	}
}
