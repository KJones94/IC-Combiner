using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class ArtilleryOnlyFilter : OptionFilter
	{
		public ArtilleryOnlyFilter()
			: base("Artillery Only") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.RangeSpecial1 > 0 || creature.RangeSpecial2 > 0;
		}

		public override BsonExpression BuildQuery()
		{
			return Query.Or(
				Query.GT("RangeSpecial1", 0),
				Query.GT("RangeSpecial2", 0));
		}

		public override string ToString()
		{
			return nameof(ArtilleryOnlyFilter);
		}
	}
}
