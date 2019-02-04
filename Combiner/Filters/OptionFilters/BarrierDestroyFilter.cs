using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class BarrierDestroyFilter : OptionFilter
	{
		public BarrierDestroyFilter()
			: base("Has BarrierDestroy") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.HasBarrierDestroy;
		}

		public override string ToString()
		{
			return nameof(BarrierDestroyFilter);
		}
	}
}
