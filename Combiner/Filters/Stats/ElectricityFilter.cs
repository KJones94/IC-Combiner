using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class ElectricityFilter : StatFilter
	{
		public ElectricityFilter()
			: base("Electricity", 0, 2000) { }

		public override bool Filter(Creature creature)
		{
			return creature.Electricity >= MinValue
				&& creature.Electricity <= MaxValue;
		}

		public override string ToString()
		{
			return nameof(ElectricityFilter);
		}
	}
}
