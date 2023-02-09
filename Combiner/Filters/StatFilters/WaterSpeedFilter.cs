using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class WaterSpeedFilter : StatFilter
	{
		public WaterSpeedFilter()
			: base("Water Speed", 0, 50) { }

		public override bool Filter(Creature creature)
		{
			return creature.WaterSpeed >= MinValue
				&& creature.WaterSpeed < (MaxValue + 1);
		}

		public override BsonExpression BuildQuery()
		{
			return Query.And(
				Query.GTE("WaterSpeed", MinValue),
				Query.LT("WaterSpeed", MaxValue + 1));
		}

		public override string ToString()
		{
			return nameof(WaterSpeedFilter);
		}
	}
}
