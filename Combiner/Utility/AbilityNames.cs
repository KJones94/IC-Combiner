namespace Combiner.Utility
{
	using System.Collections.Generic;

	public static class AbilityNames
	{
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

		public static Dictionary<string, string> ProperAbilityNames = new Dictionary<string, string>
		                                                              {
			{ Assassinate, nameof(Assassinate) },
			{ Camouflage, nameof(Camouflage) },
			{ ChargeAttack, nameof(ChargeAttack) },
			{ Colony, nameof(Colony) },
			{ Conglomerate, nameof(Conglomerate) },
			{ DefileLand, nameof(DefileLand) },
			{ DeflectionArmour, nameof(DeflectionArmour) },
			{ Digging, nameof(Digging) },
			{ DisorientingBarbs, nameof(DisorientingBarbs) },
			{ ElectricBurst, nameof(ElectricBurst) },
			{ EnduranceBonus, nameof(EnduranceBonus) },
			{ Flash, nameof(Flash) },
			{ FlashHead, nameof(FlashHead) },
			{ Frenzy, nameof(Frenzy) },
			{ HardShell, nameof(HardShell) },
			{ Herding, nameof(Herding) },
			{ Hovering, nameof(Hovering) },
			{ Infestation, nameof(Infestation) },
			{ Immunity, nameof(Immunity) },
			{ KeenSense, nameof(KeenSense) },
			{ LeapAttack, nameof(LeapAttack) },
			{ Loner, nameof(Loner) },
			{ Overpopulation, nameof(Overpopulation) },
			{ PackHunter, nameof(PackHunter) },
			{ Plague, nameof(Plague) },
			{ PoisonBite, nameof(PoisonBite) },
			{ PoisonPincers, nameof(PoisonPincers) },
			{ PoisonSting, nameof(PoisonSting) },
			{ PoisonTouch, nameof(PoisonTouch) },
			{ QuillBurst, nameof(QuillBurst) },
			{ Regeneration, nameof(Regeneration) },
			{ SonarPulse, nameof(SonarPulse) },
			{ StinkCloud, nameof(StinkCloud) },
			{ WebThrow, nameof(WebThrow) }
		};
	}
}
