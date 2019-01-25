using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
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
			ChosenLimbs = InitChosenLimbs(left, right, chosenBodyParts);
			Left = left;
			Right = right;
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
			if (stock.Name == Left.Name)
			{
				return Right.GetLimbAttributeValue(Utility.Size);
			}
			else
			{
				return Left.GetLimbAttributeValue(Utility.Size);
			}
		}

		public double CalcHitpoints()
		{
			double hitpoints = 0.0;
			StockStatCalculator side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = ChosenLimbs[limb];
				if (side == null)
					continue;
				hitpoints += side.CalcLimbHitpoints(OtherSideSize(side), limb);
			}
			return hitpoints;
		}

		public double CalcSize()
		{
			double leftSize = Left.GetLimbAttributeValue("size");
			double rightSize = Right.GetLimbAttributeValue("size");
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
				side = ChosenLimbs[limb];
				if (side == null)
					continue;
				armour += side.CalcLimbArmour(OtherSideSize(side), limb);
			}
			return armour;
		}

		public double CalcSightRadius()
		{
			return ChosenLimbs[Limb.Head].CalcLimbSightRadius();
		}

		public double CalcLandSpeed()
		{
			double landSpeed = 0.0;
			StockStatCalculator side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = ChosenLimbs[limb];
				if (side == null)
					continue;

				landSpeed += side.CalcLimbLandSpeed(OtherSideSize(side), limb);
			}
			return landSpeed;

			// TODO: put this back somewhere
			//if (LandSpeed > 0)
			//{
			//	IsLand = 1;
			//}
		}

		public double CalcAirSpeed()
		{
			double airSpeed = 0.0;
			StockStatCalculator side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = ChosenLimbs[limb];
				if (side == null)
					continue;
				airSpeed += side.CalcLimbAirSpeed(OtherSideSize(side), limb);
			}
			return airSpeed;

			// TODO: put this back somewhere
			//if (AirSpeed > 0)
			//{
			//	IsFlyer = 1;
			//}
		}

		public double CalcWaterSpeed()
		{
			double waterSpeed = 0.0;
			StockStatCalculator side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = ChosenLimbs[limb];
				if (side == null)
					continue;
				waterSpeed += side.CalcLimbWaterSpeed(OtherSideSize(side), limb);
			}
			return waterSpeed;

			// TODO: put this back somewhere
			//if (WaterSpeed > 0)
			//{
			//	IsSwimmer = 1;
			//}
		}

		public double CalcMeleeDamage()
		{
			double meleeDamage = 0.0;
			StockStatCalculator side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = ChosenLimbs[limb];
				if (side == null)
					continue;
				meleeDamage += side.CalcLimbMeleeDamage(OtherSideSize(side), limb);
			}
			return meleeDamage;
		}

		public double CalcRangeDamage(Limb limb)
		{
			StockStatCalculator side = ChosenLimbs[limb];
			if (side == null)
				return 0;
			return side.CalcLimbRangeDamage(OtherSideSize(side), limb);
		}

		public double CalcRangeMax(Limb limb)
		{
			StockStatCalculator side = ChosenLimbs[limb];
			if (side == null)
				return 0;
			return side.GetLimbRangeMax(OtherSideSize(side), limb);
		}

		public double GetRangeType(Limb limb)
		{
			StockStatCalculator side = ChosenLimbs[limb];
			if (side == null)
				return 0;
			return side.GetLimbRangeType(limb);
		}

		public double GetRangeSpecial(Limb limb)
		{
			StockStatCalculator side = ChosenLimbs[limb];
			if (side == null)
				return 0;
			return side.GetLimbRangeSpecial(limb);
		}

		public double GetMeleeType(Limb limb)
		{
			StockStatCalculator side = ChosenLimbs[limb];
			if (side == null)
				return 0;
			return side.GetLimbMeleeType(limb);
		}
	}
}
