using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class StockStatCalculator
	{
		public Stock Stock;
		public string Name
		{
			get { return Stock.Name; }
		}
		public StockType Type
		{
			get { return Stock.Type; }
		}

		public StockStatCalculator(Stock stock)
		{
			Stock = stock;
		}

		public int GetLimbAttributeBodyPart(string key)
		{
			var bodyPart = Stock.LimbAttritbutes[key] as Table;
			if (bodyPart != null)
			{
				return (int)(double)bodyPart[1];
			}
			return -1;
		}

		public double GetLimbAttributeValue(string key)
		{
			var value = Stock.LimbAttritbutes[key] as Table;
			if (value != null)
			{
				return (double)value[2];
			}
			return -1;
		}

		public bool IsGreaterSize(double stockSize)
		{
			return GetLimbAttributeValue("size") >= stockSize;
		}

		/// <summary>
		/// Gets the difference of the greater stock over the smaller if smaller, otherwise
		/// return 1.0.
		/// Used to increase the stat value of body parts that have grown from their original size.
		/// For example, an ant torso's health value will increase when combined with a sperm whale.
		/// </summary>
		/// <param name="stockSize"></param>
		/// <returns></returns>
		private double SizeRatio(double stockSize)
		{
			if (IsGreaterSize(stockSize))
			{
				return 1.0;
			}
			else
			{
				return stockSize / GetLimbAttributeValue("size");
			}
		}

		private double CalcLimbStats(Limb limb, string stat)
		{
			double limbStats = 0.0;
			switch (limb)
			{
				case Limb.FrontLegs:
					limbStats = GetLimbAttributeValue(stat + "-front");
					break;
				case Limb.BackLegs:
					limbStats = GetLimbAttributeValue(stat + "-back");
					break;
				case Limb.Head:
					limbStats = GetLimbAttributeValue(stat + "-head");
					break;
				case Limb.Torso:
					limbStats = GetLimbAttributeValue(stat + "-torso");
					break;
				case Limb.Tail:
					limbStats = GetLimbAttributeValue(stat + "-tail");
					break;
				case Limb.Wings:
					limbStats = GetLimbAttributeValue(stat + "-wings");
					break;
				case Limb.Claws:
					limbStats = GetLimbAttributeValue(stat + "-claws");
					break;
				default:
					// throw exception
					break;
			}
			return limbStats;
		}

		public double CalcLimbHitpoints(double stockSize, Limb limb)
		{
			double limbHitpoints = CalcLimbStats(limb, Utility.Hitpoints);
			double health = Math.Pow(SizeRatio(stockSize), GetLimbAttributeValue("exp_hitpoints")) * limbHitpoints;
			if (health < 0)
			{
				return 0;
			}
			return health;
		}

		public double CalcLimbArmour(double stockSize, Limb limb)
		{
			double limbArmour = CalcLimbStats(limb, Utility.Armour);
			double armour = Math.Pow(SizeRatio(stockSize), GetLimbAttributeValue("exp_armour")) * limbArmour;
			if (armour < 0)
			{
				return 0;
			}
			return armour;
		}

		public double CalcLimbSightRadius()
		{
			double sightRadius = GetLimbAttributeValue(Utility.SightRadius);
			if (sightRadius < 0)
			{
				return 0;
			}
			return sightRadius;
		}

		public double CalcLimbLandSpeed(double stockSize, Limb limb)
		{
			double limbLandSpeed = CalcLimbStats(limb, Utility.LandSpeed);
			double speed = Math.Pow(SizeRatio(stockSize), GetLimbAttributeValue("exp_speed_max")) * limbLandSpeed;
			if (speed < 0)
			{
				return 0;
			}
			return speed;
		}

		public double CalcLimbWaterSpeed(double stockSize, Limb limb)
		{
			double limbWaterSpeed = CalcLimbStats(limb, Utility.WaterSpeed);
			// Not right
			double speed = Math.Pow(SizeRatio(stockSize),
				GetLimbAttributeValue("exp_waterspeed_max")
				+ GetLimbAttributeValue("exp_speed_max"))
				* limbWaterSpeed;
			if (speed < 0)
			{
				return 0;
			}
			return speed;
		}

		public double CalcLimbAirSpeed(double stockSize, Limb limb)
		{
			double limbAirSpeed = CalcLimbStats(limb, Utility.AirSpeed);
			double speed = Math.Pow(SizeRatio(stockSize), GetLimbAttributeValue("exp_airspeed_max")) * limbAirSpeed;
			if (speed < 0)
			{
				return 0;
			}
			return speed;
		}

		public double CalcLimbMeleeDamage(double stockSize, Limb limb)
		{
			string damageName = "melee" + (int)limb + "_damage";
			string damageExp = "exp_" + damageName;
			double damage = Math.Pow(SizeRatio(stockSize), GetLimbAttributeValue(damageExp)) * GetLimbAttributeValue(damageName);
			if (damage < 0)
			{
				return 0;
			}
			return damage;
		}

		public double CalcLimbRangeDamage(double stockSize, Limb limb)
		{
			string damageName = "range" + (int)limb + "_damage";
			string damageExp = "exp_" + damageName;
			double damage = Math.Pow(SizeRatio(stockSize), GetLimbAttributeValue(damageExp)) * GetLimbAttributeValue(damageName);
			if (damage < 0)
			{
				return 0;
			}
			return damage;
		}

		public double GetLimbRangeMax(double stockSize, Limb limb)
		{
			string rangeMax = "range" + (int)limb + "_max";
			string rangeExp = "exp_range" + (int)limb + "_max";
			double value = Math.Pow(SizeRatio(stockSize), GetLimbAttributeValue(rangeExp)) * GetLimbAttributeValue(rangeMax);
			if (value < 0)
			{
				return 0;
			}
			return value;
		}

		public double GetLimbRangeType(Limb limb)
		{
			string rangeType = "range" + (int)limb + "_dmgtype";
			double value = GetLimbAttributeValue(rangeType);
			if (value < 0)
			{
				return 0;
			}
			return value;
		}

		public double GetLimbRangeSpecial(Limb limb)
		{
			string rangeSpecial = "range" + (int)limb + "_special";
			double value = GetLimbAttributeValue(rangeSpecial);
			if (value < 0)
			{
				return 0;
			}
			return value;
		}

		public double GetLimbMeleeType(Limb limb)
		{
			string meleeType = "melee" + (int)limb + "_dmgtype";
			double value = GetLimbAttributeValue(meleeType);
			if (value < 0)
			{
				return 0;
			}
			return value;
		}
	}
}
