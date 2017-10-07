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

		public Creature(Stock left, Stock right)
		{
			Left = left;
			Right = right;

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

        public Creature CreateStock(string animalName, LuaHandler lua)
        {
            return null;
        }
    }
}
