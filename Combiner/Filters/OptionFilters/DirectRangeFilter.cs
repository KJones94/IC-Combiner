using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class DirectRangeFilter : OptionFilter
	{
		public DirectRangeFilter()
			: base("Direct Range") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			bool range1HasDirect = creature.RangeDamage1 > 0
					&& creature.RangeSpecial1 == 0
					&& (int)creature.RangeType1 != (int)DamageType.Sonic;
			bool range2HasDirect = creature.RangeDamage2 > 0
				&& creature.RangeSpecial2 == 0
				&& (int)creature.RangeType2 != (int)DamageType.Sonic;

			return range1HasDirect || range2HasDirect;
		}

		public override Query BuildQuery()
		{
			var range1HasDirectQuery = Query.And(
				Query.GT("RangeDamage1", 0),
				Query.EQ("RangeSpecial1", 0),
				Query.Not("RangeType1", (int)DamageType.Sonic));

			var range2HasDirectQuery = Query.And(
				Query.GT("RangeDamage2", 0),
				Query.EQ("RangeSpecial2", 0),
				Query.Not("RangeType2", (int)DamageType.Sonic));

			return Query.Or(range1HasDirectQuery, range2HasDirectQuery);
		}

		public override string ToString()
		{
			return nameof(DirectRangeFilter);
		}
	}
}
