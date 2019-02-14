using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class MeleeDamageFilter : StatFilter
	{
		public MeleeDamageFilter()
			: base("Melee Damage", 0, 100) { }

		public override bool Filter(Creature creature)
		{
			return creature.MeleeDamage >= MinValue
				&& creature.MeleeDamage < (MaxValue + 1);
		}

		public override string ToString()
		{
			return nameof(MeleeDamageFilter);
		}
	}
}
