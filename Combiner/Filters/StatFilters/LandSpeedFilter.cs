using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class LandSpeedFilter : StatFilter
	{
		public LandSpeedFilter()
			: base("Land Speed", 0, 50) { }

		public override bool Filter(Creature creature)
		{
			return creature.LandSpeed >= MinValue
				&& creature.LandSpeed < (MaxValue + 1);
		}

		public override BsonExpression BuildQuery()
		{
			return Query.And(
				Query.GTE("LandSpeed", MinValue),
				Query.LT("LandSpeed", MaxValue + 1));
		}

		public override string ToString()
		{
			return nameof(LandSpeedFilter);
		}
	}
}
