using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class AmphibOnlyFilter : OptionFilter
	{
		public AmphibOnlyFilter()
			: base("Amphib Only") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.LandSpeed > 0
				&& creature.WaterSpeed > 0
				&& creature.AirSpeed == 0;
		}

		public override Query BuildQuery()
		{
			return Query.And(
				Query.GT("LandSpeed", 0),
				Query.EQ("AirSpeed", 0),
				Query.GT("WaterSpeed", 0));

		}

		public override string ToString()
		{
			return nameof(AmphibOnlyFilter);
		}
	}
}