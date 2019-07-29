using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class SonicRangeFilter : OptionFilter
	{
		public SonicRangeFilter()
			: base("Sonic Range") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			bool range1HasSonic = creature.RangeSpecial1 == 0
					&& (int)creature.RangeType1 == (int)DamageType.Sonic;
			bool range2HasSonic = creature.RangeSpecial2 == 0
				&& (int)creature.RangeType2 == (int)DamageType.Sonic;

			return range1HasSonic || range2HasSonic;
		}

		public override Query BuildQuery()
		{
			var range1HasSonicQuery = Query.And(
				Query.EQ("RangeSpecial1", 0),
				Query.EQ("RangeType1", (int)DamageType.Sonic));

			var range2HasSonicQuery = Query.And(
				Query.EQ("RangeSpecial2", 0),
				Query.EQ("RangeType2", (int)DamageType.Sonic));

			return Query.Or(range1HasSonicQuery, range2HasSonicQuery);
		}

		public override string ToString()
		{
			return nameof(SonicRangeFilter);
		}
	}
}
