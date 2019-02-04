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

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.HasHorns;
		}

		public override string ToString()
		{
			return nameof(HornsFilter);
		}
	}
}
