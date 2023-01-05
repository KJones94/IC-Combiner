using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class SuicideCoefficientFilter : StatFilter
	{
		public SuicideCoefficientFilter()
			: base("SuiCo", 0, 10000) { }

		public override bool Filter(Creature creature)
		{
			return creature.SuicideCoefficient >= MinValue
				&& creature.SuicideCoefficient < (MaxValue + 1);
		}

		public override Query BuildQuery()
		{
			return Query.And(
				Query.GTE("SuicideCoefficient", MinValue),
				Query.LT("SuicideCoefficient", MaxValue + 1));
		}

		public override string ToString()
		{
			return nameof(SuicideCoefficientFilter);
		}
	}
}
