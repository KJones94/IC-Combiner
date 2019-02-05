namespace Combiner.Engine
{
	using System;

	using Combiner.Enums;
	using Combiner.Models;
	using Combiner.Utility;

	using MoonSharp.Interpreter;

	public class StockStatCalculator
	{
		public Stock Stock;
		public string Name
		{
			get { return this.Stock.Name; }
		}
		public StockType Type
		{
			get { return this.Stock.Type; }
		}

		public StockStatCalculator(Stock stock)
		{
			this.Stock = stock;
		}

		public int GetLimbAttributeBodyPart(string key)
		{
			var bodyPart = this.Stock.LimbAttritbutes[key] as Table;
			if (bodyPart != null)
			{
				return (int)(double)bodyPart[1];
			}
			return -1;
		}

		public double GetLimbAttributeValue(string key)
		{
			var value = this.Stock.LimbAttritbutes[key] as Table;
			if (value != null)
			{
				return (double)value[2];
			}
			return -1;
		}

		public bool IsGreaterSize(double stockSize)
		{
			return this.GetLimbAttributeValue("size") >= stockSize;
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
			if (this.IsGreaterSize(stockSize))
			{
				return 1.0;
			}
			else
			{
				return stockSize / this.GetLimbAttributeValue("size");
			}
		}

		private double CalcLimbStats(Limb limb, string stat)
		{
			double limbStats = 0.0;
			switch (limb)
			{
				case Limb.FrontLegs:
					limbStats = this.GetLimbAttributeValue(stat + "-front");
					break;
				case Limb.BackLegs:
					limbStats = this.GetLimbAttributeValue(stat + "-back");
					break;
				case Limb.Head:
					limbStats = this.GetLimbAttributeValue(stat + "-head");
					break;
				case Limb.Torso:
					limbStats = this.GetLimbAttributeValue(stat + "-torso");
					break;
				case Limb.Tail:
					limbStats = this.GetLimbAttributeValue(stat + "-tail");
					break;
				case Limb.Wings:
					limbStats = this.GetLimbAttributeValue(stat + "-wings");
					break;
				case Limb.Claws:
					limbStats = this.GetLimbAttributeValue(stat + "-claws");
					break;
				default:
					// throw exception
					break;
			}
			return limbStats;
		}

		public double CalcLimbHitpoints(double stockSize, Limb limb)
		{
			double limbHitpoints = this.CalcLimbStats(limb, Attributes.Hitpoints);
			double health = Math.Pow(this.SizeRatio(stockSize), this.GetLimbAttributeValue("exp_hitpoints")) * limbHitpoints;
			if (health < 0)
			{
				return 0;
			}
			return health;
		}

		public double CalcLimbArmour(double stockSize, Limb limb)
		{
			double limbArmour = this.CalcLimbStats(limb, Attributes.Armour);
			double armour = Math.Pow(this.SizeRatio(stockSize), this.GetLimbAttributeValue("exp_armour")) * limbArmour;
			if (armour < 0)
			{
				return 0;
			}
			return armour;
		}

		public double CalcLimbSightRadius()
		{
			double sightRadius = this.GetLimbAttributeValue(Attributes.SightRadius);
			if (sightRadius < 0)
			{
				return 0;
			}
			return sightRadius;
		}

		public double CalcLimbLandSpeed(double stockSize, Limb limb)
		{
			double limbLandSpeed = this.CalcLimbStats(limb, Attributes.LandSpeed);
			double speed = Math.Pow(this.SizeRatio(stockSize), this.GetLimbAttributeValue("exp_speed_max")) * limbLandSpeed;
			if (speed < 0)
			{
				return 0;
			}
			return speed;
		}

		public double CalcLimbWaterSpeed(double stockSize, Limb limb)
		{
			double limbWaterSpeed = this.CalcLimbStats(limb, Attributes.WaterSpeed);
			// Not right
			double speed = Math.Pow(this.SizeRatio(stockSize),
				this.GetLimbAttributeValue("exp_waterspeed_max")
				+ this.GetLimbAttributeValue("exp_speed_max"))
				* limbWaterSpeed;
			if (speed < 0)
			{
				return 0;
			}
			return speed;
		}

		public double CalcLimbAirSpeed(double stockSize, Limb limb)
		{
			double limbAirSpeed = this.CalcLimbStats(limb, Attributes.AirSpeed);
			double speed = Math.Pow(this.SizeRatio(stockSize), this.GetLimbAttributeValue("exp_airspeed_max")) * limbAirSpeed;
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
			double damage = Math.Pow(this.SizeRatio(stockSize), this.GetLimbAttributeValue(damageExp)) * this.GetLimbAttributeValue(damageName);
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
			double damage = Math.Pow(this.SizeRatio(stockSize), this.GetLimbAttributeValue(damageExp)) * this.GetLimbAttributeValue(damageName);
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
			double value = Math.Pow(this.SizeRatio(stockSize), this.GetLimbAttributeValue(rangeExp)) * this.GetLimbAttributeValue(rangeMax);
			if (value < 0)
			{
				return 0;
			}
			return value;
		}

		public double GetLimbRangeType(Limb limb)
		{
			string rangeType = "range" + (int)limb + "_dmgtype";
			double value = this.GetLimbAttributeValue(rangeType);
			if (value < 0)
			{
				return 0;
			}
			return value;
		}

		public double GetLimbRangeSpecial(Limb limb)
		{
			string rangeSpecial = "range" + (int)limb + "_special";
			double value = this.GetLimbAttributeValue(rangeSpecial);
			if (value < 0)
			{
				return 0;
			}
			return value;
		}

		public double GetLimbMeleeType(Limb limb)
		{
			string meleeType = "melee" + (int)limb + "_dmgtype";
			double value = this.GetLimbAttributeValue(meleeType);
			if (value < 0)
			{
				return 0;
			}
			return value;
		}

		public override string ToString()
		{
			return this.Name;
		}
	}
}
