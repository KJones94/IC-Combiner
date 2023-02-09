using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class CreatureBuilder
	{
		public StockStatCalculator Left { get; set; }
		public StockStatCalculator Right { get; set; }
		public CreatureStatCalculator Calculator { get; set; }
		Dictionary<Limb, Side> ChosenBodyParts { get; set; }

		public Dictionary<string, double> GameAttributes { get; set; } = new Dictionary<string, double>();

		private const double m_MinWaterSpeed = 12.0;
		private const double m_MinLandSpeed = 15.0;
		private const double m_MinAirSpeed = 16.0;

		#region Stat Properties

		public double Size
		{
			get { return GameAttributes[Attributes.Size]; }
			set { GameAttributes[Attributes.Size] = value; }
		}
		public double SightRadius
		{
			get { return GameAttributes[Attributes.SightRadius]; }
			set { GameAttributes[Attributes.SightRadius] = value; }
		}
		public double Armour
		{
			get { return GameAttributes[Attributes.Armour]; }
			set { GameAttributes[Attributes.Armour] = value; }
		}
		public double Hitpoints
		{
			get { return GameAttributes[Attributes.Hitpoints]; }
			set { GameAttributes[Attributes.Hitpoints] = value; }
		}

		public double EffectiveHealth
		{
			get { return Hitpoints / (1 - Armour); }
		}

		public double LandSpeed
		{
			get { return GameAttributes[Attributes.LandSpeed]; }
			set { GameAttributes[Attributes.LandSpeed] = value; }
		}
		public double AirSpeed
		{
			get { return GameAttributes[Attributes.AirSpeed]; }
			set { GameAttributes[Attributes.AirSpeed] = value; }
		}
		public double WaterSpeed
		{
			get { return GameAttributes[Attributes.WaterSpeed]; }
			set { GameAttributes[Attributes.WaterSpeed] = value; }
		}
		public double MeleeDamage
		{
			get { return GameAttributes[Attributes.MeleeDamage]; }
			set { GameAttributes[Attributes.MeleeDamage] = value; }
		}

		public double IsLand
		{
			get { return GameAttributes[Attributes.IsLand]; }
			set { GameAttributes[Attributes.IsLand] = value; }
		}
		public double IsSwimmer
		{
			get { return GameAttributes[Attributes.IsSwimmer]; }
			set { GameAttributes[Attributes.IsSwimmer] = value; }
		}
		public double IsFlyer
		{
			get { return GameAttributes[Attributes.IsFlyer]; }
			set { GameAttributes[Attributes.IsFlyer] = value; }
		}

		public double Ticks
		{
			get { return GameAttributes[Attributes.Ticks]; }
			set { GameAttributes[Attributes.Ticks] = value; }
		}

		public double Rank
		{
			get { return GameAttributes[Attributes.Rank]; }
			set { GameAttributes[Attributes.Rank] = value; }
		}

		public double Coal
		{
			get { return GameAttributes[Attributes.Coal]; }
			set { GameAttributes[Attributes.Coal] = value; }
		}

		public double Electricity
		{
			get { return GameAttributes[Attributes.Electricity]; }
			set { GameAttributes[Attributes.Electricity] = value; }
		}

		public double Power
		{
			get { return GameAttributes[Attributes.Power]; }
			set { GameAttributes[Attributes.Power] = value; }
		}

		public double PopSize
		{
			get { return GameAttributes[Attributes.PopSize]; }
			set { GameAttributes[Attributes.PopSize] = value; }
		}

		public double AbilityAdjustedPower {
			get
			{
				// Check for passive abilities that affect HP, Armor, or damage
				var hp = Hitpoints;
				var armor = Armour;
				var dps = GameAttributes[Attributes.Mixed_DPS];

				// Handle pack, herding, regen, frenzy

				if (HasPassiveAbility(AbilityNames.PackHunter))
				{
					dps *= 1.3; // Pack hunter bonus
				}

				if (HasPassiveAbility(AbilityNames.Frenzy))
				{
					dps *= 1.5;
					hp /= 1.3; // Account for incoming damage increase
				}

				if (HasPassiveAbility(AbilityNames.Herding))
				{
					armor *= 1.3;
					armor = Math.Min(armor, .6);
				}

				if (HasPassiveAbility(AbilityNames.Regeneration))
				{
					// I guess maybe this is good enough?
					hp *= 1.1;
				}

				var ehp = hp / (1 - armor);

				return Math.Pow(ehp, 0.608) * ((0.22 * dps) + 2.8);
			}
		}

		private bool HasPassiveAbility(string abilityName)
		{
			return GameAttributes[abilityName] > 0;
		}

		#endregion

		public CreatureBuilder(Stock left, Stock right, Dictionary<Limb, Side> chosenBodyParts)
		{
			Left =  new StockStatCalculator(left);
			Right = new StockStatCalculator(right);
			ChosenBodyParts = chosenBodyParts;
			Calculator = new CreatureStatCalculator(Left, Right, ChosenBodyParts);
			InitGameAttributes();
			InitStats();
			InitAbilities();
			FixTunaLeapAttack();
			FixNarwhalChargeAttack();
		}

		private StockStatCalculator GetStockSide(Limb limb)
		{
			if (ChosenBodyParts[limb] == Side.Left)
			{
				return Left;
			}
			else if (ChosenBodyParts[limb] == Side.Right)
			{
				return Right;
			}
			else
			{
				return null;
			}
		}

		private Stock OtherSide(StockStatCalculator stock)
		{
			if (stock.Name == Left.Name)
			{
				return Right.Stock;
			}
			else
			{
				return Left.Stock;
			}
		}

		private void InitGameAttributes()
		{
			GameAttributes.Add(Attributes.Ticks, 0);
			GameAttributes.Add(Attributes.Rank, 0);
			GameAttributes.Add(Attributes.Coal, 0);
			GameAttributes.Add(Attributes.Electricity, 0);
			

			GameAttributes.Add(Attributes.Power, 0);
			GameAttributes.Add(Attributes.PopSize, 0);
			GameAttributes.Add(Attributes.effective_mixed_dps, 0);
			GameAttributes.Add(Attributes.scaling_size, 0);
			GameAttributes.Add(Attributes.Mixed_DPS, 0);

			GameAttributes.Add(Attributes.Size, 0);
			GameAttributes.Add(Attributes.SightRadius, 0);
			GameAttributes.Add(Attributes.Armour, 0);
			GameAttributes.Add(Attributes.Hitpoints, 0);
			GameAttributes.Add(Attributes.LandSpeed, 0);
			GameAttributes.Add(Attributes.WaterSpeed, 0);
			GameAttributes.Add(Attributes.AirSpeed, 0);
			GameAttributes.Add(Attributes.MeleeDamage, 0);

			for (int i = 2; i < 9; i++)
			{
				GameAttributes.Add(Attributes.RangeDamage[i], 0);
				GameAttributes.Add(Attributes.RangeMax[i], 0);
				GameAttributes.Add(Attributes.RangeType[i], 0);
				GameAttributes.Add(Attributes.RangeSpecial[i], 0);
				GameAttributes.Add(Attributes.MeleeType[i], 0);
			}

			GameAttributes.Add(Attributes.IsLand, 0);
			GameAttributes.Add(Attributes.IsSwimmer, 0);
			GameAttributes.Add(Attributes.IsFlyer, 0);

			foreach (string ability in AbilityNames.Abilities)
			{
				GameAttributes.Add(ability, 0);
			}

			GameAttributes.Add("damage", 0);
		}

		private void InitStats()
		{
			// Calculate total for all limbs
			Hitpoints = Calculator.CalcHitpoints();
			Size = Calculator.CalcSize();
			Armour = Calculator.CalcArmour();
			SightRadius = Calculator.CalcSightRadius();
			LandSpeed = Calculator.CalcLandSpeed();
			WaterSpeed = Calculator.CalcWaterSpeed();
			AirSpeed = Calculator.CalcAirSpeed();
			MeleeDamage = Calculator.CalcMeleeDamage();

			// Independent limb values
			CalcRangeDamage();
			CalcRangeMax();
			SetRangeType();
			SetRangeSpecial();
			SetMeleeType();

			// Ensure speed values set properly
			if (HasAirSpeed())
			{
				IsFlyer = 1;
				AirSpeed = Math.Max(AirSpeed, m_MinAirSpeed);
				LandSpeed = 0;
				WaterSpeed = 0;
			}
			else 
			{
				if (HasWaterSpeed())
				{
					IsSwimmer = 1;
					WaterSpeed = Math.Max(WaterSpeed, m_MinWaterSpeed);
				}
				if (HasLandSpeed())
				{
					IsLand = 1;
					LandSpeed = Math.Max(LandSpeed, m_MinLandSpeed);
				}
				else
				{
					LandSpeed = 0;
				}
				
			}
		}

		private void InitAbilities()
		{
			InitPassiveAbilities();
			// TODO: Could hardcode or cache the limbs for abilities to reduce inner loop
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				StockStatCalculator side = GetStockSide(limb);
				if (side == null)
					continue;
				foreach (string ability in AbilityNames.Abilities)
				{
					int bodyPart = side.GetLimbAttributeBodyPart(ability);
					if (bodyPart > -1
						&& (Limb)bodyPart == limb
						&& side.GetLimbAttributeValue(ability) > 0)
					{
						GameAttributes[ability] = 1;
					}
				}
			}
		}

		private void InitPassiveAbilities()
		{
			foreach (string ability in AbilityNames.Abilities)
			{
				if (Left.GetLimbAttributeBodyPart(ability) == 0
					&& Left.GetLimbAttributeValue(ability) > 0)
				{
					GameAttributes[ability] = 1;
					continue;
				}

				if (Right.GetLimbAttributeBodyPart(ability) == 0
					&& Right.GetLimbAttributeValue(ability) > 0)
				{
					GameAttributes[ability] = 1;
					continue;
				}
			}
		}

		private void FixTunaLeapAttack()
		{
			if (Left.Stock.Name == StockNames.Tuna || Right.Stock.Name == StockNames.Tuna)
			{
				StockStatCalculator backLegsSide = GetStockSide(Limb.BackLegs);

				// Check if using back legs for leap attack
				if (GameAttributes[AbilityNames.LeapAttack] > 0
					&& backLegsSide != null
					&& backLegsSide.GetLimbAttributeValue(AbilityNames.LeapAttack) > 0)
				{
					return; // Leap attack is good
				}

				// Check if tuna leap is good
				StockStatCalculator tailSide = GetStockSide(Limb.Tail);
				if (tailSide.Name == StockNames.Tuna
					&& !HasLandSpeed()
					&& !HasAirSpeed()
					&& GetStockSide(Limb.Torso).Name != StockNames.GiantSquid) // Special case idk why
				{
					GameAttributes[AbilityNames.LeapAttack] = 1; // Leap attack is good
				}
				else
				{
					GameAttributes[AbilityNames.LeapAttack] = 0; // Bad leap attack from tuna
				}
			}
		}

		private void FixNarwhalChargeAttack()
		{
			if (Left.Stock.Name == StockNames.Narwhal || Right.Stock.Name == StockNames.Narwhal)
			{
				StockStatCalculator backLegsSide = GetStockSide(Limb.BackLegs);

				// Check if using back legs for charge attack
				if (GameAttributes[AbilityNames.ChargeAttack] > 0
					&& backLegsSide != null
					&& backLegsSide.GetLimbAttributeValue(AbilityNames.ChargeAttack) > 0)
				{
					return; // Charge attack is good
				}

				// Check if narwhal charge is good
				StockStatCalculator tailSide = GetStockSide(Limb.Tail);
				if (tailSide.Name == StockNames.Narwhal
					&& !HasLandSpeed()
					&& !HasAirSpeed()
					&& GetStockSide(Limb.Torso).Name != StockNames.GiantSquid) // Special case idk why
				{
					GameAttributes[AbilityNames.ChargeAttack] = 1; // Charge attack is good
				}
				else
				{
					GameAttributes[AbilityNames.ChargeAttack] = 0; // Bad charge attack from tuna
				}
			}
		}

		private bool HasLandSpeed()
		{
			// If wings then no land
			if (ChosenBodyParts[Limb.Wings] == Side.Left
				|| ChosenBodyParts[Limb.Wings] == Side.Right)
			{
				return false;
			}
			// If snake torso then land
			// Except for eel
			if (GetStockSide(Limb.Torso).Type == StockType.Snake
				&& GetStockSide(Limb.Torso).Stock.Name != StockNames.ElectricEel && GetStockSide(Limb.Torso).Stock.Name != StockNames.SeaSnake)
			{
				return true;
			}
			// Both legs are land walkable
			if ((GetStockSide(Limb.FrontLegs).Type == StockType.Quadruped
				|| GetStockSide(Limb.FrontLegs).Type == StockType.Insect
				|| GetStockSide(Limb.FrontLegs).Type == StockType.Arachnid)
				&& (GetStockSide(Limb.BackLegs).Type == StockType.Quadruped
				|| GetStockSide(Limb.BackLegs).Type == StockType.Insect
				|| GetStockSide(Limb.BackLegs).Type == StockType.Arachnid
				|| GetStockSide(Limb.BackLegs).Type == StockType.Bird)
				&& GetStockSide(Limb.BackLegs).Stock.Name != StockNames.ManOWar
				&& GetStockSide(Limb.FrontLegs).Stock.Name != StockNames.ManOWar)
			{
				return true;
			}
			return false;
		}

		private bool HasWaterSpeed()
		{
			// If wings then no water
			if (ChosenBodyParts[Limb.Wings] == Side.Left
				|| ChosenBodyParts[Limb.Wings] == Side.Right)
			{
				return false;
			}
			return WaterSpeed > 0;
		}

		private bool HasAirSpeed()
		{
			// If wings then flying
			return ChosenBodyParts[Limb.Wings] == Side.Left
				|| ChosenBodyParts[Limb.Wings] == Side.Right;
		}

		#region Calculate Stats

		private void CalcRangeDamage()
		{
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				if (GameAttributes.ContainsKey(Attributes.RangeDamage[(int)limb]))
				{
					GameAttributes[Attributes.RangeDamage[(int)limb]] = Calculator.CalcRangeDamage(limb);
				}
			}
		}

		private void CalcRangeMax()
		{
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				if (GameAttributes.ContainsKey(Attributes.RangeMax[(int)limb]))
				{
					GameAttributes[Attributes.RangeMax[(int)limb]] = Calculator.CalcRangeMax(limb);
				}
			}
		}

		private void SetRangeType()
		{
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				if (GameAttributes.ContainsKey(Attributes.RangeType[(int)limb]))
				{
					GameAttributes[Attributes.RangeType[(int)limb]] = Calculator.GetRangeType(limb);
				}
			}
		}

		private void SetRangeSpecial()
		{
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				if (GameAttributes.ContainsKey(Attributes.RangeSpecial[(int)limb]))
				{
					GameAttributes[Attributes.RangeSpecial[(int)limb]] = Calculator.GetRangeSpecial(limb);
				}
			}
		}

		private void SetMeleeType()
		{
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				if (GameAttributes.ContainsKey(Attributes.MeleeType[(int)limb]))
				{
					GameAttributes[Attributes.MeleeType[(int)limb]] = Calculator.GetMeleeType(limb);
				}
			}
		}

		#endregion

		public Creature BuildCreature()
		{
			Creature creature = new Creature()
			{
				Left = this.Left.ToString(),
				Right = this.Right.ToString(),
				BodyParts = BuildBodyParts(),
				Rank = (int)this.Rank,
				Coal = this.Coal,
				Electricity = this.Electricity,
				Power = this.Power,
				AbilityAdjustedPower = this.AbilityAdjustedPower,
				EffectiveHitpoints = this.EffectiveHealth,
				Hitpoints = this.Hitpoints,
				Armour = this.Armour,
				SightRadius = this.SightRadius,
				Size = (int)this.Size,
				LandSpeed = this.LandSpeed,
				WaterSpeed = this.WaterSpeed,
				AirSpeed = this.AirSpeed,
				MeleeDamage = this.MeleeDamage,
				PopSize = Math.Ceiling(this.PopSize),
				Ticks = this.Ticks,
		
			};
			AddRangeDamageToCreature(creature);
			AddSuicideCoefficient(creature);
			AddMeleeDamageTypes(creature);
			AddAbiltiies(creature);
			AddCoalElecRatio(creature);
			AddNERating(creature);

			return creature;
		}
		private Dictionary<string, string> BuildBodyParts()
		{
			Dictionary<string, string> bodyParts = new Dictionary<string, string>();
			foreach (Limb limb in ChosenBodyParts.Keys)
			{
				Side side = ChosenBodyParts[limb];
				if (side == Side.Left)
				{
					bodyParts.Add(limb.ToString(), "L");
				}
				else if (side == Side.Right)
				{
					bodyParts.Add(limb.ToString(), "R");
				}
				else if (side == Side.Empty)
				{
					bodyParts.Add(limb.ToString(), "x");
				}
			}
			return bodyParts;
		}

		private void AddRangeDamageToCreature(Creature creature)
		{
			int firstIndex = 0;
			double firstValue = 0;
			int secondIndex = 0;
			double secondValue = 0;

			for (int i = 2; i < 9; i++)
			{
				double damage = GameAttributes[Attributes.RangeDamage[i]];
				if (damage > firstValue)
				{
					secondIndex = firstIndex;
					secondValue = firstValue;
					firstIndex = i;
					firstValue = damage;
				}
				else if (damage > secondValue)
				{
					secondIndex = i;
					secondValue = damage;
				}
			}

			if (firstIndex > 1)
			{
				creature.RangeDamage1 = GameAttributes[Attributes.RangeDamage[firstIndex]];
				creature.RangeType1 = GameAttributes[Attributes.RangeType[firstIndex]];
				creature.RangeSpecial1 = GameAttributes[Attributes.RangeSpecial[firstIndex]];
				creature.RangeMax1 = GameAttributes[Attributes.RangeMax[firstIndex]];
			}

			if (secondIndex > 1)
			{
				creature.RangeDamage2 = GameAttributes[Attributes.RangeDamage[secondIndex]];
				creature.RangeType2 = GameAttributes[Attributes.RangeType[secondIndex]];
				creature.RangeSpecial2 = GameAttributes[Attributes.RangeSpecial[secondIndex]];
				creature.RangeMax2 = GameAttributes[Attributes.RangeSpecial[secondIndex]];
			}
		}


		private void AddSuicideCoefficient(Creature creature)
        {
			if (creature.RangeDamage1 > 0)
			{ 
				creature.SuicideCoefficient = creature.EffectiveHitpoints / creature.RangeDamage1; 
			}
			else
			{ 
				creature.SuicideCoefficient = creature.EffectiveHitpoints / creature.MeleeDamage; 
			}
        }

		private void AddCoalElecRatio(Creature creature)
		{
			if (creature.Coal < 0.001 || Double.IsNegativeInfinity(creature.Coal)) creature.Coal = 0.0;
			if (creature.Electricity < 0.001 || Double.IsNegativeInfinity(creature.Electricity)) creature.Electricity = 0.0;

			if (Double.IsInfinity(creature.Coal) || Double.IsInfinity(creature.Electricity)) creature.CoalElecRatio = 0.0;
			else if (creature.Electricity == 0.0) creature.CoalElecRatio = 0.0;
			else creature.CoalElecRatio = creature.Coal / creature.Electricity;
		}

		private void AddNERating(Creature creature)
		{

			if (creature.RangeDamage1 > 0)
			{ 
				creature.NERating = (creature.EffectiveHitpoints * creature.RangeDamage1) / (creature.Coal + creature.Electricity);
			}
			else
			{ 
					creature.NERating = (creature.EffectiveHitpoints * creature.MeleeDamage) / (creature.Coal + creature.Electricity); 
			}
				
		}
		private void AddMeleeDamageTypes(Creature creature)
		{
			for (int i = 2; i < 9; i++)
			{
				AddMeleeDamageType(creature, (int)GameAttributes[Attributes.MeleeType[i]]);
			}
		}

		private void AddMeleeDamageType(Creature creature, int type)
		{
			if (((int)DamageType.Horns & type) == (int)DamageType.Horns)
			{
				creature.HasHorns = true;
			}
			if (((int)DamageType.BarrierDestroy & type) == (int)DamageType.BarrierDestroy)
			{
				creature.HasBarrierDestroy = true;
			}
			if (((int)DamageType.Poison & type) == (int)DamageType.Poison
				|| ((int)DamageType.VenomSpray & type) == (int)DamageType.VenomSpray)
			{
				creature.HasPoison = true;
			}
		}

		private void AddMeleeDamageType(Creature creature, DamageType type)
		{
			switch (type)
			{
				case DamageType.Horns:
					creature.HasHorns = true;
					break;
				case DamageType.BarrierDestroy:
					creature.HasBarrierDestroy = true;
					break;
				case DamageType.Poison:
				case DamageType.VenomSpray:
					creature.HasPoison = true;
					break;
			}
		}

		private void AddAbiltiies(Creature creature)
		{
			Dictionary<string, bool> abilities = new Dictionary<string, bool>();
			foreach (string ability in AbilityNames.Abilities)
			{
				abilities.Add(AbilityNames.ProperAbilityNames[ability], (GameAttributes[ability] > 0));
			}
			creature.Abilities = abilities;
		}
	}
}
