using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class RockArtilleryFilter : OptionFilter
	{
		public RockArtilleryFilter()
			: base("Rock Artillery") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.RangeSpecial1 == 1 || creature.RangeSpecial2 == 1;
		}

		public override Query BuildQuery()
		{
			return Query.Or(
				Query.EQ("RangeSpecial1", 1),
				Query.EQ("RangeSpecial2", 1));
		}

		public override string ToString()
		{
			return nameof(RockArtilleryFilter);
		}
	}
}
