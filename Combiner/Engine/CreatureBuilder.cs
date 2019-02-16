using System;
using System.Collections.Generic;

namespace Combiner
{
	public class CreatureBuilder
	{
		public StockStatCalculator Left { get; set; }
		public StockStatCalculator Right { get; set; }
		public CreatureStatCalculator Calculator { get; set; }
		Dictionary<Limb, Side> ChosenBodyParts { get; set; }

		public Dictionary<string, double> GameAttributes { get; set; } = new Dictionary<string, double>();

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

		public double RangeDamage
		{
			get
			{
				double greatestRange = Range2Damage;
				greatestRange = (Range3Damage > greatestRange) ? Range3Damage : greatestRange;
				greatestRange = (Range4Damage > greatestRange) ? Range4Damage : greatestRange;
				greatestRange = (Range5Damage > greatestRange) ? Range5Damage : greatestRange;
				greatestRange = (Range8Damage > greatestRange) ? Range8Damage : greatestRange;
				return greatestRange;
			}
		}

		public double RangeSpecial
		{
			get
			{
				double rangeSpecial = Range2Special;
				rangeSpecial = (Range3Special > rangeSpecial) ? Range3Special : rangeSpecial;
				rangeSpecial = (Range4Special > rangeSpecial) ? Range4Special : rangeSpecial;
				rangeSpecial = (Range5Special > rangeSpecial) ? Range5Special : rangeSpecial;
				rangeSpecial = (Range8Special > rangeSpecial) ? Range8Special : rangeSpecial;
				return rangeSpecial;
			}
		}

		public double Range2Damage
		{
			get { return GameAttributes[Attributes.Range2Damage]; }
			set { GameAttributes[Attributes.Range2Damage] = value; }
		}
		public double Range3Damage
		{
			get { return GameAttributes[Attributes.Range3Damage]; }
			set { GameAttributes[Attributes.Range3Damage] = value; }
		}
		public double Range4Damage
		{
			get { return GameAttributes[Attributes.Range4Damage]; }
			set { GameAttributes[Attributes.Range4Damage] = value; }
		}
		public double Range5Damage
		{
			get { return GameAttributes[Attributes.Range5Damage]; }
			set { GameAttributes[Attributes.Range5Damage] = value; }
		}
		public double Range8Damage
		{
			get { return GameAttributes[Attributes.Range8Damage]; }
			set { GameAttributes[Attributes.Range8Damage] = value; }
		}

		public double Range2Max
		{
			get { return GameAttributes[Attributes.Range2Max]; }
			set { GameAttributes[Attributes.Range2Max] = value; }
		}
		public double Range3Max
		{
			get { return GameAttributes[Attributes.Range3Max]; }
			set { GameAttributes[Attributes.Range3Max] = value; }
		}
		public double Range4Max
		{
			get { return GameAttributes[Attributes.Range4Max]; }
			set { GameAttributes[Attributes.Range4Max] = value; }
		}
		public double Range5Max
		{
			get { return GameAttributes[Attributes.Range5Max]; }
			set { GameAttributes[Attributes.Range5Max] = value; }
		}
		public double Range8Max
		{
			get { return GameAttributes[Attributes.Range8Max]; }
			set { GameAttributes[Attributes.Range8Max] = value; }
		}

		public double Range2Type
		{
			get { return GameAttributes[Attributes.Range2Type]; }
			set { GameAttributes[Attributes.Range2Type] = value; }
		}
		public double Range3Type
		{
			get { return GameAttributes[Attributes.Range3Type]; }
			set { GameAttributes[Attributes.Range3Type] = value; }
		}
		public double Range4Type
		{
			get { return GameAttributes[Attributes.Range4Type]; }
			set { GameAttributes[Attributes.Range4Type] = value; }
		}
		public double Range5Type
		{
			get { return GameAttributes[Attributes.Range5Type]; }
			set { GameAttributes[Attributes.Range5Type] = value; }
		}
		public double Range8Type
		{
			get { return GameAttributes[Attributes.Range8Type]; }
			set { GameAttributes[Attributes.Range8Type] = value; }
		}

		public double Range2Special
		{
			get { return GameAttributes[Attributes.Range2Special]; }
			set { GameAttributes[Attributes.Range2Special] = value; }
		}
		public double Range3Special
		{
			get { return GameAttributes[Attributes.Range3Special]; }
			set { GameAttributes[Attributes.Range3Special] = value; }
		}
		public double Range4Special
		{
			get { return GameAttributes[Attributes.Range4Special]; }
			set { GameAttributes[Attributes.Range4Special] = value; }
		}
		public double Range5Special
		{
			get { return GameAttributes[Attributes.Range5Special]; }
			set { GameAttributes[Attributes.Range5Special] = value; }
		}
		public double Range8Special
		{
			get { return GameAttributes[Attributes.Range8Special]; }
			set { GameAttributes[Attributes.Range8Special] = value; }
		}

		public double Melee2Type
		{
			get { return GameAttributes[Attributes.Melee2Type]; }
			set { GameAttributes[Attributes.Melee2Type] = value; }
		}
		public double Melee3Type
		{
			get { return GameAttributes[Attributes.Melee3Type]; }
			set { GameAttributes[Attributes.Melee3Type] = value; }
		}
		public double Melee4Type
		{
			get { return GameAttributes[Attributes.Melee4Type]; }
			set { GameAttributes[Attributes.Melee4Type] = value; }
		}
		public double Melee5Type
		{
			get { return GameAttributes[Attributes.Melee5Type]; }
			set { GameAttributes[Attributes.Melee5Type] = value; }
		}
		public double Melee8Type
		{
			get { return GameAttributes[Attributes.Melee8Type]; }
			set { GameAttributes[Attributes.Melee8Type] = value; }
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

		public double PopSize
		{
			get { return GameAttributes[Attributes.PopSize]; }
			set { GameAttributes[Attributes.PopSize] = value; }
		}

		public double Power
		{
			get { return GameAttributes[Attributes.Power]; }
			set { GameAttributes[Attributes.Power] = value; }
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
			GameAttributes.Add(Attributes.PopSize, 0);

			GameAttributes.Add(Attributes.Power, 0);

			GameAttributes.Add(Attributes.Size, 0);
			GameAttributes.Add(Attributes.SightRadius, 0);
			GameAttributes.Add(Attributes.Armour, 0);
			GameAttributes.Add(Attributes.Hitpoints, 0);
			GameAttributes.Add(Attributes.LandSpeed, 0);
			GameAttributes.Add(Attributes.WaterSpeed, 0);
			GameAttributes.Add(Attributes.AirSpeed, 0);
			GameAttributes.Add(Attributes.MeleeDamage, 0);

			GameAttributes.Add(Attributes.Range2Damage, 0);
			GameAttributes.Add(Attributes.Range2Max, 0);
			GameAttributes.Add(Attributes.Range2Type, 0);
			GameAttributes.Add(Attributes.Range2Special, 0);
			GameAttributes.Add(Attributes.Melee2Type, 0);

			GameAttributes.Add(Attributes.Range3Damage, 0);
			GameAttributes.Add(Attributes.Range3Max, 0);
			GameAttributes.Add(Attributes.Range3Type, 0);
			GameAttributes.Add(Attributes.Range3Special, 0);
			GameAttributes.Add(Attributes.Melee3Type, 0);

			GameAttributes.Add(Attributes.Range4Damage, 0);
			GameAttributes.Add(Attributes.Range4Max, 0);
			GameAttributes.Add(Attributes.Range4Type, 0);
			GameAttributes.Add(Attributes.Range4Special, 0);
			GameAttributes.Add(Attributes.Melee4Type, 0);

			GameAttributes.Add(Attributes.Range5Damage, 0);
			GameAttributes.Add(Attributes.Range5Max, 0);
			GameAttributes.Add(Attributes.Range5Type, 0);
			GameAttributes.Add(Attributes.Range5Special, 0);
			GameAttributes.Add(Attributes.Melee5Type, 0);

			GameAttributes.Add(Attributes.Range8Damage, 0);
			GameAttributes.Add(Attributes.Range8Max, 0);
			GameAttributes.Add(Attributes.Range8Type, 0);
			GameAttributes.Add(Attributes.Range8Special, 0);
			GameAttributes.Add(Attributes.Melee8Type, 0);

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
			if (HasLandSpeed())
			{
				IsLand = 1;
			}
			else
			{
				LandSpeed = 0;
			}
			if (HasWaterSpeed())
			{
				IsSwimmer = 1;
			}
			else
			{
				WaterSpeed = 0;
			}
			if (HasAirSpeed())
			{
				IsFlyer = 1;
			}
			else
			{
				AirSpeed = 0;
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
			if (Left.Stock.Name == StockNames.BluefinTuna || Right.Stock.Name == StockNames.BluefinTuna)
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
				if (tailSide.Name == StockNames.BluefinTuna
					&& ChosenBodyParts[Limb.BackLegs] == Side.Empty
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
					&& ChosenBodyParts[Limb.BackLegs] == Side.Empty
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
			// If both legs (but not from fish) then land
			else if ((ChosenBodyParts[Limb.FrontLegs] == Side.Left
				|| ChosenBodyParts[Limb.FrontLegs] == Side.Right)
				&& (ChosenBodyParts[Limb.BackLegs] == Side.Left
				|| ChosenBodyParts[Limb.BackLegs] == Side.Right)
				&& GetStockSide(Limb.FrontLegs).Type != StockType.Fish
				&& GetStockSide(Limb.BackLegs).Type != StockType.Fish)
			{
				return true;
			}
			// If snake torso then land
			// Except for eel
			else if (GetStockSide(Limb.Torso).Type == StockType.Snake
				&& Right.Stock.Name != StockNames.ElectricEel
				&& Left.Stock.Name != StockNames.ElectricEel) 
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
			if (ChosenBodyParts[Limb.Wings] == Side.Left
				|| ChosenBodyParts[Limb.Wings] == Side.Right)
			{
				return true;
			}
			return false;
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
				EffectiveHitpoints = this.EffectiveHealth,
				Hitpoints = this.Hitpoints,
				Armour = this.Armour,
				SightRadius = this.SightRadius,
				Size = (int)this.Size,
				LandSpeed = this.LandSpeed,
				WaterSpeed = this.WaterSpeed,
				AirSpeed = this.AirSpeed,
				MeleeDamage = this.MeleeDamage,
			};
			AddRangeDamageToCreature(creature);
			AddMeleeDamageTypes(creature);
			AddAbiltiies(creature);

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
			Tuple<double, double, double, double> range2 = new Tuple<double, double, double, double>(Range2Damage, Range2Type, Range2Special, Range2Max);
			Tuple<double, double, double, double> range3 = new Tuple<double, double, double, double>(Range3Damage, Range3Type, Range3Special, Range3Max);
			Tuple<double, double, double, double> range4 = new Tuple<double, double, double, double>(Range4Damage, Range4Type, Range4Special, Range4Max);
			Tuple<double, double, double, double> range5 = new Tuple<double, double, double, double>(Range5Damage, Range5Type, Range5Special, Range5Max);
			Tuple<double, double, double, double> range8 = new Tuple<double, double, double, double>(Range8Damage, Range8Type, Range8Special, Range8Max);

			Tuple<double, double, double, double> first = range2;
			Tuple<double, double, double, double> second = range3;

			Tuple<double, double, double, double> temp;
			if (second.Item1 > first.Item1)
			{
				temp = first;
				first = second;
				second = temp;
			}

			if (range4.Item1 > second.Item1)
			{
				second = range4;
				if (second.Item1 > first.Item1)
				{
					temp = first;
					first = second;
					second = temp;
				}
			}

			if (range5.Item1 > second.Item1)
			{
				second = range5;
				if (second.Item1 > first.Item1)
				{
					temp = first;
					first = second;
					second = temp;
				}
			}

			if (range8.Item1 > second.Item1)
			{
				second = range8;
				if (second.Item1 > first.Item1)
				{
					temp = first;
					first = second;
					second = temp;
				}
			}

			creature.RangeDamage1 = first.Item1;
			creature.RangeType1 = first.Item2;
			creature.RangeSpecial1 = first.Item3;
			creature.RangeMax1 = first.Item4;

			creature.RangeDamage2 = second.Item1;
			creature.RangeType2 = second.Item2;
			creature.RangeSpecial2 = second.Item3;
			creature.RangeMax2 = second.Item4;
		}

		private void AddMeleeDamageTypes(Creature creature)
		{
			AddMeleeDamageType(creature, (DamageType)Melee2Type);
			AddMeleeDamageType(creature, (DamageType)Melee3Type);
			AddMeleeDamageType(creature, (DamageType)Melee4Type);
			AddMeleeDamageType(creature, (DamageType)Melee5Type);
			AddMeleeDamageType(creature, (DamageType)Melee8Type);
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
