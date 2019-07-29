using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class WaterArtilleryFilter : OptionFilter
	{
		public WaterArtilleryFilter()
			: base("Water Artillery") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.RangeSpecial1 == 2 || creature.RangeSpecial2 == 2;
		}

		public override Query BuildQuery()
		{
			return Query.Or(
				Query.EQ("RangeSpecial1", 2),
				Query.EQ("RangeSpecial2", 2));
		}

		public override string ToString()
		{
			return nameof(WaterArtilleryFilter);
		}
	}
}
