using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class ElectricityFilter : StatFilter
	{
		public ElectricityFilter()
			: base("Electricity", 0, 2000) { }

		public override bool Filter(Creature creature)
		{
			return creature.Electricity >= MinValue
				&& creature.Electricity < (MaxValue + 1);
		}

		public override Query BuildQuery()
		{
			return Query.And(
				Query.GTE("Electricity", MinValue),
				Query.LT("Electricity", MaxValue + 1));
		}

		public override string ToString()
		{
			return nameof(ElectricityFilter);
		}
	}
}
