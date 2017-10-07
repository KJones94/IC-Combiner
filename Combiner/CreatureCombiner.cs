using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combiner
{
	// UNTESTED BE CAREFUL
    static class CreatureCombiner
    {
		private static Dictionary<Limb, bool> ConsolidateBodyParts(Stock left, Stock right)
		{


			return null;
		}

		public static void Combine(Stock left, Stock right)
		{
			List<Dictionary<Limb, Side>> unprunedBodyParts = CreateUnprunedBodyParts(left, right);
		}

		private static Dictionary<Limb, Side> CopyBodyParts(Dictionary<Limb, Side> original)
		{
			Dictionary<Limb, Side> copy = new Dictionary<Limb, Side>();
			foreach (KeyValuePair<Limb, Side> entry in original)
			{
				copy.Add(entry.Key, entry.Value);
			}
			return copy;
		}

		// Not sure if using the copy function is the best method
		private static List<Dictionary<Limb, Side>> GenerateBodyParts(Stock left, Stock right,
			Dictionary<Limb, Side> possibleBodyParts, Limb limb)
		{
			List<Dictionary<Limb, Side>> bodyPartsList = new List<Dictionary<Limb, Side>>();

			if (limb > Limb.Claws)
			{
				bodyPartsList.Add(possibleBodyParts);
				return bodyPartsList;
			}

			if (left.BodyParts[limb])
			{
				possibleBodyParts[limb] = Side.Left;
			}
			else
			{
				possibleBodyParts[limb] = Side.Empty;
			}
			bodyPartsList.AddRange(GenerateBodyParts(left, right, CopyBodyParts(possibleBodyParts), limb + 1));

			if (right.BodyParts[limb])
			{
				possibleBodyParts[limb] = Side.Right;
			}
			else
			{
				possibleBodyParts[limb] = Side.Empty;
			}
			bodyPartsList.AddRange(GenerateBodyParts(left, right, CopyBodyParts(possibleBodyParts), limb + 1));

			return bodyPartsList;
		}

		// Unelegant... Would like a better way to do this than brute force
		private static List<Dictionary<Limb, Side>> RemoveDuplicates(List<Dictionary<Limb, Side>> list)
		{
			List<Dictionary<Limb, Side>> uniqueList = new List<Dictionary<Limb, Side>>();
			foreach (Dictionary<Limb, Side> dict in list)
			{
				bool isUnique = true;
				foreach (Dictionary<Limb, Side> tempDict in uniqueList)
				{
					if (dict.SequenceEqual(tempDict))
					{
						isUnique = false;
						break;
					}
				}
				if (isUnique)
				{
					uniqueList.Add(dict);
				}
			}


			return uniqueList;
		}

		private static List<Dictionary<Limb, Side>> CreateUnprunedBodyParts(Stock left, Stock right)
		{
			Dictionary<Limb, Side> possibleBodyParts = new Dictionary<Limb, Side>();
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				possibleBodyParts.Add(limb, Side.Null);
			}

			List<Dictionary<Limb, Side>> unprunedBodyParts = GenerateBodyParts(left, right, possibleBodyParts, Limb.FrontLegs);
			return RemoveDuplicates(unprunedBodyParts);
		}

		private static List<Dictionary<Limb, Side>> PruneBodyParts(List<Dictionary<Limb, Side>> bodyParts)
		{

			return null;
		}


	}

	

	enum Side
	{
		Null,
		Empty,
		Left,
		Right
	}
}
