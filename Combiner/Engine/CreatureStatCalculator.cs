namespace Combiner.Engine
{
	using System;
	using System.Collections.Generic;

	using Combiner.Enums;
	using Combiner.Utility;

	public class CreatureStatCalculator
	{
		public StockStatCalculator Left { get; set; }
		public StockStatCalculator Right { get; set; }
		Dictionary<Limb, Side> ChosenBodyParts { get; set; }

		Dictionary<Limb, StockStatCalculator> ChosenLimbs { get; set; }

		public CreatureStatCalculator(
			StockStatCalculator left,
			StockStatCalculator right,
			Dictionary<Limb, Side> chosenBodyParts)
		{
			this.ChosenLimbs = this.InitChosenLimbs(left, right, chosenBodyParts);
			this.Left = left;
			this.Right = right;
		}

		private Dictionary<Limb, StockStatCalculator> InitChosenLimbs(
			StockStatCalculator left,
			StockStatCalculator right,
			Dictionary<Limb, Side> chosenBodyParts)
		{
			Dictionary<Limb, StockStatCalculator> chosenLimbs = new Dictionary<Limb, StockStatCalculator>();
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				Side side = chosenBodyParts[limb];
				if (side == Side.Left)
				{
					chosenLimbs.Add(limb, left);
				}
				else if (side == Side.Right)
				{
					chosenLimbs.Add(limb, right);
				}
				else
				{
					chosenLimbs.Add(limb, null);
				}
			}
			return chosenLimbs;
		}

		private double OtherSideSize(StockStatCalculator stock)
		{
			if (stock.Name == this.Left.Name)
			{
				return this.Right.GetLimbAttributeValue(Attributes.Size);
			}
			else
			{
				return this.Left.GetLimbAttributeValue(Attributes.Size);
			}
		}

		public double CalcHitpoints()
		{
			double hitpoints = 0.0;
			StockStatCalculator side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = this.ChosenLimbs[limb];
				if (side == null)
				{
					continue;
				}
				hitpoints += side.CalcLimbHitpoints(this.OtherSideSize(side), limb);
			}
			return hitpoints;
		}

		public double CalcSize()
		{
			double leftSize = this.Left.GetLimbAttributeValue(Attributes.Size);
			double rightSize = this.Right.GetLimbAttributeValue(Attributes.Size);
			if (leftSize >= rightSize)
			{
				return leftSize;
			}
			else
			{
				return rightSize;
			}
		}

		public double CalcArmour()
		{
			double armour = 0.0;
			StockStatCalculator side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = this.ChosenLimbs[limb];
				if (side == null)
				{
					continue;
				}
				armour += side.CalcLimbArmour(this.OtherSideSize(side), limb);
			}
			return armour;
		}

		public double CalcSightRadius()
		{
			return this.ChosenLimbs[Limb.Head].CalcLimbSightRadius();
		}

		public double CalcLandSpeed()
		{
			double landSpeed = 0.0;
			StockStatCalculator side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = this.ChosenLimbs[limb];
				if (side == null)
				{
					continue;
				}
				landSpeed += side.CalcLimbLandSpeed(this.OtherSideSize(side), limb);
			}
			return landSpeed;
		}

		public double CalcAirSpeed()
		{
			double airSpeed = 0.0;
			StockStatCalculator side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = this.ChosenLimbs[limb];
				if (side == null)
				{
					continue;
				}
				airSpeed += side.CalcLimbAirSpeed(this.OtherSideSize(side), limb);
			}
			return airSpeed;
		}

		public double CalcWaterSpeed()
		{
			double waterSpeed = 0.0;
			StockStatCalculator side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = this.ChosenLimbs[limb];
				if (side == null)
				{
					continue;
				}
				waterSpeed += side.CalcLimbWaterSpeed(this.OtherSideSize(side), limb);
			}
			return waterSpeed;
		}

		public double CalcMeleeDamage()
		{
			double meleeDamage = 0.0;
			StockStatCalculator side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = this.ChosenLimbs[limb];
				if (side == null)
				{
					continue;
				}
				meleeDamage += side.CalcLimbMeleeDamage(this.OtherSideSize(side), limb);
			}
			return meleeDamage;
		}

		public double CalcRangeDamage(Limb limb)
		{
			StockStatCalculator side = this.ChosenLimbs[limb];
			if (side == null)
			{
				return 0;
			}
			return side.CalcLimbRangeDamage(this.OtherSideSize(side), limb);
		}

		public double CalcRangeMax(Limb limb)
		{
			StockStatCalculator side = this.ChosenLimbs[limb];
			if (side == null)
			{
				return 0;
			}
			return side.GetLimbRangeMax(this.OtherSideSize(side), limb);
		}

		public double GetRangeType(Limb limb)
		{
			StockStatCalculator side = this.ChosenLimbs[limb];
			if (side == null)
			{
				return 0;
			}
			return side.GetLimbRangeType(limb);
		}

		public double GetRangeSpecial(Limb limb)
		{
			StockStatCalculator side = this.ChosenLimbs[limb];
			if (side == null)
			{
				return 0;
			}
			return side.GetLimbRangeSpecial(limb);
		}

		public double GetMeleeType(Limb limb)
		{
			StockStatCalculator side = this.ChosenLimbs[limb];
			if (side == null)
			{
				return 0;
			}
			return side.GetLimbMeleeType(limb);
		}
	}
}
