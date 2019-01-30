using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class WaterSpeedFilter : StatFilter
	{
		public WaterSpeedFilter()
			: base("Water Speed", 0, 50) { }

		public override bool Filter(Creature creature)
		{
			return creature.WaterSpeed >= MinValue
				&& creature.WaterSpeed <= MaxValue;
		}

		public override string ToString()
		{
			return nameof(WaterSpeedFilter);
		}
	}
}
