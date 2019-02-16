namespace Combiner
{
	public class MainVM : BaseViewModel
	{
		public MainVM(
			CreatureDataVM creatureDataVM,
			FiltersVM filtersVM,
			DatabaseVM databaseVM,
			SelectedCreatureVM selectedCreatureVM)
		{
			CreatureDataVM = creatureDataVM;
			FiltersVM = filtersVM;
			DatabaseVM = databaseVM;
			SelectedCreatureVM = selectedCreatureVM;
		}

		public CreatureDataVM CreatureDataVM { get; }

		public DatabaseVM DatabaseVM { get; }

		public FiltersVM FiltersVM { get; }

		public SelectedCreatureVM SelectedCreatureVM { get; }
	}
}
