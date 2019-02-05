namespace Combiner.Engine
{
	using System;
	using System.Collections.Generic;

	using Combiner.Enums;
	using Combiner.Models;
	using Combiner.Utility;

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
			get { return this.GameAttributes[Attributes.Size]; }
			set { this.GameAttributes[Attributes.Size] = value; }
		}
		public double SightRadius
		{
			get { return this.GameAttributes[Attributes.SightRadius]; }
			set { this.GameAttributes[Attributes.SightRadius] = value; }
		}
		public double Armour
		{
			get { return this.GameAttributes[Attributes.Armour]; }
			set { this.GameAttributes[Attributes.Armour] = value; }
		}
		public double Hitpoints
		{
			get { return this.GameAttributes[Attributes.Hitpoints]; }
			set { this.GameAttributes[Attributes.Hitpoints] = value; }
		}

		public double EffectiveHealth
		{
			get { return this.Hitpoints / (1 - this.Armour); }
		}

		public double LandSpeed
		{
			get { return this.GameAttributes[Attributes.LandSpeed]; }
			set { this.GameAttributes[Attributes.LandSpeed] = value; }
		}
		public double AirSpeed
		{
			get { return this.GameAttributes[Attributes.AirSpeed]; }
			set { this.GameAttributes[Attributes.AirSpeed] = value; }
		}
		public double WaterSpeed
		{
			get { return this.GameAttributes[Attributes.WaterSpeed]; }
			set { this.GameAttributes[Attributes.WaterSpeed] = value; }
		}
		public double MeleeDamage
		{
			get { return this.GameAttributes[Attributes.MeleeDamage]; }
			set { this.GameAttributes[Attributes.MeleeDamage] = value; }
		}

		public double RangeDamage
		{
			get
			{
				double greatestRange = this.Range2Damage;
				greatestRange = (this.Range3Damage > greatestRange) ? this.Range3Damage : greatestRange;
				greatestRange = (this.Range4Damage > greatestRange) ? this.Range4Damage : greatestRange;
				greatestRange = (this.Range5Damage > greatestRange) ? this.Range5Damage : greatestRange;
				greatestRange = (this.Range8Damage > greatestRange) ? this.Range8Damage : greatestRange;
				return greatestRange;
			}
		}

		public double RangeSpecial
		{
			get
			{
				double rangeSpecial = this.Range2Special;
				rangeSpecial = (this.Range3Special > rangeSpecial) ? this.Range3Special : rangeSpecial;
				rangeSpecial = (this.Range4Special > rangeSpecial) ? this.Range4Special : rangeSpecial;
				rangeSpecial = (this.Range5Special > rangeSpecial) ? this.Range5Special : rangeSpecial;
				rangeSpecial = (this.Range8Special > rangeSpecial) ? this.Range8Special : rangeSpecial;
				return rangeSpecial;
			}
		}

		public double Range2Damage
		{
			get { return this.GameAttributes[Attributes.Range2Damage]; }
			set { this.GameAttributes[Attributes.Range2Damage] = value; }
		}
		public double Range3Damage
		{
			get { return this.GameAttributes[Attributes.Range3Damage]; }
			set { this.GameAttributes[Attributes.Range3Damage] = value; }
		}
		public double Range4Damage
		{
			get { return this.GameAttributes[Attributes.Range4Damage]; }
			set { this.GameAttributes[Attributes.Range4Damage] = value; }
		}
		public double Range5Damage
		{
			get { return this.GameAttributes[Attributes.Range5Damage]; }
			set { this.GameAttributes[Attributes.Range5Damage] = value; }
		}
		public double Range8Damage
		{
			get { return this.GameAttributes[Attributes.Range8Damage]; }
			set { this.GameAttributes[Attributes.Range8Damage] = value; }
		}

		public double Range2Type
		{
			get { return this.GameAttributes[Attributes.Range2Type]; }
			set { this.GameAttributes[Attributes.Range2Type] = value; }
		}
		public double Range3Type
		{
			get { return this.GameAttributes[Attributes.Range3Type]; }
			set { this.GameAttributes[Attributes.Range3Type] = value; }
		}
		public double Range4Type
		{
			get { return this.GameAttributes[Attributes.Range4Type]; }
			set { this.GameAttributes[Attributes.Range4Type] = value; }
		}
		public double Range5Type
		{
			get { return this.GameAttributes[Attributes.Range5Type]; }
			set { this.GameAttributes[Attributes.Range5Type] = value; }
		}
		public double Range8Type
		{
			get { return this.GameAttributes[Attributes.Range8Type]; }
			set { this.GameAttributes[Attributes.Range8Type] = value; }
		}

		public double Range2Special
		{
			get { return this.GameAttributes[Attributes.Range2Special]; }
			set { this.GameAttributes[Attributes.Range2Special] = value; }
		}
		public double Range3Special
		{
			get { return this.GameAttributes[Attributes.Range3Special]; }
			set { this.GameAttributes[Attributes.Range3Special] = value; }
		}
		public double Range4Special
		{
			get { return this.GameAttributes[Attributes.Range4Special]; }
			set { this.GameAttributes[Attributes.Range4Special] = value; }
		}
		public double Range5Special
		{
			get { return this.GameAttributes[Attributes.Range5Special]; }
			set { this.GameAttributes[Attributes.Range5Special] = value; }
		}
		public double Range8Special
		{
			get { return this.GameAttributes[Attributes.Range8Special]; }
			set { this.GameAttributes[Attributes.Range8Special] = value; }
		}

		public double Melee2Type
		{
			get { return this.GameAttributes[Attributes.Melee2Type]; }
			set { this.GameAttributes[Attributes.Melee2Type] = value; }
		}
		public double Melee3Type
		{
			get { return this.GameAttributes[Attributes.Melee3Type]; }
			set { this.GameAttributes[Attributes.Melee3Type] = value; }
		}
		public double Melee4Type
		{
			get { return this.GameAttributes[Attributes.Melee4Type]; }
			set { this.GameAttributes[Attributes.Melee4Type] = value; }
		}
		public double Melee5Type
		{
			get { return this.GameAttributes[Attributes.Melee5Type]; }
			set { this.GameAttributes[Attributes.Melee5Type] = value; }
		}
		public double Melee8Type
		{
			get { return this.GameAttributes[Attributes.Melee8Type]; }
			set { this.GameAttributes[Attributes.Melee8Type] = value; }
		}

		public double IsLand
		{
			get { return this.GameAttributes[Attributes.IsLand]; }
			set { this.GameAttributes[Attributes.IsLand] = value; }
		}
		public double IsSwimmer
		{
			get { return this.GameAttributes[Attributes.IsSwimmer]; }
			set { this.GameAttributes[Attributes.IsSwimmer] = value; }
		}
		public double IsFlyer
		{
			get { return this.GameAttributes[Attributes.IsFlyer]; }
			set { this.GameAttributes[Attributes.IsFlyer] = value; }
		}

		public double Ticks
		{
			get { return this.GameAttributes[Attributes.Ticks]; }
			set { this.GameAttributes[Attributes.Ticks] = value; }
		}

		public double Rank
		{
			get { return this.GameAttributes[Attributes.Rank]; }
			set { this.GameAttributes[Attributes.Rank] = value; }
		}

		public double Coal
		{
			get { return this.GameAttributes[Attributes.Coal]; }
			set { this.GameAttributes[Attributes.Coal] = value; }
		}

		public double Electricity
		{
			get { return this.GameAttributes[Attributes.Electricity]; }
			set { this.GameAttributes[Attributes.Electricity] = value; }
		}

		public double PopSize
		{
			get { return this.GameAttributes[Attributes.PopSize]; }
			set { this.GameAttributes[Attributes.PopSize] = value; }
		}

		public double Power
		{
			get { return this.GameAttributes[Attributes.Power]; }
			set { this.GameAttributes[Attributes.Power] = value; }
		}

		#endregion

		public CreatureBuilder(Stock left, Stock right, Dictionary<Limb, Side> chosenBodyParts)
		{
			this.Left =  new StockStatCalculator(left);
			this.Right = new StockStatCalculator(right);
			this.ChosenBodyParts = chosenBodyParts;
			this.Calculator = new CreatureStatCalculator(this.Left, this.Right, this.ChosenBodyParts);
			this.InitGameAttributes();
			this.InitStats();
			this.InitAbilities();
			this.FixTunaLeapAttack();
			this.FixNarwhalChargeAttack();
		}

		private StockStatCalculator GetStockSide(Limb limb)
		{
			if (this.ChosenBodyParts[limb] == Side.Left)
			{
				return this.Left;
			}

			if (this.ChosenBodyParts[limb] == Side.Right)
			{
				return this.Right;
			}

			return null;
		}

		private Stock OtherSide(StockStatCalculator stock)
		{
			if (stock.Name == this.Left.Name)
			{
				return this.Right.Stock;
			}

			return this.Left.Stock;
		}

		private void InitGameAttributes()
		{
			this.GameAttributes.Add(Attributes.Ticks, 0);
			this.GameAttributes.Add(Attributes.Rank, 0);
			this.GameAttributes.Add(Attributes.Coal, 0);
			this.GameAttributes.Add(Attributes.Electricity, 0);
			this.GameAttributes.Add(Attributes.PopSize, 0);

			this.GameAttributes.Add(Attributes.Power, 0);

			this.GameAttributes.Add(Attributes.Size, 0);
			this.GameAttributes.Add(Attributes.SightRadius, 0);
			this.GameAttributes.Add(Attributes.Armour, 0);
			this.GameAttributes.Add(Attributes.Hitpoints, 0);
			this.GameAttributes.Add(Attributes.LandSpeed, 0);
			this.GameAttributes.Add(Attributes.WaterSpeed, 0);
			this.GameAttributes.Add(Attributes.AirSpeed, 0);
			this.GameAttributes.Add(Attributes.MeleeDamage, 0);

			this.GameAttributes.Add(Attributes.Range2Damage, 0);
			this.GameAttributes.Add(Attributes.Range2Max, 0);
			this.GameAttributes.Add(Attributes.Range2Type, 0);
			this.GameAttributes.Add(Attributes.Range2Special, 0);
			this.GameAttributes.Add(Attributes.Melee2Type, 0);

			this.GameAttributes.Add(Attributes.Range3Damage, 0);
			this.GameAttributes.Add(Attributes.Range3Max, 0);
			this.GameAttributes.Add(Attributes.Range3Type, 0);
			this.GameAttributes.Add(Attributes.Range3Special, 0);
			this.GameAttributes.Add(Attributes.Melee3Type, 0);

			this.GameAttributes.Add(Attributes.Range4Damage, 0);
			this.GameAttributes.Add(Attributes.Range4Max, 0);
			this.GameAttributes.Add(Attributes.Range4Type, 0);
			this.GameAttributes.Add(Attributes.Range4Special, 0);
			this.GameAttributes.Add(Attributes.Melee4Type, 0);

			this.GameAttributes.Add(Attributes.Range5Damage, 0);
			this.GameAttributes.Add(Attributes.Range5Max, 0);
			this.GameAttributes.Add(Attributes.Range5Type, 0);
			this.GameAttributes.Add(Attributes.Range5Special, 0);
			this.GameAttributes.Add(Attributes.Melee5Type, 0);

			this.GameAttributes.Add(Attributes.Range8Damage, 0);
			this.GameAttributes.Add(Attributes.Range8Max, 0);
			this.GameAttributes.Add(Attributes.Range8Type, 0);
			this.GameAttributes.Add(Attributes.Range8Special, 0);
			this.GameAttributes.Add(Attributes.Melee8Type, 0);

			this.GameAttributes.Add(Attributes.IsLand, 0);
			this.GameAttributes.Add(Attributes.IsSwimmer, 0);
			this.GameAttributes.Add(Attributes.IsFlyer, 0);

			foreach (string ability in AbilityNames.Abilities)
			{
				this.GameAttributes.Add(ability, 0);
			}

			this.GameAttributes.Add("damage", 0);
		}

		private void InitStats()
		{
			// Calculate total for all limbs
			this.Hitpoints = this.Calculator.CalcHitpoints();
			this.Size = this.Calculator.CalcSize();
			this.Armour = this.Calculator.CalcArmour();
			this.SightRadius = this.Calculator.CalcSightRadius();
			this.LandSpeed = this.Calculator.CalcLandSpeed();
			this.WaterSpeed = this.Calculator.CalcWaterSpeed();
			this.AirSpeed = this.Calculator.CalcAirSpeed();
			this.MeleeDamage = this.Calculator.CalcMeleeDamage();

			// Independent limb values
			this.CalcRangeDamage();
			this.CalcRangeMax();
			this.SetRangeType();
			this.SetRangeSpecial();
			this.SetMeleeType();

			// Ensure speed values set properly
			if (this.HasLandSpeed())
			{
				this.IsLand = 1;
			}
			else
			{
				this.LandSpeed = 0;
			}

			if (this.HasWaterSpeed())
			{
				this.IsSwimmer = 1;
			}
			else
			{
				this.WaterSpeed = 0;
			}

			if (this.HasAirSpeed())
			{
				this.IsFlyer = 1;
			}
			else
			{
				this.AirSpeed = 0;
			}
		}

		private void InitAbilities()
		{
			this.InitPassiveAbilities();

			// TODO: Could hardcode or cache the limbs for abilities to reduce inner loop
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				StockStatCalculator side = this.GetStockSide(limb);
				if (side == null)
					continue;
				foreach (string ability in AbilityNames.Abilities)
				{
					int bodyPart = side.GetLimbAttributeBodyPart(ability);
					if (bodyPart > -1 && (Limb)bodyPart == limb && side.GetLimbAttributeValue(ability) > 0)
					{
						this.GameAttributes[ability] = 1;
					}
				}
			}
		}

		private void InitPassiveAbilities()
		{
			foreach (string ability in AbilityNames.Abilities)
			{
				if (this.Left.GetLimbAttributeBodyPart(ability) == 0
					&& this.Left.GetLimbAttributeValue(ability) > 0)
				{
					this.GameAttributes[ability] = 1;
					continue;
				}

				if (this.Right.GetLimbAttributeBodyPart(ability) == 0
					&& this.Right.GetLimbAttributeValue(ability) > 0)
				{
					this.GameAttributes[ability] = 1;
				}
			}
		}

		private void FixTunaLeapAttack()
		{
			if (this.Left.Stock.Name == StockNames.BluefinTuna || this.Right.Stock.Name == StockNames.BluefinTuna)
			{
				StockStatCalculator backLegsSide = this.GetStockSide(Limb.BackLegs);

				// Check if using back legs for leap attack
				if (this.GameAttributes[AbilityNames.LeapAttack] > 0
					&& backLegsSide != null
					&& backLegsSide.GetLimbAttributeValue(AbilityNames.LeapAttack) > 0)
				{
					return; // Leap attack is good
				}

				// Check if tuna leap is good
				StockStatCalculator tailSide = this.GetStockSide(Limb.Tail);
				if (tailSide.Name == StockNames.BluefinTuna
					&& this.ChosenBodyParts[Limb.BackLegs] == Side.Empty
					&& !this.HasLandSpeed()
					&& !this.HasAirSpeed()
					&& this.GetStockSide(Limb.Torso).Name != StockNames.GiantSquid)
				{
					// Special case idk why
					this.GameAttributes[AbilityNames.LeapAttack] = 1; // Leap attack is good
				}
				else
				{
					this.GameAttributes[AbilityNames.LeapAttack] = 0; // Bad leap attack from tuna
				}
			}
		}

		private void FixNarwhalChargeAttack()
		{
			if (this.Left.Stock.Name == StockNames.Narwhal || this.Right.Stock.Name == StockNames.Narwhal)
			{
				StockStatCalculator backLegsSide = this.GetStockSide(Limb.BackLegs);

				// Check if using back legs for charge attack
				if (this.GameAttributes[AbilityNames.ChargeAttack] > 0
					&& backLegsSide != null
					&& backLegsSide.GetLimbAttributeValue(AbilityNames.ChargeAttack) > 0)
				{
					return; // Charge attack is good
				}

				// Check if narwhal charge is good
				StockStatCalculator tailSide = this.GetStockSide(Limb.Tail);
				if (tailSide.Name == StockNames.Narwhal
					&& this.ChosenBodyParts[Limb.BackLegs] == Side.Empty
					&& !this.HasLandSpeed()
					&& !this.HasAirSpeed()
					&& this.GetStockSide(Limb.Torso).Name != StockNames.GiantSquid)
				{
					// Special case idk why
					this.GameAttributes[AbilityNames.ChargeAttack] = 1; // Charge attack is good
				}
				else
				{
					this.GameAttributes[AbilityNames.ChargeAttack] = 0; // Bad charge attack from tuna
				}
			}
		}

		private bool HasLandSpeed()
		{
			// If wings then no land
			if (this.ChosenBodyParts[Limb.Wings] == Side.Left || this.ChosenBodyParts[Limb.Wings] == Side.Right)
			{
				return false;
			}

			// If both legs (but not from fish) then land
			if ((this.ChosenBodyParts[Limb.FrontLegs] == Side.Left
			     || this.ChosenBodyParts[Limb.FrontLegs] == Side.Right)
			    && (this.ChosenBodyParts[Limb.BackLegs] == Side.Left
			        || this.ChosenBodyParts[Limb.BackLegs] == Side.Right)
			    && this.GetStockSide(Limb.FrontLegs).Type != StockType.Fish
			    && this.GetStockSide(Limb.BackLegs).Type != StockType.Fish)
			{
				return true;
			}

			// If snake torso then land
			// Except for eel
			if (this.GetStockSide(Limb.Torso).Type == StockType.Snake
			    && this.Right.Stock.Name != StockNames.ElectricEel
			    && this.Left.Stock.Name != StockNames.ElectricEel) 
			{
				return true;
			}

			return false;
		}

		private bool HasWaterSpeed()
		{
			// If wings then no water
			if (this.ChosenBodyParts[Limb.Wings] == Side.Left
				|| this.ChosenBodyParts[Limb.Wings] == Side.Right)
			{
				return false;
			}

			return this.WaterSpeed > 0;
		}

		private bool HasAirSpeed()
		{
			// If wings then flying
			if (this.ChosenBodyParts[Limb.Wings] == Side.Left
				|| this.ChosenBodyParts[Limb.Wings] == Side.Right)
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
				if (this.GameAttributes.ContainsKey(Attributes.RangeDamage[(int)limb]))
				{
					this.GameAttributes[Attributes.RangeDamage[(int)limb]] = this.Calculator.CalcRangeDamage(limb);
				}
			}
		}

		// TODO: Is this used for anything?
		private void CalcRangeMax()
		{
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				if (this.GameAttributes.ContainsKey(Attributes.RangeMax[(int)limb]))
				{
					this.GameAttributes[Attributes.RangeMax[(int)limb]] = this.Calculator.CalcRangeMax(limb);
				}
			}
		}

		private void SetRangeType()
		{
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				if (this.GameAttributes.ContainsKey(Attributes.RangeType[(int)limb]))
				{
					this.GameAttributes[Attributes.RangeType[(int)limb]] = this.Calculator.GetRangeType(limb);
				}
			}
		}

		private void SetRangeSpecial()
		{
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				if (this.GameAttributes.ContainsKey(Attributes.RangeSpecial[(int)limb]))
				{
					this.GameAttributes[Attributes.RangeSpecial[(int)limb]] = this.Calculator.GetRangeSpecial(limb);
				}
			}
		}

		private void SetMeleeType()
		{
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				if (this.GameAttributes.ContainsKey(Attributes.MeleeType[(int)limb]))
				{
					this.GameAttributes[Attributes.MeleeType[(int)limb]] = this.Calculator.GetMeleeType(limb);
				}
			}
		}

		#endregion

		public Creature BuildCreature()
		{
			Creature creature = new Creature
			                    {
				Left = this.Left.ToString(),
				Right = this.Right.ToString(),
				BodyParts = this.BuildBodyParts(),
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
				MeleeDamage = this.MeleeDamage
			};
			this.AddRangeDamageToCreature(creature);
			this.AddMeleeDamageTypes(creature);
			this.AddAbiltiies(creature);

			return creature;
		}

		private Dictionary<string, string> BuildBodyParts()
		{
			Dictionary<string, string> bodyParts = new Dictionary<string, string>();
			foreach (Limb limb in this.ChosenBodyParts.Keys)
			{
				Side side = this.ChosenBodyParts[limb];
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
			Tuple<double, double, double> range2 = new Tuple<double, double, double>(this.Range2Damage, this.Range2Type, this.Range2Special);
			Tuple<double, double, double> range3 = new Tuple<double, double, double>(this.Range3Damage, this.Range3Type, this.Range3Special);
			Tuple<double, double, double> range4 = new Tuple<double, double, double>(this.Range4Damage, this.Range4Type, this.Range4Special);
			Tuple<double, double, double> range5 = new Tuple<double, double, double>(this.Range5Damage, this.Range5Type, this.Range5Special);
			Tuple<double, double, double> range8 = new Tuple<double, double, double>(this.Range8Damage, this.Range8Type, this.Range8Special);

			Tuple<double, double, double> first = range2;
			Tuple<double, double, double> second = range3;

			Tuple<double, double, double> temp;
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

			creature.RangeDamage2 = second.Item1;
			creature.RangeType2 = second.Item2;
			creature.RangeSpecial2 = second.Item3;
		}

		private void AddMeleeDamageTypes(Creature creature)
		{
			this.AddMeleeDamageType(creature, (DamageType)this.Melee2Type);
			this.AddMeleeDamageType(creature, (DamageType)this.Melee3Type);
			this.AddMeleeDamageType(creature, (DamageType)this.Melee4Type);
			this.AddMeleeDamageType(creature, (DamageType)this.Melee5Type);
			this.AddMeleeDamageType(creature, (DamageType)this.Melee8Type);
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
				abilities.Add(AbilityNames.ProperAbilityNames[ability], (this.GameAttributes[ability] > 0));
			}

			creature.Abilities = abilities;
		}
	}
}
