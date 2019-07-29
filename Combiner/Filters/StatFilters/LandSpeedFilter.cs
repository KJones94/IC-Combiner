using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class LandSpeedFilter : StatFilter
	{
		public LandSpeedFilter()
			: base("Land Speed", 0, 50) { }

		public override bool Filter(Creature creature)
		{
			return creature.LandSpeed >= MinValue
				&& creature.LandSpeed < (MaxValue + 1);
		}

		public override Query BuildQuery()
		{
			return Query.Between("LandSpeed", MinValue, MaxValue);
		}

		public override string ToString()
		{
			return nameof(LandSpeedFilter);
		}
	}
}
