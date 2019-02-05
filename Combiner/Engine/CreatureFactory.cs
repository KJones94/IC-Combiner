namespace Combiner.Engine
{
	using System.Collections.Generic;

	using Combiner.Enums;
	using Combiner.Models;

	public class CreatureFactory
	{
		public CreatureBuilder CreateCreature(Stock left, Stock right, Dictionary<Limb, Side> chosenBodyParts)
		{
			return new CreatureBuilder(left, right, chosenBodyParts);
		}
	}
}
