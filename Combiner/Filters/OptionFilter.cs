namespace Combiner.Filters
{
	using System;

	using Combiner.Models;

	public abstract class OptionFilter : CreatureFilter
	{
		public event EventHandler IsOptionCheckChanged;

		public OptionFilter(string name)
			: base(name) { }

		private bool m_IsOptionChecked;
		public bool IsOptionChecked
		{
			get { return this.m_IsOptionChecked; }
			set
			{
				if (this.m_IsOptionChecked != value)
				{
					this.m_IsOptionChecked = value;
					this.OnPropertyChanged(nameof(this.IsOptionChecked));
				}
			}
		}

		protected abstract bool OnOptionChecked(Creature creature);

		public override bool Filter(Creature creature)
		{
			if (this.IsOptionChecked)
			{
				return this.OnOptionChecked(creature);
			}
			return true;
		}

		public override void ResetFilter()
		{
			this.IsOptionChecked = false;
		}
	}
}
