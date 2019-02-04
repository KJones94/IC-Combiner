﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class PowerFilter : StatFilter
	{
		public PowerFilter()
			: base("Power", 0, 10000) { }

		public override bool Filter(Creature creature)
		{
			return creature.Power >= MinValue
				&& creature.Power <= MaxValue;
		}

		public override string ToString()
		{
			return nameof(PowerFilter);
		}
	}
}