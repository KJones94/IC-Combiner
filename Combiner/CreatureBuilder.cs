using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class CreatureBuilder
	{
		public Stock Left { get; set; }
		public Stock Right { get; set; }
		Dictionary<Limb, Side> ChosenBodyParts { get; set; }

		public string BodyParts
		{
			get { return CreateBodyPartsText(); }
		}

		private string CreateBodyPartsText()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Limb limb in ChosenBodyParts.Keys)
			{
				Side side = ChosenBodyParts[limb];
				if (side == Side.Left)
				{
					sb.Append('L');
				}
				else if (side == Side.Right)
				{
					sb.Append('R');
				}
				else if (side == Side.Empty)
				{
					sb.Append('x');
				}
			}
			return sb.ToString();
		}

		//Table GameAttributes { get; set; }  // Need to connect to script...
		public Dictionary<string, double> GameAttributes { get; set; } = new Dictionary<string, double>();

		#region Stat Properties

		public double Size
		{
			get { return GameAttributes[Utility.Size]; }
			set { GameAttributes[Utility.Size] = value; }
		}
		public double SightRadius
		{
			get { return GameAttributes[Utility.SightRadius]; }
			set { GameAttributes[Utility.SightRadius] = value; }
		}
		public double Armour
		{
			get { return GameAttributes[Utility.Armour]; }
			set { GameAttributes[Utility.Armour] = value; }
		}
		public double Hitpoints
		{
			get { return GameAttributes[Utility.Hitpoints]; }
			set { GameAttributes[Utility.Hitpoints] = value; }
		}

		public double EffectiveHealth
		{
			get { return Hitpoints / (1 - Armour); }
		}

		public double LandSpeed
		{
			get { return GameAttributes[Utility.LandSpeed]; }
			set { GameAttributes[Utility.LandSpeed] = value; }
		}
		public double AirSpeed
		{
			get { return GameAttributes[Utility.AirSpeed]; }
			set { GameAttributes[Utility.AirSpeed] = value; }
		}
		public double WaterSpeed
		{
			get { return GameAttributes[Utility.WaterSpeed]; }
			set { GameAttributes[Utility.WaterSpeed] = value; }
		}
		public double MeleeDamage
		{
			get { return GameAttributes[Utility.MeleeDamage]; }
			set { GameAttributes[Utility.MeleeDamage] = value; }
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
			get { return GameAttributes[Utility.Range2Damage]; }
			set { GameAttributes[Utility.Range2Damage] = value; }
		}
		public double Range3Damage
		{
			get { return GameAttributes[Utility.Range3Damage]; }
			set { GameAttributes[Utility.Range3Damage] = value; }
		}
		public double Range4Damage
		{
			get { return GameAttributes[Utility.Range4Damage]; }
			set { GameAttributes[Utility.Range4Damage] = value; }
		}
		public double Range5Damage
		{
			get { return GameAttributes[Utility.Range5Damage]; }
			set { GameAttributes[Utility.Range5Damage] = value; }
		}
		public double Range8Damage
		{
			get { return GameAttributes[Utility.Range8Damage]; }
			set { GameAttributes[Utility.Range8Damage] = value; }
		}

		public double Range2Type
		{
			get { return GameAttributes[Utility.Range2Type]; }
			set { GameAttributes[Utility.Range2Type] = value; }
		}
		public double Range3Type
		{
			get { return GameAttributes[Utility.Range3Type]; }
			set { GameAttributes[Utility.Range3Type] = value; }
		}
		public double Range4Type
		{
			get { return GameAttributes[Utility.Range4Type]; }
			set { GameAttributes[Utility.Range4Type] = value; }
		}
		public double Range5Type
		{
			get { return GameAttributes[Utility.Range5Type]; }
			set { GameAttributes[Utility.Range5Type] = value; }
		}
		public double Range8Type
		{
			get { return GameAttributes[Utility.Range8Type]; }
			set { GameAttributes[Utility.Range8Type] = value; }
		}

		public double Range2Special
		{
			get { return GameAttributes[Utility.Range2Special]; }
			set { GameAttributes[Utility.Range2Special] = value; }
		}
		public double Range3Special
		{
			get { return GameAttributes[Utility.Range3Special]; }
			set { GameAttributes[Utility.Range3Special] = value; }
		}
		public double Range4Special
		{
			get { return GameAttributes[Utility.Range4Special]; }
			set { GameAttributes[Utility.Range4Special] = value; }
		}
		public double Range5Special
		{
			get { return GameAttributes[Utility.Range5Special]; }
			set { GameAttributes[Utility.Range5Special] = value; }
		}
		public double Range8Special
		{
			get { return GameAttributes[Utility.Range8Special]; }
			set { GameAttributes[Utility.Range8Special] = value; }
		}

		public double Melee2Type
		{
			get { return GameAttributes[Utility.Melee2Type]; }
			set { GameAttributes[Utility.Melee2Type] = value; }
		}
		public double Melee3Type
		{
			get { return GameAttributes[Utility.Melee3Type]; }
			set { GameAttributes[Utility.Melee3Type] = value; }
		}
		public double Melee4Type
		{
			get { return GameAttributes[Utility.Melee4Type]; }
			set { GameAttributes[Utility.Melee4Type] = value; }
		}
		public double Melee5Type
		{
			get { return GameAttributes[Utility.Melee5Type]; }
			set { GameAttributes[Utility.Melee5Type] = value; }
		}
		public double Melee8Type
		{
			get { return GameAttributes[Utility.Melee8Type]; }
			set { GameAttributes[Utility.Melee8Type] = value; }
		}

		public double IsSwimmer
		{
			get { return GameAttributes[Utility.IsSwimmer]; }
			set { GameAttributes[Utility.IsSwimmer] = value; }
		}

		public double IsFlyer
		{
			get { return GameAttributes[Utility.IsFlyer]; }
			set { GameAttributes[Utility.IsFlyer] = value; }
		}

		public double Ticks
		{
			get { return GameAttributes[Utility.Ticks]; }
			set { GameAttributes[Utility.Ticks] = value; }
		}

		public double Rank
		{
			get { return GameAttributes[Utility.Rank]; }
			set { GameAttributes[Utility.Rank] = value; }
		}

		public double Coal
		{
			get { return GameAttributes[Utility.Coal]; }
			set { GameAttributes[Utility.Coal] = value; }
		}

		public double Electricity
		{
			get { return GameAttributes[Utility.Electricity]; }
			set { GameAttributes[Utility.Electricity] = value; }
		}

		public double PopSize
		{
			get { return GameAttributes[Utility.PopSize]; }
			set { GameAttributes[Utility.PopSize] = value; }
		}

		public double Power
		{
			get { return GameAttributes[Utility.Power]; }
			set { GameAttributes[Utility.Power] = value; }
		}

		#endregion

		public CreatureBuilder(Stock left, Stock right, Dictionary<Limb, Side> chosenBodyParts)
		{
			Left = left;
			Right = right;
			ChosenBodyParts = chosenBodyParts;
			InitGameAttributes();
			InitStats();
			InitAbilities();
		}

		private Stock GetSide(Limb limb)
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

		private Stock OtherSide(Stock stock)
		{
			if (stock.Name == Left.Name)
			{
				return Right;
			}
			else
			{
				return Left;
			}
		}

		private void InitGameAttributes()
		{
			GameAttributes.Add(Utility.Ticks, 0);
			GameAttributes.Add(Utility.Rank, 0);
			GameAttributes.Add(Utility.Coal, 0);
			GameAttributes.Add(Utility.Electricity, 0);
			GameAttributes.Add(Utility.PopSize, 0);

			GameAttributes.Add(Utility.Power, 0);

			GameAttributes.Add(Utility.Size, 0);
			GameAttributes.Add(Utility.SightRadius, 0);
			GameAttributes.Add(Utility.Armour, 0);
			GameAttributes.Add(Utility.Hitpoints, 0);
			GameAttributes.Add(Utility.LandSpeed, 0);
			GameAttributes.Add(Utility.WaterSpeed, 0);
			GameAttributes.Add(Utility.AirSpeed, 0);
			GameAttributes.Add(Utility.MeleeDamage, 0);

			GameAttributes.Add(Utility.Range2Damage, 0);
			GameAttributes.Add(Utility.Range2Max, 0);
			GameAttributes.Add(Utility.Range2Type, 0);
			GameAttributes.Add(Utility.Range2Special, 0);
			GameAttributes.Add(Utility.Melee2Type, 0);

			GameAttributes.Add(Utility.Range3Damage, 0);
			GameAttributes.Add(Utility.Range3Max, 0);
			GameAttributes.Add(Utility.Range3Type, 0);
			GameAttributes.Add(Utility.Range3Special, 0);
			GameAttributes.Add(Utility.Melee3Type, 0);

			GameAttributes.Add(Utility.Range4Damage, 0);
			GameAttributes.Add(Utility.Range4Max, 0);
			GameAttributes.Add(Utility.Range4Type, 0);
			GameAttributes.Add(Utility.Range4Special, 0);
			GameAttributes.Add(Utility.Melee4Type, 0);

			GameAttributes.Add(Utility.Range5Damage, 0);
			GameAttributes.Add(Utility.Range5Max, 0);
			GameAttributes.Add(Utility.Range5Type, 0);
			GameAttributes.Add(Utility.Range5Special, 0);
			GameAttributes.Add(Utility.Melee5Type, 0);

			GameAttributes.Add(Utility.Range8Damage, 0);
			GameAttributes.Add(Utility.Range8Max, 0);
			GameAttributes.Add(Utility.Range8Type, 0);
			GameAttributes.Add(Utility.Range8Special, 0);
			GameAttributes.Add(Utility.Melee8Type, 0);

			GameAttributes.Add(Utility.IsSwimmer, 0);
			GameAttributes.Add(Utility.IsFlyer, 0);

			foreach (string ability in Utility.Abilities)
			{
				GameAttributes.Add(ability, 0);
			}

			GameAttributes.Add("damage", 0);
		}

		private void InitStats()
		{
			CalcHitpoints();
			CalcSize();
			CalcArmour();
			CalcSightRadius();
			CalcLandSpeed();
			CalcWaterSpeed();
			CalcAirSpeed();
			CalcMeleeDamage();
			CalcRangeDamage();
			SetRangeMax();
			SetRangeType();
			SetRangeSpecial();
			SetMeleeType();
		}

		private void InitAbilities()
		{
			InitPassiveAbilities();
			// TODO: Could hardcode or cache the limbs for abilities to reduce inner loop
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				Stock side = GetSide(limb);
				if (side == null)
					continue;
				foreach (string ability in Utility.Abilities)
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
			foreach (string ability in Utility.Abilities)
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

		public bool HasAbilities(IEnumerable<string> abilities)
		{
			bool hasAbilities = true;
			foreach (string ability in abilities)
			{
				if (GameAttributes.ContainsKey(ability))
				{
					hasAbilities = (GameAttributes[ability] > 0);
					if (!hasAbilities)
					{
						break;
					}
				}
			}

			return hasAbilities;
		}

		#region Calculate Stats

		private void CalcHitpoints()
		{
			double hitpoints = 0.0;
			Stock side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = GetSide(limb);
				if (side == null)
					continue;
				hitpoints += side.CalcLimbHitpoints(OtherSide(side), limb);
			}
			//GameAttributes.Set("hitpoints", DynValue.NewNumber(health));
			Hitpoints = hitpoints;
		}

		private void CalcSize()
		{
			if (Left.IsGreaterSize(Right))
			{
				Size = Left.GetLimbAttributeValue("size");
			}
			else
			{
				Size = Right.GetLimbAttributeValue("size");
			}
		}

		private void CalcArmour()
		{
			double armour = 0.0;
			Stock side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = GetSide(limb);
				if (side == null)
					continue;
				armour += side.CalcLimbArmour(OtherSide(side), limb);
			}
			Armour = armour;
		}

		private void CalcSightRadius()
		{
			Stock side = GetSide(Limb.Head);
			SightRadius = side.CalcLimbSightRadius();
		}

		private void CalcLandSpeed()
		{
			double landSpeed = 0.0;
			Stock side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = GetSide(limb);
				if (side == null)
					continue;
				landSpeed += side.CalcLimbLandSpeed(OtherSide(side), limb);
			}
			LandSpeed = landSpeed;
		}

		private void CalcAirSpeed()
		{
			double airSpeed = 0.0;
			Stock side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = GetSide(limb);
				if (side == null)
					continue;
				airSpeed += side.CalcLimbAirSpeed(OtherSide(side), limb);
			}
			AirSpeed = airSpeed;
			if (AirSpeed > 0)
			{
				IsFlyer = 1;
			}
		}

		private void CalcWaterSpeed()
		{
			double waterSpeed = 0.0;
			Stock side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = GetSide(limb);
				if (side == null)
					continue;
				waterSpeed += side.CalcLimbWaterSpeed(OtherSide(side), limb);
			}
			WaterSpeed = waterSpeed;
			if (WaterSpeed > 0)
			{
				IsSwimmer = 1;
			}
		}

		private void CalcMeleeDamage()
		{
			double meleeDamage = 0.0;
			Stock side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = GetSide(limb);
				if (side == null)
					continue;
				meleeDamage += side.CalcLimbMeleeDamage(OtherSide(side), limb);
			}
			MeleeDamage = meleeDamage;
		}

		private void CalcRangeDamage()
		{
			Stock side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = GetSide(limb);
				if (side == null)
					continue;
				if (GameAttributes.ContainsKey(Utility.RangeDamage[(int)limb]))
				{
					GameAttributes[Utility.RangeDamage[(int)limb]] = side.CalcLimbRangeDamage(OtherSide(side), limb);
				}
			}
		}

		private void SetRangeMax()
		{
			Stock side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = GetSide(limb);
				if (side == null)
					continue;
				if (GameAttributes.ContainsKey(Utility.RangeMax[(int)limb]))
				{
					GameAttributes[Utility.RangeMax[(int)limb]] = side.GetLimbRangeMax(OtherSide(side), limb);
					if (GameAttributes[Utility.RangeMax[(int)limb]] > 0)
					{
						Console.WriteLine("hello");
					}
				}
			}
		}

		private void SetRangeType()
		{
			Stock side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = GetSide(limb);
				if (side == null)
					continue;
				if (GameAttributes.ContainsKey(Utility.RangeType[(int)limb]))
				{
					GameAttributes[Utility.RangeType[(int)limb]] = side.GetLimbRangeType(limb);
				}
			}
		}

		private void SetRangeSpecial()
		{
			Stock side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = GetSide(limb);
				if (side == null)
					continue;
				if (GameAttributes.ContainsKey(Utility.RangeSpecial[(int)limb]))
				{
					GameAttributes[Utility.RangeSpecial[(int)limb]] = side.GetLimbRangeSpecial(limb);
				}
			}
		}

		private void SetMeleeType()
		{
			Stock side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = GetSide(limb);
				if (side == null)
					continue;
				if (GameAttributes.ContainsKey(Utility.MeleeType[(int)limb]))
				{
					GameAttributes[Utility.MeleeType[(int)limb]] = side.GetLimbMeleeType(limb);
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
			Tuple<double, double, double> range2 = new Tuple<double, double, double>(Range2Damage, Range2Type, Range2Special);
			Tuple<double, double, double> range3 = new Tuple<double, double, double>(Range3Damage, Range3Type, Range3Special);
			Tuple<double, double, double> range4 = new Tuple<double, double, double>(Range4Damage, Range4Type, Range4Special);
			Tuple<double, double, double> range5 = new Tuple<double, double, double>(Range5Damage, Range5Type, Range5Special);
			Tuple<double, double, double> range8 = new Tuple<double, double, double>(Range8Damage, Range8Type, Range8Special);

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
			foreach (string ability in Utility.Abilities)
			{
				abilities.Add(ability, (GameAttributes[ability] > 0));
			}
			creature.Abilities = abilities;
		}
	}
}
