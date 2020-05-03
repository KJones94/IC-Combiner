using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Combiner
{
	public class MainVM : BaseViewModel
	{
		private CreatureDataVM m_CreatureDataVM;
		public CreatureDataVM CreatureDataVM
		{
			get
			{
				return m_CreatureDataVM;
			}
			set
			{
				if (value != m_CreatureDataVM)
				{
					m_CreatureDataVM = value;
					OnPropertyChanged(nameof(CreatureDataVM));
				}
			}
		}

		private FiltersVM m_FiltersVM;
		public FiltersVM FiltersVM
		{
			get
			{
				return m_FiltersVM;
			}
			set
			{
				if (value != m_FiltersVM)
				{
					m_FiltersVM = value;
					OnPropertyChanged(nameof(FiltersVM));
				}
			}
		}

		private SelectedCreatureVM m_SelectedCreatureVM;
		public SelectedCreatureVM SelectedCreatureVM
		{
			get
			{
				return m_SelectedCreatureVM;
			}
			set
			{
				if (value != m_SelectedCreatureVM)
				{
					m_SelectedCreatureVM = value;
					OnPropertyChanged(nameof(SelectedCreatureVM));
				}
			}
		}

		private ProgressVM m_ProgressVM;
		public ProgressVM ProgressVM
		{
			get
			{
				return m_ProgressVM;
			}
			set
			{
				if (value != m_ProgressVM)
				{
					m_ProgressVM = value;
					OnPropertyChanged(nameof(ProgressVM));
				}
			}
		}

		private DatabaseManagerVM m_DatabaseManagerVM;
		public DatabaseManagerVM DatabaseManagerVM
		{
			get { return m_DatabaseManagerVM; }
			set
			{
				if (value != m_DatabaseManagerVM)
				{
					m_DatabaseManagerVM = value;
					OnPropertyChanged(nameof(DatabaseManagerVM));
				}
			}
		}

		private ModManagerVM m_ModManagerVM;
		public ModManagerVM ModManagerVM
		{
			get { return m_ModManagerVM; }
			set
			{
				if (value != m_ModManagerVM)
				{
					m_ModManagerVM = value;
					OnPropertyChanged(nameof(ModManagerVM));
				}
			}
		}

		private CsvWriterVM m_CsvWriterVM;
		public CsvWriterVM CsvWriterVM
		{
			get { return m_CsvWriterVM; }
			set
			{
				if (value != m_CsvWriterVM)
				{
					m_CsvWriterVM = value;
					OnPropertyChanged(nameof(CsvWriterVM));
				}
			}
		}

		private ICommand m_OpenDatabaseManagerWindowCommand;
		public ICommand OpenDatabaseManagerWindowCommand
		{
			get
			{
				return m_OpenDatabaseManagerWindowCommand ??
				  (m_OpenDatabaseManagerWindowCommand = new RelayCommand(OpenDatabaseManagerWindow));
			}
			set
			{
				if (value != m_OpenDatabaseManagerWindowCommand)
				{
					m_OpenDatabaseManagerWindowCommand = value;
					OnPropertyChanged(nameof(OpenDatabaseManagerWindow));
				}
			}
		}
		private void OpenDatabaseManagerWindow(object o)
		{
			DatabaseManagerWindow window = new DatabaseManagerWindow();
			window.DataContext = DatabaseManagerVM;
			//window.Show();
			window.ShowDialog();
		}

		private ICommand m_OpenModManagerWindowCommand;
		public ICommand OpenModManagerWindowCommand
		{
			get
			{
				return m_OpenModManagerWindowCommand ??
				  (m_OpenModManagerWindowCommand = new RelayCommand(OpenModManagerWindow));
			}
			set
			{
				if (value != m_OpenModManagerWindowCommand)
				{
					m_OpenModManagerWindowCommand = value;
					OnPropertyChanged(nameof(OpenModManagerWindow));
				}
			}
		}
		private void OpenModManagerWindow(object o)
		{
			ModManagerWindow window = new ModManagerWindow();
			window.DataContext = ModManagerVM;
			//window.Show();
			window.ShowDialog();
		}

		public MainVM()
		{
			Database database = new Database();
			ImportExportHandler importExportHandler = new ImportExportHandler(database);
			CreatureCsvWriter creatureCsvWriter = new CreatureCsvWriter();

			ProgressVM = new ProgressVM();
			DatabaseManagerVM = new DatabaseManagerVM(database, importExportHandler);
			ModManagerVM = new ModManagerVM(database, ProgressVM);
			CreatureDataVM = new CreatureDataVM(database, DatabaseManagerVM);
			CsvWriterVM = new CsvWriterVM(CreatureDataVM, creatureCsvWriter);
			FiltersVM = new FiltersVM(CreatureDataVM, ProgressVM, database, DatabaseManagerVM);
			SelectedCreatureVM = new SelectedCreatureVM(CreatureDataVM);


		}

	}
}
