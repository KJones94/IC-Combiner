using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class AirSpeedFilter : StatFilter
	{
		public AirSpeedFilter()
			: base("Air Speed", 0, 50) { }

		public override bool Filter(Creature creature)
		{
			return creature.AirSpeed >= MinValue
				&& creature.AirSpeed < (MaxValue + 1);
		}

		public override BsonExpression BuildQuery()
		{
			return Query.And(
				Query.GTE("AirSpeed", MinValue),
				Query.LT("AirSpeed", MaxValue + 1));
		}

		public override string ToString()
		{
			return nameof(AirSpeedFilter);
		}
	}
}
