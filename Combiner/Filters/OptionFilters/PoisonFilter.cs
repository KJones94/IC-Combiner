using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class PoisonFilter : OptionFilter
	{
		public PoisonFilter()
			: base("Has Poison") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.HasPoison;
		}

		public override string ToString()
		{
			return nameof(PoisonFilter);
		}
	}
}
