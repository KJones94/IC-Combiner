using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Combiner
{
	public class NewCreatureVM : BaseViewModel
	{
		private ObservableCollection<Creature> m_Creatures;
		public ObservableCollection<Creature> Creatures
		{
			get
			{
				return m_Creatures ?? (m_Creatures = new ObservableCollection<Creature>());
			}
			set
			{
				if (value != m_Creatures)
				{
					m_Creatures = value;
					CreaturesView = CollectionViewSource.GetDefaultView(m_Creatures);
					OnPropertyChanged(nameof(Creatures));
				}
			}
		}

		private ICollectionView m_CreaturesView;
		public ICollectionView CreaturesView
		{
			get
			{
				return m_CreaturesView ?? (m_CreaturesView = CollectionViewSource.GetDefaultView(Creatures));
			}
			set
			{
				if (value != m_CreaturesView)
				{
					m_CreaturesView = value;
					OnPropertyChanged(nameof(CreaturesView));
				}
			}
		}

		private Creature m_SelectedCreature;
		public Creature SelectedCreature
		{
			get
			{
				return m_SelectedCreature;
			}
			set
			{
				if (value != m_SelectedCreature)
				{
					m_SelectedCreature = value;
					OnPropertyChanged(nameof(SelectedCreature));
				}
			}
		}

		public NewCreatureVM()
		{
			SetDefaultFilters();
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
		private void CreateDatabase(object obj)
		{
			string text = "Creating a new database will delete and replace your current database. This could take a while (around 20-30 minutes), but a dialog box will appear when it is finished. Would you like to continue?";
			MessageBoxResult result = MessageBox.Show(text, "Database Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
			if (result == MessageBoxResult.Yes)
			{
				Database.CreateDB();
				MessageBox.Show("Finished creating the database.");
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
			Creatures = new ObservableCollection<Creature>(Database.GetAllCreatures());
			CreaturesView.Filter = CreatureFilter;
		}

		private ICommand m_LoadSavedCreaturesCommand;
		public ICommand LoadSavedCreaturesCommand
		{
			get
			{
				return m_LoadSavedCreaturesCommand ??
				  (m_LoadSavedCreaturesCommand = new RelayCommand(LoadSavedCreatures));
			}
			set
			{
				if (value != m_LoadSavedCreaturesCommand)
				{
					m_LoadSavedCreaturesCommand = value;
					OnPropertyChanged(nameof(LoadSavedCreaturesCommand));
				}
			}
		}
		private void LoadSavedCreatures(object obj)
		{
			Creatures = new ObservableCollection<Creature>(Database.GetSavedCreatures());
			CreaturesView.Filter = CreatureFilter;
		}

		private ICommand m_SaveCreatureCommand;
		public ICommand SaveCreatureCommand
		{
			get
			{
				return m_SaveCreatureCommand ??
					(m_SaveCreatureCommand = new RelayCommand(SaveCreature));
			}
			set
			{
				if (value != m_SaveCreatureCommand)
				{
					m_SaveCreatureCommand = value;
					OnPropertyChanged(nameof(SaveCreatureCommand));
				}
			}
		}
		public void SaveCreature(object obj)
		{
			if (SelectedCreature != null)
			{
				Database.SaveCreature(SelectedCreature);
			}
		}

		private ObservableCollection<string> m_AbilityChoices;
		public ObservableCollection<string> AbilityChoices
		{
			get
			{
				if (m_AbilityChoices == null)
				{
					m_AbilityChoices = new ObservableCollection<string>();
					foreach (string ability in Utility.Abilities)
					{
						m_AbilityChoices.Add(ability);
					}
				}
				return m_AbilityChoices;
			}
			set
			{
				if (value != m_AbilityChoices)
				{
					m_AbilityChoices = value;
					OnPropertyChanged(nameof(AbilityChoices));
				}
			}
		}
		public string SelectedAddAbility { get; set; }

		private ObservableCollection<string> m_ChosenAbilities = new ObservableCollection<string>();
		public ObservableCollection<string> ChosenAbilities
		{
			get
			{
				return m_ChosenAbilities;
			}
			set
			{
				if (value != m_ChosenAbilities)
				{
					m_ChosenAbilities = value;
					OnPropertyChanged(nameof(ChosenAbilities));
				}
			}
		}
		public string SelectedRemoveAbility { get; set; }

		private ICommand m_AddAbilityChoiceCommand;
		public ICommand AddAbilityChoiceCommand
		{
			get
			{
				return m_AddAbilityChoiceCommand ??
				  (m_AddAbilityChoiceCommand = new RelayCommand(AddAbilityChoice));
			}
			set
			{
				if (value != m_AddAbilityChoiceCommand)
				{
					m_AddAbilityChoiceCommand = value;
					OnPropertyChanged(nameof(AddAbilityChoiceCommand));
				}
			}
		}
		private void AddAbilityChoice(object obj)
		{
			if (!ChosenAbilities.Contains(SelectedAddAbility))
			{
				ChosenAbilities.Add(SelectedAddAbility);
				ChosenAbilities = new ObservableCollection<string>(ChosenAbilities.OrderBy(s => s));
				// sort chosen stock
				AbilityChoices.Remove(SelectedAddAbility);
			}
		}

		private ICommand m_RemoveAbilityChoiceCommand;
		public ICommand RemoveAbilityChoiceCommand
		{
			get
			{
				return m_RemoveAbilityChoiceCommand ??
				  (m_RemoveAbilityChoiceCommand = new RelayCommand(RemoveAbilityChoice));
			}
			set
			{
				if (value != m_RemoveAbilityChoiceCommand)
				{
					m_RemoveAbilityChoiceCommand = value;
					OnPropertyChanged(nameof(RemoveAbilityChoiceCommand));
				}
			}
		}
		private void RemoveAbilityChoice(object obj)
		{
			AbilityChoices.Add(SelectedRemoveAbility);
			AbilityChoices = new ObservableCollection<string>(AbilityChoices.OrderBy(s => s));
			// sort stock choices
			ChosenAbilities.Remove(SelectedRemoveAbility);
		}

		private ICommand m_RemoveAllAbilityChoicesCommand;
		public ICommand RemoveAllAbilityChoicesCommand
		{
			get
			{
				return m_RemoveAllAbilityChoicesCommand ??
				  (m_RemoveAllAbilityChoicesCommand = new RelayCommand(RemoveAllAbilityChoices));
			}
			set
			{
				if (value != m_RemoveAllAbilityChoicesCommand)
				{
					m_RemoveAllAbilityChoicesCommand = value;
					OnPropertyChanged(nameof(RemoveAllAbilityChoicesCommand));
				}
			}
		}
		private void RemoveAllAbilityChoices(object obj)
		{
			foreach (string ability in ChosenAbilities)
			{
				AbilityChoices.Add(ability);
			}
			ChosenAbilities = new ObservableCollection<string>();
			AbilityChoices = new ObservableCollection<string>(AbilityChoices.OrderBy(s => s));
		}

		private ObservableCollection<string> m_StockChoices;
		public ObservableCollection<string> StockChoices
		{
			get
			{
				if (m_StockChoices == null)
				{
					m_StockChoices = new ObservableCollection<string>();
					var stockNames = Directory.GetFiles(Utility.StockDirectory).
						Select(s => s.Replace(".lua", "").Replace(Utility.StockDirectory, ""));
					foreach (string stock in stockNames)
					{
						m_StockChoices.Add(stock);
					}
				}
				return m_StockChoices;
			}
			set
			{
				if (value != m_StockChoices)
				{
					m_StockChoices = value;
					OnPropertyChanged(nameof(StockChoices));
				}
			}
		}
		public string SelectedAddStock { get; set; }

		private ICommand m_AddStockChoiceCommand;
		public ICommand AddStockChoiceCommand
		{
			get
			{
				return m_AddStockChoiceCommand ??
				  (m_AddStockChoiceCommand = new RelayCommand(AddStockChoice));
			}
			set
			{
				if (value != m_AddStockChoiceCommand)
				{
					m_AddStockChoiceCommand = value;
					OnPropertyChanged(nameof(AddStockChoiceCommand));
				}
			}
		}
		private void AddStockChoice(object obj)
		{
			if (!ChosenStock.Contains(SelectedAddStock))
			{
				ChosenStock.Add(SelectedAddStock);
				ChosenStock = new ObservableCollection<string>(ChosenStock.OrderBy(s => s));
				// sort chosen stock
				StockChoices.Remove(SelectedAddStock);
			}
		}

		private ObservableCollection<string> m_ChosenStock = new ObservableCollection<string>();
		public ObservableCollection<string> ChosenStock
		{
			get
			{
				return m_ChosenStock;
			}
			set
			{
				if (value != m_ChosenStock)
				{
					m_ChosenStock = value;
					OnPropertyChanged(nameof(ChosenStock));
				}
			}
		}
		public string SelectedRemoveStock { get; set; }

		private ICommand m_RemoveStockChoiceCommand;
		public ICommand RemoveStockChoiceCommand
		{
			get
			{
				return m_RemoveStockChoiceCommand ??
				  (m_RemoveStockChoiceCommand = new RelayCommand(RemoveStockChoice));
			}
			set
			{
				if (value != m_RemoveStockChoiceCommand)
				{
					m_RemoveStockChoiceCommand = value;
					OnPropertyChanged(nameof(RemoveStockChoiceCommand));
				}
			}
		}
		private void RemoveStockChoice(object obj)
		{
			StockChoices.Add(SelectedRemoveStock);
			StockChoices = new ObservableCollection<string>(StockChoices.OrderBy(s => s));
			// sort stock choices
			ChosenStock.Remove(SelectedRemoveStock);
		}

		private ICommand m_RemoveAllStockChoicesCommand;
		public ICommand RemoveAllStockChoicesCommand
		{
			get
			{
				return m_RemoveAllStockChoicesCommand ??
				  (m_RemoveAllStockChoicesCommand = new RelayCommand(RemoveAllStockChoices));
			}
			set
			{
				if (value != m_RemoveStockChoiceCommand)
				{
					m_RemoveAllStockChoicesCommand = value;
					OnPropertyChanged(nameof(RemoveAllStockChoicesCommand));
				}
			}
		}
		private void RemoveAllStockChoices(object obj)
		{
			foreach (string stock in ChosenStock)
			{
				StockChoices.Add(stock);
			}
			ChosenStock = new ObservableCollection<string>();
			StockChoices = new ObservableCollection<string>(StockChoices.OrderBy(s => s));
		}

		private ICommand m_FilterCreaturesCommand;
		public ICommand FilterCreaturesCommand
		{
			get
			{
				return m_FilterCreaturesCommand ??
				  (m_FilterCreaturesCommand = new RelayCommand(FilterCreatures));
			}
			set
			{
				if (value != m_FilterCreaturesCommand)
				{
					m_FilterCreaturesCommand = value;
					OnPropertyChanged(nameof(FilterCreaturesCommand));
				}
			}
		}
		private void FilterCreatures(object obj)
		{
			CreaturesView.Filter = CreatureFilter;
		}

		private bool CreatureFilter(object obj)
		{
			Creature creature = obj as Creature;
			if (creature != null)
			{
				return FilterStats(creature)
					&& FilterSingleRangeDamage(creature)
					&& FilterRange(creature)
					&& FilterDirectRange(creature)
					&& FilterSonicRange(creature)
					&& FilterArtilleryOnly(creature)
					&& FilterWaterArtillery(creature)
					&& FilterRockArtillery(creature)
					&& FilterChemicalArtillery(creature)
					&& FilterRangeDamage(creature)
					&& FilterHorns(creature)
					&& FilterBarrierDestroy(creature)
					&& FilterPoison(creature)
					&& FilterAbilities(creature)
					&& FilterStockName(creature);
			}
			return false;
		}

		private bool FilterStats(Creature creature)
		{
			return creature.Rank >= MinRank
					&& creature.Rank <= MaxRank
					&& creature.Coal >= MinCoal
					&& creature.Coal <= MaxCoal
					&& creature.Electricity >= MinElec
					&& creature.Electricity <= MaxElec
					&& creature.Power >= MinPower
					&& creature.Power <= MaxPower
					&& creature.EffectiveHitpoints >= MinEHP
					&& creature.EffectiveHitpoints <= MaxEHP
					&& creature.Hitpoints >= MinHitpoints
					&& creature.Hitpoints <= MaxHitpoints
					&& creature.Armour >= MinArmour
					&& creature.Armour <= MaxArmour
					&& creature.SightRadius >= MinSightRadius
					&& creature.SightRadius <= MaxSightRadius
					&& creature.Size >= MinSize
					&& creature.Size <= MaxSize
					&& creature.LandSpeed >= MinLandSpeed
					&& creature.LandSpeed <= MaxLandSpeed
					&& creature.WaterSpeed >= MinWaterSpeed
					&& creature.WaterSpeed <= MaxWaterSpeed
					&& creature.AirSpeed >= MinAirSpeed
					&& creature.AirSpeed <= MaxAirSpeed
					&& creature.MeleeDamage >= MinMeleeDamage
					&& creature.MeleeDamage <= MaxMeleeDamage;
		}

		private bool FilterRangeDamage(Creature creature)
		{
			bool isBothUnderMax = creature.RangeDamage1 <= MaxRangeDamage
				&& creature.RangeDamage2 <= MaxRangeDamage;

			bool isOneOverMin = creature.RangeDamage1 >= MinRangeDamage
				|| creature.RangeDamage2 >= MinRangeDamage;

			return isBothUnderMax && isOneOverMin;
		}

		private bool FilterAbilities(Creature creature)
		{
			bool hasAbilities = true;
			foreach (string ability in ChosenAbilities)
			{
				if (creature.Abilities.ContainsKey(ability))
				{
					hasAbilities = (creature.Abilities[ability]);
					if (!hasAbilities)
					{
						break;
					}
				}
			}
			return hasAbilities;
		}

		private bool FilterArtilleryOnly(Creature creature)
		{
			if (DoArtilleryOnlyFilter)
			{
				return creature.RangeSpecial1 > 0 || creature.RangeSpecial2 > 0;
			}
			return true;
		}

		private bool FilterWaterArtillery(Creature creature)
		{
			if (DoWaterArtilleryFilter)
			{
				return creature.RangeSpecial1 == 2 || creature.RangeSpecial2 == 2;
			}
			return true;
		}

		private bool FilterRockArtillery(Creature creature)
		{
			if (DoRockArtilleryFilter)
			{
				return creature.RangeSpecial1 == 1 || creature.RangeSpecial2 == 1;
			}
			return true;
		}

		private bool FilterChemicalArtillery(Creature creature)
		{
			if (DoChemicalArtilleryFilter)
			{
				return creature.RangeSpecial1 == 3 || creature.RangeSpecial2 == 3;
			}
			return true;
		}

		private bool FilterRange(Creature creature)
		{
			if (DoRangeFilter)
			{
				return creature.RangeDamage1 > 0
					&& creature.RangeSpecial1 == 0
					&& creature.RangeSpecial2 == 0;
			}
			return true;
		}

		private bool FilterDirectRange(Creature creature)
		{
			if (DoDirectRangeFilter)
			{
				bool range1HasDirect = creature.RangeDamage1 > 0
					&& creature.RangeSpecial1 == 0
					&& (int)creature.RangeType1 != (int)DamageType.Sonic;
				bool range2HasDirect = creature.RangeDamage2 > 0
					&& creature.RangeSpecial2 == 0
					&& (int)creature.RangeType2 != (int)DamageType.Sonic;

				return range1HasDirect || range2HasDirect;
			}
			return true;
		}

		private bool FilterSonicRange(Creature creature)
		{
			if (DoSonicRangeFilter)
			{
				bool range1HasSonic = creature.RangeSpecial1 == 0
					&& (int)creature.RangeType1 == (int)DamageType.Sonic;
				bool range2HasSonic = creature.RangeSpecial2 == 0
					&& (int)creature.RangeType2 == (int)DamageType.Sonic;

				return range1HasSonic || range2HasSonic;
			}
			return true;
		}

		private bool FilterSingleRangeDamage(Creature creature)
		{
			if (DoSingleRangeFilter)
			{
				return !(creature.RangeDamage2 > 0);
			}
			return true;
		}

		private bool FilterHorns(Creature creature)
		{
			if (DoHornsFilter)
			{
				return creature.HasHorns;
			}
			return true;
		}

		private bool FilterBarrierDestroy(Creature creature)
		{
			if (DoBarrierDestroyFilter)
			{
				return creature.HasBarrierDestroy;
			}
			return true;
		}

		private bool FilterPoison(Creature creature)
		{
			if (DoPoisonFilter)
			{
				return creature.HasPoison;
			}
			return true;
		}

		private bool FilterStockName(Creature creature)
		{
			if (ChosenStock.Count > 0)
			{
				foreach(string name in ChosenStock)
				{
					if (creature.Left == name
						|| creature.Right == name)
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}



		private ICommand m_SetDefaultFiltersCommand;
		public ICommand SetDefaultFiltersCommand
		{
			get
			{
				return m_SetDefaultFiltersCommand ??
				  (m_SetDefaultFiltersCommand = new RelayCommand(SetDefaultFilters));
			}
			set
			{
				if (value != m_SetDefaultFiltersCommand)
				{
					m_SetDefaultFiltersCommand = value;
					OnPropertyChanged(nameof(SetDefaultFiltersCommand));
				}
			}
		}
		private void SetDefaultFilters(object obj)
		{
			SetDefaultFilters();
		}

		private void SetDefaultFilters()
		{
			MinRank = 0;
			MaxRank = 5;
			MinCoal = 0;
			MaxCoal = 2000;
			MinElec = 0;
			MaxElec = 2000;
			MinPower = 0;
			MaxPower = 10000;
			MinEHP = 0;
			MaxEHP = 5000;
			MinHitpoints = 0;
			MaxHitpoints = 2000;
			MinArmour = 0;
			MaxArmour = 100;
			MinSightRadius = 0;
			MaxSightRadius = 50;
			MinSize = 0;
			MaxSize = 10;
			MinLandSpeed = 0;
			MaxLandSpeed = 50;
			MinWaterSpeed = 0;
			MaxWaterSpeed = 50;
			MinAirSpeed = 0;
			MaxAirSpeed = 50;
			MinMeleeDamage = 0;
			MaxMeleeDamage = 100;
			MinRangeDamage = 0;
			MaxRangeDamage = 100;
			DoArtilleryOnlyFilter = false;
			DoSingleRangeFilter = false;
			DoDirectRangeFilter = false;
			DoSonicRangeFilter = false;
			DoHornsFilter = false;
			DoBarrierDestroyFilter = false;
			DoPoisonFilter = false;

			RemoveAllAbilityChoices(null);
			RemoveAllStockChoices(null);
		}

		private void RemoveOtherRangeFilters(string filter)
		{
			if (filter != nameof(DoRangeFilter))
			{
				DoRangeFilter = false;
			}
			if (filter != nameof(DoDirectRangeFilter))
			{
				DoDirectRangeFilter = false;
			}
			if (filter != nameof(DoSonicRangeFilter))
			{
				DoSonicRangeFilter = false;
			}
			if (filter != nameof(DoArtilleryOnlyFilter))
			{
				DoArtilleryOnlyFilter = false;
			}
			if (filter != nameof(DoWaterArtilleryFilter))
			{
				DoWaterArtilleryFilter = false;
			}
			if (filter != nameof(DoRockArtilleryFilter))
			{
				DoRockArtilleryFilter = false;
			}
			if (filter != nameof(DoChemicalArtilleryFilter))
			{
				DoChemicalArtilleryFilter = false;
			}
		}

		private int m_MinRank;
		public int MinRank
		{
			get { return m_MinRank; }
			set
			{
				if (m_MinRank != value)
				{
					m_MinRank = value;
					OnPropertyChanged(nameof(MinRank));
				}
			}
		}

		private int m_MaxRank;
		public int MaxRank
		{
			get { return m_MaxRank; }
			set
			{
				if (m_MaxRank != value)
				{
					m_MaxRank = value;
					OnPropertyChanged(nameof(MaxRank));
				}
			}
		}

		private int m_MinCoal;
		public int MinCoal
		{
			get { return m_MinCoal; }
			set
			{
				if (m_MinCoal != value)
				{
					m_MinCoal = value;
					OnPropertyChanged(nameof(MinCoal));
				}
			}
		}

		private int m_MaxCoal;
		public int MaxCoal
		{
			get { return m_MaxCoal; }
			set
			{
				if (m_MaxCoal != value)
				{
					m_MaxCoal = value;
					OnPropertyChanged(nameof(MaxCoal));
				}
			}
		}

		private int m_MinElec;
		public int MinElec
		{
			get { return m_MinElec; }
			set
			{
				if (m_MinElec != value)
				{
					m_MinElec = value;
					OnPropertyChanged(nameof(MinElec));
				}
			}
		}

		private int m_MaxElec;
		public int MaxElec
		{
			get { return m_MaxElec; }
			set
			{
				if (m_MaxElec != value)
				{
					m_MaxElec = value;
					OnPropertyChanged(nameof(MaxElec));
				}
			}
		}

		private int m_MinPower;
		public int MinPower
		{
			get { return m_MinPower; }
			set
			{
				if (m_MinPower != value)
				{
					m_MinPower = value;
					OnPropertyChanged(nameof(MinPower));
				}
			}
		}

		private int m_MaxPower;
		public int MaxPower
		{
			get { return m_MaxPower; }
			set
			{
				if (m_MaxPower != value)
				{
					m_MaxPower = value;
					OnPropertyChanged(nameof(MaxPower));
				}
			}
		}

		private int m_MinEHP;
		public int MinEHP
		{
			get { return m_MinEHP; }
			set
			{
				if (m_MinEHP != value)
				{
					m_MinEHP = value;
					OnPropertyChanged(nameof(MinEHP));
				}
			}
		}

		private int m_MaxEHP;
		public int MaxEHP
		{
			get { return m_MaxEHP; }
			set
			{
				if (m_MaxEHP != value)
				{
					m_MaxEHP = value;
					OnPropertyChanged(nameof(MaxEHP));
				}
			}
		}

		private int m_MinHitpoints;
		public int MinHitpoints
		{
			get { return m_MinHitpoints; }
			set
			{
				if (m_MinHitpoints != value)
				{
					m_MinHitpoints = value;
					OnPropertyChanged(nameof(MinHitpoints));
				}
			}
		}

		private int m_MaxHitpoints;
		public int MaxHitpoints
		{
			get { return m_MaxHitpoints; }
			set
			{
				if (m_MaxHitpoints != value)
				{
					m_MaxHitpoints = value;
					OnPropertyChanged(nameof(MaxHitpoints));
				}
			}
		}

		private int m_MinArmour;
		public int MinArmour
		{
			get { return m_MinArmour; }
			set
			{
				if (m_MinArmour != value)
				{
					m_MinArmour = value;
					OnPropertyChanged(nameof(MinArmour));
				}
			}
		}

		private int m_MaxArmour;
		public int MaxArmour
		{
			get { return m_MaxArmour; }
			set
			{
				if (m_MaxArmour != value)
				{
					m_MaxArmour = value;
					OnPropertyChanged(nameof(MaxArmour));
				}
			}
		}

		private int m_MinSightRadius;
		public int MinSightRadius
		{
			get { return m_MinSightRadius; }
			set
			{
				if (m_MinSightRadius != value)
				{
					m_MinSightRadius = value;
					OnPropertyChanged(nameof(MinSightRadius));
				}
			}
		}

		private int m_MaxSightRadius;
		public int MaxSightRadius
		{
			get { return m_MaxSightRadius; }
			set
			{
				if (m_MaxSightRadius != value)
				{
					m_MaxSightRadius = value;
					OnPropertyChanged(nameof(MaxSightRadius));
				}
			}
		}

		private int m_MinSize;
		public int MinSize
		{
			get { return m_MinSize; }
			set
			{
				if (m_MinSize != value)
				{
					m_MinSize = value;
					OnPropertyChanged(nameof(MinSize));
				}
			}
		}

		private int m_MaxSize;
		public int MaxSize
		{
			get { return m_MaxSize; }
			set
			{
				if (m_MaxSize != value)
				{
					m_MaxSize = value;
					OnPropertyChanged(nameof(MaxSize));
				}
			}
		}

		private int m_MinLandSpeed;
		public int MinLandSpeed
		{
			get { return m_MinLandSpeed; }
			set
			{
				if (m_MinLandSpeed != value)
				{
					m_MinLandSpeed = value;
					OnPropertyChanged(nameof(MinLandSpeed));
				}
			}
		}

		private int m_MaxLandSpeed;
		public int MaxLandSpeed
		{
			get { return m_MaxLandSpeed; }
			set
			{
				if (m_MaxLandSpeed != value)
				{
					m_MaxLandSpeed = value;
					OnPropertyChanged(nameof(MaxLandSpeed));
				}
			}
		}

		private int m_MinWaterSpeed;
		public int MinWaterSpeed
		{
			get { return m_MinWaterSpeed; }
			set
			{
				if (m_MinWaterSpeed != value)
				{
					m_MinWaterSpeed = value;
					OnPropertyChanged(nameof(MinWaterSpeed));
				}
			}
		}

		private int m_MaxWaterSpeed;
		public int MaxWaterSpeed
		{
			get { return m_MaxWaterSpeed; }
			set
			{
				if (m_MaxWaterSpeed != value)
				{
					m_MaxWaterSpeed = value;
					OnPropertyChanged(nameof(MaxWaterSpeed));
				}
			}
		}

		private int m_MinAirSpeed;
		public int MinAirSpeed
		{
			get { return m_MinAirSpeed; }
			set
			{
				if (m_MinAirSpeed != value)
				{
					m_MinAirSpeed = value;
					OnPropertyChanged(nameof(MinAirSpeed));
				}
			}
		}

		private int m_MaxAirSpeed;
		public int MaxAirSpeed
		{
			get { return m_MaxAirSpeed; }
			set
			{
				if (m_MaxAirSpeed != value)
				{
					m_MaxAirSpeed = value;
					OnPropertyChanged(nameof(MaxAirSpeed));
				}
			}
		}

		private int m_MinMeleeDamage;
		public int MinMeleeDamage
		{
			get { return m_MinMeleeDamage; }
			set
			{
				if (m_MinMeleeDamage != value)
				{
					m_MinMeleeDamage = value;
					OnPropertyChanged(nameof(MinMeleeDamage));
				}
			}
		}

		private int m_MaxMeleeDamage;
		public int MaxMeleeDamage
		{
			get { return m_MaxMeleeDamage; }
			set
			{
				if (m_MaxMeleeDamage != value)
				{
					m_MaxMeleeDamage = value;
					OnPropertyChanged(nameof(MaxMeleeDamage));
				}
			}
		}

		private int m_MinRangeDamage;
		public int MinRangeDamage
		{
			get { return m_MinRangeDamage; }
			set
			{
				if (m_MinRangeDamage != value)
				{
					m_MinRangeDamage = value;
					OnPropertyChanged(nameof(MinRangeDamage));
				}
			}
		}

		private int m_MaxRangeDamage;
		public int MaxRangeDamage
		{
			get { return m_MaxRangeDamage; }
			set
			{
				if (m_MaxRangeDamage != value)
				{
					m_MaxRangeDamage = value;
					OnPropertyChanged(nameof(MaxRangeDamage));
				}
			}
		}

		private bool m_DoArtilleryOnlyFilter;
		public bool DoArtilleryOnlyFilter
		{
			get
			{
				return m_DoArtilleryOnlyFilter;
			}
			set
			{
				if (m_DoArtilleryOnlyFilter != value)
				{
					// turned on
					if (value)
					{
						RemoveOtherRangeFilters(nameof(DoArtilleryOnlyFilter));
					}
					m_DoArtilleryOnlyFilter = value;
					OnPropertyChanged(nameof(DoArtilleryOnlyFilter));
				}
			}
		}

		private bool m_DoWaterArtilleryFilter;
		public bool DoWaterArtilleryFilter
		{
			get
			{
				return m_DoWaterArtilleryFilter;
			}
			set
			{
				if (m_DoWaterArtilleryFilter != value)
				{
					// turned on
					if (value)
					{
						RemoveOtherRangeFilters(nameof(DoWaterArtilleryFilter));
					}
					m_DoWaterArtilleryFilter = value;
					OnPropertyChanged(nameof(DoWaterArtilleryFilter));
				}
			}
		}

		private bool m_DoRockArtilleryFilter;
		public bool DoRockArtilleryFilter
		{
			get
			{
				return m_DoRockArtilleryFilter;
			}
			set
			{
				if (m_DoRockArtilleryFilter != value)
				{
					// turned on
					if (value)
					{
						RemoveOtherRangeFilters(nameof(DoRockArtilleryFilter));
					}
					m_DoRockArtilleryFilter = value;
					OnPropertyChanged(nameof(DoRockArtilleryFilter));
				}
			}
		}

		private bool m_DoChemicalArtilleryFilter;
		public bool DoChemicalArtilleryFilter
		{
			get
			{
				return m_DoChemicalArtilleryFilter;
			}
			set
			{
				if (m_DoChemicalArtilleryFilter != value)
				{
					// turned on
					if (value)
					{
						RemoveOtherRangeFilters(nameof(DoChemicalArtilleryFilter));
					}
					m_DoChemicalArtilleryFilter = value;
					OnPropertyChanged(nameof(DoChemicalArtilleryFilter));
				}
			}
		}

		private bool m_DoRangeFilter;
		public bool DoRangeFilter
		{
			get
			{
				return m_DoRangeFilter;
			}
			set
			{
				if (m_DoRangeFilter != value)
				{
					// turned on
					if (value)
					{
						RemoveOtherRangeFilters(nameof(DoRangeFilter));
					}
					m_DoRangeFilter = value;
					OnPropertyChanged(nameof(DoRangeFilter));
				}
			}
		}

		private bool m_DoDirectRangeFilter;
		public bool DoDirectRangeFilter
		{
			get
			{
				return m_DoDirectRangeFilter;
			}
			set
			{
				if (m_DoDirectRangeFilter != value)
				{
					// turned on
					if (value)
					{
						RemoveOtherRangeFilters(nameof(DoDirectRangeFilter));
					}
					m_DoDirectRangeFilter = value;
					OnPropertyChanged(nameof(DoDirectRangeFilter));
				}
			}
		}

		private bool m_DoSonicRangeFilter;
		public bool DoSonicRangeFilter
		{
			get
			{
				return m_DoSonicRangeFilter;
			}
			set
			{
				if (m_DoSonicRangeFilter != value)
				{
					// turned on
					if (value)
					{
						RemoveOtherRangeFilters(nameof(DoSonicRangeFilter));
					}
					m_DoSonicRangeFilter = value;
					OnPropertyChanged(nameof(DoSonicRangeFilter));
				}
			}
		}

		private bool m_DoSingleRangeFilter;
		public bool DoSingleRangeFilter
		{
			get
			{
				return m_DoSingleRangeFilter;
			}
			set
			{
				if (m_DoSingleRangeFilter != value)
				{
					m_DoSingleRangeFilter = value;
					OnPropertyChanged(nameof(DoSingleRangeFilter));
				}
			}
		}

		private bool m_DoHornsFilter;
		public bool DoHornsFilter
		{
			get
			{
				return m_DoHornsFilter;
			}
			set
			{
				if (m_DoHornsFilter != value)
				{
					m_DoHornsFilter = value;
					OnPropertyChanged(nameof(DoHornsFilter));
				}
			}
		}

		private bool m_DoBarrierDestroyFilter;
		public bool DoBarrierDestroyFilter
		{
			get
			{
				return m_DoBarrierDestroyFilter;
			}
			set
			{
				if (m_DoBarrierDestroyFilter != value)
				{
					m_DoBarrierDestroyFilter = value;
					OnPropertyChanged(nameof(DoBarrierDestroyFilter));
				}
			}
		}

		private bool m_DoPoisonFilter;
		public bool DoPoisonFilter
		{
			get
			{
				return m_DoPoisonFilter;
			}
			set
			{
				if (m_DoPoisonFilter != value)
				{
					m_DoPoisonFilter = value;
					OnPropertyChanged(nameof(DoPoisonFilter));
				}
			}
		}

	}
}
