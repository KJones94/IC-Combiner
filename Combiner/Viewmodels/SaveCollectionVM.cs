using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Combiner
{
	public class SaveCollectionVM : BaseViewModel
	{
		DatabaseManagerVM m_DatabaseManagerVM;
		Creature m_CreatureToSave;

		public SaveCollectionVM(Creature creature, DatabaseManagerVM databaseManagerVM)
		{
			m_CreatureToSave = creature;
			m_DatabaseManagerVM = databaseManagerVM;
			// Should this reference a property instead of a function?
			SaveableCollections = new ObservableCollection<string>(m_DatabaseManagerVM.SaveableCollections());
		}


		private string m_SelectedCollection;
		public string SelectedCollection
		{
			get { return m_SelectedCollection; }
			set
			{
				if (value != m_SelectedCollection)
				{
					m_SelectedCollection = value;
					OnPropertyChanged(nameof(SelectedCollection));
				}
			}
		}

		private ObservableCollection<string> m_SaveableCollections;
		public ObservableCollection<string> SaveableCollections
		{
			get {
				return m_SaveableCollections ??
				  (m_SaveableCollections = new ObservableCollection<string>());
			}
			set
			{
				if (value != m_SaveableCollections)
				{
					m_SaveableCollections = value;
					OnPropertyChanged(nameof(SaveableCollections));
				}
			}
		}

		private ICommand m_SaveCreatureCommand;
		public ICommand SaveCreatureCommand
		{
			get
			{
				return m_SaveCreatureCommand ??
					(m_SaveCreatureCommand = new RelayCommand(SaveCreature));
			}
			set
			{
				if (value != m_SaveCreatureCommand)
				{
					m_SaveCreatureCommand = value;
					OnPropertyChanged(nameof(SaveCreatureCommand));
				}
			}
		}
		public void SaveCreature(object obj)
		{
			if (SelectedCollection != null)
			{
				m_DatabaseManagerVM.SaveCreature(m_CreatureToSave, SelectedCollection);
			}
		}
	}
}
