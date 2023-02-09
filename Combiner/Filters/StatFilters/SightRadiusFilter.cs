using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class SightRadiusFilter : StatFilter
	{
		public SightRadiusFilter()
			: base("Sight Radius", 0, 50) { }

		public override bool Filter(Creature creature)
		{
			return creature.SightRadius >= MinValue
				&& creature.SightRadius < (MaxValue + 1);
		}

		public override BsonExpression BuildQuery()
		{
			return Query.And(
				Query.GTE("SightRadius", MinValue),
				Query.LT("SightRadius", MaxValue + 1));
		}

		public override string ToString()
		{
			return nameof(SightRadiusFilter);
		}
	}
}
