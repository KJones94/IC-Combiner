using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class CoalFilter : StatFilter
	{
		public CoalFilter()
			: base("Coal", 0, 2000) { }

		public override bool Filter(Creature creature)
		{
			return creature.Coal >= MinValue
				&& creature.Coal < (MaxValue + 1);
		}

		public override string ToString()
		{
			return nameof(CoalFilter);
		}
	}
}
