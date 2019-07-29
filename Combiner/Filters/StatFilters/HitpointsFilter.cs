using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class HitpointsFilter : StatFilter
	{
		public HitpointsFilter()
			: base("Hitpoints", 0, 3000) { }

		public override bool Filter(Creature creature)
		{
			return creature.Hitpoints >= MinValue
				&& creature.Hitpoints < (MaxValue + 1);
		}

		public override Query BuildQuery()
		{
			return Query.Between("Hitpoints", MinValue, MaxValue);
		}

		public override string ToString()
		{
			return nameof(HitpointsFilter);
		}
	}
}
