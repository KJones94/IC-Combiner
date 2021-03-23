using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class TicksFilter : StatFilter
	{
		public TicksFilter()
			: base("Build Time", 0, 1000) { }

		public override bool Filter(Creature creature)
		{
			return creature.Ticks >= MinValue
				&& creature.Ticks < (MaxValue + 1);
		}

		public override Query BuildQuery()
		{
			return Query.And(
				Query.GTE("Ticks", MinValue),
				Query.LT("Ticks", MaxValue + 1));
		}

		public override string ToString()
		{
			return nameof(TicksFilter);
		}
	}
}
