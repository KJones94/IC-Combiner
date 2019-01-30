using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class RangeDamageFilter : StatFilter
	{
		public RangeDamageFilter()
			: base("Range Damage", 0, 100) { }

		public override bool Filter(Creature creature)
		{
			bool isBothUnderMax = creature.RangeDamage1 <= MaxValue
				&& creature.RangeDamage2 <= MaxValue;

			bool isOneOverMin = creature.RangeDamage1 >= MinValue
				|| creature.RangeDamage2 >= MinValue;

			return isBothUnderMax && isOneOverMin;
		}

		public override string ToString()
		{
			return nameof(RangeDamageFilter);
		}
	}
}
