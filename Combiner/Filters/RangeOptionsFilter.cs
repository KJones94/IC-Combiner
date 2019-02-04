using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class RangeOptionsFilter : CreatureFilter
	{
		public RangeOptionsFilter()
			: base("Range Options")
		{
			RangeOnlyFilter.PropertyChanged += OnIsOptionCheckChanged;
			DirectRangeFilter.PropertyChanged += OnIsOptionCheckChanged;
			SonicRangeFilter.PropertyChanged += OnIsOptionCheckChanged;
			ArtilleryOnlyFilter.PropertyChanged += OnIsOptionCheckChanged;
			RockArtilleryFilter.PropertyChanged += OnIsOptionCheckChanged;
			WaterArtilleryFilter.PropertyChanged += OnIsOptionCheckChanged;
			ChemicalArtilleryFilter.PropertyChanged += OnIsOptionCheckChanged;
		}

		private RangeOnlyFilter m_RangeOnlyFilter;
		public RangeOnlyFilter RangeOnlyFilter
		{
			get
			{
				return m_RangeOnlyFilter
					?? (m_RangeOnlyFilter = new RangeOnlyFilter());
			}
			set
			{
				if (m_RangeOnlyFilter != value)
				{
					m_RangeOnlyFilter = value;
					OnPropertyChanged(nameof(RangeOnlyFilter));
				}
			}
		}

		private DirectRangeFilter m_DirectRangeFilter;
		public DirectRangeFilter DirectRangeFilter
		{
			get
			{
				return m_DirectRangeFilter
					?? (m_DirectRangeFilter = new DirectRangeFilter());
			}
			set
			{
				if (m_DirectRangeFilter != value)
				{
					m_DirectRangeFilter = value;
					OnPropertyChanged(nameof(DirectRangeFilter));
				}
			}
		}

		private SonicRangeFilter m_SonicRangeFilter;
		public SonicRangeFilter SonicRangeFilter
		{
			get
			{
				return m_SonicRangeFilter
					?? (m_SonicRangeFilter = new SonicRangeFilter());
			}
			set
			{
				if (m_SonicRangeFilter != value)
				{
					m_SonicRangeFilter = value;
					OnPropertyChanged(nameof(SonicRangeFilter));
				}
			}
		}

		private ArtilleryOnlyFilter m_ArtilleryOnlyFilter;
		public ArtilleryOnlyFilter ArtilleryOnlyFilter
		{
			get
			{
				return m_ArtilleryOnlyFilter
					?? (m_ArtilleryOnlyFilter = new ArtilleryOnlyFilter());
			}
			set
			{
				if (m_ArtilleryOnlyFilter != value)
				{
					m_ArtilleryOnlyFilter = value;
					OnPropertyChanged(nameof(ArtilleryOnlyFilter));
				}
			}
		}

		private RockArtilleryFilter m_RockArtilleryFilter;
		public RockArtilleryFilter RockArtilleryFilter
		{
			get
			{
				return m_RockArtilleryFilter
					?? (m_RockArtilleryFilter = new RockArtilleryFilter());
			}
			set
			{
				if (m_RockArtilleryFilter != value)
				{
					m_RockArtilleryFilter = value;
					OnPropertyChanged(nameof(RockArtilleryFilter));
				}
			}
		}

		private WaterArtilleryFilter m_WaterArtilleryFilter;
		public WaterArtilleryFilter WaterArtilleryFilter
		{
			get
			{
				return m_WaterArtilleryFilter
					?? (m_WaterArtilleryFilter = new WaterArtilleryFilter());
			}
			set
			{
				if (m_WaterArtilleryFilter != value)
				{
					m_WaterArtilleryFilter = value;
					OnPropertyChanged(nameof(WaterArtilleryFilter));
				}
			}
		}

		private ChemicalArtilleryFilter m_ChemicalArtilleryFilter;
		public ChemicalArtilleryFilter ChemicalArtilleryFilter
		{
			get
			{
				return m_ChemicalArtilleryFilter
					?? (m_ChemicalArtilleryFilter = new ChemicalArtilleryFilter());
			}
			set
			{
				if (m_ChemicalArtilleryFilter != value)
				{
					m_ChemicalArtilleryFilter = value;
					OnPropertyChanged(nameof(ChemicalArtilleryFilter));
				}
			}
		}

		private ObservableCollection<OptionFilter> m_OptionFilters;
		public ObservableCollection<OptionFilter> OptionFilters
		{
			get { return m_OptionFilters; }
			set
			{
				if (m_OptionFilters != value)
				{
					m_OptionFilters = value;
					OnPropertyChanged(nameof(OptionFilters));
				}
			}
		}

		private OptionFilter m_ActiveFilter;

		private void SetActiveFilter(OptionFilter filter)
		{
			if (filter.IsOptionChecked)
			{
				m_ActiveFilter = filter;
				RemoveOtherRangeOptions(filter);
			}
			else if (filter == m_ActiveFilter)
			{
				m_ActiveFilter = null;
			}
		}

		private void RemoveOtherRangeOptions(OptionFilter filter)
		{
			if (!(filter is RangeOnlyFilter))
			{
				RangeOnlyFilter.IsOptionChecked = false;
			}
			if (!(filter is DirectRangeFilter))
			{
				DirectRangeFilter.IsOptionChecked = false;
			}
			if (!(filter is SonicRangeFilter))
			{
				SonicRangeFilter.IsOptionChecked = false;
			}
			if (!(filter is ArtilleryOnlyFilter))
			{
				ArtilleryOnlyFilter.IsOptionChecked = false;
			}
			if (!(filter is RockArtilleryFilter))
			{
				RockArtilleryFilter.IsOptionChecked = false;
			}
			if (!(filter is WaterArtilleryFilter))
			{
				WaterArtilleryFilter.IsOptionChecked = false;
			}
			if (!(filter is ChemicalArtilleryFilter))
			{
				ChemicalArtilleryFilter.IsOptionChecked = false;
			}
		}

		public override bool Filter(Creature creature)
		{
			if (m_ActiveFilter != null)
			{
				return m_ActiveFilter.Filter(creature);
			}
			return true;
		}

		public override void ResetFilter()
		{
			RangeOnlyFilter.IsOptionChecked = false;
			DirectRangeFilter.IsOptionChecked = false;
			SonicRangeFilter.IsOptionChecked = false;
			ArtilleryOnlyFilter.IsOptionChecked = false;
			RockArtilleryFilter.IsOptionChecked = false;
			WaterArtilleryFilter.IsOptionChecked = false;
			ChemicalArtilleryFilter.IsOptionChecked = false;

			m_ActiveFilter = null;
		}

		private void OnIsOptionCheckChanged(object sender, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == nameof(OptionFilter.IsOptionChecked))
			{
				OptionFilter filter = sender as OptionFilter;
				if (filter != null)
				{
					SetActiveFilter(filter);
				}
			}
		}

		public override string ToString()
		{
			return nameof(RangeOptionsFilter);
		}
	}
}
