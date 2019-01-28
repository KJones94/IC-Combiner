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
					if (Name == StockNames.ManOWar)
					{
						BodyParts[Limb.FrontLegs] = false;
						BodyParts[Limb.BackLegs] = false;
						BodyParts[Limb.Wings] = false;
					}
					else if (StockNames.ClawedArachnids.Contains(Name))
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
					if (Name == StockNames.HumpbackWhale)
					{
						BodyParts[Limb.BackLegs] = false;
						BodyParts[Limb.Claws] = false;
						BodyParts[Limb.Wings] = false;
					}
					else if (Name == StockNames.BlueRingedOctopus)
					{
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

		public override string ToString()
		{
			return Name;
		}
	}
}
