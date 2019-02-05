namespace Combiner.Utility
{
	public static class Attributes
	{

		public static string Ticks = "constructionticks";
		public static string Rank = "creature_rank";
		public static string Coal = "cost";
		public static string Electricity = "costrenew";
		public static string PopSize = "popsize";

		public static string Power = "Power";

		public static string Size = "size";
		public static string SightRadius = "sight_radius1";
		public static string Armour = "armour";
		public static string Hitpoints = "hitpoints";
		public static string LandSpeed = "speed_max";
		public static string WaterSpeed = "waterspeed_max";
		public static string AirSpeed = "airspeed_max";
		public static string MeleeDamage = "melee_damage";

		public static string Range2Damage = "range2_damage";
		public static string Range3Damage = "range3_damage";
		public static string Range4Damage = "range4_damage";
		public static string Range5Damage = "range5_damage";
		public static string Range6Damage = "range6_damage";
		public static string Range7Damage = "range7_damage";
		public static string Range8Damage = "range8_damage";

		public static string[] RangeDamage = {
			string.Empty,
			string.Empty,
			Range2Damage,
			Range3Damage,
			Range4Damage,
			Range5Damage,
			Range6Damage,
			Range7Damage,
			Range8Damage
		};

		public static string Range2Max = "range2_max";
		public static string Range3Max = "range3_max";
		public static string Range4Max = "range4_max";
		public static string Range5Max = "range5_max";
		public static string Range6Max = "range6_max";
		public static string Range7Max = "range7_max";
		public static string Range8Max = "range8_max";

		public static string[] RangeMax = {
			string.Empty,
			string.Empty,
			Range2Max,
			Range3Max,
			Range4Max,
			Range5Max,
			Range6Max,
			Range7Max,
			Range8Max
		};

		public static string Range2Type = "range2_dmgtype";
		public static string Range3Type = "range3_dmgtype";
		public static string Range4Type = "range4_dmgtype";
		public static string Range5Type = "range5_dmgtype";
		public static string Range6Type = "range6_dmgtype";
		public static string Range7Type = "range7_dmgtype";
		public static string Range8Type = "range8_dmgtype";

		public static string[] RangeType = {
			string.Empty,
			string.Empty,
			Range2Type,
			Range3Type,
			Range4Type,
			Range5Type,
			Range6Type,
			Range7Type,
			Range8Type
		};

		public static string Range2Special = "range2_special";
		public static string Range3Special = "range3_special";
		public static string Range4Special = "range4_special";
		public static string Range5Special = "range5_special";
		public static string Range6Special = "range6_special";
		public static string Range7Special = "range7_special";
		public static string Range8Special = "range8_special";

		public static string[] RangeSpecial = {
			string.Empty,
			string.Empty,
			Range2Special,
			Range3Special,
			Range4Special,
			Range5Special,
			Range6Special,
			Range7Special,
			Range8Special
		};

		public static string Melee2Damage = "melee2_damage";
		public static string Melee3Damage = "melee3_damage";
		public static string Melee4Damage = "melee4_damage";
		public static string Melee5Damage = "melee5_damage";
		public static string Melee6Damage = "melee6_damage";
		public static string Melee7Damage = "melee7_damage";
		public static string Melee8Damage = "melee8_damage";

		public static string Melee2Type = "melee2_dmgtype";
		public static string Melee3Type = "melee3_dmgtype";
		public static string Melee4Type = "melee4_dmgtype";
		public static string Melee5Type = "melee5_dmgtype";
		public static string Melee6Type = "melee6_dmgtype";
		public static string Melee7Type = "melee7_dmgtype";
		public static string Melee8Type = "melee8_dmgtype";

		public static string[] MeleeType = {
			string.Empty,
			string.Empty,
			Melee2Type,
			Melee3Type,
			Melee4Type,
			Melee5Type,
			Melee6Type,
			Melee7Type,
			Melee8Type
		};

		public static readonly string IsLand = "is_land";
		public static readonly string IsSwimmer = "is_swimmer";
		public static readonly string IsFlyer = "is_flyer";
	}
}
