namespace Combiner.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Combiner.Enums;
	using Combiner.Utility;

	using MoonSharp.Interpreter;

	public class Stock
	{
		public string Name { get; set; }
		public Dictionary<Limb, bool> BodyParts { get; set; }
		public Table LimbAttritbutes { get; set; }
		public StockType Type { get; set; }

		public Stock(string name, Table limbAttributes)
		{
			this.Name = name;
			this.LimbAttritbutes = limbAttributes;
			this.Type = this.DoubleToStockType(this.GetLimbAttributeValue("stocktype"));
			this.InitBodyParts();
		}

		public double GetLimbAttributeValue(string key)
		{
			var value = this.LimbAttritbutes[key] as Table;
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
			this.BodyParts = new Dictionary<Limb, bool>();
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				this.BodyParts.Add(limb, true);
			}
			this.BodyParts[Limb.Nothing] = false;

			switch (this.Type)
			{
				case StockType.Bird:
					this.BodyParts[Limb.FrontLegs] = false;
					this.BodyParts[Limb.Claws] = false;
					break;

				case StockType.Quadruped:
					this.BodyParts[Limb.Claws] = false;
					this.BodyParts[Limb.Wings] = false;
					break;

				case StockType.Arachnid:
					if (this.Name == StockNames.ManOWar)
					{
						this.BodyParts[Limb.FrontLegs] = false;
						this.BodyParts[Limb.BackLegs] = false;
						this.BodyParts[Limb.Wings] = false;
					}
					else if (StockNames.ClawedArachnids.Contains(this.Name))
					{
						this.BodyParts[Limb.Wings] = false;
					}
					else
					{
						this.BodyParts[Limb.Claws] = false;
						this.BodyParts[Limb.Wings] = false;
					}
					break;

				case StockType.Snake:
					this.BodyParts[Limb.FrontLegs] = false;
					this.BodyParts[Limb.BackLegs] = false;
					this.BodyParts[Limb.Claws] = false;
					this.BodyParts[Limb.Wings] = false;
					break;

				case StockType.Insect:
					this.BodyParts[Limb.Claws] = false;
					break;

				case StockType.Fish:
					if (this.Name == StockNames.HumpbackWhale)
					{
						this.BodyParts[Limb.BackLegs] = false;
						this.BodyParts[Limb.Claws] = false;
						this.BodyParts[Limb.Wings] = false;
					}
					else if (this.Name == StockNames.BlueRingedOctopus)
					{
						this.BodyParts[Limb.Claws] = false;
						this.BodyParts[Limb.Wings] = false;
					}
					else
					{
						this.BodyParts[Limb.FrontLegs] = false;
						this.BodyParts[Limb.BackLegs] = false;
						this.BodyParts[Limb.Claws] = false;
						this.BodyParts[Limb.Wings] = false;
					}
					break;

				default:
					break;
			}
		}

		public override string ToString()
		{
			return this.Name;
		}
	}
}
