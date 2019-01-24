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
					ChosenLimbs.Add(limb, left);
				}
				else if (side == Side.Right)
				{
					ChosenLimbs.Add(limb, right);
				}
				else
				{
					ChosenLimbs.Add(limb, null);
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

		private double CalcHitpoints()
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

		private double CalcSize()
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

		private double CalcArmour()
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

		private double CalcSightRadius()
		{
			return ChosenLimbs[Limb.Head].CalcLimbSightRadius();
		}

		private double CalcLandSpeed()
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

		private double CalcAirSpeed()
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

		private double CalcWaterSpeed()
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

		private double CalcMeleeDamage()
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

		// TODO: Need to calculate range damages individually
		private void CalcRangeDamage(Dictionary<string, double> gameAttributes)
		{
			StockStatCalculator side;
			foreach (Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = ChosenLimbs[limb];
				if (side == null)
					continue;
				if (gameAttributes.ContainsKey(Utility.RangeDamage[(int)limb]))
				{
					gameAttributes[Utility.RangeDamage[(int)limb]] = side.CalcLimbRangeDamage(OtherSideSize(side), limb);
				}
			}
		}

		private double CalcRangeDamage(Limb limb, StockStatCalculator side)
		{
			return side.CalcLimbRangeDamage(OtherSideSize(side), limb);
		}
	}
}
