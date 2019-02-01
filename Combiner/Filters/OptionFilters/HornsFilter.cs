using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class HornsFilter : OptionFilter
	{
		public HornsFilter()
			: base("Has Horns") { }

		public override bool Filter(Creature creature)
		{
			throw new NotImplementedException();
		}
	}
}
