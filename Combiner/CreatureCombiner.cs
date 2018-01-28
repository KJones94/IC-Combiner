using System;
using System.Collections.Generic;
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

		public static List<Creature> Combine(Stock left, Stock right)
		{
			List<Dictionary<Limb, Side>> unprunedBodyParts = CreateUnprunedBodyParts(left, right);
			List<Dictionary<Limb, Side>> prunedBodyParts = PruneBodyParts(left, right, unprunedBodyParts);

			List<Creature> creatures = new List<Creature>();
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

			string[] clawedArachnids = new string[] { "lobster", "shrimp", "scorpion", "praying_mantis", "tarantula", "pistol shrimp", "siphonophore" };
			List<Dictionary<Limb, Side>> prunedBodyParts = new List<Dictionary<Limb, Side>>();
			foreach (Dictionary<Limb, Side> dict in bodyParts)
			{
				// check front legs edge case
				if (dict[Limb.FrontLegs] == Side.Left)
				{
					if (left.Type == StockType.Quadruped && right.Type == StockType.Bird)
					{
						if (dict[Limb.Torso] != Side.Left)
						{
							continue;
						}
					}
				}
				else if (dict[Limb.FrontLegs] == Side.Right)
					{
						if (right.Type == StockType.Quadruped && left.Type == StockType.Bird)
						{
							if (dict[Limb.Torso] != Side.Right)
							{
								continue;
							}
						}
					}

				// Torso can't be empty or null
				if (dict[Limb.Torso] == Side.Left)
				{
					switch (left.Type)
					{
						case StockType.Bird:
							if (dict[Limb.BackLegs] != Side.Empty && dict[Limb.Wings] != Side.Empty)
							{
								prunedBodyParts.Add(dict);
							}
							break;

						case StockType.Quadruped:
							if (dict[Limb.FrontLegs] != Side.Empty && dict[Limb.BackLegs] != Side.Empty)
							{
								prunedBodyParts.Add(dict);
							}
							break;

						case StockType.Arachnid:
							if (left.Name == "siphonophore")
							{
								if (dict[Limb.Claws] != Side.Empty)
									prunedBodyParts.Add(dict);
							}
							else if (dict[Limb.FrontLegs] != Side.Empty && dict[Limb.BackLegs] != Side.Empty)
							{
								if (clawedArachnids.Contains(left.Name) && dict[Limb.Claws] == Side.Empty)
								{
									continue;
								}
								prunedBodyParts.Add(dict);
							}
							break;

						case StockType.Insect:
							if (dict[Limb.FrontLegs] != Side.Empty && dict[Limb.BackLegs] != Side.Empty
							&& dict[Limb.Wings] != Side.Empty)
							{
								prunedBodyParts.Add(dict);
							}
							break;

						//case StockType.Fish:
						//	if (left.Name == "humpback" )
						//	{

						//	}
						//	break;

						default:
							prunedBodyParts.Add(dict);
							break;
					}
				}
				else if (dict[Limb.Torso] == Side.Right)
				{
					switch (right.Type)
					{
						case StockType.Bird:
							if (dict[Limb.BackLegs] != Side.Empty && dict[Limb.Wings] != Side.Empty)
							{
								prunedBodyParts.Add(dict);
							}
							break;

						case StockType.Quadruped:
							if (dict[Limb.FrontLegs] != Side.Empty && dict[Limb.BackLegs] != Side.Empty)
							{
								prunedBodyParts.Add(dict);
							}
							break;

						case StockType.Arachnid:
							if (right.Name == "siphonophore")
							{
								if (dict[Limb.Claws] != Side.Empty)
									prunedBodyParts.Add(dict);
							}
							else if (dict[Limb.FrontLegs] != Side.Empty && dict[Limb.BackLegs] != Side.Empty)
							{
								if (clawedArachnids.Contains(right.Name) && dict[Limb.Claws] == Side.Empty)
								{
									continue;
								}
								prunedBodyParts.Add(dict);
							}
							break;

						case StockType.Insect:
							if (dict[Limb.FrontLegs] != Side.Empty && dict[Limb.BackLegs] != Side.Empty
							&& dict[Limb.Wings] != Side.Empty)
							{
								prunedBodyParts.Add(dict);
							}
							break;

						default:
							prunedBodyParts.Add(dict);
							break;
					}
				}
			}

			return prunedBodyParts;
		}


	}

	

	public enum Side
	{
		Null,
		Empty,
		Left,
		Right
	}
}
