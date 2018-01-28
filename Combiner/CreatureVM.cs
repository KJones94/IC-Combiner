using Combiner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Combiner
{
	public class CreatureVM : BaseViewModel
	{
		//private CreatureContainer CreatureContainer = new CreatureContainer();

		private ObservableCollection<Creature> m_Creatures;
		public ObservableCollection<Creature> Creatures
		{
			get { return m_Creatures ?? (m_Creatures = new ObservableCollection<Creature>()); }
			set
			{
				if (value != m_Creatures)
				{
					m_Creatures = value;
					OnPropertyChanged(nameof(Creatures));
				}
			}
		}

		public CreatureVM()
		{
			
		}

		private ICommand m_CreateCreaturesCommand;
		public ICommand CreateCreaturesCommand
		{
			get { return m_CreateCreaturesCommand ?? 
					(m_CreateCreaturesCommand = new RelayCommand(CreateCreatures)); }
			// TODO: Do I need a set?
			set
			{
				if (value != m_CreateCreaturesCommand)
				{
					m_CreateCreaturesCommand = value;
					OnPropertyChanged(nameof(CreateCreaturesCommand));
				}
			}
		}

		private void CreateCreatures(object obj)
		{
			LuaHandler lua = new LuaHandler();

			Stock s1 = StockFactory.Instance.CreateStock("coyote", lua);
			Stock s2 = StockFactory.Instance.CreateStock("camel", lua);
			List<Creature> creatures = CreatureCombiner.Combine(s1, s2);
			foreach (Creature creature in creatures)
			{
				lua.LoadScript(creature);
				Creatures.Add(creature);
			}
			
		}
	}
}
