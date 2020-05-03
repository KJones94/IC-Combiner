--Tellurian Attrcombiner (5/1/2019)

--Changelog for Tel 2.7
--Power is different for differnt types of ranged attacks dependent on range distance (using damager)
--Double range is no longer charged; unit is charged for its most powerful attack, at shortest range,
--for most expensive damagetype.
--Added ranged piercing ability for porcupine.
--Power used to calculate certain ability pricing. Abilities affected:
--Herding, Pack, Barrier Destroy, Horns, Camouflage, Ranged Piercing
--Extensive code changes to support this.
--Pure swimmer cost lowered, swimmer ranged cost more than melee.

--Let's go!


-----------------
-----------------
--vars and shit--
-----------------
-----------------

--Elec, coal, rank, minrank bein set up here
CostRenew = 0;
CostGather = 0;
CreatureRank = 0;
MinRank = 0;

herdCost = 0;

-----------------
--uncapped vars--
-----------------
---- Let's start by determining effective speed.

speed_max 	= getgameattribute( "speed_max" );
airspeed_max 	= getgameattribute( "airspeed_max" );
waterspeed_max 	= getgameattribute( "waterspeed_max" );

if (getgameattribute("is_flyer") == 1) then
	speed = airspeed_max*1.3;
end

if (getgameattribute("is_swimmer") == 1 and getgameattribute("is_land") == 1) then
	speed = ((waterspeed_max^2)/(36+(speed_max*4))) + speed_max;
end

if (getgameattribute("is_swimmer") == 1 and getgameattribute("is_land") == 0) then
	speed = waterspeed_max*0.4;
end

if (getgameattribute("is_swimmer") == 0 and getgameattribute("is_land") == 1) then
	speed = speed_max;
end

---- End of speed section.

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

--Initialize damager
damager = 0;
minRangeDist = 1000;

-- These will be set to flag if the creature has any direct or any artillery range attack.
has_direct = nil;
has_artillery = nil;
has_sonic = nil;

--Ranged Costs and MinRank
-- used to determine whether the range type is splash damage

function get_range_var( limb, var )
	local str = "range"..limb.."_"..var

	if checkgameattribute(str) == 1 then
		return getgameattribute( str )
	else
		return 0;
	end
end

function range_artillerytype( limb )
	-- if this creature has a special field it has artillery
	return get_range_var( limb, "special");
end

BodyPartsThatCanHaveRange = { 2, 3, 4, 5, 8 };

-- determine type of ranged attack and set range damage to be equal to the
-- highest damage ranged attack, with minRangeDist being the minimum ranged distance.

for index, part in pairs(BodyPartsThatCanHaveRange) do
	part_damage = getgameattribute( "range" .. part .. "_damage" );
	part_range = getgameattribute( "range" .. part .. "_max" );
	part_dtype = getgameattribute( "range" .. part .. "_dmgtype" );
	if ( part_damage > 0 ) then
		if ( part_range < minRangeDist ) then
			minRangeDist = part_range;
		end
		
		if ( part_damage > damager ) then
			damager = part_damage;
		end		
		
		if ( range_artillerytype( part ) == 0 ) then
			if ( part_dtype == 16 ) then --sonic attack identified
				has_sonic = 1;								
			else --directranged, non-sonic
				has_direct = 1;					
			end
			
		else --artillery
			has_artillery = 1;			
		end
	end
end


-- Now we sort out power and costs on ranged attacks. The following code also
-- determines how to charge multiple ranged attacks.
-- NOTE: It is possible, using this method, that combining ranged attacks could
-- result in unintended cost discounts for very exotic art/range combos.
 if ( damager > 0 ) then
	 -- Ask if one of the range attacks is sonic.
	 if ( has_sonic == 1 ) then
		 damager = damager*(1+(minRangeDist/20));
		 CostRenew = CostRenew + (damager * 3);
		
	 else -- Ask if one of the range attacks is not artillery.
		 if ( has_direct == 1 ) then
			 damager = damager*(1+(minRangeDist/28));
			 CostRenew = CostRenew + (damager * 1.5);
			
		 else -- Then we've got only an artillery attack.
			 damager = damager*1.1*(1+(minRangeDist/35));
			 CostRenew = CostRenew + (damager * 1.7);
			
		 end
	 end
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

-- The following array simply stores melee damage, range damage
-- (factoring in multiple attacks and distance) and combined damage.
-- It is used later to target specific attack types in ability costing.
AttackTypes = 
{
	damagem,
	damager,
	damage
};
	

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
rank4pow = 230;
rank5pow = 400;

power = Power(damage, hitpoints, armour);

speedCost = ((speed/22)^0.28);

sightCost = sight_radius1*0.4;

rank_cmp = { rank2pow, rank3pow, rank4pow, rank5pow };

CreatureRank = Rank( power, rank_cmp );

-----------------
-----------------
-------ui--------
-----------------
-----------------


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

if (getgameattribute("pack_hunter") == 1 and getgameattribute("herding") == 1) then
	setgameattribute("herding", 0);
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

RB_DamageCost = 3;
RB_EhpCost = 4;
RB_MaxCost = 5;
RB_AttType = 6;

AbilityData =
{
	--All special case abilities are charged after power discount is applied, this is presently only to abilities that already have
	--power as a factor in their cost and a number of special abilities either because the ability is potentially problematic with the power discount
	--Note that the special case ranks are still controlled in this array

	--{ ability_type, ability_id, minrank, costrenew, costgather, costrenew plus perrank, Rank that the increase starts at }

	{ ABT_Ability, 	"is_immune", 		0, 	5, 	0, 	2.5 },
	{ ABT_Ability, 	"keen_sense", 		0, 	10, 0, 	0 },
	{ ABT_Ability, 	"can_dig", 			0, 	10, 0, 	10 },
	{ ABT_Ability, 	"sonar_pulse", 		0, 	15, 0, 	5 },
	{ ABT_Ability, 	"is_stealthy", 		0, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"stink_attack", 	0, 	50, 0, 	5 },
	{ ABT_Ability, 	"stink",			0, 	50, 0, 	5 },
	{ ABT_Ability, 	"flash", 			0, 	0, 	0, 	0 },
	{ ABT_Ability, 	"end_bonus", 		0, 	10, 0,	0 },
 	{ ABT_Ability, 	"speed_boost", 		0, 	0, 	0, 	0 },
 	{ ABT_Ability, 	"overpopulation", 	2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"poplow", 			1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"poplowtorso", 		1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"herding", 			1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"pack_hunter", 		1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"regeneration", 	1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"frenzy_attack", 	1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"plague_attack", 	1, 	50, 0, 	5 },
	{ ABT_Ability, 	"AutoDefense", 		1, 	15, 0, 	5 },
	{ ABT_Ability, 	"assassinate", 		1, 	10, 10,	20 },
	{ ABT_Ability, 	"can_SRF", 			1, 	15, 0, 	10 },
	{ ABT_Ability, 	"quill_burst", 		2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"leap_attack", 		2, 	15, 0, 	5 },
	{ ABT_Ability, 	"is_swimmer", 		2, 	0, 	0,	0 },
	{ ABT_Ability, 	"deflection_armour",2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"infestation", 		2, 	30, 0, 	0 },
	{ ABT_Ability, 	"charge_attack", 	3, 	15, 0, 	5 },
	{ ABT_Ability, 	"is_flyer", 		3, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"electric_burst", 	2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"poison_touch", 	3, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"web_throw", 		3, 	0, 	0, 	0 },	--special
 	{ ABT_Ability, 	"poison_bite", 		3,  0, 	0, 	0 },	--accounted for in damagetype
 	{ ABT_Ability, 	"poison_sting", 	3,  0, 	0, 	0 },	--accounted for in damagetype
	{ ABT_Ability, 	"poison_pincers", 	3, 	0, 	0, 	0 },	--accounted for in damagetype
	{ ABT_Ability, 	"loner", 			2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"soiled_land", 		3, 	50, 0, 	0 },
	{ ABT_Ability, 	"ranged_piercing", 	2, 	0, 	0, 	0 }, --special

	{ ABT_Range, 	DT_Electric, 		2, 	0, 	0,	0 },
	{ ABT_Range, 	DT_Poison, 			3, 	0, 	0,	0 },
	{ ABT_Range, 	DT_Sonic, 			2, 	0, 	0,	0 },
	{ ABT_Range, 	DT_VenomSpray, 		3, 	0, 	0,	0 },

	{ ABT_Melee, 	DT_BarrierDestroy, 	0, 	0, 	0, 	0},     --special
	{ ABT_Melee, 	DT_HornNegateFull, 	2, 	30, 0, 	10 },
	{ ABT_Melee, 	DT_HornNegateArmour,3, 	0, 	0, 	0 },	--special
	{ ABT_Melee, 	DT_Poison, 			3, 	0, 	0, 	0 },	--special
};

-- Set points for max EHP and Melee (arbitrary)
ehpMax = 3000;
dammMax = 100;

AbilityRefPoints =
{
	--The following abilities are scaled based on reference points.
	--{ ability_type, ability_id, damageCost, ehpCost, maxCost, AttackType}
	-- Attack Type 1 is melee, 2 is range, 3 is combined.

	{ ABT_Ability, 	"herding", 			120, 	150, 	350,	3 },
	{ ABT_Ability, 	"pack_hunter", 		160, 	190, 	500,	3 },
	{ ABT_Ability, 	"is_stealthy", 		200, 	90, 	300,	3 },
	{ ABT_Ability, 	"regeneration", 	110, 	110, 	250,	3 },
	{ ABT_Ability, 	"frenzy_attack", 	190, 	160, 	400,	3 },	
	{ ABT_Ability, 	"ranged_piercing", 	80, 	50, 	200,	2 },

	{ ABT_Melee, 	DT_BarrierDestroy, 		140, 	60, 	400, 	1 },
	{ ABT_Melee, 	DT_HornNegateArmour, 	210, 	190, 	700,	1 },

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


-- Total the costs for reference-point ability costs
 for n, ab in pairs(AbilityRefPoints) do
	-- If we have this ability...
	if ABT_CheckFunctions[ab[AB_AbilityType]]( ab[AB_Id] ) == 1 then
		
		-- Calculate shape value.				
		shapeValue = (ab[RB_MaxCost]-ab[RB_EhpCost]-ab[RB_DamageCost])/(ehpMax*dammMax);
		
		-- add on the costs.
		abCost = (shapeValue*AttackTypes[ab[RB_AttType]]*(hitpoints/(1-armour)))+((hitpoints/(1-armour))*ab[RB_EhpCost]/ehpMax)+(AttackTypes[ab[RB_AttType]]*ab[RB_DamageCost]/dammMax);
		
		if (ab[AB_Id] == "herding") then
			herdCost = abCost;
		
		else
			CostRenew = CostRenew + abCost;
		
		end		
	end
end

--Sorting out costs and ranks

if (CreatureRank == 1) then
	max_power = rank2pow;
	CostGather = 60; 
	elseif (CreatureRank == 2) then
		max_power = rank3pow;
		CostGather = 110;
		elseif (CreatureRank == 3) then
		    max_power = rank4pow;
		    CostGather = 170;
		    elseif (CreatureRank == 4) then
		        max_power = rank5pow;
				CostGather = 240;
		        else
		    	max_power = 1000;
			    CostGather = 450;
end

-----------------
-----------------
----cost mods----
-----------------
-----------------

-- Here's where we actually charge for herding.
if (herdCost > 0) then	
	extraArmour = (armour*0.3);
	armourOverCap = (armour + extraArmour) - maxArmour;
	
	if (armourOverCap > 0) then
		CostRenew = CostRenew + ((1 - (armourOverCap / extraArmour)) * herdCost);
	
	else	
		CostRenew = CostRenew + herdCost;
		
	end
end	

-- This is applying a cost to venom spray (can't do it from the above table)
if hasrangedmgtype(DT_VenomSpray) == 1 then
	CostRenew = CostRenew + 1000;
end

if (getgameattribute("poplow") == 1) and (power/150 > 1) then
	CostRenew = CostRenew + (power*0.1);
end

if (getgameattribute("poplowtorso") == 1) and (power/150 > 1) then
	CostRenew = CostRenew + (power*0.1);
end

if getgameattribute("flash") == 1 then
	CostRenew = CostRenew + (50+(10*CreatureRank))*flCost;
end

if getgameattribute("headflashdisplay") == 1 then
	CostRenew = CostRenew + (50+(10*CreatureRank));
end

--if getgameattribute("regeneration") == 1 then
--	CostRenew = CostRenew+(CreatureRank*40*armour);
--end

if getgameattribute("deflection_armour") == 1 then
	CostRenew = CostRenew+((hitpoints/(1-armour))/5.5);
end

if getgameattribute("overpopulation") == 1 then
	CostRenew = CostRenew+(power/5);
end

-- Sorting out cost for Poisons.
-- NOTE: If you want to give a venomous ranged unit poison cost, apply Damage Type 1 to its melee attack on the relevant body part.

if hasmeleedmgtype(DT_Poison) == 1 then
	CostRenew = CostRenew + 20 + (20 * (CreatureRank - 2));
end

-- If poisonous, ptouch is free!

if (getgameattribute("poison_touch") == 1) and (hasmeleedmgtype(DT_Poison) == 0) then
	CostRenew = CostRenew + 20 + (20 * (CreatureRank - 2));
end

-- Build time
build_time = (30 * CreatureRank)*((power*1.2/max_power)^1.2)*popMult;

-- FINAL COST SCALERS. These basically control how good spam is; a higher exponent benefits spam more,
-- because it increases the range of costs within a level.
CostGather = (CostGather)*speedCost*((power*1.3/max_power)^0.8)*RangeCostMult*1.1+(2/CreatureRank)*1.25;
CostRenew = CostRenew*((power*1.3/max_power)^0.6);


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

