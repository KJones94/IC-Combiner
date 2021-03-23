using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class CoalElecRatioFilter : StatFilter
	{
		public CoalElecRatioFilter()
			: base("Coal/Elec Ratio", 0, 1000) { }

		public override bool Filter(Creature creature)
		{
			return creature.CoalElecRatio >= MinValue
				&& creature.CoalElecRatio < (MaxValue + 1);
		}

		public override Query BuildQuery()
		{
			return Query.And(
				Query.GTE("CoalElecRatio", MinValue),
				Query.LT("CoalElecRatio", MaxValue + 1));
		}

		public override string ToString()
		{
			return nameof(CoalElecRatioFilter);
		}
	}
}