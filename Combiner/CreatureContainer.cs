using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class CreatureContainer
	{
		private LuaHandler m_Lua;
		public List<Creature> Creatures { get; set; }

		public CreatureContainer(LuaHandler lua)
		{
			m_Lua = lua;
			Creatures = new List<Creature>();
		}

		public void Combine(string left, string right)
		{
			Creatures.AddRange(CreatureCombiner.Combine(
					StockFactory.Instance.CreateStock(left, m_Lua),
					StockFactory.Instance.CreateStock(right, m_Lua)));
		}

		public void RunAttrcombiner()
		{

		}
	}
}
