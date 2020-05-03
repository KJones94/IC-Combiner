using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combiner
{
	public class CreatureCombiner
	{
		// This is the only shared object
		private Dictionary<string, Stock> m_StockPool;
		private LuaCreatureProxy m_LuaCreatureProxy;
		private string m_AttrPath;
		private string m_StockPath;

		public CreatureCombiner(List<string> stockNames, string attrPath, string stockPath)
		{
			m_AttrPath = attrPath;
			m_StockPath = stockPath;
			m_StockPool = InitStockPool(stockNames);
		}

		private Dictionary<string, Stock> InitStockPool(List<string> stockNames)
		{
			StockFactory stockFactory = new StockFactory();
			Dictionary<string, Stock> stockPool = new Dictionary<string, Stock>();
			foreach (var stockName in stockNames)
			{
				LuaStockProxy lua = new LuaStockProxy();
				stockPool.Add(StockNames.ProperStockNames[stockName], stockFactory.CreateStock(stockName, lua, m_StockPath));
			}
			return stockPool;
		}

		public List<Creature> CreateAllPossibleCreatures(string leftName, string rightName)
		{
			List<Creature> creatures = new List<Creature>();
			List<CreatureBuilder> builders = Combine(m_StockPool[leftName], m_StockPool[rightName]);
			m_LuaCreatureProxy = new LuaCreatureProxy(m_AttrPath);
			foreach (var creature in builders)
			{
				// Use same LuaCreatureProxy to reduce time, but increases memory usage spikes
				m_LuaCreatureProxy.LoadScript(creature);
				creatures.Add(creature.BuildCreature());
			}
			return creatures;
		}

		public List<CreatureBuilder> Combine(Stock left, Stock right)
		{
			List<Dictionary<Limb, Side>> unprunedBodyParts = CreateUnprunedBodyParts(left, right);
			List<Dictionary<Limb, Side>> prunedBodyParts = PruneBodyParts(left, right, unprunedBodyParts);

			CreatureFactory creatureFactory = new CreatureFactory();
			List<CreatureBuilder> creatures = new List<CreatureBuilder>();
			foreach (Dictionary<Limb, Side> dict in prunedBodyParts)
			{
				creatures.Add(creatureFactory.CreateCreature(left, right, dict));
			}
			return creatures;
		}


		private Dictionary<Limb, Side> CopyBodyParts(Dictionary<Limb, Side> original)
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
		private List<Dictionary<Limb, Side>> GenerateBodyParts(Stock left, Stock right,
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
		private List<Dictionary<Limb, Side>> RemoveDuplicates(List<Dictionary<Limb, Side>> list)
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
		private List<Dictionary<Limb, Side>> CreateUnprunedBodyParts(Stock left, Stock right)
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
		private List<Dictionary<Limb, Side>> PruneBodyParts(Stock left, Stock right,
			List<Dictionary<Limb, Side>> bodyParts)
		{
			// Prune based on stock type and limbs
			List<Dictionary<Limb, Side>> prunedBodyParts = new List<Dictionary<Limb, Side>>();
			foreach (Dictionary<Limb, Side> dict in bodyParts)
			{
				if (dict[Limb.Torso] != Side.Empty || dict[Limb.Torso] != Side.Null)
				{
					if (IsTorsoPartsCorrect(left, right, dict))
					{
						prunedBodyParts.Add(dict);
					}
				}			
			}

			return prunedBodyParts;
		}

		// this assumes theres a fish
		private bool IsTorsoPartsCorrect(Stock left, Stock right, Dictionary<Limb, Side> dict)
		{
			if (dict[Limb.Torso] == Side.Left)
			{
				// Fish can't have front or back legs without torso
				// Snakes can't have front or back legs without torso
				// Walrus can't have front or back legs without torso
				// Man o' war can't have front or back legs without torso
				if (right.Type == StockType.Fish
					|| right.Type == StockType.Snake
					|| right.Name == StockNames.Walrus
					|| right.Name == StockNames.ManOWar)
				{
					if (dict[Limb.FrontLegs] == Side.Right || dict[Limb.BackLegs] == Side.Right)
					{
						return false;
					}
				}
				// Birds can't have front legs without torso
				if (right.Type == StockType.Bird)
				{
					if (dict[Limb.FrontLegs] == Side.Right)
					{
						return false;
					}
				}
				// Birds must have front legs with torso
				if (left.Type == StockType.Bird)
				{
					if (dict[Limb.FrontLegs] != Side.Left)
					{
						return false;
					}
				}

				// Must have wings with insect torso
				// Must have wings with bird torso
				if (left.Type == StockType.Insect 
					|| left.Type == StockType.Bird)
				{
					if (dict[Limb.Wings] == Side.Empty)
					{
						return false;
					}
				}

				// Must have claws with Clawed Arachnid torso
				if (left.Type == StockType.Arachnid)
				{
					if (StockNames.ClawedArachnids.Contains(left.Name) && dict[Limb.Claws] == Side.Empty)
					{
						return false;
					}
				}
			}

			else if (dict[Limb.Torso] == Side.Right)
			{
				// Fish can't have front or back legs without torso
				// Snake can't have front or back legs without torso
				// Walrus can't have front or back legs without torso
				// Man o' war can't have front or back legs without torso
				if (left.Type == StockType.Fish
					|| left.Type == StockType.Snake
					|| left.Name == StockNames.Walrus
					|| left.Name == StockNames.ManOWar)
				{
					if (dict[Limb.FrontLegs] == Side.Left || dict[Limb.BackLegs] == Side.Left)
					{
						return false;
					}
				}
				// Birds can't have front legs without torso
				if (left.Type == StockType.Bird)
				{
					if (dict[Limb.FrontLegs] == Side.Left)
					{
						return false;
					}
				}
				// Birds must have front legs with torso
				if (right.Type == StockType.Bird)
				{
					if (dict[Limb.FrontLegs] != Side.Right)
					{
						return false;
					}
				}
				// Must have wings with bird torso
				// Must have wings with insect torso
				if (right.Type == StockType.Insect 
					|| right.Type == StockType.Bird)
				{
					if (dict[Limb.Wings] == Side.Empty)
					{
						return false;
					}
				}
				// Must have claws with Clawed Arachnid torso
				if (right.Type == StockType.Arachnid)
				{
					if (StockNames.ClawedArachnids.Contains(right.Name) && dict[Limb.Claws] == Side.Empty)
					{
						return false;
					}
				}
			}


			return true;
		}
	}
}
