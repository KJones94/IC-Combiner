namespace Combiner.Viewmodels
{
	using Combiner.Base;
	using Combiner.Utility;

	public class MainVM : BaseViewModel
	{
		private CreatureDataVM m_CreatureDataVM;
		public CreatureDataVM CreatureDataVM
		{
			get
			{
				return this.m_CreatureDataVM;
			}

			set
			{
				if (value != this.m_CreatureDataVM)
				{
					this.m_CreatureDataVM = value;
					this.OnPropertyChanged(nameof(this.CreatureDataVM));
				}
			}
		}

		private DatabaseVM m_DatabaseVM;
		public DatabaseVM DatabaseVM
		{
			get
			{
				return this.m_DatabaseVM;
			}

			set
			{
				if (value != this.m_DatabaseVM)
				{
					this.m_DatabaseVM = value;
					this.OnPropertyChanged(nameof(this.DatabaseVM));
				}
			}
		}

		private FiltersVM m_FiltersVM;
		public FiltersVM FiltersVM
		{
			get
			{
				return this.m_FiltersVM;
			}

			set
			{
				if (value != this.m_FiltersVM)
				{
					this.m_FiltersVM = value;
					this.OnPropertyChanged(nameof(this.FiltersVM));
				}
			}
		}

		public MainVM()
		{
			Database database = new Database();
			ImportExportHandler importExportHandler = new ImportExportHandler(database);
			CreatureCsvWriter creatureCsvWriter = new CreatureCsvWriter();

			this.CreatureDataVM = new CreatureDataVM(database);
			this.FiltersVM = new FiltersVM(this.CreatureDataVM);
			this.DatabaseVM = new DatabaseVM(this.CreatureDataVM, this.FiltersVM, database, importExportHandler, creatureCsvWriter);
		}

		
	}
}
