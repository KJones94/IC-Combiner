using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combiner
{
	// UNTESTED BE CAREFUL
	public static class CreatureCombiner
	{
		private static Dictionary<Limb, bool> ConsolidateBodyParts(Stock left, Stock right)
		{


			return null;
		}

		public static List<CreatureBuilder> Combine(Stock left, Stock right)
		{
			List<Dictionary<Limb, Side>> unprunedBodyParts = CreateUnprunedBodyParts(left, right);
			List<Dictionary<Limb, Side>> prunedBodyParts = PruneBodyParts(left, right, unprunedBodyParts);

			List<CreatureBuilder> creatures = new List<CreatureBuilder>();
			foreach (Dictionary<Limb, Side> dict in prunedBodyParts)
			{
				creatures.Add(CreatureFactory.Instance.CreateCreature(left, right, dict));
			}
			return creatures;
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
		/// <summary>
		/// Recursively generates all of the possible body part combinations for the two stock.
		/// This can create duplicates.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="possibleBodyParts"></param>
		/// <param name="limb"></param>
		/// <returns></returns>
		private static List<Dictionary<Limb, Side>> GenerateBodyParts(Stock left, Stock right,
			Dictionary<Limb, Side> possibleBodyParts, Limb limb)
		{
			List<Dictionary<Limb, Side>> bodyPartsList = new List<Dictionary<Limb, Side>>();

			// End condition
			if (limb > Limb.Claws)
			{
				bodyPartsList.Add(possibleBodyParts);
				return bodyPartsList;
			}

			// Build left side possible body parts
			if (left.BodyParts[limb])
			{
				possibleBodyParts[limb] = Side.Left;
			}
			else
			{
				possibleBodyParts[limb] = Side.Empty;
			}
			bodyPartsList.AddRange(GenerateBodyParts(left, right, CopyBodyParts(possibleBodyParts), limb + 1));

			// Build right side possible body parts
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
		/// <summary>
		/// Post processing of possible body parts to remove any duplicates.
		/// If both left and right are empty there can be duplicates generated.
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Creates all possible body parts ignoring any special restrictions due to base animal type, 
		/// combination of types, specific animals, etc.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Prunes the given body parts to remove any that fall under special restrictions due to
		/// base animal type, combination of types, specific animals, etc.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="bodyParts"></param>
		/// <returns></returns>
		private static List<Dictionary<Limb, Side>> PruneBodyParts(Stock left, Stock right,
			List<Dictionary<Limb, Side>> bodyParts)
		{
			// Prune based on stock type and limbs

			// Rules:
			// (if with bird) quad front legs -> torso
			// bird torso -> back legs, wings
			// quad torso -> front legs, back legs
			// arachnid torso -> front legs, back legs, claws (if clawed)
			// insect torso -> front legs, back legs, wings

			List<Dictionary<Limb, Side>> prunedBodyParts = new List<Dictionary<Limb, Side>>();
			foreach (Dictionary<Limb, Side> dict in bodyParts)
			{
				if (!CheckSpecialCases(left, right, dict))
				{
					continue; //bad body parts
				}

				// check front legs edge case
				if (!IsQuadrupedBirdFrontLegsCorrect(left, right, dict))
				{
					continue; // bad body parts
				}

				// Torso can't be empty or null
				if (dict[Limb.Torso] == Side.Left)
				{
					if (IsTorsoRelatedPartsCorrect(left, dict))
					{
						prunedBodyParts.Add(dict);
					}
				}
				else if (dict[Limb.Torso] == Side.Right)
				{
					if (IsTorsoRelatedPartsCorrect(right, dict))
					{
						prunedBodyParts.Add(dict);
					}
				}
			}

			return prunedBodyParts;
		}

		/// <summary>
		/// Ensures if using quadruped torso is is also using quadruped legs when
		/// combined with a bird.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="dict"></param>
		/// <returns></returns>
		private static bool IsQuadrupedBirdFrontLegsCorrect(Stock left, Stock right, Dictionary<Limb, Side> dict)
		{
			if (dict[Limb.FrontLegs] == Side.Left)
			{
				if (left.Type == StockType.Quadruped && right.Type == StockType.Bird)
				{
					if (dict[Limb.Torso] != Side.Left)
					{
						return false;
					}
				}
			}
			else if (dict[Limb.FrontLegs] == Side.Right)
			{
				if (right.Type == StockType.Quadruped && left.Type == StockType.Bird)
				{
					if (dict[Limb.Torso] != Side.Right)
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>
		/// Ensures the correct parts when using the torso from the given stock type
		/// </summary>
		/// <param name="stock"></param>
		/// <param name="dict"></param>
		/// <returns></returns>
		private static bool IsTorsoRelatedPartsCorrect(Stock stock, Dictionary<Limb, Side> dict)
		{
			switch (stock.Type)
			{
				case StockType.Bird:
					if (dict[Limb.BackLegs] != Side.Empty && dict[Limb.Wings] != Side.Empty)
					{
						return true;
					}
					break;

				case StockType.Quadruped:
					if (dict[Limb.FrontLegs] != Side.Empty && dict[Limb.BackLegs] != Side.Empty)
					{
						return true;
					}
					break;

				case StockType.Arachnid:
					if (stock.Name == "siphonophore")
					{
						if (dict[Limb.Claws] != Side.Empty)
						{
							return true;
						}
					}
					else if (dict[Limb.FrontLegs] != Side.Empty && dict[Limb.BackLegs] != Side.Empty)
					{
						if (Utility.ClawedArachnids.Contains(stock.Name) && dict[Limb.Claws] == Side.Empty)
						{
							return false;
						}
						return true;
					}
					break;

				case StockType.Insect:
					if (dict[Limb.FrontLegs] != Side.Empty && dict[Limb.BackLegs] != Side.Empty
						&& dict[Limb.Wings] != Side.Empty)
					{
						return true;
					}
					break;

				default:
					return true;
			}
			return false;
		}

		/// <summary>
		/// Returns false if the special cases has bad body parts.
		/// Returns true if it is not a special case, or if the body parts are good.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="dict"></param>
		/// <returns></returns>
		private static bool CheckSpecialCases(Stock left, Stock right, Dictionary<Limb, Side> dict)
		{
			if (left.Name == "humpback" || right.Name == "humpback")
			{
				return IsHumpbackCorrect(left, right, dict);
			}
			if (left.Name == "octopus" || right.Name == "octopus")
			{
				return IsBlueRingedOctopusCorrect(left, right, dict);
			}
			if (left.Name == "walrus" || right.Name == "walrus")
			{
				return IsWalrusCorrect(left, right, dict);
			}

			return true;
		}

		/// <summary>
		/// Ensures humpback has the its torso if it has its legs.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="dict"></param>
		/// <returns></returns>
		private static bool IsHumpbackCorrect(Stock left, Stock right, Dictionary<Limb, Side> dict)
		{
			if (left.Name == "humpback")
			{
				if (right.Type == StockType.Bird
					|| right.Type == StockType.Fish
					|| right.Type == StockType.Snake)
				{
					if (dict[Limb.Torso] == Side.Left
						|| dict[Limb.FrontLegs] == Side.Left)
					{
						return dict[Limb.Torso] == Side.Left
							&& dict[Limb.FrontLegs] == Side.Left;
					}
				}

				if (dict[Limb.FrontLegs] == Side.Left)
				{
					return dict[Limb.Torso] == Side.Left;
				}
			}
			else if (right.Name == "humpback")
			{
				if (left.Type == StockType.Bird
					|| left.Type == StockType.Fish
					|| left.Type == StockType.Snake)
				{
					if (dict[Limb.Torso] == Side.Right
						|| dict[Limb.FrontLegs] == Side.Right)
					{
						return dict[Limb.Torso] == Side.Right
							&& dict[Limb.FrontLegs] == Side.Right;
					}
				}

				if (dict[Limb.FrontLegs] == Side.Right)
				{
					return dict[Limb.Torso] == Side.Right;
				}
			}
			return true;
		}

		private static bool IsBlueRingedOctopusCorrect(Stock left, Stock right, Dictionary<Limb, Side> dict)
		{
			if (left.Name == "octopus")
			{
				if (right.Type == StockType.Fish
					|| right.Type == StockType.Snake)
				{
					if (dict[Limb.Torso] == Side.Left
						|| dict[Limb.FrontLegs] == Side.Left
						|| dict[Limb.BackLegs] == Side.Left)
					{
						return dict[Limb.Torso] == Side.Left
							&& dict[Limb.FrontLegs] == Side.Left
							&& dict[Limb.BackLegs] == Side.Left;
					}
				}

				if (right.Type == StockType.Bird)
				{
					if (dict[Limb.Torso] == Side.Left
						|| dict[Limb.FrontLegs] == Side.Left)
					{
						return dict[Limb.Torso] == Side.Left
							&& dict[Limb.FrontLegs] == Side.Left;
					}
				}

				if (dict[Limb.FrontLegs] == Side.Left)
				{
					return dict[Limb.Torso] == Side.Left;
				}

				if (dict[Limb.BackLegs] == Side.Left)
				{
					return dict[Limb.Torso] == Side.Left;
				}
			}
			else if (right.Name == "octopus")
			{
				if (left.Type == StockType.Fish
					|| left.Type == StockType.Snake)
				{
					if (dict[Limb.Torso] == Side.Right
						|| dict[Limb.FrontLegs] == Side.Right
						|| dict[Limb.BackLegs] == Side.Right)
					{
						return dict[Limb.Torso] == Side.Right
							&& dict[Limb.FrontLegs] == Side.Right
							&& dict[Limb.BackLegs] == Side.Right;
					}
				}

				if (left.Type == StockType.Bird)
				{
					if (dict[Limb.Torso] == Side.Right
						|| dict[Limb.FrontLegs] == Side.Right)
					{
						return dict[Limb.Torso] == Side.Right
							&& dict[Limb.FrontLegs] == Side.Right;
					}
				}

				if (dict[Limb.FrontLegs] == Side.Right)
				{
					return dict[Limb.Torso] == Side.Right;
				}

				if (dict[Limb.BackLegs] == Side.Right)
				{
					return dict[Limb.Torso] == Side.Right;
				}
			}


			return true;
		}

		private static bool IsWalrusCorrect(Stock left, Stock right, Dictionary<Limb, Side> dict)
		{
			if (left.Name == "walrus")
			{
				// What about quadruped?

				if (dict[Limb.FrontLegs] == Side.Left
					|| dict[Limb.BackLegs] == Side.Left)
				{
					return dict[Limb.Torso] == Side.Left;
				}
			}
			else if (right.Name == "walrus")
			{
				if (dict[Limb.FrontLegs] == Side.Right
					|| dict[Limb.BackLegs] == Side.Right)
				{
					return dict[Limb.Torso] == Side.Right;
				}
			}

			return true;
		}
	}
}
