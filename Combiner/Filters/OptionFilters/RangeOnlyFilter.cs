using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class RangeOnlyFilter : OptionFilter
	{
		public RangeOnlyFilter()
			: base("Range Only") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.RangeDamage1 > 0
					&& creature.RangeSpecial1 == 0
					&& creature.RangeSpecial2 == 0;
		}

		public override Query BuildQuery()
		{
			return Query.And(
				Query.GT("RangeDamage1", 0),
				Query.EQ("RangeSpecial1", 0),
				Query.EQ("RangeSpecial2", 0));
		}

		public override string ToString()
		{
			return nameof(RangeOnlyFilter);
		}
	}
}
