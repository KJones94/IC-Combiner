using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class RankFilter : StatFilter
	{
		public RankFilter()
			: base("Rank", 1, 5) { }

		public override bool Filter(Creature creature)
		{
			return creature.Rank >= MinValue
				&& creature.Rank <= MaxValue;
		}

		public override Query BuildQuery()
		{
			return Query.Between("Rank", MinValue, MaxValue);
		}

		public override string ToString()
		{
			return nameof(RankFilter);
		}
	}
}
