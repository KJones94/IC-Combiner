using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class RangeDistanceFilter : StatFilter
	{
		public RangeDistanceFilter()
			: base("Range Distance", 0, 100) { }

		public override bool Filter(Creature creature)
		{
			bool isBothUnderMax = creature.RangeMax1 < (MaxValue + 1)
				&& creature.RangeMax2 < (MaxValue + 1);

			bool isOneOverMin = creature.RangeMax1 >= MinValue
				|| creature.RangeMax2 >= MinValue;

			return isBothUnderMax && isOneOverMin;
		}

		public override Query BuildQuery()
		{
			var isBothUnderMaxQuery = Query.And(
				Query.LT("RangeMax1", MaxValue + 1),
				Query.LT("RangeMax2", MaxValue + 1));

			var isOneOverMinQuery = Query.Or(
				Query.GTE("RangeMax1", MinValue),
				Query.GTE("RangeMax2", MinValue));

			return Query.And(isBothUnderMaxQuery, isOneOverMinQuery);
		}

		public override string ToString()
		{
			return nameof(RangeDistanceFilter);
		}
	}
}
