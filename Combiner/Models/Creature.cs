using System.Collections.Generic;

namespace Combiner
{
	public class Creature
	{
		public int Id { get; set; }

		public string Left { get; set; }
		public string Right { get; set; }
		public Dictionary<string, string> BodyParts { get; set; }

		public int Rank { get; set; }
		public double Coal { get; set; }
		public double Electricity { get; set; }

		public double Power { get; set; }
		public double EffectiveHitpoints { get; set; }

		public double Hitpoints { get; set; }
		public double Armour { get; set; }
		public double SightRadius { get; set; }
		public double Size { get; set; }
		public double LandSpeed { get; set; }
		public double WaterSpeed { get; set; }
		public double AirSpeed { get; set; }
		public double MeleeDamage { get; set; }
		public bool HasHorns { get; set; }
		public bool HasBarrierDestroy { get; set; }
		public bool HasPoison { get; set; }
		public double RangeDamage1 { get; set; }
		public double RangeDamage2 { get; set; }
		public double RangeSpecial1 { get; set; }
		public double RangeSpecial2 { get; set; }
		public double RangeType1 { get; set; }
		public double RangeType2 { get; set; }
		public double RangeMax1 { get; set; }
		public double RangeMax2 { get; set; }

		public Dictionary<string, bool> Abilities { get; set; }
	}
}
