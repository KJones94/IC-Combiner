--Tellurian Attrcombiner (5/1/2019)

--Changelog (since Tel 2.4 release):
--Power from ranged distance slightly increased.
--Colony/Conglomerate cost eliminated for units that only take up 1 pop space.
--Immunity cost reduced.
--Sonic minimum level changed to L2. Sonic base cost reduced.
--Power equation modified to slightly reduce power of low damage units.
--Build time for low power units significantly reduced.

--Let's go!

--function combine_creature()

-----------------
-----------------
--vars and shit--
-----------------
-----------------

-----------------
--uncapped vars--
-----------------

speed_max 	= getgameattribute( "speed_max" );
airspeed_max 	= getgameattribute( "airspeed_max" );
waterspeed_max 	= getgameattribute( "waterspeed_max" );
speed 		= max( speed_max, max( airspeed_max, waterspeed_max ) );

size 		= getgameattribute( "size" );
hitpoints 	= getgameattribute( "hitpoints" );
damagem 	= getgameattribute( "melee_damage" );

range4_max 	= getgameattribute("range4_max");
range2_max	= getgameattribute("range2_max");
range5_max 	= getgameattribute("range5_max");
range8_max 	= getgameattribute("range8_max");
range3_max 	= getgameattribute("range3_max");
range_max 	= max(range4_max, max(range2_max, max(range5_max, max(range8_max, range3_max))));

range2_damage 	= getgameattribute( "range2_damage" );
range3_damage 	= getgameattribute( "range3_damage" );
range4_damage 	= getgameattribute( "range4_damage" );
range5_damage 	= getgameattribute( "range5_damage" );
range8_damage 	= getgameattribute( "range8_damage" );

RangeCostMult 	= 1.0;

damager = max (range2_damage, max (range4_damage, max(range5_damage, max(range3_damage, range8_damage))));

if (range_max > 0) then
	damager = damager*(1+(range_max/28));
end

if damager > damagem then
	rangedrmod = 0.92;
	rangedmmod = 0.55;
	else
	rangedrmod = 0.55;
	rangedmmod = 0.92;
end

if damager > 0 then
	damage = (damagem * rangedmmod) + (damager * rangedrmod);
	else
	damage = damagem;
end

-----------------
---capped vars---
-----------------

--Minimum SR of 20 and max of 50
sight_radius = max(getgameattribute("sight_radius1"), 20)
setgameattribute("sight_radius1", min(sight_radius, 50))

maxArmour = 0.60;
armour = min(getgameattribute( "armour" ), maxArmour);
setgameattribute("armour", armour);

--Tie down flier range
if ( getgameattribute("is_flyer") == 1 ) then		
	if (range2_max > 24) then
		range2_max = 24
		setgameattribute("range2_max",range2_max);
	end
	if (range4_max > 24) then
		range4_max = 24
		setgameattribute("range4_max",range4_max);
	end	
	if (range5_max > 24) then
		range5_max = 24
		setgameattribute("range5_max",range5_max);
	end
	if (range8_max > 24) then
		range8_max = 24
		setgameattribute("range8_max",range8_max);
	end
	if (range3_max > 24) then
		range3_max = 24
		setgameattribute("range3_max",range3_max);
	end
	range_max = min(range_max, 24);	
end

-----------------
-----------------
----functions----
-----------------
-----------------

--Elec, coal, rank, minrank bein set up here
CostRenew = 0;
CostGather = 0;
CreatureRank = 0;
MinRank = 0;

if 	speed_max == 0 and
	waterspeed_max == 0 and
	airspeed_max == 0 then
		speed_max = 8.0;
		setgameattribute("speed_max", speed_max);
end

--makes things easier innit
function setattribute( attribute_string, value )
	setgameattribute(attribute_string,value);
	setuiattribute(attribute_string,value);      
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
function Power(d, h, a)
    return (((h/(1-a))^(0.608))*((0.22*d) + 2.8));
end

-----------------
-----------------
-----ranking-----
-----------------
-----------------

rank2pow = 60;
rank3pow = 120;
rank4pow = 230;
rank5pow = 400;

power = Power(damage, hitpoints, armour);

speedCost = speed*0.75;

rank_cmp = { rank2pow, rank3pow, rank4pow, rank5pow };

CreatureRank = Rank( power, rank_cmp );

-----------------
-----------------
--ability  cost--
-----------------
-----------------

-- Unusable abilities removed
if (range2_damage>0) or (range4_damage>0) or (range5_damage>0) or (range8_damage>0) then
	setgameattribute("charge_attack", 0);
	setgameattribute("leap_attack", 0);	
end

if (getgameattribute("is_flyer") == 1) then
	setgameattribute("can_dig", 0);
	setgameattribute("leap_attack", 0);
	setgameattribute("charge_attack", 0);
	setgameattribute("can_SRF", 0);
end

if (getgameattribute("is_swimmer") == 1 and getgameattribute("is_land") == 0) then
	setgameattribute("can_dig", 0);
end

-- Ability type constants.
ABT_Ability = 1;
ABT_Melee = 2;
ABT_Range = 3;

-- Functions that are called with the ability id parameter and return a 1 if the ability is present.
-- These correspond to the ability type constants above.
ABT_CheckFunctions =
{
	getgameattribute,
	hasmeleedmgtype,
	hasrangedmgtype,
};

AB_AbilityType = 1;
AB_Id = 2;
AB_MinRank = 3;
AB_CostRenew = 4;
AB_CostGather = 5;
AB_CostRenewIncrement = 6;
AB_CostRenewIncrementStartRank = 7;

AbilityData =
{
	--All special case abilities are charged after power discount is applied, this is presently only to abilities that already have
	--power as a factor in their cost and a number of special abilities either because the ability is potentially problematic with the power discount
	--Note that the special case ranks are still controlled in this array

	--{ ability_type, ability_id, minrank, costrenew, costgather, costrenew plus perrank, Rank that the increase starts at }

	{ ABT_Ability, 	"is_immune", 		0, 	5, 	0, 	2.5 },
	{ ABT_Ability, 	"keen_sense", 		0, 	10, 	0, 	0 },
	{ ABT_Ability, 	"can_dig", 		0, 	10, 	0, 	10 },
	{ ABT_Ability, 	"sonar_pulse", 		0, 	15, 	0, 	5 },
	{ ABT_Ability, 	"is_stealthy", 		0, 	20, 	0, 	10 },
	{ ABT_Ability, 	"stink_attack", 	0, 	50, 	0, 	5 },
	{ ABT_Ability, 	"stink",		0, 	50, 	0, 	5 },
	{ ABT_Ability, 	"flash", 		0, 	0, 	0, 	0 },
	{ ABT_Ability, 	"end_bonus", 		0, 	10, 	0,	0 },
 	{ ABT_Ability, 	"speed_boost", 		0, 	0, 	0, 	0 },
 	{ ABT_Ability, 	"overpopulation", 	2, 	10, 	0, 	5 },
	{ ABT_Ability, 	"poplow", 		1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"poplowtorso", 		1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"herding", 		1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"pack_hunter", 		1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"regeneration", 	1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"frenzy_attack", 	1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"plague_attack", 	1, 	50, 	0, 	5 },
	{ ABT_Ability, 	"AutoDefense", 		1, 	15, 	0, 	5 },
	{ ABT_Ability, 	"assassinate", 		1, 	10, 	10, 	20 },
	{ ABT_Ability, 	"can_SRF", 		1, 	15, 	0, 	10 },
	{ ABT_Ability, 	"quill_burst", 		2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"leap_attack", 		2, 	15, 	0, 	5 },
	{ ABT_Ability, 	"is_swimmer", 		2, 	0, 	0,	0 },
	{ ABT_Ability, 	"deflection_armour", 	2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"infestation", 		2, 	30, 	0, 	0 },
	{ ABT_Ability, 	"charge_attack", 	3, 	15, 	0, 	5 },
	{ ABT_Ability, 	"is_flyer", 		3, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"electric_burst", 	2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"poison_touch", 	3, 	40, 	0, 	20 },
	{ ABT_Ability, 	"web_throw", 		3, 	0, 	0, 	0 },	--special
 	{ ABT_Ability, 	"poison_bite", 		3,  	40, 	0, 	20 },
 	{ ABT_Ability, 	"poison_sting", 	3,  	40, 	0, 	20 },
	{ ABT_Ability, 	"poison_pincers", 	3, 	40, 	0, 	20 },
	{ ABT_Ability, 	"loner", 		2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"soiled_land", 		3, 	50, 	0, 	0 },

	{ ABT_Range, 	DT_Electric, 		2, 	0, 	0,	0 },
	{ ABT_Range, 	DT_Poison, 		3, 	0, 	0,	0 },
	{ ABT_Range, 	DT_Sonic, 		2, 	0, 	0,	0 },
	{ ABT_Range, 	DT_VenomSpray, 		3, 	0, 	0,	0 },

	{ ABT_Melee, 	DT_BarrierDestroy, 	0, 	0, 	0, 	0},     --special
	{ ABT_Melee, 	DT_HornNegateFull, 	2, 	30, 	0, 	10 },
	{ ABT_Melee, 	DT_HornNegateArmour, 	3, 	0, 	0, 	0 },	--special
	{ ABT_Melee, 	DT_Poison, 		3, 	0, 	0, 	0 },
};

-- Total the costs and find min rank for all abilities.
for n, ab in pairs(AbilityData) do
	-- If we have this ability...
	if ABT_CheckFunctions[ab[AB_AbilityType]]( ab[AB_Id] ) == 1 then
		-- add on the costs.
		if ab[AB_CostRenew] then
			CostRenew = CostRenew + ab[AB_CostRenew];
		end
		if ab[AB_CostGather] then
			CostGather = CostGather + ab[AB_CostGather];
		end
		-- check for min rank.
		CreatureRank = max( CreatureRank, ab[AB_MinRank] );
	end
end

--Ranged Costs and MinRank
-- used to determine whether the range type is splash damage

function get_range_var( limb, var )
	local str = "range"..limb.."_"..var

	if checkgameattribute(str) == 1 then
		return getgameattribute( str )
	end
	return 0;
end

function range_artillerytype( limb )
	-- if this creature has a special field it has artillery
	return get_range_var( limb, "special");
	end

-- These will be set to flag if the creature has any direct or any artillery range attack.
has_direct = nil;
has_artillery = nil;

--Ranged unit minimum rank
if damager > 0 then
	CreatureRank = max(CreatureRank, 2);
end

BodyPartsThatCanHaveRange = { 2, 3, 4, 5, 8 };

for index, part in pairs(BodyPartsThatCanHaveRange) do
	part_damage = getgameattribute( "range" .. part .. "_damage" );
	if ( part_damage > 0 ) then
		-- if not artillery range
		if ( range_artillerytype( part ) == 0 ) then
			has_direct = 1;

			if ( hasrangedmgtype(DT_Sonic) == 1 ) then
				CostRenew = CostRenew + (damager * 3);
			else
				CostRenew = CostRenew + (damager * 1.5);
			end
		else
			has_artillery = 1;
			CostRenew = CostRenew + (damager * 1.7);
		end
	end
end

-- Total the costs and find min rank for all abilities.
 for n, ab in pairs(AbilityData) do
	-- If we have this ability...
	if ABT_CheckFunctions[ab[AB_AbilityType]]( ab[AB_Id] ) == 1 then
		
		-- add on the costs.
		if ab[AB_CostRenewIncrement] then
			CostRenew = CostRenew + ab[AB_CostRenewIncrement] * (CreatureRank - 1);
		end
		
	end
end

--Sorting out costs and ranks

if (CreatureRank == 1) then
	max_power = rank2pow;
	CostGather = 50; 
	elseif (CreatureRank == 2) then
		max_power = rank3pow;
		CostGather = 90;
		elseif (CreatureRank == 3) then
		    max_power = rank4pow;
		    CostGather = 150;
		    elseif (CreatureRank == 4) then
		        max_power = rank5pow;
				CostGather = 250;
		        else
		    	max_power = 1000;
			    CostGather = 425;
end

-----------------
-----------------
----cost mods----
-----------------
-----------------

-- Dedicated swimmer cost modifiers
artillerypureswimmercostmodifier = 0.85;
directpureswimmercostmodifier = 0.75;
meleepureswimmercostmodifier = 0.4;

-- Reduce cost if unit is a dedicated swimmer
if 	speed_max == 0 and
	waterspeed_max> 0 and
	airspeed_max == 0 then
	
	if has_direct == 1 then 
		CostGather = CostGather * directpureswimmercostmodifier;
		CostRenew = CostRenew * directpureswimmercostmodifier;
	end
	if has_artillery == 1 then
		CostGather = CostGather * artillerypureswimmercostmodifier;
		CostRenew = CostRenew * artillerypureswimmercostmodifier;
	end
	if has_direct == nil and has_artillery == nil then
		CostGather = CostGather * meleepureswimmercostmodifier;
		CostRenew = CostRenew * meleepureswimmercostmodifier;
	end

	if has_direct == 1 and has_artillery == 1 then
		CostGather = CostGather/directpureswimmercostmodifier;
		CostRenew = CostRenew/directpureswimmercostmodifier;
	end
end

--more ability costs

-- The 0.65 here is equal to 1 - the horns multiplier from tuning.lua
if hasmeleedmgtype(DT_HornNegateArmour) == 1 then
	CostRenew = CostRenew + ((( ((1-(maxArmour*0.65))/(1-maxArmour)) *damagem)-damagem)*(CreatureRank*2));
end

-- The 1.5 here is the barrier destroy multiplier for damage to structures from tuning.lua
if hasmeleedmgtype(DT_BarrierDestroy) == 1 then
	CostRenew = CostRenew + (((damage*1.5) - damage)*2.5);
end

if getgameattribute("frenzy_attack") == 1 then
	CostRenew = CostRenew + (damage*3);
end

if (getgameattribute("poplow") == 1) and (power/150 > 1) then
	CostRenew = CostRenew + (power*0.1);
end

if (getgameattribute("poplowtorso") == 1) and (power/150 > 1) then
	CostRenew = CostRenew + (power*0.1);
end

if (getgameattribute("herding") == 1) and (getgameattribute("pack_hunter") == 0) then
	herdarmour = min(armour*1.3, maxArmour);
	CostRenew = CostRenew + 10 + (((hitpoints/(1-herdarmour)) - (hitpoints/(1-armour)))/4);
end

if getgameattribute("pack_hunter") == 1 then
	CostRenew = CostRenew + (((damage*1.3) - damage)*10);
end

if getgameattribute("headflashdisplay") == 1 or getgameattribute("flash") == 1 then
	CostRenew = CostRenew + (50+(10*CreatureRank));
end

if getgameattribute("regeneration") == 1 then
	CostRenew = CostRenew+(CreatureRank*40*armour);
end

if getgameattribute("deflection_armour") == 1 then
	CostRenew = CostRenew+((hitpoints/(1-armour))/5);
end

-- Build time

build_time = (30 * CreatureRank)*((power*1.2/max_power)^1.2);
CostGather = (CostGather + speedCost)*((power*1.3/max_power)^0.8)*RangeCostMult*1.1+(2/CreatureRank)*1.25;
CostRenew = CostRenew*((power*1.3/max_power)^0.8);

if getgameattribute("overpopulation") == 1 then
	build_time = max(build_time * 0.5, 8);
else
	build_time = max(build_time, 16);
end

--no discount ability costs
if getgameattribute("quill_burst") == 1 then
	CostRenew = CostRenew + 10 + (CreatureRank * 5);
end

if getgameattribute("electric_burst") == 1 then
	CostRenew = CostRenew + 20 + (CreatureRank * 30);
end
                            
if getgameattribute("web_throw") == 1 then
   CostRenew = CostRenew + 30 + (CreatureRank * 15);
end

if getgameattribute("is_flyer") == 1 then
	CostGather = CostGather + 30;
	CostRenew = CostRenew + 40;
end

if getgameattribute("loner") == 1 then
	CostRenew = CostRenew + 100 + (power/2);
end

-----------------
-----------------
----Size Hack----
-----------------
-----------------

--Size fixes
if size == 10 then
	size = 9
	setgameattribute("size", size);
--	truesize = 10;
--	falsesize = 9;

end
if size == 11 then
	size = 10
	setgameattribute("size", size);
--	truesize = 11;
--	falsesize = 10;

end
if size >= 12 then
	size = 10
	setgameattribute("size", size);
--	truesize = 12;
--	falsesize = 10;

end

-----------------
-----------------
-----outputs-----
-----------------
-----------------

--Popspace calc
if (getgameattribute("poplow") == 1) or (getgameattribute("poplowtorso") == 1) then
	Pop = power/300;
else
	Pop = power/150;
end


setattribute( "buildtime", 10 );

setgameattribute("constructionticks", build_time);

--Final Output
setattribute( "creature_rank", CreatureRank );
setattribute( "costrenew", CostRenew );
setattribute( "cost", CostGather );
setattribute( "popsize", Pop )

--end