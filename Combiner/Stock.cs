using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combiner
{
    class Stock
    {
        public string Name { get; set; }
        public Dictionary<Limb, bool> BodyParts { get; set; }
        public Table LimbAttritbutes { get; set; }
		public StockType Type { get; set; }

        public Stock(string name, Table limbAttributes)
        {
            Name = name;
            LimbAttritbutes = limbAttributes;
			Type = DoubleToStockType(GetLimbAttribute("stocktype"));
			InitBodyParts();
        }

        private double GetLimbAttribute(string key)
        {
            return (double)(LimbAttritbutes[key] as Table)[2];
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

        
    }

    // Not thread safe
    class StockFactory
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
			string path = Path.Combine(Environment.CurrentDirectory, @"..\..\Stock\");
			return new Stock(animalName, lua.GetLimbAttributes(path + animalName + ".lua"));
        }
    }

    enum Limb
    {
        Nothing,
        General,
        FrontLegs,
        BackLegs,
        Head,
        Tail,
        Torso,
        Wings,
        Claws
    }

    enum StockType
    {
        Bird,
        Quadruped,
        Arachnid,
        Snake,
        Insect,
        Fish
    }

    
}
