using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class BarrierDestroyFilter : OptionFilter
	{
		public BarrierDestroyFilter()
			: base("Has Barrier Destroy") { }

		protected override bool OnOptionChecked(Creature creature)
		{
			return creature.HasBarrierDestroy;
		}

		public override BsonExpression BuildQuery()
		{
			return Query.EQ("HasBarrierDestroy", true);
		}

		public override string ToString()
		{
			return nameof(BarrierDestroyFilter);
		}
	}
}
