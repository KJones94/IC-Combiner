using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combiner
{
    public class Stock
    {
        public string Name { get; set; }
        public Dictionary<Limb, bool> BodyParts { get; set; }
        public Table LimbAttritbutes { get; set; }
		public StockType Type { get; set; }

        public Stock(string name, Table limbAttributes)
        {
            Name = name;
            LimbAttritbutes = limbAttributes;
			Type = DoubleToStockType(GetLimbAttributeValue("stocktype"));
			InitBodyParts();
        }

        public double GetLimbAttributeValue(string key)
        {
			var value = LimbAttritbutes[key] as Table;
			if (value != null)
			{
				return (double)value[2];
			}
			return -1;
        }

		// TODO: will casting to int mess this up?
		public int GetLimbAttributeBodyPart(string key)
		{
			var bodyPart = LimbAttritbutes[key] as Table;
			if (bodyPart != null)
			{
				return (int)(double)bodyPart[1];
			}
			return -1;
		}

		private StockType DoubleToStockType(double d)
        {
            foreach (StockType stockType in Enum.GetValues(typeof(StockType)))
            {
                if ((int)stockType == (int)d)
                {
                    return stockType;
                }
            }
            return StockType.Bird;
        }

        private void InitBodyParts()
        {
            BodyParts = new Dictionary<Limb, bool>();
            foreach (Limb limb in Enum.GetValues(typeof(Limb)))
            {
                BodyParts.Add(limb, true);
            }
            BodyParts[Limb.Nothing] = false;

            string[] clawedArachnids = new string [] { "lobster", "shrimp", "scorpion", "praying_mantis", "tarantula", "pistol shrimp", "siphonophore" };
            switch (Type)
            {
                case StockType.Bird:
                    BodyParts[Limb.FrontLegs] = false;
                    BodyParts[Limb.Claws] = false;
                    break;

                case StockType.Quadruped:
                    BodyParts[Limb.Claws] = false;
                    BodyParts[Limb.Wings] = false;
                    break;

                case StockType.Arachnid:
                    if (Name == "siphonophore")
                    {
                        BodyParts[Limb.FrontLegs] = false;
                        BodyParts[Limb.BackLegs] = false;
                        BodyParts[Limb.Wings] = false;
                    }
                    else if (clawedArachnids.Contains(Name))
                    {
                        BodyParts[Limb.Wings] = false;
                    }
                    else
                    {
                        BodyParts[Limb.Claws] = false;
                        BodyParts[Limb.Wings] = false;
                    }
                    break;

                case StockType.Snake:
					BodyParts[Limb.FrontLegs] = false;
					BodyParts[Limb.BackLegs] = false;
					BodyParts[Limb.Claws] = false;
					BodyParts[Limb.Wings] = false;
					break;

                case StockType.Insect:
                    BodyParts[Limb.Claws] = false;
                    break;

                case StockType.Fish:
                    if (Name == "humpback")
                    {
                        BodyParts[Limb.BackLegs] = false;
                        BodyParts[Limb.Claws] = false;
                        BodyParts[Limb.Wings] = false;
                    }
                    else
                    {
                        BodyParts[Limb.FrontLegs] = false;
                        BodyParts[Limb.BackLegs] = false;
                        BodyParts[Limb.Claws] = false;
                        BodyParts[Limb.Wings] = false;
                    }
                    break;

                default:
                    break;
            }
        }

        public bool IsGreaterSize(Stock stock)
		{
			return GetLimbAttributeValue("size") >= stock.GetLimbAttributeValue("size");
		}

		/// <summary>
		/// Gets the difference of the greater stock over the smaller if smaller, otherwise
		/// return 1.0.
		/// Used to increase the stat value of body parts that have grown from their original size.
		/// For example, an ant torso's health value will increase when combined with a sperm whale.
		/// </summary>
		/// <param name="stock"></param>
		/// <returns></returns>
		private double SizeRatio(Stock stock)
		{
			if (IsGreaterSize(stock))
			{
				return 1.0;
			}
			else
			{
				return stock.GetLimbAttributeValue("size") / GetLimbAttributeValue("size");
			}
		}

		private double SizeDifference(Stock stock)
		{
			if (IsGreaterSize(stock))
			{
				return 1.0;
			}
			else
			{
				return stock.GetLimbAttributeValue("size") - GetLimbAttributeValue("size");
			}
		}

		#region Calculate Limb Stats

		// TODO: Might want to move calculations to a separate class
		// TODO: GetLimbStats and CalcLimbStats
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

		public double CalcLimbHitpoints(Stock stock, Limb limb)
		{
			double limbHitpoints = CalcLimbStats(limb, "hitpoints");
			double health = Math.Pow(SizeRatio(stock), GetLimbAttributeValue("exp_hitpoints")) * limbHitpoints;
			if (health < 0)
			{
				return 0;
			}
			return health;
		}

		public double CalcLimbArmour(Stock stock, Limb limb)
		{
			double limbArmour = CalcLimbStats(limb, "armour");
			if (limbArmour < 0)
			{
				return 0;
			}
			return limbArmour;
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

		public double CalcLimbLandSpeed(Stock stock, Limb limb)
		{
			double limbLandSpeed = CalcLimbStats(limb, "speed_max");
			double speed = Math.Pow(SizeRatio(stock), GetLimbAttributeValue("exp_speed_max")) * limbLandSpeed;
			if (speed < 0)
			{
				return 0;
			}
			return speed;
		}

		public double CalcLimbWaterSpeed(Stock stock, Limb limb)
		{
			double limbWaterSpeed = CalcLimbStats(limb, "waterspeed_max");
			// Not right
			double speed = Math.Pow(SizeRatio(stock), 
				GetLimbAttributeValue("exp_waterspeed_max") 
				+ GetLimbAttributeValue("exp_speed_max"))
				* limbWaterSpeed;
			if (speed < 0)
			{
				return 0;
			}
			return speed;
		}

		public double CalcLimbAirSpeed(Stock stock, Limb limb)
		{
			double limbAirSpeed = CalcLimbStats(limb, "airspeed_max");
			double speed = Math.Pow(SizeRatio(stock), GetLimbAttributeValue("exp_airspeed_max")) * limbAirSpeed;
			if (speed < 0)
			{
				return 0;
			}
			return speed;
		}

		public double CalcLimbMeleeDamage(Stock stock, Limb limb)
		{
			string damageName = "melee" + (int)limb + "_damage";
			string damageExp = "exp_" + damageName;
			double damage = Math.Pow(SizeRatio(stock), GetLimbAttributeValue(damageExp)) * GetLimbAttributeValue(damageName);
			if (damage < 0 )
			{
				return 0;
			}
			return damage;
		}

		public double CalcLimbRangeDamage(Stock stock, Limb limb)
		{
			string damageName = "range" + (int)limb + "_damage";
			string damageExp = "exp_" + damageName;
			double damage = Math.Pow(SizeRatio(stock), GetLimbAttributeValue(damageExp)) * GetLimbAttributeValue(damageName);
			if (damage < 0)
			{
				return 0;
			}
			return damage;
		}

		public double GetLimbRangeMax(Stock stock, Limb limb)
		{
			string rangeMax = "range" + (int)limb + "_max";
			string rangeExp = "exp_range" + (int)limb + "_max";
			double value = Math.Pow(SizeRatio(stock), GetLimbAttributeValue(rangeExp)) * GetLimbAttributeValue(rangeMax);
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

		#endregion

		public override string ToString()
		{
			return Name;
		}
	}

    // Not thread safe
    public class StockFactory
    {
        private static readonly StockFactory _instance = new StockFactory();

        public static StockFactory Instance
        {
            get
            {
                return _instance;
            }
        }

		private Dictionary<Limb, bool> CreateBodyParts(bool[] values)
		{
			Limb[] limbs = (Limb[])Enum.GetValues(typeof(Limb));
			return limbs.Zip(values, (k, v) => new { k, v })
				.ToDictionary(x => x.k, x => x.v);
		}

		private StockFactory() { }

        public Stock CreateStock(string animalName, LuaHandler lua)
        {
			Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
			string path = Path.Combine(Environment.CurrentDirectory, Utility.StockDirectory);
			return new Stock(animalName, lua.GetLimbAttributes(path + animalName + ".lua"));
        }

		public Stock CreateStockFromFile(string file, LuaHandler lua)
		{
			Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
			string path = Path.Combine(Environment.CurrentDirectory, Utility.StockDirectory);
			return new Stock(file.Remove(file.Count() - 4), lua.GetLimbAttributes(path + file));
		}
    }
}
