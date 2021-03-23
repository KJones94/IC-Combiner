using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class AirOnlyFilter : OptionFilter
	{
		public AirOnlyFilter()
			: base("Air Only") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.LandSpeed == 0
				&& creature.WaterSpeed == 0
				&& creature.AirSpeed > 0;
		}

		public override Query BuildQuery()
		{
			return Query.And(
				Query.EQ("LandSpeed", 0),
				Query.GT("AirSpeed", 0),
				Query.EQ("WaterSpeed", 0));

		}

		public override string ToString()
		{
			return nameof(AirOnlyFilter);
		}
	}
}