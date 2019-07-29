using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class SightRadiusFilter : StatFilter
	{
		public SightRadiusFilter()
			: base("Sight Radius", 0, 50) { }

		public override bool Filter(Creature creature)
		{
			return creature.SightRadius >= MinValue
				&& creature.SightRadius < (MaxValue + 1);
		}

		public override Query BuildQuery()
		{
			return Query.Between("SightRadius", MinValue, MaxValue);
		}

		public override string ToString()
		{
			return nameof(SightRadiusFilter);
		}
	}
}
