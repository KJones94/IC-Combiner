using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class AirSpeedFilter : StatFilter
	{
		public AirSpeedFilter()
			: base("Air Speed", 0, 50) { }

		public override bool Filter(Creature creature)
		{
			return creature.AirSpeed >= MinValue
				&& creature.AirSpeed < (MaxValue + 1);
		}

		public override Query BuildQuery()
		{
			return Query.Between("AirSpeed", MinValue, MaxValue);
		}

		public override string ToString()
		{
			return nameof(AirSpeedFilter);
		}
	}
}
