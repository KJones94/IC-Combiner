using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class CreatureFactory
	{
		public CreatureBuilder CreateCreature(Stock left, Stock right, Dictionary<Limb, Side> chosenBodyParts)
		{
			return new CreatureBuilder(left, right, chosenBodyParts);
		}
	}
}
