--Changelog for Tellurian 2.9.1.0 (relative to Public Beta 2.9.1.1):
    -- Frontend Changes (if you're a player, the below is relevant to you!)
    -- Herding rewritten and given a newly-established "herding_ehp_bonus" domain; as such, herding creatures with low defense will no longer be overcharged.
    -- Horns cost reduced (parameters changed to {-10, 100, 180, 530} from {0, 120, 200, 550}).
    -- Flyer base cost completely removed. Instead, flight now applies an imaginary multiplier to EHP, damage and mobility; these adjusted numbers
    -- are then used for all cost calculations.
    -- Poison frog can now have leap attack (thanks BChamp!).
    -- Charge Attack and Leap Attack moved to EHP and Damage domains; charge a bit more expensive.
    -- Minimum flyer build time increased to 40 ticks.
    -- Sonic cost reduced slightly.
    -- Slight increase in cost of water speed (for both pure swimmer and amphib).
    -- Pack Hunter Direct Range and Sonic cost increased by a new multiplier, set to 1.15.
    -- Flying range now has a small associated elec cost (as flying range now gets a 25% damage boost).
    -- Barrier destroy cost slightly increased.
    -- Level 5 spam cheaper, very high-power slightly more expensive.

    -- Backend Changes
    -- Range costs now calculated as part of the ability cost calculation step.
    -- Revamped flyer cost has seen substantial changes to attr. Check out how the flyer mults are used for further explanation.
    -- dofilepath is here! We can now access variables directly from tuning, and all attr functions have been moved to attr_functions.lua.
    -- We now use dofilepath to include attr_parameters.lua; this lua allows us to easily choose between sight radius and power display and more!
    -- To implement the size/pop space switch, UI scaling has been moved to the end of attrcombiner.



-- * Global Tuning Values
high_ground_range_multiplier = 1.25;

---------------------------------------------------------------------
-- Poison Tuning values

Poison =
{
	-- Total Time the Poison Lasts for
	poisontime				= 10.0,

	-- Total amount of damage the Poison should deal, per attacker rank
	poisondamage1			= 05.0,
	poisondamage2			= 10.0,
	poisondamage3			= 20.0,
	poisondamage4			= 25.0,
	poisondamage5			= 50.0,

	-- The amount to multiply the creature's speed by when poisoned
	speedmultiplier			= 0.75,

	-- The amount to multiply the creature's attack damage by when poisoned
	damagemultiplier		= 0.85,

	-- The poisoned victom has a reduced sight radius to this value
	poisonsightradius		= 0.0,
}

---------------------------------------------------------------------
-- Venom Spray Tuning values

VenomSpray =
{
	-- Total Time the Venom Spray Lasts for
	venomtime				= 10.0,
	-- Total amount of damage the Venom Spray should deal
	venomdamage				= 25.0,
	-- Amount to set the victim's sight radius to while it is blinded
	venomsightradius			= 0.0,
}

---------------------------------------------------------------------
-- Endurance Tuning values

Endurance =
{
	-- the base amount of endurance for all animals/creatures
	endurancebase		= 100.0,

	-- the ammount of regeneration that occurs over 1 second
	regen_normal		= 2.0,

	regen_bonus			= 5.0,

	regen_penalty		= 3.0,
}

---------------------------------------------------------------------
-- Defense Tuning values

Defense =
{
	-- the max your defence can be set to through modifications
	defensemax		= 0.6,
}


---------------------------------------------------------------------
-- Loner Bonus

Loner =
{
	-- The radius at which the loner bonus becomes effective (in metres)
	lonerRadius			= 30.0,

	-- Include henchmen in radius check?
	-- yes = 1 or no = 0 (default)
	checkForHenchmen		= 0,

	-- Artillery ranged bonus per creature size
	-- This *is a multiplier*, e.g. a 50% bonus should be written as 1.5.
	artilleryBonusBaseSize1		= 1.0,
	artilleryBonusBaseSize2		= 1.0,
	artilleryBonusBaseSize3		= 1.0,
	artilleryBonusBaseSize4		= 1.0,
	artilleryBonusBaseSize5		= 1.0,
	artilleryBonusBaseSize6		= 1.0,
	artilleryBonusBaseSize7		= 1.0,
	artilleryBonusBaseSize8		= 1.0,
	artilleryBonusBaseSize9		= 1.0,
	artilleryBonusBaseSize10	= 1.0,

	-- Artillery bonus modifier per creature rank (+/-)
	-- This is a modifier to the above base percentage.
	artilleryBonusModRank1		= 6.0,
	artilleryBonusModRank2		= 3.0,
	artilleryBonusModRank3		= 3.0,
	artilleryBonusModRank4		= 4.0,
	artilleryBonusModRank5		= 5.0,

	-- Direct ranged bonus per creature size
	-- This *is a multiplier*, e.g. a 50% bonus should be written as 1.5.
	rangedBonusBaseSize1		= 1.0,
	rangedBonusBaseSize2		= 1.0,
	rangedBonusBaseSize3		= 1.0,
	rangedBonusBaseSize4		= 1.0,
	rangedBonusBaseSize5		= 1.0,
	rangedBonusBaseSize6		= 1.0,
	rangedBonusBaseSize7		= 1.0,
	rangedBonusBaseSize8		= 1.0,
	rangedBonusBaseSize9		= 1.0,
	rangedBonusBaseSize10		= 1.0,

	-- Ranged bonus modifier per creature rank (+/-)
	-- This is a modifier to the above base percentage.
	rangedBonusModRank1		= 6.0,
	rangedBonusModRank2		= 4.0,
	rangedBonusModRank3		= 4.0,
	rangedBonusModRank4		= 5.0,
	rangedBonusModRank5		= 6.0,

	-- Melee bonus per creature size
	-- This *is a multiplier*, e.g. a 50% bonus should be written as 1.5.
	meleeBonusBaseSize1		= 1.0,
	meleeBonusBaseSize2		= 1.0,
	meleeBonusBaseSize3		= 1.0,
	meleeBonusBaseSize4		= 1.0,
	meleeBonusBaseSize5		= 1.0,
	meleeBonusBaseSize6		= 1.0,
	meleeBonusBaseSize7		= 1.0,
	meleeBonusBaseSize8		= 1.0,
	meleeBonusBaseSize9		= 1.0,
	meleeBonusBaseSize10	= 1.0,

	-- Melee bonus modifier per creature rank (+/-)
	-- This is a modifier to the above base percentage.
	meleeBonusModRank1		= 6.0,
	meleeBonusModRank2		= 3.0,
	meleeBonusModRank3		= 3.0,
	meleeBonusModRank4		= 4.0,
	meleeBonusModRank5		= 5.0,

	-- Damage Reduction
	-- The is the percentage of damage reduction a loner creature has
	-- valid range: 0.0 - 1.0  = 0% reduction to 100% reduction
	damageReductionBaseSize1	= 0.250,
	damageReductionBaseSize2	= 0.250,
	damageReductionBaseSize3	= 0.250,
	damageReductionBaseSize4	= 0.250,
	damageReductionBaseSize5	= 0.250,
	damageReductionBaseSize6	= 0.250,
	damageReductionBaseSize7	= 0.250,
	damageReductionBaseSize8	= 0.250,
	damageReductionBaseSize9	= 0.250,
	damageReductionBaseSize10	= 0.250,

	-- Damage reduction modifier per creature rank (+/-)
	-- This is a modifier to the above base percentage.
	-- WH: Commented this out; unnecessary.
	--damageReductionModRank1		= 0.0,
	--damageReductionModRank2		= 0.0,
	--damageReductionModRank3		= 0.0,
	--damageReductionModRank4		= 0.0,
	--damageReductionModRank5		= 0.25,

	-- Build speed modifier per creature rank (multiplies to base tick)
	buildSpeedModRank1			= 2.0,
	buildSpeedModRank2			= 2.0,
	buildSpeedModRank3			= 2.0,
	buildSpeedModRank4			= 2.0,
	buildSpeedModRank5			= 2.0,

	-- Speed increase
	--This *is a multiplier*, e.g. a 50% bonus should be written as 1.5.
	speedMultiplier		= 1.5,

	-- Sight radius increase
	-- This *is a multiplier*, e.g. a 50% bonus should be written as 1.5.
	sightRadiusMultiplier		= 1.5,
}


---------------------------------------------------------------------
-- Deflection Tuning values

Deflection =
{
	-- Next 3 sub-sections are normalised chances of deflection.
	-- Valid range is 0.0 (0% - never deflect) to 1.0 (100% - always deflect)

	-- Base deflection chance per creature size
	deflectionBaseSize1	= 0.255,
	deflectionBaseSize2	= 0.26,
	deflectionBaseSize3	= 0.265,
	deflectionBaseSize4	= 0.27,
	deflectionBaseSize5	= 0.275,
	deflectionBaseSize6	= 0.28,
	deflectionBaseSize7	= 0.285,
	deflectionBaseSize8	= 0.29,
	deflectionBaseSize9	= 0.295,
	deflectionBaseSize10	= 0.3,

	-- Deflection modifier per creature rank (+/-)
	deflectionModRank1	= 0.0,
	deflectionModRank2	= 0.0,
	deflectionModRank3	= 0.0,
	deflectionModRank4	= 0.0,
	deflectionModRank5	= 0.0,

	-- Deflection modifier per defenders attack type (+/-)
	-- Order of preference (artillery, direct ranged, melee)
	deflectionModArtillery	= 0.0,
	deflectionModRanged	= 0.0,
	deflectionModMelee	= 0.0,

	-- Deflect sonic attacks?
	-- yes = 1 (default) or no = 0
	deflectSonicAttack	= 1,
}


---------------------------------------------------------------------
-- Flash Tuning values

Flash =
{
	-- Endurance cost
	enduranceCost		= 80.0,

	-- Duration in game ticks
	duration		= 80.0, -- about 10 seconds

	-- Flash Radius in metres (based on flashing creatures' size)
	effectiveRadiusSize1	= 40.0,
	effectiveRadiusSize2	= 40.0,
	effectiveRadiusSize3	= 42.0,
	effectiveRadiusSize4	= 42.0,
	effectiveRadiusSize5	= 44.0,
	effectiveRadiusSize6	= 46.0,
	effectiveRadiusSize7	= 48.0,
	effectiveRadiusSize8	= 50.0,
	effectiveRadiusSize9	= 52.0,
	effectiveRadiusSize10	= 54.0,

	-- Reduced size radius in metres
	reducedSightRadius 	= 1.0,

	-- Height above the ground that initial effect will happen (in metres)
	fxTargetHeight		= 8.0,
}


---------------------------------------------------------------------
-- Health Regeneration Tuning values

HealthRegen =
{
	-- the amount of health that regenerates per second, by creature rank
	regenamount1		= 0.25,
	regenamount2		= 0.75,
	regenamount3		= 1.25,
	regenamount4		= 1.75,
	regenamount5		= 2.0,
}

---------------------------------------------------------------------
-- Stealth Modifier Tuning values

Stealth =
{
	-- the ammount of time in seconds that the creature becomes unCloaked for
	-- when it doesn an attack
	stealthtimeout		= 10.0,
}

---------------------------------------------------------------------
-- Pack Bonus

PackBonus =
{
	-- the minimum number of entities needed for the bonus to kick in
	minentitycount			= 4.0,

	-- the base damage modifier, new damage is this * normal damage
	basedamagemodifier		= 1.3,

	-- the base defense modifier, new defense is this * normal damage
	basedefensemodifier		= 1.3,

	-- the search radius that the pack bonus is effective for, meters
	searchradius			= 20.0,

	-- the amount of sim ticks that need to pass for packing to be reset
	delayticks				= 8,
}

---------------------------------------------------------------------
-- Building

Building =
{
	-- value for decreasing additional henchman to build a structure [0.0,1.0]
	buildDecrease			= 0.01,

	-- value for decreasing additional henchman to repair a structure [0.0,1.0]
	repairDecrease			= 0.08,

	-- amount of health a single henchman repairs [1.0, 1000.0]
	repairHealth			= 1.5,

	-- repair multiplier for labs
	repairLabMult			= 2.5,

	-- how far away does a structure have to be away from a resource before it can be built, in meters
	-- it will make buildings not placeable within a square around the resource of this size
	resourceNoBuildSize		= 22.0,

	-- what percentage of health should a building start with [0.0 to 1.0]
	startHealthPercent		= 0.2,

	-- what percentage of scrap do you get back when you self destroy a building
	scrapBackPercentage		= 0.85,

	-- what percentage of electricity you get back when you self destroy a building
	elecBackPercentage		= 0.85,
}

---------------------------------------------------------------------
-- ResearchBonus

ResearchBonus =
{
	-- hitpoint bonus to give to the lab with the advanced structure research
	advancedStructLabMultiplier		= 1.5,

	-- hitpoint bonus to give to buildings (no lab) with the increase building
	-- integrity research
	incBuildingIntegrityMultiplier		= 2.0,

	-- hitpoint bonus to give to bramble fences with the strengthen-fences
	-- research
	strengthenFenceMultiplier		= 5.0,

	-- collection rate bonus to give to all electricity collectors with the
	-- strengthen-electrical-grid research
	strengthenElecGridMultiplier		= 1.5,

	-- henchman improved healing
	henchmanImpHealingMultiplier		= 2.0,
}

---------------------------------------------------------------------
-- SiteDecal

SiteDecal =
{
	-- default decal names
	defaultLiveDecalName		= "build_con.tga",
	defaultDeadDecalName		= "build_des.tga",

	-- coal decal info
	coalDecalName			= "coal_01.tga",
	coalDecalScale			= 1.75,

	-- soiled land decal info
	soiledLandDecalName			= "Data:Sigma/Decals/soiled_land_decal.tga",
	soiledLandDecalScale		= 2.0,

	-- soiled land in water decal info
	soiledLandInWaterDecalName		= "Data:Sigma/Decals/soiled_water_decal.tga",
	soiledLandInWaterDecalScale		= 2.0,

	-- fade-in and fade-out times for live and dead building decals
	liveDecalFadeInTime			= 0.0,
	liveDecalFadeOutTime		= 1.0,
	deadDecalFadeInTime			= 1.0,
	deadDecalLifeTime			= 5.0,
	deadDecalFadeOutTime		= 1.0,
}

---------------------------------------------------------------------
-- Stink Cloud

StinkCloud =
{
	-- the minimum number of entities needed for the bonus to kick in
	enduranceCost			= 80.0,

	-- how long the cloud lasts, in ticks, per creature rank
	duration1				= 64.0,
	duration2				= 84.0,
	duration3				= 104.0,
	duration4				= 124.0,
	duration5				= 144.0,

	-- damage radius = radiusOffset + creature size (in meters) * radiusScale
	radiusOffset			= 17.0,
	radiusScale				= 0.5,

	--
	postDuration			= 10.0,  -- 10.0 basically means full speed returns as the cloud disappears.

	--
	protect				= 0.0,

	--
	descentSpeed			= 5.0,	-- descend 5m per second

	--
	reducedVictimSpeedTo	= 0.2,  -- 20% of full speed
}

---------------------------------------------------------------------
-- ElectricBurst

ElectricBurst =
{
	-- the minimum number of entities needed for the bonus to kick in
	enduranceCost			= 100.0,

	-- this is the duration of the attack in ticks, and modifies the base damage
	-- new damage is this * dmgPerTick (see below)
	duration				= 0.0,

	-- damage radius : C = (creature size (in meters)) O = offset, S = scale
	-- Final radius = O + C*S  (where O is the minimum radius)
	radiusOffset			= 12.0,
	radiusScale				= 1.0,

	-- damage per tick
	-- new damage is this * duration (see above)
	dmgPerTick1				= 0.0,
	dmgPerTick2				= 60.0,
	dmgPerTick3				= 102.0,
	dmgPerTick4				= 135.0,
	dmgPerTick5				= 200.0,

	-- not used!
	dmgToBuilding			= .10
}

---------------------------------------------------------------------
-- QuillBurst

QuillBurst =
{
	-- the minimum number of entities needed for the bonus to kick in
	enduranceCost			= 60.0,

	-- this is the duration of the attack in ticks, and modifies the base damage
	-- new damage is this * dmgPerTick (see below)
	duration				= 5.0,

	-- damage radius : C = (creature size (in meters)) O = offset, S = scale
	-- Final radius = O + C*S  (where O is the minimum radius)
	radiusOffset			= 7.0,
	radiusScale				= 1.25,

	-- damage per tick
	-- new damage is this * duration (see above)
	dmgPerTick1				= 5,
	dmgPerTick2				= 7,
	dmgPerTick3				= 10,
	dmgPerTick4				= 15,
	dmgPerTick5				= 20.0,

	-- not used
	dmgToBuilding			= 0
}

---------------------------------------------------------------------
-- Web Throw

WebThrow =
{
	-- the minimum number of entities needed for the bonus to kick in
	enduranceCost			= 100.0,

	-- this is the duration of the attack in ticks, and modifies the base damage
	-- new damage is this * normal damage
	duration				= 40.0,

	-- attack range : C = (creature size (in meters)) O = offset, S = scale
	-- Final radius = O + C*S  (where O is the minimum radius)
	rangeOffset				= 6.0,
	rangeScale				= 0.5,

	-- damage radius : C = (creature size (in meters)) O = offset, S = scale
	-- Final radius = O + C*S  (where O is the minimum radius)
	radiusOffset				= 8.0,
	radiusScale				= 0.5,

	-- height above the ground that the initial web effect will happen
	effectTargetHeight		= 8.0,
}


---------------------------------------------------------------------
-- Assassinate

Assassinate =
{
	-- Endurance cost
	enduranceCost		= 100.0,

	-- Attack damage done by the assassination attempt.
	-- This is a percentage of the victims maximum hitpoints and it ignores
	-- all defenses and damage reduction (valid values 0.0 to 1.0).
	damageTotalBaseSize1	= 0.45,
	damageTotalBaseSize2	= 0.45,
	damageTotalBaseSize3	= 0.45,
	damageTotalBaseSize4	= 0.45,
	damageTotalBaseSize5	= 0.45,
	damageTotalBaseSize6	= 0.45,
	damageTotalBaseSize7	= 0.45,
	damageTotalBaseSize8	= 0.45,
	damageTotalBaseSize9	= 0.45,
	damageTotalBaseSize10	= 0.45,
}

---------------------------------------------------------------------
-- Infestation

Infestation =
{
	-- Endurance cost to use this ability
	enduranceCost		= 100.0,

	-- Duration of the attack in ticks, and modifies the base damage
	-- new damage is this * normal damage
	duration				= 375.0,

	-- Damage per tick to the bulding
	dmgPerTick				= 4.00,

	-- Is infestation fully effective on building with struture defence ON?
	-- 1 = YES, infest with full damage ; 0 = NO, cannot infest
	canInfestOnStructureDefence	= 0;

}

---------------------------------------------------------------------
-- Night

Night =
{
	-- the scalar that will be applied to all animals sight radius at night
	sightModifier			= 0.5
}

---------------------------------------------------------------------
-- Gyrocopter

Gyrocopter =
{
	-- the number of seconds it take to lift a crate (animation tweak)
	airliftTime			= 2.5,

	-- the number of seconds it takes the passenger to fade out
	liftFadeTime		= 2.0,

	-- the number of seconds it take to drop a crate (animation tweak)
	dropTime			= 4.0, --was 3

	-- the number of seconds it take to during the drop animation before the creature appears
	dropFadeTime		= 4.0, --was 2

	-- the number of seconds it take before validating pending pick up passengers
	pendingAirliftTime	= 1.0,

	-- the radius around gyrocopter that will pick up incoming pending passengers
	pendingAirliftRadius	=	15.0,

	-- the number of health point the gyro is repaired per second when landed on the pad
	landedRepairPerSecond	=	3.0,

	-- the number of seconds it takes the gyrocopter to land once it is over the pad
	landingSeconds			=	3.0
}


---------------------------------------------------------------------
-- Digging

Digging =
{
	--
	speedMultiplier			= 1.2,

	-- the scalar that will be applied to all sight radius when underground
	sightMultiplier			= 0.2,

	-- the defense bonus that will be applied when the creatures is underground 0.0 to 1.0
	defenseBonus			= 0.5,

	-- the amount of time to get underground
	digDownTime			= .1,

	-- the amount of time to get out of underground
	digUpTime			= .1,

	-- the image file to use for the digging
	dugDecal			= "Data:Sigma/Decals/dug_in_ring.tga",

	-- a multiplier for endurance regeneration when underground
	-- e.g. 0.5 means half, 2.0 means double
	enduranceRegenMultiplier 	= 0.5,

	-- a multiplier for health regeneration when underground
	-- e.g. 0.5 means half, 2.0 means double
	healthRegenMultiplier		= 1.0,
}
---------------------------------------------------------------------
-- Effect

Effect =
{
	-- Location types :
	--  0 = root
	--  1 = random marker attachment
	--  2 = sparse marker attachement
	--  3 = unattached
	--  4 = over the object

	impact_fx = "COMBAT_IMPACT_COMBO",
	impact_location = "random",
	impact_count = 1,

	poison_fx =  "POISON_SPRAY_halo",
	poison_location = "random",
	poison_count = 2,

	electric_fx = "ELECTRIC_DMG_SPRAY",
	electric_location = "centre",
	electric_count = 1,

	sonic_fx = "SONIC_ATTACK_DMG_spray",
	sonic_location = "random",
	sonic_count = 1,

	stink_fx = "damage_stink",
	stink_location = "centre",
	stink_count = 1,

	plague_fx = "disease_SPRAY_halo",
	plague_location = "random",
	plague_count = 2,

	pack_fx = "pack_ring2",
	pack_location = "centre",
	pack_count = 1,

	herd_fx = "herd_ring2",
	herd_location = "centre",
	herd_count = 1,

	digging_fx = "digging_dirtmound_spray",
	digging_location = "centre",
	digging_count = 1,

	dig_stationary_fx = "null",
	dig_stationary_location = "centre",
	dig_stationary_count = 1,

	dig_moving_fx = "dig_combo",
	dig_moving_location = "centre",
	dig_moving_count = 1,

	frenzy_fx = "FRENZY_ring",
	frenzy_location = "centre",
	frenzy_count = 1,

	sonarpulse_fx = "sonar_pulse_combo",
	sonarpulse_location = "",
	sonarpulse_count = 1,

	barrier_fx = "null",
	barrier_location = "random",
	barrier_count = 1,

	gyrodmg_fx = "COMBAT_IMPACT_COMBO",
	gyrodmg_location = "random",
	gyrodmg_count = 1,

	henchmandmg_fx = "henchmen_combat_impact_combo",
	henchmandmg_location = "random",
	henchmandmg_count = 1,

	loner_fx = "lonercombo02",
	loner_location = "centre",
	loner_count = 1,

	flash_fx =  "flash_combo_head",
	flash_location = "head",
	flash_count = 2,
}


---------------------------------------------------------------------
-- AttackBonus

AttackBonus =
{
	-- multiplier of damage applied to a building when struck with barrier destroy damage
	barrierBuildingBonusMult		= 1.5,

	-- multiplier of damage applied to a bramble fence when struck with barrier destroy damage
	barrierFenceBonusMult			= 3.0,

	-- multiplier of damage applied to a building when struck with electric type damage
	electricBuildingBonusMult		= 1.0,

	-- multiplier of damage applied to a building when struck with horn negate armour type damage
	negateArmourBuildingBonusMult	= 1.0,

	-- multiplier of damage applied to a creature when struck with horn negate armour type damage
	negateArmourCreatureBonusMult	= 0.35,

	-- minimum elevation difference (in meters) between the attacker and the attackee for the terrain-height bonus to kick in
	terrainHeightBonusMinElevDiff		= 10.0,

	-- multiplier of damage applied to an entity when the attacker attacks from higher elevation
	terrainHeightBonusMult			= high_ground_range_multiplier,

	-- multiplier of damage applied to a building when struck with direct range attack
	directRangeBuildingMult			= 1.0,

	-- multiplier of damage applied to a bramble fence when struck with direct range attack
	directRangeFenceMult			= 1.0,

	-- multiplier of damage applied to a building when struck with artillery attack
	artilleryBuildingMult			= 1.0,

	-- multiplier of damage applied to a flyer when it is attacked by a non-flyer unit using a direct ranged attack
	nonFlyerToFlyerDirectRangeDamageMult	= 1.25,

	-- fraction of a flyer's defense to remove when it is attacked by a non-flyer unit using a direct ranged attack
	nonFlyerToFlyerDirectRangeDefenseMult	= 0.0,

	-- multiplier of damage applied to a flyer when it is attacked by an artillery attack
	artilleryFlyerDamageMult		= 1.0,

	-- fraction of a flyer's defense to remove when it is attacked by an artillery attack
	artilleryFlyerDefenseMult		= 0.0,
}

---------------------------------------------------------------------
-- SonicAttack

SonicAttack =
{
	-- angle of the cone - should match up with effect
	cone_angle		= 21,

	-- duration of effect in seconds
	duration		= 32,

	-- reduction per hit
	speedReduction  = 0.05,

	-- how much percent of your speed can be reduced to
	speedMinPercentage = 0.5,

	-- reduction per hit for loner unit
	speedReductionForLoner  = 0.5,

	-- how much percent of loner's speed can be reduced to
	speedMinPercentageForLoner = 0.5,

	-- damage multiplier for loner unit (1.0 is par value)
	-- It actually multiplies ALL damage taken by loners, not just sonic!
	dmgMultiplierForLoner = 1.0,
}

---------------------------------------------------------------------
-- DoctorHeal

DoctorHeal =
{
	-- number of health points per second a henchman will heal
	healthPerSecond		= 5,

	-- maximum distance between the doctor and the patient
	healRange		= 27.5,
}

---------------------------------------------------------------------
-- GuardInfo

GuardInfo =
{
	-- The Inner Guard Radius is the area around the guard target the creature will try and stay within
	creatureInnerGuardRadius		= 8.0,
	-- The Outer Guard Radius is the area around the guard target the creature will chase down enemies to
	creatureOuterGuardRadius		= 30.0,

	-- size of the henchman guard radius, measured in sight radii.  1.0 means everything the henchman sees is in the guard radius
	henchmanGuardRadius		= 1.0,
	-- the fraction of the guard radius that the Entity will randomly wander in
	randomWanderFraction	= 0.5,
}

---------------------------------------------------------------------
-- ChargeInfo

ChargeInfo =
{
	-- Charge distance, the creature must be at least min meters away to turn charge on
	-- but can be up to max meters away to enable charge
	distchargemin		= 7.0,
	distchargemax		= 30.0,
	-- multiply to melee damage to get charge damage.
	damagemultiplier	= 2.0,
	-- Charge speed is a modifier to speed whilst charging..
	speedmultiplier		= 2.0,

	-- set to 1.0 if the charger requires a clear straight line to the target before charging
	requiresStraightLine = 0.0,

	-- set to 1.0 if the charge will abort when the charger changes direction ie. if has to go around an object
	directionChangeAborts = 0.0,

	-- the creature will charge (at most) every this number of seconds
	chargeRateSeconds	  = 8.0,
}

---------------------------------------------------------------------
-- LeapInfo

LeapInfo =
{
	-- Leap distance, the creature must be at least min meters away to turn charge on
	-- but can be up to max meters away to enable charge
	distleapmin			= 7.0,
	distleapmax			= 36.0,
	-- multiply to melee damage to get charge damage.
	damagemultiplier	= 1.5,

	-- the creature will leap (at most) every this number of seconds
	leapRateSeconds		= 3.0,

	-- how long it takes a leap to reach its target once it started leaping ( in seconds )
	attackSeconds		= 0.7,

	-- how long it take a leaper to cool after a leap attack ( in seconds ); cooling means the unit can do NOTHING during this duration.
	coolSeconds			= 0.0,

	-- how many seconds of damage a leap attack does.
	--	Used to determine the damage of a leap attack by multiplying the attackers damagePerSecond
	secondsOfDamage		= 1.0
}

---------------------------------------------------------------------
-- Henchman

Henchman =
{
	-- sight radius scalar for henchmen with binoculars
	binocularsSightModifier	= 2.5,

	-- distance to search for a gathersite after current one is depleted
	gatherSearchRadius = 50.0,

	--
	yokeBonus = 1.5,

	-- land speed bonus (in km/h) for motivated henchmen
	motivatedLandSpeedBonus = 10.0,

	-- water speed bonus (in km/h) for motivated henchmen
	motivatedWaterSpeedBonus = 10.0,

	-- damage multiplier henchman do against other henchman
	henchmanDmgBonus = 1.5,

	-- damage multiplier for sonic attack on henchman
	sonicDmgMult = 0.25,

	-- damage multiplier for burst attack on henchman
	burstDmgMult = 0.35,

	-- damage multiplier for artillery attack on henchmen
	artilleryDmgMult = 0.5,

	-- damage multiplier for direct ranged attack on henchmen
	directRangedDmgMult = 0.5,

	-- how much damage extra do we do against henchman when we are within our lab bonus radius
	hencmanLabDmgMult = 2.0,

	-- how large is the lab morale bonus area
	labMoraleRad = 40.0,
}

---------------------------------------------------------------------
-- Frenzy

Frenzy =
{

	-- Endurance cost per second for the frenzy attack
	endurancePerSecond	= 8.5,

	-- Damage issued multiplier, i.e. I do normal damage times x, when frenzied
	dmgIssuedMult		= 1.5,

	-- Damage received multiplier, i.e. I take normal damage times x, when frenzied
	dmgReceivedMult		= 1.3,

	-- When a creature is fenzied it's movement rate is multiplied by x,
	moveRateMult		= 1.5,

	-- Minimum ammount of endurance needed to trigger frenzy attack
	enduranceMinimum	= 22.5,
}

---------------------------------------------------------------------
-- SonarPulse

SonarPulse =
{
	-- Endurance cost for the sonar pulse
	enduranceCost		= 100.0,

	-- Reveal Radius, the radius of the area revealed by the pulse
	revealRadius		= 100.0,

	-- Reveal timeout, how many seconds does the reveal area last for
	duration			= 40.0,
}

---------------------------------------------------------------------
-- Defile Land

SoiledLand =
{
	-- Endurance cost per tick for the soiled mode
	endurancePerTick			= 0.7,

	-- Endurance cost per tick for the soiled mode
	endurancePerTickForFlyer	= 1.2,

	-- Random drop zone size for flyer soiling land
	-- (1=1x1 cell, 2=2x2 cells, 3=3x3 cells, etc around the position of the flyer)
	dropZoneSize		= 3,

	-- Number of misses before another soiled land generated from flyers
	dropMiss			= 1,

	-- Soiled land will damage creatures within this effective radius
	findTargetRadius	= 1.5,

	-- 1 = Percentage based damage (range 0-100) ; 0 = point based damage
	dmgIsPercentage		= 1,

	-- Damage (percentage or point-based) received by the soiled victim per tick
	dmgPerTick			= 0.2,

	-- The amount to multiply the creature's speed by when soiled (normal speed = 1.0)
	speedMultiplier		= 0.3,

	-- The time (in ticks) it takes the soiled damage on animal to expire
	-- after leaving the soiled land
	durationDmg			= 50,

	-- The time (in ticks) it takes for the soiled land to expire
	-- and change back to normal land
	durationLand		= 500,

	-- Minimum amount of endurance needed to trigger soiled land
	enduranceMinimum	= 35,
}

---------------------------------------------------------------------
-- Plague

Plague =
{
	-- How much damage the plague does per second, per attacker rank
	damagePerSecond1	= 3.0,
	damagePerSecond2	= 3.0,
	damagePerSecond3	= 6.0,
	damagePerSecond4	= 9.0,
	damagePerSecond5	= 12.0,

	-- How long (seconds) does the plague last
	timeSeconds			= 15.0,

	-- How far does a plagued entity search to spread the plague
	spreadRadius		= 20.0,

	-- How much endurance does the plague use
	enduranceCost		= 100.0,

	-- plague will transfer every this number of seconds
	transferRateSeconds	= 3.0
}

---------------------------------------------------------------------
-- Player Info

Player =
{
	-- starting resources for a player (Standard)
	starting_gather_res = 500.0,
	starting_renew_res  = 100.0,

	-- starting resources for a player (QuickStart)
	quickstart_gather_res = 1500.0,
	quickstart_renew_res  = 500.0,

	-- resource amount modifier for scrap yards
	resource_mod_low	  = 0.5,
	resource_mod_high	  = 2.0,

	-- percentage of the donation that is lost during the transaction
	donationPenaltyPercentage = 0.10,
}

---------------------------------------------------------------------
-- AIPlayer Info

AIPlayer =
{
	-- electricity bonus for AI
	resRenewBonusEasy = 0.75,
	resRenewBonusStandard = 1.00,
	resRenewBonusHard = 1.20,
	resRenewBonusHardest = 1.50,

	-- coal bonus for AI
	resGatherBonusEasy = 0.75,
	resGatherBonusStandard = 1.00,
	resGatherBonusHard = 1.3,
	resGatherBonusHardest = 1.75,
}

---------------------------------------------------------------------
-- Stance Cap

Stance =
{
	-- Radius that guys will attack within when in Territorial Stance
	territoryRadius		= 25.0,
}


---------------------------------------------------------------------
-- Diplomacy

Diplomacy =
{
	-- Percentage of donation received by recipient.
	scrapDonationInc	= 100.0,
	electricityDonationInc	= 100.0,
}

---------------------------------------------------------------------
-- Animal

Animal =
{
	-- maximum distance for wandering from starting point
	movementRadius   = 50,

	-- maximum delay after stopping for wandering
	movementMaxDelay = 240,

	-- multiplier to slow down animals when they wander
	movementSpeedMult = 0.2,

	-- search radius for threat avoidance
	threatAvoidRadius = 16.0,

	-- time to wait between checking for threats
	threatAvoidCoolTicks = 4,
}

---------------------------------------------------------------------
-- Sabotage

Sabotage =
{
	-- damage per tick
	damagePerTick 	= 10.0,

	-- total amount of damage
	damage		= 2000.0,
}

---------------------------------------------------------------------
-- Flyer

Flyer =
{
	-- the height above terrain that flyers fly at.  In meters.
	flyingHeight 	= 8.0,

	-- stopped speed, meters per second
	stoppedSpeed 	= 1.5,

	-- fliers will attack every... this number of seconds
	secondsPerAttack = 3.0,

	-- number of ticks it takes a flyer to swoop down to deliver a triggered attack
	swoopDownTicks = 10.0,

	-- number of ticks it takes a flyer to follow thru after swooping down
	swoopUpTicks = 5.0,

	-- the number of seconds of damage a flyer's first attack counts as
	firstAttackSeconds = 1.0,

	-- melee damage multiplier
	meleeDmgMult = 1.0,

	-- ranged damage multiplier; we're going to use this to fake a high-ground bonus on flyers.
	rangedDmgMult = high_ground_range_multiplier,

	-- artillery damage multiplier
	artilleryDmgMult = 1.0,

	-- value to increase the min range of ranged attacks
	minRangeOffset = 0.0,

	-- damage multiplier for damage done to henchman during melee attack
	-- e.g. A value of 0.25 means henchmen receive 25% damage 
	meleeDmgMultHenchman = 0.5,
}

---------------------------------------------------------------------
-- GarrisonHeal

GarrisonHeal =
{
	-- amount of healing to apply per tick to garrisoned entities
	healPerTick	= 2.0,
}

---------------------------------------------------------------------
-- AttackGround

AttackGround =
{
	-- the error radius, so that AttackGround aim isn't perfect
	errorRadius	= 4.0,
}

---------------------------------------------------------------------
-- CreatureUpgrade

CreatureUpgrade =
{
	-- amount to add to a creature's defense
	-- (defense is represented as a percentage)
	defenseBonus = 3,

	-- amount to add to a creature's speed
	speedBonus = 5,

	-- flag that indicates whether the melee damage bonuses
	--  should be interpreted as a multiplier or a flat number
	--  1 for multipliers.  
	--  0 for flat numbers.
	meleeDamageBonusAsMult = 1,

	-- melee damage bonuses
	meleeDamageBonusRank1 = 1.2,
	meleeDamageBonusRank2 = 1.2,
	meleeDamageBonusRank3 = 1.2,
	meleeDamageBonusRank4 = 1.2,
	meleeDamageBonusRank5 = 1.2,

	-- flag that indicates whether the hitpoints bonuses
	--  should be interpreted as a multiplier or a flat number
	--  1 for multipliers.  
	--  0 for flat numbers.
	hitpointBonusAsMult = 1,

	-- hitpoints bonuses
	hitpointsBonusRank1 = 1.2,
	hitpointsBonusRank2 = 1.2,
	hitpointsBonusRank3 = 1.2,
	hitpointsBonusRank4 = 1.2,
	hitpointsBonusRank5 = 1.2,

	-- amount to add to a creature's sight radius 
	sightRadiusBonus = 10,

	-- flag that indicates whether the ranged damage bonuses
	--  should be interpreted as a multiplier or a flat number
	--  1 for multipliers.  
	--  0 for flat numbers.
	rangedDamageBonusAsMult = 1,

	-- ranged damage bonuses
	rangedDamageBonusRank1 = 1.2,
	rangedDamageBonusRank2 = 1.2,
	rangedDamageBonusRank3 = 1.2,
	rangedDamageBonusRank4 = 1.2,
	rangedDamageBonusRank5 = 1.2,

	-- amount to reduce an entity's defense by when it is 
	--  attacked by an attacker with splash-damage upgrade
	splashDmgDefenseMultiplier = 0.1,

	-- area attack radius multiplier
	areaAttackRadiusMult = 1.25,
}

---werking

Death =
{
	--	 ticks to fade out body
	deathTicks	= 3*8.0,
	--	((tick-startTick)/deathTicks)^deathFadeOutCurve ... determines the fade out curve
	deathFadeOutCurve = 2.0,
}

---------------------------------------------------------------------
-- Construction

Construction =
{
	--	 the max distance (in meters) to search in order to find a construction site to move to after the current one is finished
	constructionSiteSearchRadius = 25.0,

}

---------------------------------------------------------------------
-- FogOfWar

FogOfWar =
{
	--	 the number of seconds an attacker is reveal in the victims FoW
	attackerRevealTime = 4.0,

	--	 the number of seconds a projectile is reveal in the victims FoW
	projectileRevealTime = 10.0,

}

---------------------------------------------------------------------
-- UI

UI =
{
	-- delay between event cue henchman idle
	henchmanidle = 160,

	-- battle track
	btrackTimeTracked =  5,
	btrackTimeMin     = 30,
	btrackCountBegin  = 10,
	btrackCountEnd    =  5,
}

---------------------------------------------------------------------
-- AutoDefense

AutoDefense =
{
	radius = 4.0,
	rechargeTicks = 32,
	durationTicks = 64,
}

---------------------------------------------------------------------
-- Swamp Slowdown

SwampSlow =
{
	-- Swamp slowdown multiplier per size
	-- Valid range is  > 0.0 (terrain is impassible) 
	--                <= 1.0 (no slowdown)
	-- e.g. 0.75 is slowdown to 75% of previous speed
	slowdown1		= 1.0,
	slowdown2		= 1.0,
	slowdown3		= 1.0,
	slowdown4		= 1.0,
	slowdown5		= 1.0,
	slowdown6		= 0.9,
	slowdown7		= 0.9,
	slowdown8		= 0.9,
	slowdown9		= 0.9,
	slowdown10		= 0.85,
}

---------------------------------------------------------------------
-- Hovering

Jumping =
{
	-- Endurance cost to use this ability
	enduranceCost		= 75,

	-- Maximum jump distance 
	maxDistance			= 150.0,

	-- Speed while jumping, in meters per tick
	speed				= 0.5,

	-- Height at the top of the parabola 
	maxHeight			= 8.0,

	-- WH: I'm setting all the parameters below to zero to see if it helps hovercrash...
	-- The maximum distance of horizontal deviation from a straight line path
	-- (since the flight path is erratic)
	maxPathDeviation	= 0.0,

	-- A multiplier for the amplitude of the vertical wave-like flight behaviour 
	-- Valid range is >= 0.0 (no wave)
	--                <= 1.0 (hitting the ground)
	amplitudeMultiplier	= 0.0,

	-- Ther period of the vertical wave-like flight behaviour
	-- e.g. 8 means there it takes 8 ticks per wave
	periodTicks			= 0,
}


---------------------------------------------------------------------
-- Resource Conversion

ResourceConversion =
{
	-- A multiplier for the conversion of electricity to coal
	-- e.g. 0.25 means 50 electricity -> 12.5 coal
	elecToCoal = 0.80,

	-- A multiplier for the conversion of coal to electricity
	-- e.g. 0.25 means 50 coal -> 12.5 electricity
	coalToElec = 0.80,
}

---------------------------------------------------------------------
-- Friendly Fire

FriendlyFire =
{
	-- These are multipliers for damage reduction for friendly fire
	-- e.g. A value of 0.25 means friendly units receive 25% damage
	-- valid range is >= 0.0 (no damage received) 
	--                <= 1.0 (usual damaged received)

	-- burst attacks
	electricBurstMultiplier	= 0.7,
	quillBurstMultiplier	= 0.4,

	-- artillery attacks
	rockMultiplier			= 0.8,
	waterSpitMultiplier		= 0.8,
	chemSprayMultiplier		= 0.8,
}

---------------------------------------------------------------------
-- Chemical Spray

ChemicalSpray =
{
	-- A multiplier for damage reduction for the first strike
	-- e.g. A value of 0.25 means 25% of usual damage on the first strike
	-- valid range is >= 0.0 (no damage received on first strike)
	--                <= 1.0 (same damage as usual on first strike)
	firstStrikeMultiplier = 1.0,
}

---------------------------------------------------------------------
-- Movement

Movement =
{
	-- valid range is >= 1.0 ( minimun min speed all units can move)
	--                <= 20.0 (maximum min speed all units can move)
	minMoveSpeed = 3.0,
}

------------------------------------------------------------------------------------
-- JUNCTION END ------


function setattribute( attribute_string, value )
    setgameattribute(attribute_string,value);
    setuiattribute(attribute_string,value);
end

function Attr(attribute_string)
    if attribute_string == "null" then
        return(1);
    else
        return getgameattribute(attribute_string);
    end
end

-- Find where x falls in the array of ranges.
function Rank( x, rank_upper_bounds )
    local i = 1;
    while rank_upper_bounds[i] do
        if x <= rank_upper_bounds[i] then
            return i;
        end
        i = i + 1;
    end
    return i;
end

--power equation
function Power(ehp_in, damage_in)
	return 7;
    --return (ehp_in^0.608)*((0.22*damage_in) + 2.8);
end


dom_max = 1;
dom_val = 2;

function ShapeValueCurve(x_domain, y_domain, x0y0, x1y0, x0y1, x1y1)
    if x_domain == null_domain and y_domain == null_domain then
        return(x0y0);
    else
        --Shape factor is the amount by which the result is increased or decreased due to synergy between the x and y values.
        shape_factor = (Attr(x_domain[dom_val]) * Attr(y_domain[dom_val])) * (x1y1 - x1y0 - x0y1) /
                (x_domain[dom_max] * y_domain[dom_max]);

        return
        (
                shape_factor
                        + ((x1y0 - x0y0) / x_domain[dom_max]) * Attr(x_domain[dom_val]) --This is the contribution to the final value from the x domain value.
                        + ((x0y1 - x0y0) / y_domain[dom_max]) * Attr(y_domain[dom_val]) --This is the contribution to the final value from the y domain value.
                        + x0y0 + ((Attr(x_domain[dom_val]) * Attr(y_domain[dom_val]) * x0y0) / (x_domain[dom_max] * y_domain[dom_max])) --This offsets the value based on x0y0.
        );
    end
end

function get_range_var( limb, var )
    local str = "range"..limb.."_"..var

    if checkgameattribute(str) == 1 then
        return Attr( str )
    else
        return 0;
    end
end

function range_artillerytype( limb )
    -- if this creature has a special field it has artillery
    return get_range_var( limb, "special");
end

AttrParameters = {
    show_power      = 0,    -- Replaces Sight Radius with Power (OVERRIDES show_build_time)
    show_build_time = 0,    -- Replaces Sight Radius with Build Time
    show_ehp        = 0,    -- Replaces Health with EHP
    show_pop_space  = 0,    -- Replaces Size with Pop Space
}

--deleteStart
function combine_creature()
    --deleteEnd

    ---------------------
    ---------------------
    -- Constant Tables --
    ---------------------
    ---------------------

        -- Ranking Constants --
        --Table of maximum power thresholds, base coal costs and cost exponents for each level.
        --Table is defined here, but only used in the costs section towards the bottom of Attrcombiner.
        --{max power, base coal cost, cost_exponent}
        max_pow = 1;
        base_coal_cost = 2;
        cost_exponent = 3;

        RankTable =
        {
            {60,    60,     1},     --L1
            {120,   100,    1},     --L2
            {230,   170,    1},     --L3
            {400,   240,    0.8},   --L4
            {1000,  410,    0.75}    --L5
        };

        --Just some candy; this table is only ever used during the final elec cost scaling to associate damagetypes
        --with strings for display in combotest.
        DTStringTable =
        {
            {1, "DT_Poison"},
            {2, "DT_Horns"},
            {4, "DT_Barrier_Destroy"},
            {8, "DT_Electric"},
            {16, "DT_Sonic"},
            {4096, "DT_Ranged_Poison"},
        };

        dt_number = 1;
        dt_string = 2;

    -----------------
    -----------------
    -- Tweakables ---
    -----------------
    -----------------

        -- This section contains some handy parameters for easy tweaking.

        -- Multipliers on ranged attack costs.
        ranged_coal_cost_mult       = 1.1;
        direct_range_elec_mult      = 1.5;
        sonic_elec_mult             = 2.3;
        flying_artillery_elec_mult  = 1.5;
        range_pack_hunter_mult      = 1.15;
        artillery_targets_hit       = 1;

        -- The below multipliers apply a factor to various parameters if the unit is a flyer; they're only used for cost calculations, not power.
        damage_flyer_mult    = 1.15;
        ehp_flyer_mult      = 1.15;
        mobility_flyer_mult = 1.6;

        -- Define Limits for some variables
        max_armour 			 = 0.60;
        min_sight 			 = 20;
        max_sight 			 = 50;
        max_flyer_range_dist = 24;
        min_landspeed		 = 15;
        min_waterspeed		 = 12;
        min_airspeed		 = 16;
        min_build_time       = 16;
        flyer_min_build_time = 40;

        -- Variables that inform the shape of the mobility cost curve (ONLY cost, not mobility itself).
        mobility_divisor = 25;
        mobility_exp = 0.4;
        flyer_mobility_exp = 0.35;

        -- Sight cost.
        sight_cost_multiplier = 0.4;

        -- A rebate for defense; you only pay for this much of your defense.
        defense_cost_multiplier = 0.9;

        -- These variables are grabbed from tuning!
        herding_def_multiplier = PackBonus.basedefensemodifier; -- I know this says pack but it's herding

    -----------------
    -----------------
    -- Attributes ---
    -----------------
    -----------------

        --Mobility attributes:
        has_flying		= Attr( "is_flyer" );
        has_swim		= Attr( "is_swimmer" );
        has_land		= Attr( "is_land" );

        --Set Limits
        setattribute("armour", min(Attr("armour"), max_armour));
        setattribute("sight_radius1", min(max(Attr("sight_radius1"), min_sight), max_sight));

        if has_land == 1 then
            setattribute("speed_max", max(Attr("speed_max"), min_landspeed));
        end

        if has_swim == 1 then
            setattribute("waterspeed_max", max(Attr("waterspeed_max"), min_waterspeed));
        end

        if has_flying == 1 then
            setattribute("airspeed_max", max(Attr("airspeed_max"), min_airspeed));
        end

        ehp_flyer_factor = ((has_flying==1) and ehp_flyer_mult or 1);
        damage_flyer_factor = ((has_flying==1) and damage_flyer_mult or 1);
        mobility_flyer_factor = ((has_flying==1) and mobility_flyer_mult or 1);

        --Create derived attributes:
        setattribute("ehp", Attr( "hitpoints" )/(1-Attr( "armour" ))); --effective HP, a measure of HP and defense. Used for power, doesn't account for flyer bonus.
        setattribute("cost_ehp", Attr( "ehp" ) * ehp_flyer_factor); --EHP with flyer bonus accounted for.
        setattribute("scaling_size", Attr("size"));  --For creatures over size 9, this is their size as displayed in army builder; their "size" attribute will be set to 10 or 9 in the Size Hack section.
        setattribute("range_damage", 0);   --the maximum damage dealt by all of the creature's ranged attacks.
        setattribute("range_distance", 0);  --the ranged attack distance of the unit.
        setattribute("range_damage_distance", 0); --an equivalent melee damage based on a creature's ranged attack attributes. Uses damage and distance
        setattribute("mixed_dps", 0);       --an equivalent melee damage based on all of a creature's attack attributes.
        setattribute("mobility", 0);        --an equivalent land speed based on all of a creature's speed attributes.
        setattribute("power_rank", 1);      --the rank of the creature based on power alone.
        setattribute("effective_melee", Attr("melee_damage") * damage_flyer_factor); --Melee damage, multiplied by a flyer-specific factor, used for cost calcs.

        -- Let's calculate the extra EHP we receive from herding here, and store it as an attribute. We'll build a domain from it later.
        setattribute("herding_ehp_bonus", 0);

        if (Attr("herding") == 1) then
            extraArmour = (Attr( "armour" )*(herding_def_multiplier - 1));
            armourOverCap = max(0, (Attr( "armour" ) + extraArmour) - max_armour);
            cappedExtraArmour = extraArmour - armourOverCap;

            setattribute("herding_ehp_bonus", ( (Attr( "hitpoints" ) * ehp_flyer_factor )/(1-(Attr( "armour" ) + cappedExtraArmour))) - Attr( "cost_ehp" ) );
        end

        cost_coal       = 0;
        cost_elec       = 0;

        -- Ranged attributes:
        -- These will be set to flag if the creature has any direct or any artillery range attack.
        has_range = nil;
        has_direct = nil;
        has_sonic = nil;
        setattribute("has_artillery", 0);

        BodyPartsThatCanHaveRange = { 2, 3, 4, 5, 8 };
        -- determine type of ranged attack and set range damage to be equal to the
        -- highest damage ranged attack, with range_distance being the minimum ranged distance.

        --pairsBelow
        for index, part in BodyPartsThatCanHaveRange do
            --endPairs
            part_damage 	= get_range_var( part, "damage" );
            part_range 		= get_range_var( part, "max" ); --Attr( "range" .. part .. "_max" );
            part_dtype 		= get_range_var( part, "dmgtype" ); --Attr( "range" .. part .. "_dmgtype" );

            if ( part_damage > 0 ) then
                has_range = 1;

                --Set flying range distance limit
                if (has_flying == 1 and (part_range > max_flyer_range_dist)) then
                    part_range = max_flyer_range_dist;
                    setattribute("range"..part.."_max", max_flyer_range_dist);
                end

                --Find part with shortest range distance and set it as range_distance.
                --Creatures with multiple range limbs travel to shortest distance before attacking, so use this for future calculations.
                if ( part_range < Attr("range_distance") ) or Attr("range_distance") == 0 then
                    setattribute("range_distance", part_range);
                end

                --If creature has multiple range attacks, use maximum damage one
                if ( part_damage > Attr("range_damage") ) or Attr("range_damage") == 0 then
                    setattribute("range_damage", part_damage);
                    if ( range_artillerytype( part ) ~= 0 ) then
                        setattribute("artillery_damage", Attr("range_damage") * artillery_targets_hit);
                    end

                end

                if ( range_artillerytype( part ) == 0 ) then
                    if ( part_dtype == 16 ) then --sonic attack identified
                        has_sonic = 1;
                    else --directranged, non-sonic
                        has_direct = 1;
                    end

                else --artillery
                    setattribute("has_artillery", 1);

                end
            end
        end

        -- Helpful variable here, used later.
        setattribute("non_flyer_direct_range", 0);
        setattribute("flyer_direct_range", 0);

        if (has_sonic == 1 or has_direct == 1) then
            if (has_flying == 0) then
                setattribute("non_flyer_direct_range", 1);
            else
                setattribute("flyer_direct_range", 1);
            end
        end

        -- Finally, unusable abilities are removed
        if (has_range == 1) then
            setattribute("charge_attack", 0);
            setattribute("leap_attack", 0);

            --Give frog leap attack back when combined with itself (for creature selection)
            if (Attr("lefthalf_name") == 24134 and Attr("righthalf_name") == 24135) then
                setattribute("leap_attack", 1);
            end
        end

        if (has_flying == 1) then
            setattribute("can_dig", 0);
            setattribute("leap_attack", 0);
            setattribute("charge_attack", 0);
            setattribute("can_SRF", 0);
        end

        if (has_swim == 1 and has_land == 0) then
            setattribute("can_dig", 0);
        end

        if (Attr("pack_hunter") == 1 and Attr("herding") == 1) then
            setattribute("herding", 0);
        end

        --Not unusable, but removing so that loner units won't be charged for herding or pack hunter
        if (Attr("loner") == 1) then
            setattribute("herding", 0);
            setattribute("pack_hunter", 0);
        end

    ------------------------
    ------------------------
    -- Domain Definitions --
    ------------------------
    ------------------------

        --Set points for shapevalue curves; these represent the (more or less) maximum values of attributes,
        --which are then used as the max points for the domains they represent. They're paired with this creature's
        --specific value for the given attribute, used for actual scaling.
        dom_max = 1;
        dom_val = 2;

        power_domain            = {1000,  "Power"};
        true_size_domain        = {10,    "size"};
        scaling_size_domain     = {12,    "scaling_size"}; --For creatures over size 9, this their size as displayed in army builder.
        ehp_domain              = {3000,  "ehp"};
        cost_ehp_domain         = {3000,  "cost_ehp"};
        melee_dps_domain        = {100,   "melee_damage"}; --Pure melee damage (for power calcs)
        eff_melee_dps_domain    = {100,   "effective_melee"}; --Pure melee damage with flyer adjustment (for cost calcs)
        range_dps_domain        = {50,    "range_damage"}; --Pure range damage, no distance
        artillery_dps_domain    = {50,    "artillery_damage"}; --Artillery damage with adjustment for area of effect
        distance_domain         = {100,   "range_distance"};
        dist_dam_domain         = {100,   "range_damage_distance"}; --Ranged DPS factor incorporating distance
        mixed_dps_domain        = {100,   "mixed_dps"};  --Combined melee and damage_distance from ranged (for power calcs)
        eff_mixed_dps_domain    = {100,   "effective_mixed_dps"};  --Mixed DPS with melee flyer adjustment (for cost calcs)
        defense_domain          = {60,    "armour"};
        rank_domain             = {5,     "creature_rank"}; --NOTE: rank value changes based on ability requirements; rank_doman[2] is thus updated later.
        landspeed_domain        = {45,    "speed_max"};
        waterspeed_domain       = {47,    "waterspeed_max"};
        airspeed_domain         = {40,    "airspeed_max"};
        mobility_domain         = {60,    "mobility"};
        sight_domain            = {50,    "sight_radius1"};
        herd_boost_domain       = {700,  "herding_ehp_bonus"}; -- Extra EHP gained from herding (about 700 for musk ox blue whale!)
        null_domain             = {1,     "null"};

        --Use the null domain when you don't want to scale on either (or both) of the x and y axes.
        --To get a constant unscaled cost, set all xy numbers to 0 and then set x0y0 to equal your desired cost.

        --A NOTE ON BALANCING WITH ABILITY REF POINTS:
        --Select your domains based on what two factors you want to influence the cost of the ability.
        --For example, if you want to balance an ability based on EHP and damage, select those as your domains.
        --This will have the effect of letting you balance an ability to benefit either glassy or meaty units.
        --ADVANCED NOTE: SuiCo for "average" [Norbert] units is around 27, though it changes with level.
        --SUPERADVANCED NOTE: It's possible to rewrite this system to use gradients and shapevalue; this would allow us
        --to completely remove the domains, but it's a little less clear to understand.

    ------------------------
    ------------------------
    -- Effective Mobility --
    ------------------------
    ------------------------

        --Now we calculate effective mobility. Note: only having landspeed is the default case (speed = speed_max).
        if (has_flying == 1) then
            setattribute("mobility", Attr( "airspeed_max" )* mobility_flyer_factor);
        else
            setattribute("mobility",ShapeValueCurve(landspeed_domain, waterspeed_domain, 0, 45, 30, 65));
        end

    --------------------------
    --------------------------
    ------ Range Power -------
    --------------------------
    --------------------------

        -- Now we sort out power and costs on ranged attacks. The following code also
        -- determines how to charge multiple ranged attacks.
        -- NOTE: It is possible, using this method, that combining ranged attacks could
        -- result in unintended cost discounts for very exotic art/range combos.
        if ( has_range == 1 ) then
            -- Ask if one of the range attacks is sonic.
            if ( has_sonic == 1 ) then
                    setattribute("range_damage_distance", ShapeValueCurve(range_dps_domain, distance_domain, 0, 50, 5, 280));

            elseif ( has_direct == 1 ) then  -- Ask if one of the range attacks is not artillery.
                    setattribute("range_damage_distance", ShapeValueCurve(range_dps_domain, distance_domain, 0, 50, 0, 250));

            else -- Then we've got only an artillery attack.
                --Note that artillery dist_dam is a function of distance and damage, and then cost is a function of
                --dist_dam and distance, essentially allowing us to charge exponentially for distance.
                setattribute( "range_damage_distance", ShapeValueCurve(artillery_dps_domain, distance_domain, 0, 0, 8, 250));
            end
        end

        if (has_range == 1) then
            if has_flying == 1 then --Flyer?
                if Attr("has_artillery") == 1 then
                    setattribute("mixed_dps", ShapeValueCurve(melee_dps_domain, dist_dam_domain, 0, 30, 110, 120));
                    setattribute("effective_mixed_dps", ShapeValueCurve(eff_melee_dps_domain, dist_dam_domain, 0, 30, 110, 120));
                else
                    -- For flying direct range, ranged distance is basically useless (theoretically), so just set RDD to damage multiplied by the flyer_damage_mult.
                    setattribute( "range_damage_distance", Attr("range_damage") * damage_flyer_factor);
                    setattribute("mixed_dps", ShapeValueCurve(melee_dps_domain, dist_dam_domain, 0, 20, 90, 80));
                    setattribute("effective_mixed_dps", ShapeValueCurve(eff_melee_dps_domain, dist_dam_domain, 0, 20, 90, 80));
                end
            else --Otherwise, not a flyer.
                setattribute("mixed_dps", ShapeValueCurve(melee_dps_domain, dist_dam_domain, 0, 50, 90, 100));
                setattribute("effective_mixed_dps", Attr("mixed_dps"));
            end
        else
            setattribute("mixed_dps", Attr( "melee_damage" ));
            setattribute("effective_mixed_dps", Attr("effective_melee"));
        end

    -----------------
    -----------------
    -----ranking-----
    -----------------
    -----------------

        setattribute( "Power", Power(Attr("ehp"), Attr("mixed_dps")) );

        rank_cmp = {
            RankTable[1][max_pow],
            RankTable[2][max_pow],
            RankTable[3][max_pow],
            RankTable[4][max_pow],
            --Anything over level 4's max_pow will be level 5.
        };

        setattribute("power_rank", Rank( Attr("Power"), rank_cmp ));



    -----------------
    -----------------
    ----Size Hack----
    -----------------
    -----------------

        if Attr( "size" ) == 10 then
            setattribute("size", 9);
        elseif Attr( "size" ) > 10 then
            setattribute("size", 10);
        end

    -----------------
    -----------------
    --ability costs--
    -----------------
    -----------------

        -- Ability type constants.
        ABT_Ability = 1;
        ABT_Melee = 2;
        ABT_Range = 3;

        -- Functions that are called with the ability id parameter and return a 1 if the ability is present.
        -- These correspond to the ability type constants above.
        ABT_CheckFunctions =
        {
            Attr,
            hasmeleedmgtype,
            hasrangedmgtype,
        };

        --The following abilities and damagetypes are missing from the table as they've been made redundant:
        --{ ABT_Ability, 	"poison_bite",          3, null_domain,     null_domain,            0,      0,      0,      0   },
        --{ ABT_Ability, 	"poison_sting",         3, null_domain,     null_domain,            0,      0,      0,      0   },
        --{ ABT_Ability, 	"poison_pincers",       3, null_domain,     null_domain,            0,      0,      0,      0   },

        --{ ABT_Range, 	DT_VenomSpray, 			3, null_domain,     null_domain,            0,   	0,	    0,      0   },	--DEFUNCT, now only poison is used.
        --Also note that a special 10th column is added to the table if an ability is found; this will carry the ability's calculated cost for later.
        AbilityRefPoints =
        {
        --{ ability_type,    ability_id, minimum_level, x_domain,            y_domain,      x0y0_cost, x1y0_cost, x0y1_cost, x1y1_cost}
            { ABT_Ability, 	"flash", 			      1, rank_domain, 	    null_domain,            15,     55, 	15,     55  },
            { ABT_Ability, 	"headflashdisplay", 	  1, rank_domain, 	    null_domain,            15,     55, 	15,     55  },
            { ABT_Ability, 	"stink_attack", 		  1, rank_domain, 	    null_domain,            0,      50, 	0,      50  },
            { ABT_Ability, 	"assassinate", 			  1, rank_domain, 	    null_domain,           -10,     90,    -10,     90  },
            { ABT_Ability, 	"can_SRF", 			      2, rank_domain, 	    null_domain,            20,     45, 	20,     45  },
            { ABT_Ability, 	"AutoDefense", 	          1, rank_domain,         null_domain, 	        10,     35,     10,     35  },
            { ABT_Ability, 	"plague_attack", 	      1, rank_domain,         null_domain, 	        5,      80,     5,      80  },
            { ABT_Ability,  "sonar_pulse",            1, rank_domain,         null_domain,          10,     35,     10,     35  },
            { ABT_Ability, 	"quill_burst",            2, rank_domain,         null_domain,          10,     35,     10,     35  },
            { ABT_Ability, 	"electric_burst",     	  2, rank_domain, 	    null_domain, 	        20,     170,    20,     170 },
            { ABT_Ability, 	"web_throw",     	      3, rank_domain, 	    null_domain, 	        20,     90,     20,     90  },
            { ABT_Ability, 	"poison_touch",     	  3, rank_domain, 	    null_domain, 	       -20,     80,    -20,     80  },
            { ABT_Ability, 	"poplow",                 1, null_domain,         null_domain,          0,      0,      0,      0   },	--special
            { ABT_Ability, 	"poplowtorso",            1, null_domain,         null_domain,          0,      0,      0,      0   },	--special
            { ABT_Ability, 	"is_swimmer",             2, null_domain,         null_domain,          0,      0,      0,      0   },	--special
            { ABT_Ability,  "keen_sense",             1, null_domain,         null_domain,          10,     0,      0,      0   },
            { ABT_Ability,  "infestation",            2, mobility_domain,     cost_ehp_domain,      10,     35,     25,     40  },
            { ABT_Ability,  "end_bonus",              1, null_domain,         null_domain,          10,     0,      0,      0   },
            { ABT_Ability, 	"loner",     			  2, power_domain, 	      null_domain, 	        100,    600,    100,    600 },
            { ABT_Ability, 	"overpopulation",         1, power_domain,        null_domain,          0,      200,    0,      200 },
            { ABT_Ability,  "soiled_land",            3, mobility_domain,     rank_domain,          30,     90,     50,     110 },
            { ABT_Ability, 	"is_immune", 		      1, power_domain, 	      defense_domain, 	    10,     60,     30,     90  },
            { ABT_Ability, 	"deflection_armour",      2, cost_ehp_domain,     rank_domain,          10,     300,    10,     420 },
            { ABT_Ability, 	"herding", 			      1, herd_boost_domain,   eff_mixed_dps_domain, 5,      150,    20,     350 },
            { ABT_Ability, 	"pack_hunter", 		      1, cost_ehp_domain,     eff_mixed_dps_domain, 0,      190, 	160, 	500 },
            { ABT_Ability, 	"is_stealthy", 		      1, cost_ehp_domain,     eff_mixed_dps_domain, 0,      90,     200, 	300 },
            { ABT_Ability, 	"can_dig", 			      1, cost_ehp_domain,     eff_mixed_dps_domain, 0,      60, 	150, 	210 },
            { ABT_Ability, 	"regeneration", 	      1, cost_ehp_domain,     eff_mixed_dps_domain, 0,      110, 	110,    250 },
            { ABT_Ability, 	"frenzy_attack", 	      1, cost_ehp_domain,     eff_mixed_dps_domain, 0,      120, 	170, 	400 },
            { ABT_Ability, 	"is_flyer",     		  2, null_domain, 	      null_domain, 	        0,      0,      0,      0   },
            { ABT_Ability, 	"ranged_piercing", 	      1, cost_ehp_domain,     range_dps_domain,     0,      50, 	80, 	200 },
            { ABT_Ability, 	"leap_attack", 	          2, cost_ehp_domain,     eff_melee_dps_domain, 10,     35,     40,     60  },
            { ABT_Ability, 	"charge_attack", 	      3, cost_ehp_domain,     eff_melee_dps_domain, 20,     45,     65,     80  },
            { ABT_Ability,  "flyer_direct_range",     1, cost_ehp_domain,     range_dps_domain,     0,      95,     80,     250 },
            { ABT_Ability,  "non_flyer_direct_range", 1, null_domain,         null_domain,          0,      0,      0,      0   },  --special case, but it really shouldn't be... TODO: fix this
            { ABT_Ability,  "has_artillery",          1, dist_dam_domain,     distance_domain,      0,      20,     75,     225 },

            { ABT_Range, 	DT_Electric, 		      2, null_domain,         null_domain,          0,      0, 	    0,	    0   },	--special
            { ABT_Range, 	DT_Sonic, 			      2, null_domain,         null_domain,          0,   	0,	    0,      0   },	--special
            { ABT_Range, 	DT_Poison,	              3, rank_domain,         null_domain,         -20,     40,    -20,     40  },	--Cost for chemical artillery (which has the melee poison damagetype).

            { ABT_Melee,    DT_Poison,                3, rank_domain,         null_domain,         -20,     80,    -20,     80  },
            { ABT_Melee, 	DT_HornNegateFull, 	      2, rank_domain,         null_domain,          10,     50,     10, 	50  },
            { ABT_Melee, 	DT_BarrierDestroy,        1, ehp_domain,          melee_dps_domain,     5,      60,     140,	400 },
            { ABT_Melee, 	DT_HornNegateArmour,      1, cost_ehp_domain,     eff_melee_dps_domain,-10,     100, 	180, 	530 }
        };

        --The variables below describe what each column of the AbilityRefPoints table represents.
        --"rc" is "Reference Column".
        rc_ability_type = 1;
        rc_id = 2;
        rc_min_rank = 3;
        rc_x_dom = 4;
        rc_y_dom = 5;
        rc_x0y0_cost = 6;
        rc_x1y0_cost = 7;
        rc_x0y1_cost = 8;
        rc_x1y1_cost = 9;
        ability_calculated_cost = 10;

        --First find min rank for all abilities.
        ability_rank = Attr("power_rank");

        --pairsStart
        for n, ab in AbilityRefPoints do
            --pairsEnd
            -- If we have this ability...
            if ABT_CheckFunctions[ab[rc_ability_type]]( ab[rc_id] ) == 1 then

                -- check for min rank.
                ability_rank = max(ability_rank, ab[rc_min_rank]);
            end
        end

        setattribute( "creature_rank",  max( Attr("power_rank"), ability_rank ) );

        --Now we add ability costs.
        charged_for_poison = 0;
        --pairsStart
        for n, ab in AbilityRefPoints do
            --pairsEnd

            ab[ability_calculated_cost] = 0;

            --If we have this ability...
            if ABT_CheckFunctions[ab[rc_ability_type]]( ab[rc_id] ) == 1 then

                ability_cost = ShapeValueCurve(ab[rc_x_dom], ab[rc_y_dom],
                        ab[rc_x0y0_cost], ab[rc_x1y0_cost], ab[rc_x0y1_cost], ab[rc_x1y1_cost]);

                --If we have either melee or ranged poison DT, don't charge for poison touch.
                if (ab[rc_id] == "poison_touch" or ab[rc_id] == DT_Poison) then
                    if charged_for_poison == 0 then
                        charged_for_poison = 1;
                        ab[ability_calculated_cost] = ability_cost;
                    end

                -- Give pack hunter ranged units a tax (range units get more benefit from pack due to their ability to stack)
                elseif (ab[rc_id] == "pack_hunter" and has_range == 1) then
                    ab[ability_calculated_cost] = ability_cost * range_pack_hunter_mult;

                --Poplow isn't charged on a curve.
                elseif (ab[rc_id] == "poplow" or ab[rc_id] == "poplowtorso") and ((Attr( "scaling_size" ) - Attr("creature_rank"))/2) > 1.0 then
                    poplow_benefit = floor(min(Attr("creature_rank") + 1, (Attr( "scaling_size" ) - Attr("creature_rank"))) / 2);
                    ab[ability_calculated_cost] = poplow_benefit * 10;

                elseif (ab[rc_id] == "has_artillery") and (has_flying == 1) then
                    ab[ability_calculated_cost] = ability_cost * flying_artillery_elec_mult;

                elseif (ab[rc_id] == "non_flyer_direct_range") then -- This should definitely just be on a curve!
                    if (has_sonic == 1) then
                        ab[ability_calculated_cost] = Attr("range_damage_distance") * sonic_elec_mult;
                    else
                        ab[ability_calculated_cost] = Attr("range_damage_distance") * direct_range_elec_mult;
                    end

                --All other abilities:
                else
                    ab[ability_calculated_cost] = ability_cost;
                end
            end
        end

    -----------------
    -----------------
    ----cost mods----
    -----------------
    -----------------

        --cost_power equation (power with a 10% rebate for defense, used for calculating costs).
        --Note that below we employ the ehp_flyer_factor directly onto hitpoints; multiplying hp by a factor and then calculating ehp
        --has the exact same result as multiplying ehp by that same factor.
        defense_rebate_ehp = ( Attr( "hitpoints" ) * ehp_flyer_factor ) / ( 1-(Attr( "armour" ) * defense_cost_multiplier) );
        cost_power = Power(defense_rebate_ehp, Attr( "effective_mixed_dps" ));

        ----------------------------
        --mobility cost multiplier--
        ----------------------------

        mobility_cost = ((Attr("mobility")/mobility_divisor)^( (has_flying==1) and flyer_mobility_exp or mobility_exp));

        --------------
        --sight cost--
        --------------

        sightCost = Attr( "sight_radius1" )*sight_cost_multiplier;

        -- FINAL COST CALCULATION AND SCALING.
        --Intra-level modulator changes the costs of spam units relative to power units.
        intra_level_modulator = (cost_power*1.3/RankTable[Attr("creature_rank")][max_pow])^(RankTable[Attr("creature_rank")][cost_exponent]);
        setattribute("final_cost_scaler", intra_level_modulator);

        --If the unit has direct range or sonic and DOES NOT fly, add a multiplier to coal cost.
        cost_coal = RankTable[Attr("creature_rank")][base_coal_cost] * mobility_cost * 1.1 * ((Attr("non_flyer_direct_range") == 1) and ranged_coal_cost_mult or 1) * Attr("final_cost_scaler");

        total_abilities_cost = 0;

        --pairsStart
        -- This is where we scale all our previously-calculated ability costs.
        for n, ab in AbilityRefPoints do
        --pairsEnd
            if ab[ability_calculated_cost] > 0.0 then

                ability_name = ab[rc_id];

                --First, if we're dealing with a dt, we need to grab the string associated with it.
                --The first check makes sure ability_name is not a string.
                if ab[rc_ability_type] == ABT_Melee or ab[rc_ability_type] == ABT_Range then
                    for i, string_pair in DTStringTable do
                        if string_pair[dt_number] == ability_name then
                            ability_name = string_pair[dt_string];
                        end
                    end
                end

                --Let's not scale assassinate; logically I don't think it should be any cheaper on spam than it is on power.
                if ability_name == "assassinate" then
                    total_abilities_cost = total_abilities_cost + ab[ability_calculated_cost];
                else
                    scaled_ability_cost = ab[ability_calculated_cost] * Attr("final_cost_scaler");
                    setattribute("===--------------->>"..ability_name.."_cost", scaled_ability_cost);

                    total_abilities_cost = total_abilities_cost + scaled_ability_cost;
                end
            end
        end

        cost_elec = total_abilities_cost;

    -----------------
    -----------------
    -----outputs-----
    -----------------
    -----------------

        --Popspace calc. NOTE: Pop space is capped at creature level + 1.
        if (Attr("poplow") == 1) or (Attr("poplowtorso") == 1) then
            Pop = min(ceil(max(1, Attr( "scaling_size" ) - Attr("creature_rank")) / 2), Attr("creature_rank") + 1);
        else
            Pop = min(max(1, Attr( "scaling_size" ) - Attr("creature_rank")), Attr("creature_rank") + 1);
        end

        --Buildtime calc
        --Overpop buildtime multiplier
        if (Attr("overpopulation") == 1) then
            build_time_multiplier = 0.5;
        else
            build_time_multiplier = 1.0;
        end

        -- Build time equation
        build_time = (30 * Attr("creature_rank"))*((Attr("Power")*1.2/RankTable[Attr("creature_rank")][max_pow])^1.2)*build_time_multiplier;

        overpop_adjusted_min_build = ((has_flying==1) and flyer_min_build_time or min_build_time);

        --set minimum build time
        if Attr("overpopulation") == 1 then
            overpop_adjusted_min_build = overpop_adjusted_min_build / 2;
        end

        build_time = max(build_time, overpop_adjusted_min_build);
        setattribute("constructionticks", build_time);

        --Final Output
        setattribute( "cost", cost_coal );
        setattribute( "costRenew", cost_elec );
        setattribute( "popsize", Pop );

    --deleteStart

    -----------------
    -----------------
    -------ui--------
    -----------------
    -----------------

        --deleteStart

        -- Attribute data column ids.
        AT_Name = 1;
        AT_ZeroOK = 2;
        AT_Min = 3;
        AT_Max = 4;
        AT_RankList = 5;
        AT_UIName = 6;
        AT_UIScale = 7;

        -- Game attribute bound and rank data.
        AttributeData =
        {
            -- { attribute name, zero ok, min, max, rank list, ui attribute name, game->ui scale factor }

            { "hitpoints",  nil, 1, nil, {0.0, 224.0, 349.0, 574.0}, "health", 1 },
            { "armour", 1, 0, nil, {0.0, 0.15, 0.30, 0.45}, "armour", 100 },
            { "speed_max", 1, min_landspeed, nil, {15.0, 21.0, 26.0, 31.0}, "landspeed", 1 },
            { "waterspeed_max", 1, min_waterspeed, nil, {12.0, 20.0, 25.0, 30.0}, "waterspeed", 1 },
            { "airspeed_max", 1, min_airspeed, nil, {16.0, 20.0, 24.0, 28.0}, "airspeed", 1 },
            { "sight_radius1", nil, nil, nil, {20.0, 30.0, 35.0, 45.0}, "sightradius", 1 },
            { "scaling_size", nil, 1, nil, {0, 3, 6, 9}, "size", 1},

            { "melee_damage", 1, 0, nil, {1.0, 10.0, 17.0, 26.0}, "damage",  1 },
            { "range2_max", 1, 0, nil, {1.0, 8.0, 14.0, 21.0}, "range2_max", 1 },
            { "range4_max", 1, 0, nil, {1.0, 8.0, 14.0, 21.0}, "range4_max", 1 },
            { "range5_max", 1, 0, nil, {1.0, 8.0, 14.0, 21.0}, "range5_max", 1 },
            { "range8_max", 1, 0, nil, {1.0, 8.0, 14.0, 21.0}, "range8_max", 1 },
            { "range3_max", 1, 0, nil, {1.0, 8.0, 14.0, 21.0}, "range3_max", 1 },
        };

        -- Accesses the "show_power" or "show_build_time" variable from attr_parameters.
        if (AttrParameters.show_power == 1) then
            AttributeData[6] = { "Power", nil, nil, nil, {rank2pow, rank3pow, rank4pow, rank5pow}, "sightradius", 1 }
        elseif (AttrParameters.show_build_time == 1) then
            AttributeData[6] = { "constructionticks", nil, nil, nil, {1, 40, 80, 120}, "sightradius", 1 }
        end

        -- Accesses "show_ehp" from attr_parameters.
        if (AttrParameters.show_ehp == 1) then
            AttributeData[1] = { "ehp", nil, 1, nil, {1, 200, 400, 600}, "health", 1}
        end

        -- Accesses "show_pop_space" from attr_parameters.
        if (AttrParameters.show_pop_space == 1) then
            AttributeData[7] = { "popsize", nil, 1, nil, {0, 2, 4, 6}, "size", 1}
        end

        -- Apply UI boundaries and UI rank attributes.

        for k, at in AttributeData do

            local attribute = at[AT_Name];
            local val = 0;
            local rating = 1;

            if checkgameattribute( attribute ) == 1 then

                val = Attr( attribute );

                -- Ranking.
                if at[AT_RankList] then
                    rating = Rank( val, at[AT_RankList] );
                end
            end

            if at[AT_UIName] then
                -- Add the rating to the creature's variable list -- rating is in the range [0-4].
                setattribute( at[AT_UIName].. "_rating", rating - 1 );
                -- Add the display version to the creature's variable list.
                setattribute( at[AT_UIName] .. "_val", val * at[AT_UIScale] );
            end

        end

        --Sets UI for damage icon and values of ranged attacks.
        --pairsBelow
        for index, part in BodyPartsThatCanHaveRange do
            --endPairs
            if checkgameattribute( "range"..part.."_damage" ) == 1 then
                val = Attr( "range"..part.."_damage" );
                rating = Rank( val, {-1.0,12.0,20.0,26.0} );

                --It seems like for UI purposes, a creature is only considered "ranged" if it has ranged attributes
                --on part 2 or part 4. So for our stocks with non-2-or-4 range parts, we have to first create fake
                --ranged attributes on part 2; then we can create our attributes for the part we actually want,
                --and it all seems to work fine.
                if (part ~= 4 and part ~= 2) then
                    setattribute( "range2_damage_rating", rating - 1 );
                    setattribute( "range2_damage_val", val );
                end

                setattribute( "range"..part.."_damage_rating", rating - 1 );
                setattribute( "range"..part.."_damage_val", val );
            end
        end

        --deleteEnd
::done::
end
    --deleteEnd