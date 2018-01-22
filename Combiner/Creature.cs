using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combiner
{
    class Creature
    {
        public Stock Left { get; set; }
        public Stock Right { get; set; }
        Dictionary<Limb, bool> PossibleBodyParts { get; set; }
        Dictionary<Limb, Side> ChosenBodyParts { get; set; }

		//Table GameAttributes { get; set; }  // Need to connect to script...
		public Dictionary<string, double> GameAttributes { get; set; } = new Dictionary<string, double>();
		public Dictionary<string, bool> Abilities { get; set; } = new Dictionary<string, bool>();

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

		#endregion


		// Abilities

		public Creature(Stock left, Stock right, Dictionary<Limb, Side> chosenBodyParts)
		{
			Left = left;
			Right = right;
			ChosenBodyParts = chosenBodyParts;
			InitGameAttributes();
			InitStats();
			InitAbilities();
		}

		// Not sure what this is for yet...
		private void ConsolidatePossibleBodyParts()
        {
            PossibleBodyParts = new Dictionary<Limb, bool>(Left.BodyParts);
            foreach (Limb limb in Right.BodyParts.Keys)
            {
                if (Right.BodyParts[limb])
                {
                    PossibleBodyParts[limb] = true;
                }
            }
        }

		// Not sure what this is for yet...
        private void InitChosenBodyParts()
        {
            ChosenBodyParts = new Dictionary<Limb, Side>();
            foreach (Limb limb in PossibleBodyParts.Keys)
            {
                if (PossibleBodyParts[limb])
                {
                    if (Left.BodyParts[limb])
                    {
                        ChosenBodyParts.Add(limb, Side.Left);
                    }
                    else if (Right.BodyParts[limb])
                    {
                        ChosenBodyParts.Add(limb, Side.Right);
                    }
                    else
                    {
                        ChosenBodyParts.Add(limb, Side.Empty);
                    }
                }
                else
                {
                    ChosenBodyParts.Add(limb, Side.Null);
                }
            }
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
			GameAttributes.Add(Utility.Size, 0);
			GameAttributes.Add(Utility.SightRadius, 0);
			GameAttributes.Add(Utility.Armour, 0);
			GameAttributes.Add(Utility.Hitpoints, 0);
			GameAttributes.Add(Utility.LandSpeed, 0);
			GameAttributes.Add(Utility.WaterSpeed, 0);
			GameAttributes.Add(Utility.AirSpeed, 0);
			GameAttributes.Add(Utility.MeleeDamage, 0);
			GameAttributes.Add(Utility.RangeDamage, 0);

			//GameAttributes.Add(Utility.Assassinate, 0);
			//GameAttributes.Add(Utility.Camouflage, 0);
			//GameAttributes.Add(Utility.ChargeAttack, 0);
			//GameAttributes.Add(Utility.DefileLand, 0);
			//GameAttributes.Add(Utility.DeflectionArmour, 0);
			//GameAttributes.Add(Utility.Digging, 0);
			//GameAttributes.Add(Utility.DisorientingBarbs, 0);
			//GameAttributes.Add(Utility.ElectricBurst, 0);
			//GameAttributes.Add(Utility.EnduranceBonus, 0);
			//GameAttributes.Add(Utility.Flash, 0);
			//GameAttributes.Add(Utility.FlashHead, 0);
			//GameAttributes.Add(Utility.Frenzy, 0);
			//GameAttributes.Add(Utility.HardShell, 0);
			//GameAttributes.Add(Utility.Herding, 0);
			//GameAttributes.Add(Utility.Hovering, 0);
			//GameAttributes.Add(Utility.Infestation, 0);
			//GameAttributes.Add(Utility.Immunity, 0);
			//GameAttributes.Add(Utility.KeenSense, 0);
			//GameAttributes.Add(Utility.LeapAttack, 0);
			//GameAttributes.Add(Utility.Loner, 0);
			//GameAttributes.Add(Utility.Overpopulation, 0);
			//GameAttributes.Add(Utility.PackHunter, 0);
			//GameAttributes.Add(Utility.Plague, 0);
			//GameAttributes.Add(Utility.PoisonBite, 0);
			//GameAttributes.Add(Utility.PoisonPincers, 0);
			//GameAttributes.Add(Utility.PoisonSting, 0);
			//GameAttributes.Add(Utility.PoisonTouch, 0);
			//GameAttributes.Add(Utility.Colony, 0);
			//GameAttributes.Add(Utility.Conglomerate, 0);
			//GameAttributes.Add(Utility.QuillBurst, 0);
			//GameAttributes.Add(Utility.Regeneration, 0);
			//GameAttributes.Add(Utility.SonarPulse, 0);
			//GameAttributes.Add(Utility.StinkCloud, 0);
			//GameAttributes.Add(Utility.WebThrow, 0);

			foreach (string ability in Utility.Abilities)
			{
				Abilities.Add(ability, false);
			}
		}

		private void InitStats()
		{
			CalcHitpoints(); // Stalling here, why?
			CalcSize();
			CalcArmour();
			CalcLandSpeed();
			CalcWaterSpeed();
			CalcAirSpeed();
			CalcMeleeDamage();
		}

		private void InitAbilities()
		{
			/**
			 * Iterate through each ability
			 * Check each body part of the creature with that ability's body part
			 * If matches, check if ability is present
			 * 
			 * 
			 **/

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
						Abilities[ability] = true;
					}
				}
			}
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
		}

		private void CalcMeleeDamage()
		{
			double meleeDamage = 0.0;
			Stock side;
			foreach(Limb limb in Enum.GetValues(typeof(Limb)))
			{
				side = GetSide(limb);
				if (side == null)
					continue;
				meleeDamage += side.CalcLimbMeleeDamage(OtherSide(side), limb);
			}
			MeleeDamage = meleeDamage;
		}

		#endregion


    }

    class CreatureFactory
    {
        private static readonly CreatureFactory _instance = new CreatureFactory();

        public static CreatureFactory Instance
        {
            get
            {
                return _instance;
            }
        }

        private CreatureFactory() { }

        public Creature CreateCreature(Stock left, Stock right, Dictionary<Limb, Side> chosenBodyParts)
        {
            return new Creature(left, right, chosenBodyParts);
        }
    }
}
