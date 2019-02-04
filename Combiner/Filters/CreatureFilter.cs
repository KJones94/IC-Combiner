using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public abstract class CreatureFilter : BaseViewModel
	{
		public abstract bool Filter(Creature creature);

		public abstract void ResetFilter();

		public string Name { get; private set; }

		public CreatureFilter(string name)
		{
			Name = name;
		}

	}
}
