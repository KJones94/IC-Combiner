using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class NERatingFilter : StatFilter
	{
		public NERatingFilter()
			: base("N-Eff Rating", 0, 20000) { }

		public override bool Filter(Creature creature)
		{
			return creature.CoalElecRatio >= MinValue
				&& creature.CoalElecRatio < (MaxValue + 1);
		}

		public override Query BuildQuery()
		{
			return Query.And(
				Query.GTE("NERating", MinValue),
				Query.LT("NERating", MaxValue + 1));
		}

		public override string ToString()
		{
			return nameof(NERatingFilter);
		}
	}
}