namespace Combiner.Viewmodels
{
	using System.Collections.ObjectModel;
	using System.Windows.Data;
	using System.Windows.Input;

	using Combiner.Base;
	using Combiner.Models;
	using Combiner.Utility;

	public class CreatureDataVM : BaseViewModel
	{
		private Database m_Database;

		public CreatureDataVM(Database database)
		{
			this.m_Database = database;
		}

		private ObservableCollection<Creature> m_Creatures;
		public ObservableCollection<Creature> Creatures
		{
			get
			{
				return this.m_Creatures ?? (this.m_Creatures = new ObservableCollection<Creature>());
			}

			set
			{
				if (value != this.m_Creatures)
				{
					this.m_Creatures = value;
					this.CreaturesView = (ListCollectionView)CollectionViewSource.GetDefaultView(this.m_Creatures);
					this.OnPropertyChanged(nameof(this.Creatures));
				}
			}
		}

		private ListCollectionView m_CreaturesView;
		public ListCollectionView CreaturesView
		{
			get
			{
				return this.m_CreaturesView ?? (this.m_CreaturesView = (ListCollectionView)CollectionViewSource.GetDefaultView(this.Creatures));
			}

			set
			{
				if (value != this.m_CreaturesView)
				{
					this.m_CreaturesView = value;
					this.OnPropertyChanged(nameof(this.CreaturesView));
				}
			}
		}

		private Creature m_SelectedCreature;
		public Creature SelectedCreature
		{
			get
			{
				return this.m_SelectedCreature;
			}

			set
			{
				if (value != this.m_SelectedCreature)
				{
					this.m_SelectedCreature = value;
					this.OnPropertyChanged(nameof(this.SelectedCreature));
				}
			}
		}

		private ICommand m_SaveCreatureCommand;
		public ICommand SaveCreatureCommand
		{
			get
			{
				return this.m_SaveCreatureCommand ??
					(this.m_SaveCreatureCommand = new RelayCommand(this.SaveCreature));
			}

			set
			{
				if (value != this.m_SaveCreatureCommand)
				{
					this.m_SaveCreatureCommand = value;
					this.OnPropertyChanged(nameof(this.SaveCreatureCommand));
				}
			}
		}
		public void SaveCreature(object obj)
		{
			if (this.SelectedCreature != null)
			{
				this.m_Database.SaveCreature(this.SelectedCreature);
			}
		}

		private ICommand m_UnsaveCreatureCommand;
		public ICommand UnsaveCreatureCommand
		{
			get
			{
				return this.m_UnsaveCreatureCommand ??
					(this.m_UnsaveCreatureCommand = new RelayCommand(this.UnSaveCreature));
			}

			set
			{
				if (value != this.m_UnsaveCreatureCommand)
				{
					this.m_UnsaveCreatureCommand = value;
					this.OnPropertyChanged(nameof(this.UnsaveCreatureCommand));
				}
			}
		}
		public void UnSaveCreature(object obj)
		{
			if (this.SelectedCreature != null)
			{
				this.m_Database.UnsaveCreature(this.SelectedCreature);
			}
		}

	}
}
