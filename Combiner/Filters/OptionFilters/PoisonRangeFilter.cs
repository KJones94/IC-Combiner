using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class PoisonRangeFilter : OptionFilter
	{
		public PoisonRangeFilter()
			: base("Poison Range") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			bool range1HasPoison = creature.RangeSpecial1 == 0
					&& (int)creature.RangeType1 == (int)DamageType.Poison;
			bool range2HasPoison = creature.RangeSpecial2 == 0
				&& (int)creature.RangeType2 == (int)DamageType.Poison;

			return range1HasPoison || range2HasPoison;
		}
	}
}
