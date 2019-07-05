--Tellurian Attrcombiner (5/1/2019)

--Changelog (since Tel 2.4 release):
--Power from ranged distance slightly increased.
--Colony/Conglomerate cost eliminated for units that only take up 1 pop space.
--Immunity cost reduced.
--Sonic minimum level changed to L2. Sonic base cost reduced.
--Power equation modified to slightly reduce power of low damage units.
--Build time for low power units significantly reduced.

--Let's go!

-- commented out:
-- function combine_creature()

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

range_max1 	= getgameattribute("range4_max");
range_max2	= getgameattribute("range2_max");
range_max3 	= getgameattribute("range5_max");
range_max4 	= getgameattribute("range8_max");
range_max5 	= getgameattribute("range3_max");
range_max 	= max(range_max1, max(range_max2, max(range_max3, max(range_max4, range_max5))));

damage2 	= getgameattribute( "range2_damage" );
damage3 	= getgameattribute( "range3_damage" );
damage4 	= getgameattribute( "range4_damage" );
damage5 	= getgameattribute( "range5_damage" );
damage8 	= getgameattribute( "range8_damage" );

RangeCostMult 	= 1.0;

damager = max (damage2, max (damage4, max(damage5, max(damage3, damage8))));

if (range_max > 0) then
	damager = damager*(1+(range_max/28));
end

if damager > damagem then
	rangedrmod = 0.8;
	rangedmmod = 0.2;
	else
	rangedrmod = 0.2;
	rangedmmod = 0.8;
end

if damager > 0 then
	damage = damagem * rangedmmod + damager * rangedrmod;
	else
	damage = damagem;
end

--Makes anglerflash free so ya don't get double charged!! wahey!!
if (getgameattribute("headflashdisplay") == 1) then
	flCost = 0.0;
	else
	flCost = 1.0;                          
end

--Overpop buildtime multiplier
if (getgameattribute("overpopulation") == 1) then
	popMult = 0.5;
	else
	popMult = 1.0;
end

-----------------
---capped vars---
-----------------

--Minimum SR of 20 and max of 50
setgameattribute("sight_radius1", max(getgameattribute("sight_radius1"), 20));

if getgameattribute("sight_radius1") > 50 then
    setgameattribute("sight_radius1", 50);
end

maxArmour = 0.60;

sight_radius1 = getgameattribute("sight_radius1" );
armour = min(getgameattribute( "armour" ), maxArmour);

setgameattribute("armour", armour);

--Tie down flier range
if ( getgameattribute("is_flyer") == 1 ) then		
	if (getgameattribute("range2_max") > 24) then
		setgameattribute("range2_max",24);
	end
	if (getgameattribute("range4_max") > 24) then
		setgameattribute("range4_max",24);
	end	
	if (getgameattribute("range5_max") > 24) then
		setgameattribute("range5_max",24);
	end
	if (getgameattribute("range8_max") > 24) then
		setgameattribute("range8_max",24);
	end
	if (getgameattribute("range3_max") > 24) then
		setgameattribute("range3_max",24);
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

if 	getgameattribute("speed_max") == 0 and
	getgameattribute("waterspeed_max") == 0 and
	getgameattribute("airspeed_max") == 0 then
		setgameattribute("speed_max",8.0)
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
rank4pow = 220;
rank5pow = 400;

power = Power(damage, hitpoints, armour);

speedCost = speed*0.75;

sightCost = sight_radius1*0.4;

rank_cmp = { rank2pow, rank3pow, rank4pow, rank5pow };

CreatureRank = Rank( power, rank_cmp );

-----------------
-----------------
--ability  cost--
-----------------
-----------------

-- Unusable abilities removed
if (damage2>0) or (damage4>0) or (damage5>0) or (damage8>0) then
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
 	{ ABT_Ability, 	"overpopulation", 	2, 	0, 	0, 	0 },
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
	{ ABT_Ability, 	"deflection_armour", 	2, 	30, 	10, 	20 },
	{ ABT_Ability, 	"infestation", 		2, 	30, 	0, 	0 },
	{ ABT_Ability, 	"charge_attack", 	3, 	15, 	0, 	5 },
	{ ABT_Ability, 	"is_flyer", 		3, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"electric_burst", 	2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"poison_touch", 	3, 	40, 	0, 	10 },
	{ ABT_Ability, 	"web_throw", 		3, 	0, 	0, 	0 },	--special
 	{ ABT_Ability, 	"poison_bite", 			3,  	30, 	0, 	10 },
 	{ ABT_Ability, 	"poison_sting", 	3,  	30, 	0, 	10 },
	{ ABT_Ability, 	"poison_pincers", 	3, 	30, 	0, 	10 },
	{ ABT_Ability, 	"loner", 		2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"soiled_land", 		3, 	50, 	0, 	0 },

	{ ABT_Range, 	DT_Electric, 		2, 	0, 	0,	0 },
	{ ABT_Range, 	DT_Poison, 		3, 	0, 	0,	0 },
	{ ABT_Range, 	DT_Sonic, 		2, 	10, 	0,	2.5 },
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
			RangeCostMult = 1.15;
			CostRenew = CostRenew + 30 + 10*(CreatureRank - 1);
		else
			has_artillery = 1;
			RangeCostMult = 1.2;
			CostRenew = CostRenew + 30 + 10*(CreatureRank - 1);
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
if 	getgameattribute("speed_max") == 0 and
	getgameattribute("waterspeed_max") > 0 and
	getgameattribute("airspeed_max") == 0 then
	
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

if getgameattribute("flash") == 1 then
	CostRenew = CostRenew + (50+(10*CreatureRank))*flCost;
end

if getgameattribute("headflashdisplay") == 1 then
	CostRenew = CostRenew + (50+(10*CreatureRank));
end

if getgameattribute("regeneration") == 1 then
	CostRenew = CostRenew+(CreatureRank*40*armour);
end

-- Build time

build_time = (30 * CreatureRank)*((power*1.2/max_power)^1.2)*popMult;
CostGather = (CostGather + speedCost)*((power*1.3/max_power)^0.8)*RangeCostMult*1.1+(2/CreatureRank)*1.25;
CostRenew = CostRenew*((power*1.3/max_power)^0.8);

if getgameattribute("overpopulation") == 0 then
build_time = max(build_time, 16);
else
build_time = max(build_time, 8);
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
if getgameattribute("size") == 10 then
	setgameattribute("size", 9);
--	truesize = 10;
--	falsesize = 9;

end
if getgameattribute("size") == 11 then
	setgameattribute("size", 10);
--	truesize = 11;
--	falsesize = 10;

end
if getgameattribute("size") >= 12 then
	setgameattribute("size", 10);
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

setgameattribute("Power", power);

-- commented out:
-- end