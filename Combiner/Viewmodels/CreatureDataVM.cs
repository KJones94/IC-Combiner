using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Combiner
{
	public class CreatureDataVM : BaseViewModel
	{
		private readonly Database m_Database;

		public CreatureDataVM(Database database)
		{
			m_Database = database;
			SaveCreatureCommand = new RelayCommand(SaveCreature);
			UnsaveCreatureCommand = new RelayCommand(UnSaveCreature);
		}

		private ObservableCollection<Creature> m_Creatures;
		public ObservableCollection<Creature> Creatures
		{
			get
			{
				return m_Creatures ?? (m_Creatures = new ObservableCollection<Creature>());
			}
			set
			{
				if (value != m_Creatures)
				{
					m_Creatures = value;
					CreaturesView = (ListCollectionView)CollectionViewSource.GetDefaultView(m_Creatures);
					OnPropertyChanged(nameof(Creatures));
				}
			}
		}

		private ListCollectionView m_CreaturesView;
		public ListCollectionView CreaturesView
		{
			get
			{
				return m_CreaturesView ?? (m_CreaturesView = (ListCollectionView)CollectionViewSource.GetDefaultView(Creatures));
			}
			set
			{
				if (value != m_CreaturesView)
				{
					m_CreaturesView = value;
					OnPropertyChanged(nameof(CreaturesView));
				}
			}
		}

		private Creature m_SelectedCreature;
		public Creature SelectedCreature
		{
			get
			{
				return m_SelectedCreature;
			}
			set
			{
				if (value != m_SelectedCreature)
				{
					m_SelectedCreature = value;
					OnPropertyChanged(nameof(SelectedCreature));
				}
			}
		}

		public ICommand SaveCreatureCommand { get; }

		public ICommand UnsaveCreatureCommand { get; }

		private void SaveCreature(object obj)
		{
			if (SelectedCreature != null)
			{
				m_Database.SaveCreature(SelectedCreature);
			}
		}

		private void UnSaveCreature(object obj)
		{
			if (SelectedCreature != null)
			{
				m_Database.UnsaveCreature(SelectedCreature);
			}
		}
	}
}
