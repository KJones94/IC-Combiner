using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class QuillRangeFilter : OptionFilter
	{
		public QuillRangeFilter()
			: base("Quill Range") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			bool range1HasQuill = creature.RangeSpecial1 == 0
					&& (int)creature.RangeType1 == (int)DamageType.Horns;
			bool range2HasQuill = creature.RangeSpecial2 == 0
				&& (int)creature.RangeType2 == (int)DamageType.Horns;

			return range1HasQuill || range2HasQuill;
		}

		public override Query BuildQuery()
		{
			var range1HasQuillQuery = Query.And(
				Query.EQ("RangeSpecial1", 0),
				Query.EQ("RangeType1", (int)DamageType.Horns));

			var range2HasQuillQuery = Query.And(
				Query.EQ("RangeSpecial2", 0),
				Query.EQ("RangeType2", (int)DamageType.Horns));

			return Query.Or(range1HasQuillQuery, range2HasQuillQuery);
		}

		public override string ToString()
		{
			return nameof(QuillRangeFilter);
		}
	}
}
