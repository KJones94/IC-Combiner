using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class SingleRangedFilter : OptionFilter
	{
		public SingleRangedFilter()
			: base("Single Ranged Attack") { }

		public override bool Filter(Creature creature)
		{
			throw new NotImplementedException();
		}
	}
}
