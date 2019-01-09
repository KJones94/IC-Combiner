using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Combiner
{
	public class DatabaseVM : BaseViewModel
	{
		private FiltersVM m_FiltersVM;
		private CreatureDataVM m_NewCreatureVM;

		public DatabaseVM(
			CreatureDataVM newCreatureVM,
			FiltersVM filtersVM)
		{
			m_NewCreatureVM = newCreatureVM;
			m_FiltersVM = filtersVM;
		}

		private ICommand m_CreateDatabaseCommand;
		public ICommand CreateDatabaseCommand
		{
			get
			{
				return m_CreateDatabaseCommand ??
				  (m_CreateDatabaseCommand = new RelayCommand(CreateDatabase));
			}
			set
			{
				if (value != m_CreateDatabaseCommand)
				{
					m_CreateDatabaseCommand = value;
					OnPropertyChanged(nameof(CreateDatabaseCommand));
				}
			}
		}
		private void CreateDatabase(object obj)
		{
			string text = "Creating a new database will delete and replace your current database. This could take a while (around 20-30 minutes), but a dialog box will appear when it is finished. Would you like to continue?";
			MessageBoxResult result = MessageBox.Show(text, "Database Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
			if (result == MessageBoxResult.Yes)
			{
				Database.CreateDB();
				MessageBox.Show("Finished creating the database.");
			}
		}

		private ICommand m_LoadCreaturesCommand;
		public ICommand LoadCreaturesCommand
		{
			get
			{
				return m_LoadCreaturesCommand ??
				  (m_LoadCreaturesCommand = new RelayCommand(LoadCreatures));
			}
			set
			{
				if (value != m_LoadCreaturesCommand)
				{
					m_LoadCreaturesCommand = value;
					OnPropertyChanged(nameof(LoadCreaturesCommand));
				}
			}
		}
		private void LoadCreatures(object obj)
		{
			m_NewCreatureVM.Creatures = new ObservableCollection<Creature>(Database.GetAllCreatures());
			m_NewCreatureVM.CreaturesView.Filter = m_FiltersVM.CreatureFilter;
		}

		private ICommand m_LoadSavedCreaturesCommand;
		public ICommand LoadSavedCreaturesCommand
		{
			get
			{
				return m_LoadSavedCreaturesCommand ??
				  (m_LoadSavedCreaturesCommand = new RelayCommand(LoadSavedCreatures));
			}
			set
			{
				if (value != m_LoadSavedCreaturesCommand)
				{
					m_LoadSavedCreaturesCommand = value;
					OnPropertyChanged(nameof(LoadSavedCreaturesCommand));
				}
			}
		}
		private void LoadSavedCreatures(object obj)
		{
			m_NewCreatureVM.Creatures = new ObservableCollection<Creature>(Database.GetSavedCreatures());
			m_NewCreatureVM.CreaturesView.Filter = m_FiltersVM.CreatureFilter;
		}


		private ICommand m_DeleteSavedCreaturesCommand;
		public ICommand DeleteSavedCreaturesCommand
		{
			get
			{
				return m_DeleteSavedCreaturesCommand ??
				  (m_DeleteSavedCreaturesCommand = new RelayCommand(DeleteSavedCreatures));
			}
			set
			{
				if (value != m_DeleteSavedCreaturesCommand)
				{
					m_DeleteSavedCreaturesCommand = value;
					OnPropertyChanged(nameof(DeleteSavedCreaturesCommand));
				}
			}
		}
		private void DeleteSavedCreatures(object obj)
		{
			Database.DeleteSavedCreatures();
		}

		private ICommand m_ExportSavedCreaturesCommand;
		public ICommand ExportSavedCreaturesCommand
		{
			get
			{
				return m_ExportSavedCreaturesCommand ??
					(m_ExportSavedCreaturesCommand = new RelayCommand(ExportSavedCreature));
			}
			set
			{
				if (value != m_ExportSavedCreaturesCommand)
				{
					m_ExportSavedCreaturesCommand = value;
					OnPropertyChanged(nameof(ExportSavedCreaturesCommand));
				}
			}
		}
		public void ExportSavedCreature(object obj)
		{
			ImportExportHandler.Export();
		}

		private ICommand m_ImportSavedCreaturesCommand;
		public ICommand ImportSavedCreaturesCommand
		{
			get
			{
				return m_ImportSavedCreaturesCommand ??
					(m_ImportSavedCreaturesCommand = new RelayCommand(ImportSavedCreature));
			}
			set
			{
				if (value != m_ImportSavedCreaturesCommand)
				{
					m_ImportSavedCreaturesCommand = value;
					OnPropertyChanged(nameof(ImportSavedCreaturesCommand));
				}
			}
		}
		public void ImportSavedCreature(object obj)
		{
			ImportExportHandler.Import();
		}
	}
}
