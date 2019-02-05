namespace Combiner.Filters.StatFilters
{
	using Combiner.Models;

	public class MeleeDamageFilter : StatFilter
	{
		public MeleeDamageFilter()
			: base("Melee Damage", 0, 100) { }

		public override bool Filter(Creature creature)
		{
			return creature.MeleeDamage >= this.MinValue
				&& creature.MeleeDamage <= this.MaxValue;
		}

		public override string ToString()
		{
			return nameof(MeleeDamageFilter);
		}
	}
}
