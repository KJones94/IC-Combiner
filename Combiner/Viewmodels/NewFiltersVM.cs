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
			if (ChosenStatFilters.Count == 0)
			{
				return true;
			}

			Creature creature = obj as Creature;
			if (creature != null)
			{
				bool result = true;
				foreach (StatFilter filter in ChosenStatFilters)
				{
					result = result && filter.Filter(creature);
				}
				return result;
			}
			return false;
		}

		private ObservableCollection<StatFilter> m_StatFilterChoices;
		public ObservableCollection<StatFilter> StatFilterChoices
		{
			get
			{
				return m_StatFilterChoices ?? 
					(m_StatFilterChoices = InitStatFilterChoices());
			}
			set
			{
				if (m_StatFilterChoices != value)
				{
					m_StatFilterChoices = value;
					OnPropertyChanged(nameof(StatFilterChoices));
				}
			}
		}

		private ObservableCollection<StatFilter> m_ChosenStatFilters;
		public ObservableCollection<StatFilter> ChosenStatFilters
		{
			get
			{
				return m_ChosenStatFilters ?? 
					(m_ChosenStatFilters = new ObservableCollection<StatFilter>());
			}
			set
			{
				if (m_ChosenStatFilters != value)
				{
					m_ChosenStatFilters = value;
					OnPropertyChanged(nameof(ChosenStatFilters));
				}
			}
		}

		private ObservableCollection<StatFilter> InitStatFilterChoices()
		{
			ObservableCollection<StatFilter> statFilterChoices = new ObservableCollection<StatFilter>();
			statFilterChoices.Add(new RankFilter());
			statFilterChoices.Add(new CoalFilter());
			statFilterChoices.Add(new ElectricityFilter());
			statFilterChoices.Add(new PowerFilter());
			statFilterChoices.Add(new HitpointsFilter());
			statFilterChoices.Add(new ArmourFilter());
			statFilterChoices.Add(new SightRadiusFilter());
			statFilterChoices.Add(new SizeFilter());
			statFilterChoices.Add(new EffectiveHitpointsFilter());
			statFilterChoices.Add(new LandSpeedFilter());
			statFilterChoices.Add(new WaterSpeedFilter());
			statFilterChoices.Add(new AirSpeedFilter());
			statFilterChoices.Add(new MeleeDamageFilter());
			statFilterChoices.Add(new RangeDamageFilter());
			return statFilterChoices;
		}

		public StatFilter SelectedStatFilter { get; set; }

		private RelayCommand m_AddStatFilterCommand;
		public RelayCommand AddStatFilterCommand
		{
			get
			{
				return m_AddStatFilterCommand ??
					  (m_AddStatFilterCommand = new RelayCommand(AddStatFilter));
			}
			set
			{
				if (m_AddStatFilterCommand != value)
				{
					m_AddStatFilterCommand = value;
					OnPropertyChanged(nameof(AddStatFilter));
				}
			}
		}

		private void AddStatFilter(object o)
		{
			if (SelectedStatFilter != null)
			{
				ChosenStatFilters.Add(SelectedStatFilter);
				ChosenStatFilters = new ObservableCollection<StatFilter>(ChosenStatFilters.OrderBy(s => s.ToString()));
				StatFilterChoices.Remove(SelectedStatFilter);
			}
		}

		private RelayCommand m_DropStatFilterCommand;
		public RelayCommand DropStatFilterCommand
		{
			get
			{
				return m_DropStatFilterCommand ??
					(m_DropStatFilterCommand = new RelayCommand(DropStatFilter));
			}
			set
			{
				if (m_DropStatFilterCommand != value)
				{
					m_DropStatFilterCommand = value;
					OnPropertyChanged(nameof(DropStatFilterCommand));
				}
			}
		}

		private void DropStatFilter(object o)
		{
			StatFilter filter = o as StatFilter;
			if (filter != null)
			{
				StatFilterChoices.Add(filter);
				StatFilterChoices = new ObservableCollection<StatFilter>(StatFilterChoices.OrderBy(s => s.ToString()));
				ChosenStatFilters.Remove(filter);
			}
		}
	}
}
