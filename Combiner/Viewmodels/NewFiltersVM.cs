using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class NewFiltersVM : BaseViewModel
	{
		private CreatureDataVM m_creatureDataVM;

		public NewFiltersVM(CreatureDataVM creatureDataVM)
		{
			m_creatureDataVM = creatureDataVM;
		}

		public bool CreatureFilter(object obj)
		{
			if (ChosenFilters.Count == 0)
			{
				return true;
			}

			Creature creature = obj as Creature;
			if (creature != null)
			{
				bool result = true;
				foreach (CreatureFilter filter in ChosenFilters)
				{
					result = result && filter.Filter(creature);
				}
				return result;
			}
			return false;
		}

		private ObservableCollection<CreatureFilter> m_FilterChoices;
		public ObservableCollection<CreatureFilter> FilterChoices
		{
			get
			{
				return m_FilterChoices ?? 
					(m_FilterChoices = InitFilterChoices());
			}
			set
			{
				if (m_FilterChoices != value)
				{
					m_FilterChoices = value;
					OnPropertyChanged(nameof(FilterChoices));
				}
			}
		}

		private ObservableCollection<CreatureFilter> m_ChosenFilters;
		public ObservableCollection<CreatureFilter> ChosenFilters
		{
			get
			{
				return m_ChosenFilters ?? 
					(m_ChosenFilters = new ObservableCollection<CreatureFilter>());
			}
			set
			{
				if (m_ChosenFilters != value)
				{
					m_ChosenFilters = value;
					OnPropertyChanged(nameof(ChosenFilters));
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
			return new ObservableCollection<CreatureFilter>(filterChoices.OrderBy(s => s.Name));
		}

		public CreatureFilter SelectedFilter { get; set; }

		private RelayCommand m_AddFilterCommand;
		public RelayCommand AddFilterCommand
		{
			get
			{
				return m_AddFilterCommand ??
					  (m_AddFilterCommand = new RelayCommand(AddFilter));
			}
			set
			{
				if (m_AddFilterCommand != value)
				{
					m_AddFilterCommand = value;
					OnPropertyChanged(nameof(AddFilter));
				}
			}
		}

		private void AddFilter(object o)
		{
			if (SelectedFilter != null)
			{
				ChosenFilters.Add(SelectedFilter);
				ChosenFilters = new ObservableCollection<CreatureFilter>(ChosenFilters.OrderBy(s => s.Name));
				FilterChoices.Remove(SelectedFilter);
			}
		}

		private RelayCommand m_DropFilterCommand;
		public RelayCommand DropFilterCommand
		{
			get
			{
				return m_DropFilterCommand ??
					(m_DropFilterCommand = new RelayCommand(DropFilter));
			}
			set
			{
				if (m_DropFilterCommand != value)
				{
					m_DropFilterCommand = value;
					OnPropertyChanged(nameof(DropFilterCommand));
				}
			}
		}

		private void DropFilter(object o)
		{
			CreatureFilter filter = o as CreatureFilter;
			if (filter != null)
			{
				FilterChoices.Add(filter);
				FilterChoices = new ObservableCollection<CreatureFilter>(FilterChoices.OrderBy(s => s.Name));
				ChosenFilters.Remove(filter);
			}
		}
	}
}
