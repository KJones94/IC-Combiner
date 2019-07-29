using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class EffectiveHitpointsFilter : StatFilter
	{
		public EffectiveHitpointsFilter()
			: base("E.HP", 0, 5000) { }

		public override bool Filter(Creature creature)
		{
			return creature.EffectiveHitpoints >= MinValue
				&& creature.EffectiveHitpoints < (MaxValue + 1);
		}

		public override Query BuildQuery()
		{
			return Query.Between("EffectiveHitpoints", MinValue, MaxValue);
		}

		public override string ToString()
		{
			return nameof(EffectiveHitpointsFilter);
		}
	}
}
