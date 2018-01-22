using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public static class Utility
	{
		public static string StockDirectory = @"..\..\Stock\2.4\";
		public static string Attrcombiner = "../../Scripts/2.4/attrcombiner.lua";
		public static string Testcombiner = "../../Scripts/testcombiner.lua";


		public static string Size = "size";
		public static string SightRadius = "sight_radius1";
		public static string Armour = "armour";
		public static string Hitpoints = "hitpoints";
		public static string LandSpeed = "speed_max";
		public static string WaterSpeed = "waterspeed_max";
		public static string AirSpeed = "airspeed_max";
		public static string MeleeDamage = "melee_damage";
		public static string RangeDamage = "range_damage";




		public static string Assassinate = "assassinate";
		public static string Camouflage = "is_stealthy";
		public static string ChargeAttack = "charge_attack";
		public static string DefileLand = "soiled_lang";
		public static string DeflectionArmour = "deflection_armour";
		public static string Digging = "can_dig";
		public static string DisorientingBarbs = "AutoDefense";
		public static string ElectricBurst = "electric_burst";
		public static string EnduranceBonus = "end_bonus";
		public static string Flash = "flash";
		public static string FlashHead = "headflashdisplay";
		public static string Frenzy = "frenzy_attack";
		public static string HardShell = "hard_shell";
		public static string Herding = "herding";
		public static string Hovering = "can_SRF";
		public static string Infestation = "infestation";
		public static string Immunity = "is_immune";
		public static string KeenSense = "keen_sense";
		public static string LeapAttack = "leap_attack";
		public static string Loner = "loner";
		public static string Overpopulation = "overpopulation";
		public static string PackHunter = "pack_hunter";
		public static string Plague = "plague_attack";
		public static string PoisonBite = "poison_bite";
		public static string PoisonPincers = "poison_pincers";
		public static string PoisonSting = "poison_sting";
		public static string PoisonTouch = "poison_touch";
		public static string Colony = "poplow";
		public static string Conglomerate = "poplowtorso";
		public static string QuillBurst = "quill_burst";
		public static string Regeneration = "regeneration";
		public static string SonarPulse = "sonar_pulse";
		public static string StinkCloud = "stink_attack";
		public static string WebThrow = "web_throw";

		public static string[] Abilities =
		{
			Assassinate,
			Camouflage,
			ChargeAttack,
			DefileLand,
			DeflectionArmour,
			Digging,
			DisorientingBarbs,
			ElectricBurst,
			EnduranceBonus,
			Flash,
			FlashHead,
			Frenzy,
			HardShell,
			Herding,
			Hovering,
			Infestation,
			Immunity,
			KeenSense,
			LeapAttack,
			Loner,
			Overpopulation,
			PackHunter,
			Plague,
			PoisonBite,
			PoisonPincers,
			PoisonSting,
			PoisonTouch,
			Colony,
			Conglomerate,
			QuillBurst,
			Regeneration,
			SonarPulse,
			StinkCloud,
			WebThrow
		};
	}
}
