namespace Combiner.Viewmodels
{
	using System.Collections.ObjectModel;
	using System.Windows;
	using System.Windows.Input;

	using Combiner.Base;
	using Combiner.Models;
	using Combiner.Utility;

	public class DatabaseVM : BaseViewModel
	{
		private FiltersVM m_FiltersVM;
		private CreatureDataVM m_NewCreatureVM;
		private Database m_Database;
		private ImportExportHandler m_ImportExportHandler;
		private CreatureCsvWriter m_CreatureCsvWriter;

		public DatabaseVM(
			CreatureDataVM newCreatureVM,
			FiltersVM filtersVM,
			Database database,
			ImportExportHandler importExportHandler,
			CreatureCsvWriter creatureCsvWriter)
		{
			this.m_NewCreatureVM = newCreatureVM;
			this.m_FiltersVM = filtersVM;
			this.m_Database = database;
			this.m_ImportExportHandler = importExportHandler;
			this.m_CreatureCsvWriter = creatureCsvWriter;
		}

		private ICommand m_CreateDatabaseCommand;
		public ICommand CreateDatabaseCommand
		{
			get
			{
				return this.m_CreateDatabaseCommand ??
				  (this.m_CreateDatabaseCommand = new RelayCommand(this.CreateDatabase));
			}

			set
			{
				if (value != this.m_CreateDatabaseCommand)
				{
					this.m_CreateDatabaseCommand = value;
					this.OnPropertyChanged(nameof(this.CreateDatabaseCommand));
				}
			}
		}
		private void CreateDatabase(object obj)
		{
			string text = string.Empty;
			if (this.m_Database.Exists())
			{
				text = "The database has already been created. If you continue the database will be over written, including saved creatures. Would you like to continue?";
			}
			else
			{
				text = "Creating a new database will delete and replace your current database. This could take a while (around 20-30 minutes), but a dialog box will appear when it is finished. Would you like to continue?";
			}

			MessageBoxResult result = MessageBox.Show(text, "Database Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
			if (result == MessageBoxResult.Yes)
			{
				this.m_Database.CreateDB();
				MessageBox.Show("Finished creating the database.");
			}
		}

		private ICommand m_LoadCreaturesCommand;
		public ICommand LoadCreaturesCommand
		{
			get
			{
				return this.m_LoadCreaturesCommand ??
				  (this.m_LoadCreaturesCommand = new RelayCommand(this.LoadCreatures));
			}

			set
			{
				if (value != this.m_LoadCreaturesCommand)
				{
					this.m_LoadCreaturesCommand = value;
					this.OnPropertyChanged(nameof(this.LoadCreaturesCommand));
				}
			}
		}
		private void LoadCreatures(object obj)
		{
			this.m_NewCreatureVM.Creatures = new ObservableCollection<Creature>(this.m_Database.GetAllCreatures());
			this.m_NewCreatureVM.CreaturesView.Filter = this.m_FiltersVM.CreatureFilter;
		}

		private ICommand m_LoadSavedCreaturesCommand;
		public ICommand LoadSavedCreaturesCommand
		{
			get
			{
				return this.m_LoadSavedCreaturesCommand ??
				  (this.m_LoadSavedCreaturesCommand = new RelayCommand(this.LoadSavedCreatures));
			}

			set
			{
				if (value != this.m_LoadSavedCreaturesCommand)
				{
					this.m_LoadSavedCreaturesCommand = value;
					this.OnPropertyChanged(nameof(this.LoadSavedCreaturesCommand));
				}
			}
		}
		private void LoadSavedCreatures(object obj)
		{
			this.m_NewCreatureVM.Creatures = new ObservableCollection<Creature>(this.m_Database.GetSavedCreatures());
			this.m_NewCreatureVM.CreaturesView.Filter = this.m_FiltersVM.CreatureFilter;
		}


		private ICommand m_DeleteSavedCreaturesCommand;
		public ICommand DeleteSavedCreaturesCommand
		{
			get
			{
				return this.m_DeleteSavedCreaturesCommand ??
				  (this.m_DeleteSavedCreaturesCommand = new RelayCommand(this.DeleteSavedCreatures));
			}

			set
			{
				if (value != this.m_DeleteSavedCreaturesCommand)
				{
					this.m_DeleteSavedCreaturesCommand = value;
					this.OnPropertyChanged(nameof(this.DeleteSavedCreaturesCommand));
				}
			}
		}
		private void DeleteSavedCreatures(object obj)
		{
			this.m_Database.DeleteSavedCreatures();
		}

		private ICommand m_ExportSavedCreaturesCommand;
		public ICommand ExportSavedCreaturesCommand
		{
			get
			{
				return this.m_ExportSavedCreaturesCommand ??
					(this.m_ExportSavedCreaturesCommand = new RelayCommand(this.ExportSavedCreature));
			}

			set
			{
				if (value != this.m_ExportSavedCreaturesCommand)
				{
					this.m_ExportSavedCreaturesCommand = value;
					this.OnPropertyChanged(nameof(this.ExportSavedCreaturesCommand));
				}
			}
		}
		public void ExportSavedCreature(object obj)
		{
			this.m_ImportExportHandler.Export();
		}

		private ICommand m_ImportSavedCreaturesCommand;
		public ICommand ImportSavedCreaturesCommand
		{
			get
			{
				return this.m_ImportSavedCreaturesCommand ??
					(this.m_ImportSavedCreaturesCommand = new RelayCommand(this.ImportSavedCreature));
			}

			set
			{
				if (value != this.m_ImportSavedCreaturesCommand)
				{
					this.m_ImportSavedCreaturesCommand = value;
					this.OnPropertyChanged(nameof(this.ImportSavedCreaturesCommand));
				}
			}
		}
		public void ImportSavedCreature(object obj)
		{
			this.m_ImportExportHandler.Import();
		}

		private ICommand m_ExportToCsvCommand;
		public ICommand ExportToCsvCommand
		{
			get
			{
				return this.m_ExportToCsvCommand ??
					(this.m_ExportToCsvCommand = new RelayCommand(this.ExportToCsv));
			}

			set
			{
				if (value != this.m_ExportToCsvCommand)
				{
					this.m_ExportToCsvCommand = value;
					this.OnPropertyChanged(nameof(this.ExportToCsvCommand));
				}
			}
		}
		public void ExportToCsv(object obj)
		{
			if (this.m_NewCreatureVM.Creatures.Count > 0)
			{
				this.m_CreatureCsvWriter.WriteFile(this.m_NewCreatureVM.Creatures);
			}
		}
	}
}
