using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public static class Utility
	{
		public static string StockDirectory = "../../Stock/2.5/";
		//public static string StockDirectory = "../../Stock/Test/";
		public static string Attrcombiner = "../../Scripts/2.4/attrcombiner.lua";
		public static string Testcombiner = "../../Scripts/2.5/testcombiner.lua";
		public static string DatabaseString = "../../Database/Creatures.db";
		//public static string DatabaseString = "../../Database/Test.db";
		//public static string DatabaseMasterString = "../../Database/Creatures - Master.db";

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

		public static string[] RangeDamage = new string[]
		{
			"",
			"",
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

		public static string[] RangeMax = new string[]
		{
			"",
			"",
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

		public static string[] RangeType = new string[]
		{
			"",
			"",
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

		public static string[] RangeSpecial = new string[]
		{
			"",
			"",
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

		public static string[] MeleeType = new string[]
		{
			"",
			"",
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

		public static string Assassinate = "assassinate";
		public static string Camouflage = "is_stealthy";
		public static string ChargeAttack = "charge_attack";
		public static string DefileLand = "soiled_land";
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

		public static Dictionary<string, string> ProperAbilityNames = ConstructProperAbilityNames();

		private static Dictionary<string, string> ConstructProperAbilityNames()
		{
			Dictionary<string, string> abilities = new Dictionary<string, string>();
			abilities.Add(Assassinate, nameof(Assassinate));
			abilities.Add(Camouflage, nameof(Camouflage));
			abilities.Add(ChargeAttack, nameof(ChargeAttack));
			abilities.Add(Colony, nameof(Colony));
			abilities.Add(Conglomerate, nameof(Conglomerate));
			abilities.Add(DefileLand, nameof(DefileLand));
			abilities.Add(DeflectionArmour, nameof(DeflectionArmour));
			abilities.Add(Digging, nameof(Digging));
			abilities.Add(DisorientingBarbs, nameof(DisorientingBarbs));
			abilities.Add(ElectricBurst, nameof(ElectricBurst));
			abilities.Add(EnduranceBonus, nameof(EnduranceBonus));
			abilities.Add(Flash, nameof(Flash));
			abilities.Add(FlashHead, nameof(FlashHead));
			abilities.Add(Frenzy, nameof(Frenzy));
			abilities.Add(HardShell, nameof(HardShell));
			abilities.Add(Herding, nameof(Herding));
			abilities.Add(Hovering, nameof(Hovering));
			abilities.Add(Infestation, nameof(Infestation));
			abilities.Add(Immunity, nameof(Immunity));
			abilities.Add(KeenSense, nameof(KeenSense));
			abilities.Add(LeapAttack, nameof(LeapAttack));
			abilities.Add(Loner, nameof(Loner));
			abilities.Add(Overpopulation, nameof(Overpopulation));
			abilities.Add(PackHunter, nameof(PackHunter));
			abilities.Add(Plague, nameof(Plague));
			abilities.Add(PoisonBite, nameof(PoisonBite));
			abilities.Add(PoisonPincers, nameof(PoisonPincers));
			abilities.Add(PoisonSting, nameof(PoisonSting));
			abilities.Add(PoisonTouch, nameof(PoisonTouch));
			abilities.Add(QuillBurst, nameof(QuillBurst));
			abilities.Add(Regeneration, nameof(Regeneration));
			abilities.Add(SonarPulse, nameof(SonarPulse));
			abilities.Add(StinkCloud, nameof(StinkCloud));
			abilities.Add(WebThrow, nameof(WebThrow));

			return abilities;
		}

		public static Dictionary<string, string> ProperStockNames = ConstructProperStockNames();

		public static Dictionary<string, string> ConstructProperStockNames()
		{
			Dictionary<string, string> names = new Dictionary<string, string>();
			names.Add("albatross", "Albatross");
			names.Add("anaconda", "Anaconda");
			names.Add("anglerfish", "Anglerfish");
			names.Add("ant", "Ant");
			names.Add("archerfish", "Archerfish");
			names.Add("armadillo", "Armadillo");
			names.Add("baboon", "Baboon");
			names.Add("vampire_bat", "Bat");
			names.Add("behemoth", "Behemoth");
			names.Add("beluga", "Beluga");
			names.Add("black_widow", "Black Widow");
			names.Add("octopus", "Blue Ringed Octopus");
			names.Add("blue whale", "Blue Whale");
			names.Add("bolas_spider", "Bolas Spider");
			names.Add("bombardier_beetle", "Bombardier Beetle");
			names.Add("bull", "Bull");
			names.Add("camel", "Camel");
			names.Add("chameleon", "Chameleon");
			names.Add("cheetah", "Cheetah");
			names.Add("chimpanzee", "Chimpanzee");
			names.Add("cockroach", "Cockroach");
			names.Add("condor", "Condor");
			names.Add("coyote", "Coyote");
			names.Add("crocodile", "Crocodile");
			names.Add("cuttlefish", "Cuttlefish");
			names.Add("diamond_python", "Diamond Python");
			names.Add("dolphin", "Dolphin");
			names.Add("dragonfly", "Dragonfly");
			names.Add("eagle", "Eagle");
			names.Add("electric_eel", "Electric Eel");
			names.Add("elephant", "Elephant");
			names.Add("fire_fly", "Firefly");
			names.Add("garden_spider", "Garden Spider");
			names.Add("garfish", "Garfish");
			names.Add("giant_anteater", "Giant Anteater");
			names.Add("squid", "Giant Squid");
			names.Add("giraffe", "Giraffe");
			names.Add("gorilla", "Gorilla");
			names.Add("great_white_shark", "Great White Shark");
			names.Add("grizzly_bear", "Grizzly Bear");
			names.Add("hammerhead_shark", "Hammerhead Shark");
			names.Add("hatchetfish", "Hatchetfish");
			names.Add("hercules_beetle", "Hercules Beetle");
			names.Add("hippopotamus", "Hippopotamus");
			names.Add("hornbill", "Hornbill");
			names.Add("hornet", "Hornet");
			names.Add("horse", "Horse");
			names.Add("humpback", "Humpback Whale");
			names.Add("hyena", "Hyena");
			names.Add("kangaroo", "Kangaroo");
			names.Add("killer_whale", "Killer Whale");
			names.Add("komodo_dragon", "Komodo Dragon");
			names.Add("lammergeier", "Lammergeier");
			names.Add("lemming", "Lemming");
			names.Add("lioness", "Lioness");
			names.Add("lobster", "Lobster");
			names.Add("tapir", "Malayan Tapir");
			names.Add("siphonophore", "Man O' War");
			names.Add("mantis shrimp", "Mantis Shrimp");
			names.Add("moose", "Moose");
			names.Add("mountain_lion", "Mountain Lion");
			names.Add("musk_ox", "Musk Ox");
			names.Add("finished naked mole rat", "Naked Mole Rat");
			names.Add("narwhal", "Narwhal");
			names.Add("oogpister", "Oogpister");
			names.Add("panther", "Panther");
			names.Add("piranha", "Piranha");
			names.Add("shrimp", "Pistol Shrimp");
			names.Add("poison_frog", "Poison Frog");
			names.Add("polar_bear", "Polar Bear");
			names.Add("porcupine", "Porcupine");
			names.Add("praying_mantis", "Praying Mantis");
			names.Add("ram", "Ram");
			names.Add("rat", "Rat");
			names.Add("rattlesnake", "Rattlesnake");
			names.Add("raven", "Raven");
			names.Add("rhinoceros", "Rhinoceros");
			names.Add("scorpion", "Scorpion");
			names.Add("stink_bug", "Shield Bug");
			names.Add("bob", "Skipper Caterpillar");
			names.Add("skunk", "Skunk");
			names.Add("snail", "Snail");
			names.Add("snapping_turtle", "Snapping Turtle");
			names.Add("snowy_owl", "Snowy Owl");
			names.Add("sperm_whale", "Sperm Whale");
			names.Add("spitting_cobra", "Spitting Cobra");
			names.Add("spider", "Spitting Spider");
			names.Add("tarantula", "Tarantula");
			names.Add("termite", "Termite");
			names.Add("tiger", "Tiger");
			names.Add("tuna", "Bluefin Tuna");
			names.Add("velvet_worm", "Velvet Worm");
			names.Add("vulture", "Vulture");
			names.Add("stick_bug", "Walking Stick");
			names.Add("walrus", "Walrus");
			names.Add("warthog", "Warthog");
			names.Add("spider_wasp", "Wasp");
			names.Add("whale shark", "Whale Shark");
			names.Add("wolf", "Wolf");
			names.Add("wolverine", "Wolverine");
			names.Add("woolly_mammoth", "Woolly Mammoth");
			names.Add("zebra", "Zebra");

			return names;
		}

		public static string[] ClawedArachnids = 
		{
			"lobster",
			"shrimp",
			"scorpion",
			"praying_mantis",
			"tarantula",
			"pistol shrimp",
			"siphonophore",
			"mantis shrimp"
		};

	}

	public enum DamageType
	{
		Normal = 0,
		VenomSpray = 1,
		Horns = 2,
		BarrierDestroy = 4,
		Electric = 8,
		Sonic = 16,
		Poison = 256,
	}

	public enum ArtilleryType
	{
		Normal = 0,
		Rock = 1,
		Water = 2,
		Chemical = 3,
	}
}
