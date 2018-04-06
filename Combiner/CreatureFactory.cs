using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class CreatureFactory
	{
		private static readonly CreatureFactory _instance = new CreatureFactory();

		public static CreatureFactory Instance
		{
			get
			{
				return _instance;
			}
		}

		private CreatureFactory() { }

		public Creature CreateCreature(Stock left, Stock right, Dictionary<Limb, Side> chosenBodyParts)
		{
			return new Creature(left, right, chosenBodyParts);
		}
	}
}
