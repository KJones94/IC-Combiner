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

		private DatabaseVM m_DatabaseVM;
		public DatabaseVM DatabaseVM
		{
			get
			{
				return m_DatabaseVM;
			}
			set
			{
				if (value != m_DatabaseVM)
				{
					m_DatabaseVM = value;
					OnPropertyChanged(nameof(DatabaseVM));
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

		public MainVM()
		{
			Database database = new Database();
			ImportExportHandler importExportHandler = new ImportExportHandler(database);
			CreatureCsvWriter creatureCsvWriter = new CreatureCsvWriter();

			CreatureDataVM = new CreatureDataVM(database);
			FiltersVM = new FiltersVM(CreatureDataVM);
			DatabaseVM = new DatabaseVM(CreatureDataVM, FiltersVM, database, importExportHandler, creatureCsvWriter);
			SelectedCreatureVM = new SelectedCreatureVM(CreatureDataVM);
		}

	}
}
