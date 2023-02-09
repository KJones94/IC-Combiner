using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class PopSizeFilter : StatFilter
	{
		public PopSizeFilter()
			: base("PopSize", 0, 10000) { }

		public override bool Filter(Creature creature)
		{
			return creature.PopSize >= MinValue
				&& creature.PopSize < (MaxValue + 1);
		}

		public override BsonExpression BuildQuery()
		{
			return Query.And(
				Query.GTE("PopSize", MinValue),
				Query.LT("PopSize", MaxValue + 1));
		}

		public override string ToString()
		{
			return nameof(PopSizeFilter);
		}
	}
}