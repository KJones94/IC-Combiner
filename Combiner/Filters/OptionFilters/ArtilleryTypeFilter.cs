using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class ArtilleryTypeFilter : OptionFilter
	{
		public ArtilleryTypeFilter()
			: base("Artillery Type") { }

		public override bool Filter(Creature creature)
		{
			throw new NotImplementedException();
		}
	}
}
