using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Combiner
{
	public class DatabaseVM : BaseViewModel
	{
		private FiltersVM m_FiltersVM;
		private CreatureDataVM m_CreatureVM;
		private ProgressVM m_ProgressVM;
		private Database m_Database;
		private ImportExportHandler m_ImportExportHandler;
		private CreatureCsvWriter m_CreatureCsvWriter;

		public DatabaseVM(
			CreatureDataVM newCreatureVM,
			FiltersVM filtersVM,
			ProgressVM progressVM,
			Database database,
			ImportExportHandler importExportHandler,
			CreatureCsvWriter creatureCsvWriter)
		{
			m_CreatureVM = newCreatureVM;
			m_FiltersVM = filtersVM;
			m_ProgressVM = progressVM;
			m_Database = database;
			m_ImportExportHandler = importExportHandler;
			m_CreatureCsvWriter = creatureCsvWriter;
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
		private async void CreateDatabase(object obj)
		{
			string text = string.Empty;
			if (m_Database.Exists())
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
				m_ProgressVM.StartWork();

				// TODO: How does this act if some other code tries to use the ProgressVM?
				await Task.Run(() => m_Database.CreateDB());

				m_ProgressVM.EndWork();

				MessageBox.Show("Finished creating the database.");
				m_CreatureVM.UpdateTotalCreatureCount();
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
			m_CreatureVM.Creatures = new ObservableCollection<Creature>(m_Database.GetAllCreatures());
			m_CreatureVM.CreaturesView.Filter = m_FiltersVM.CreatureFilter;
		}

		private ICommand m_ExportToCsvCommand;
		public ICommand ExportToCsvCommand
		{
			get
			{
				return m_ExportToCsvCommand ??
					(m_ExportToCsvCommand = new RelayCommand(ExportToCsv));
			}
			set
			{
				if (value != m_ExportToCsvCommand)
				{
					m_ExportToCsvCommand = value;
					OnPropertyChanged(nameof(ExportToCsvCommand));
				}
			}
		}
		public void ExportToCsv(object obj)
		{
			if (m_CreatureVM.Creatures.Count > 0)
			{
				m_CreatureCsvWriter.WriteFile(m_CreatureVM.Creatures);
			}
		}
	}
}
