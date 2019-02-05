namespace Combiner.Filters
{
	using System.Collections.ObjectModel;
	using System.ComponentModel;

	using Combiner.Filters.OptionFilters;
	using Combiner.Models;

	public class RangeOptionsFilter : CreatureFilter
	{
		public RangeOptionsFilter()
			: base("Range Options")
		{
			this.RangeOnlyFilter.PropertyChanged += this.OnIsOptionCheckChanged;
			this.DirectRangeFilter.PropertyChanged += this.OnIsOptionCheckChanged;
			this.SonicRangeFilter.PropertyChanged += this.OnIsOptionCheckChanged;
			this.ArtilleryOnlyFilter.PropertyChanged += this.OnIsOptionCheckChanged;
			this.RockArtilleryFilter.PropertyChanged += this.OnIsOptionCheckChanged;
			this.WaterArtilleryFilter.PropertyChanged += this.OnIsOptionCheckChanged;
			this.ChemicalArtilleryFilter.PropertyChanged += this.OnIsOptionCheckChanged;
		}

		private RangeOnlyFilter m_RangeOnlyFilter;
		public RangeOnlyFilter RangeOnlyFilter
		{
			get
			{
				return this.m_RangeOnlyFilter
					?? (this.m_RangeOnlyFilter = new RangeOnlyFilter());
			}

			set
			{
				if (this.m_RangeOnlyFilter != value)
				{
					this.m_RangeOnlyFilter = value;
					this.OnPropertyChanged(nameof(this.RangeOnlyFilter));
				}
			}
		}

		private DirectRangeFilter m_DirectRangeFilter;
		public DirectRangeFilter DirectRangeFilter
		{
			get
			{
				return this.m_DirectRangeFilter
					?? (this.m_DirectRangeFilter = new DirectRangeFilter());
			}

			set
			{
				if (this.m_DirectRangeFilter != value)
				{
					this.m_DirectRangeFilter = value;
					this.OnPropertyChanged(nameof(this.DirectRangeFilter));
				}
			}
		}

		private SonicRangeFilter m_SonicRangeFilter;
		public SonicRangeFilter SonicRangeFilter
		{
			get
			{
				return this.m_SonicRangeFilter
					?? (this.m_SonicRangeFilter = new SonicRangeFilter());
			}

			set
			{
				if (this.m_SonicRangeFilter != value)
				{
					this.m_SonicRangeFilter = value;
					this.OnPropertyChanged(nameof(this.SonicRangeFilter));
				}
			}
		}

		private ArtilleryOnlyFilter m_ArtilleryOnlyFilter;
		public ArtilleryOnlyFilter ArtilleryOnlyFilter
		{
			get
			{
				return this.m_ArtilleryOnlyFilter
					?? (this.m_ArtilleryOnlyFilter = new ArtilleryOnlyFilter());
			}

			set
			{
				if (this.m_ArtilleryOnlyFilter != value)
				{
					this.m_ArtilleryOnlyFilter = value;
					this.OnPropertyChanged(nameof(this.ArtilleryOnlyFilter));
				}
			}
		}

		private RockArtilleryFilter m_RockArtilleryFilter;
		public RockArtilleryFilter RockArtilleryFilter
		{
			get
			{
				return this.m_RockArtilleryFilter
					?? (this.m_RockArtilleryFilter = new RockArtilleryFilter());
			}

			set
			{
				if (this.m_RockArtilleryFilter != value)
				{
					this.m_RockArtilleryFilter = value;
					this.OnPropertyChanged(nameof(this.RockArtilleryFilter));
				}
			}
		}

		private WaterArtilleryFilter m_WaterArtilleryFilter;
		public WaterArtilleryFilter WaterArtilleryFilter
		{
			get
			{
				return this.m_WaterArtilleryFilter
					?? (this.m_WaterArtilleryFilter = new WaterArtilleryFilter());
			}

			set
			{
				if (this.m_WaterArtilleryFilter != value)
				{
					this.m_WaterArtilleryFilter = value;
					this.OnPropertyChanged(nameof(this.WaterArtilleryFilter));
				}
			}
		}

		private ChemicalArtilleryFilter m_ChemicalArtilleryFilter;
		public ChemicalArtilleryFilter ChemicalArtilleryFilter
		{
			get
			{
				return this.m_ChemicalArtilleryFilter
					?? (this.m_ChemicalArtilleryFilter = new ChemicalArtilleryFilter());
			}

			set
			{
				if (this.m_ChemicalArtilleryFilter != value)
				{
					this.m_ChemicalArtilleryFilter = value;
					this.OnPropertyChanged(nameof(this.ChemicalArtilleryFilter));
				}
			}
		}

		private ObservableCollection<OptionFilter> m_OptionFilters;
		public ObservableCollection<OptionFilter> OptionFilters
		{
			get { return this.m_OptionFilters; }
			set
			{
				if (this.m_OptionFilters != value)
				{
					this.m_OptionFilters = value;
					this.OnPropertyChanged(nameof(this.OptionFilters));
				}
			}
		}

		private OptionFilter m_ActiveFilter;

		private void SetActiveFilter(OptionFilter filter)
		{
			if (filter.IsOptionChecked)
			{
				this.m_ActiveFilter = filter;
				this.RemoveOtherRangeOptions(filter);
			}
			else if (filter == this.m_ActiveFilter)
			{
				this.m_ActiveFilter = null;
			}
		}

		private void RemoveOtherRangeOptions(OptionFilter filter)
		{
			if (!(filter is RangeOnlyFilter))
			{
				this.RangeOnlyFilter.IsOptionChecked = false;
			}

			if (!(filter is DirectRangeFilter))
			{
				this.DirectRangeFilter.IsOptionChecked = false;
			}

			if (!(filter is SonicRangeFilter))
			{
				this.SonicRangeFilter.IsOptionChecked = false;
			}

			if (!(filter is ArtilleryOnlyFilter))
			{
				this.ArtilleryOnlyFilter.IsOptionChecked = false;
			}

			if (!(filter is RockArtilleryFilter))
			{
				this.RockArtilleryFilter.IsOptionChecked = false;
			}

			if (!(filter is WaterArtilleryFilter))
			{
				this.WaterArtilleryFilter.IsOptionChecked = false;
			}

			if (!(filter is ChemicalArtilleryFilter))
			{
				this.ChemicalArtilleryFilter.IsOptionChecked = false;
			}
		}

		public override bool Filter(Creature creature)
		{
			if (this.m_ActiveFilter != null)
			{
				return this.m_ActiveFilter.Filter(creature);
			}

			return true;
		}

		public override void ResetFilter()
		{
			this.RangeOnlyFilter.IsOptionChecked = false;
			this.DirectRangeFilter.IsOptionChecked = false;
			this.SonicRangeFilter.IsOptionChecked = false;
			this.ArtilleryOnlyFilter.IsOptionChecked = false;
			this.RockArtilleryFilter.IsOptionChecked = false;
			this.WaterArtilleryFilter.IsOptionChecked = false;
			this.ChemicalArtilleryFilter.IsOptionChecked = false;

			this.m_ActiveFilter = null;
		}

		private void OnIsOptionCheckChanged(object sender, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == nameof(OptionFilter.IsOptionChecked))
			{
				OptionFilter filter = sender as OptionFilter;
				if (filter != null)
				{
					this.SetActiveFilter(filter);
				}
			}
		}

		public override string ToString()
		{
			return nameof(RangeOptionsFilter);
		}
	}
}
