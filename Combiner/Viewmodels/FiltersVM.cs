namespace Combiner.Viewmodels
{
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Windows.Input;

	using Combiner.Base;
	using Combiner.Filters;
	using Combiner.Filters.OptionFilters;
	using Combiner.Filters.SelectionFilters;
	using Combiner.Filters.StatFilters;
	using Combiner.Models;

	public class FiltersVM : BaseViewModel
	{
		private CreatureDataVM m_CreatureDataVM;

		public FiltersVM(CreatureDataVM creatureDataVM)
		{
			this.m_CreatureDataVM = creatureDataVM;
		}


		private ObservableCollection<CreatureFilter> m_FilterChoices;
		public ObservableCollection<CreatureFilter> FilterChoices
		{
			get
			{
				return this.m_FilterChoices ??
					(this.m_FilterChoices = this.InitFilterChoices());
			}
			set
			{
				if (this.m_FilterChoices != value)
				{
					this.m_FilterChoices = value;
					this.OnPropertyChanged(nameof(this.FilterChoices));
				}
			}
		}

		private ObservableCollection<CreatureFilter> m_ChosenFilters;
		public ObservableCollection<CreatureFilter> ChosenFilters
		{
			get
			{
				return this.m_ChosenFilters ??
					(this.m_ChosenFilters = new ObservableCollection<CreatureFilter>());
			}
			set
			{
				if (this.m_ChosenFilters != value)
				{
					this.m_ChosenFilters = value;
					this.OnPropertyChanged(nameof(this.ChosenFilters));
				}
			}
		}

		private ObservableCollection<CreatureFilter> InitFilterChoices()
		{
			ObservableCollection<CreatureFilter> filterChoices = new ObservableCollection<CreatureFilter>();
			filterChoices.Add(new RankFilter());
			filterChoices.Add(new CoalFilter());
			filterChoices.Add(new ElectricityFilter());
			filterChoices.Add(new PowerFilter());
			filterChoices.Add(new HitpointsFilter());
			filterChoices.Add(new ArmourFilter());
			filterChoices.Add(new SightRadiusFilter());
			filterChoices.Add(new SizeFilter());
			filterChoices.Add(new EffectiveHitpointsFilter());
			filterChoices.Add(new LandSpeedFilter());
			filterChoices.Add(new WaterSpeedFilter());
			filterChoices.Add(new AirSpeedFilter());
			filterChoices.Add(new MeleeDamageFilter());
			filterChoices.Add(new RangeDamageFilter());
			filterChoices.Add(new AbilityFilter());
			filterChoices.Add(new StockFilter());
			filterChoices.Add(new SingleRangedFilter());
			filterChoices.Add(new HornsFilter());
			filterChoices.Add(new BarrierDestroyFilter());
			filterChoices.Add(new PoisonFilter());
			filterChoices.Add(new RangeOptionsFilter());
			return new ObservableCollection<CreatureFilter>(filterChoices.OrderBy(s => s.Name));
		}

		public CreatureFilter SelectedFilter { get; set; }

		private RelayCommand m_AddFilterCommand;
		public RelayCommand AddFilterCommand
		{
			get
			{
				return this.m_AddFilterCommand ??
					  (this.m_AddFilterCommand = new RelayCommand(this.AddFilter));
			}
			set
			{
				if (this.m_AddFilterCommand != value)
				{
					this.m_AddFilterCommand = value;
					this.OnPropertyChanged(nameof(this.AddFilter));
				}
			}
		}

		private void AddFilter(object o)
		{
			if (this.SelectedFilter != null)
			{
				this.ChosenFilters.Add(this.SelectedFilter);
				this.ChosenFilters = new ObservableCollection<CreatureFilter>(this.ChosenFilters.OrderBy(s => s.Name));
				this.FilterChoices.Remove(this.SelectedFilter);
			}
		}

		private RelayCommand m_DropFilterCommand;
		public RelayCommand DropFilterCommand
		{
			get
			{
				return this.m_DropFilterCommand ??
					(this.m_DropFilterCommand = new RelayCommand(this.DropFilter));
			}
			set
			{
				if (this.m_DropFilterCommand != value)
				{
					this.m_DropFilterCommand = value;
					this.OnPropertyChanged(nameof(this.DropFilterCommand));
				}
			}
		}

		private void DropFilter(object o)
		{
			CreatureFilter filter = o as CreatureFilter;
			if (filter != null)
			{
				this.FilterChoices.Add(filter);
				this.FilterChoices = new ObservableCollection<CreatureFilter>(this.FilterChoices.OrderBy(s => s.Name));
				this.ChosenFilters.Remove(filter);
			}
		}

		private RelayCommand m_DropAllFiltersCommand;
		public RelayCommand DropAllFiltersCommand
		{
			get
			{
				return this.m_DropAllFiltersCommand ??
					(this.m_DropAllFiltersCommand = new RelayCommand(this.DropAllFilters));
			}
			set
			{
				if (this.m_DropAllFiltersCommand != value)
				{
					this.m_DropAllFiltersCommand = value;
					this.OnPropertyChanged(nameof(this.DropAllFiltersCommand));
				}
			}
		}

		private void DropAllFilters(object o)
		{
			this.ChosenFilters.ToList().ForEach(this.FilterChoices.Add);
			this.FilterChoices = new ObservableCollection<CreatureFilter>(this.FilterChoices.OrderBy(s => s.Name));
			this.ChosenFilters = new ObservableCollection<CreatureFilter>();
		}

		private RelayCommand m_AddAllFiltersCommand;
		public RelayCommand AddAllFiltersCommand
		{
			get
			{
				return this.m_AddAllFiltersCommand ??
					(this.m_AddAllFiltersCommand = new RelayCommand(this.AddAllFilters));
			}
			set
			{
				if (this.m_AddAllFiltersCommand != value)
				{
					this.m_AddAllFiltersCommand = value;
					this.OnPropertyChanged(nameof(this.AddAllFiltersCommand));
				}
			}
		}

		private void AddAllFilters(object o)
		{
			this.FilterChoices.ToList().ForEach(this.ChosenFilters.Add);
			this.ChosenFilters = new ObservableCollection<CreatureFilter>(this.ChosenFilters.OrderBy(s => s.Name));
			this.FilterChoices = new ObservableCollection<CreatureFilter>();
		}

		private RelayCommand m_ResetFiltersCommand;
		public RelayCommand ResetFiltersCommand
		{
			get
			{
				return this.m_ResetFiltersCommand ??
					(this.m_ResetFiltersCommand = new RelayCommand(this.ResetFilters));
			}
			set
			{
				if (this.m_ResetFiltersCommand != value)
				{
					this.m_ResetFiltersCommand = value;
					this.OnPropertyChanged(nameof(this.ResetFiltersCommand));
				}
			}
		}

		private void ResetFilters(object o)
		{
			this.DropAllFilters(o);
			foreach (CreatureFilter filter in this.FilterChoices)
			{
				filter.ResetFilter();
			}
		}

		private ICommand m_FilterCreaturesCommand;
		public ICommand FilterCreaturesCommand
		{
			get
			{
				return this.m_FilterCreaturesCommand ??
				  (this.m_FilterCreaturesCommand = new RelayCommand(this.FilterCreatures));
			}
			set
			{
				if (value != this.m_FilterCreaturesCommand)
				{
					this.m_FilterCreaturesCommand = value;
					this.OnPropertyChanged(nameof(this.FilterCreaturesCommand));
				}
			}
		}
		private void FilterCreatures(object obj)
		{
			this.m_CreatureDataVM.CreaturesView.Filter = this.CreatureFilter;
		}

		public bool CreatureFilter(object obj)
		{
			if (this.ChosenFilters.Count == 0)
			{
				return true;
			}

			Creature creature = obj as Creature;
			if (creature != null)
			{
				bool result = true;
				foreach (CreatureFilter filter in this.ChosenFilters)
				{
					result = result && filter.Filter(creature);
				}
				return result;
			}
			return false;
		}
	}
}
