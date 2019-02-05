namespace Combiner.Filters
{
	using Combiner.Base;
	using Combiner.Models;

	public abstract class CreatureFilter : BaseViewModel
	{
		public abstract bool Filter(Creature creature);

		public abstract void ResetFilter();

		public string Name { get; private set; }

		public CreatureFilter(string name)
		{
			this.Name = name;
		}

	}
}
