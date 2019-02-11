using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class MeleeOnlyFilter : OptionFilter
	{
		public MeleeOnlyFilter()
			: base("Melee Only") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.RangeDamage1 == 0;
		}

		public override string ToString()
		{
			return nameof(MeleeOnlyFilter);
		}
	}
}
