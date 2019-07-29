using LiteDB;
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

		protected override bool OnOptionChecked(Creature creature)
		{
			return !(creature.RangeDamage2 > 0);
		}

		public override Query BuildQuery()
		{
			return Query.And(
				Query.GT("RangeDamage1", 0),
				Query.EQ("RangeDamage2", 0));
		}

		public override string ToString()
		{
			return nameof(SingleRangedFilter);
		}
	}
}
