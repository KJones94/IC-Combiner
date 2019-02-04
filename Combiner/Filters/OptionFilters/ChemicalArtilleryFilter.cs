using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class ChemicalArtilleryFilter : OptionFilter
	{
		public ChemicalArtilleryFilter()
			: base("ChemicalArtillery") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.RangeSpecial1 == 3 || creature.RangeSpecial2 == 3;
		}

		public override string ToString()
		{
			return nameof(ChemicalArtilleryFilter);
		}
	}
}
