using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class ChemicalArtilleryFilter : OptionFilter
	{
		public ChemicalArtilleryFilter()
			: base("Chemical Artillery") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.RangeSpecial1 == 3 || creature.RangeSpecial2 == 3;
		}

		public override Query BuildQuery()
		{
			return Query.Or(
				Query.EQ("RangeSpecial1", 3),
				Query.EQ("RangeSpecial2", 3));
		}

		public override string ToString()
		{
			return nameof(ChemicalArtilleryFilter);
		}
	}
}
