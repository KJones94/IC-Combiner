--Tellurian Attrcombiner (5/1/2019)

--Changelog for Tel 2.8
--Costs for deflect modified to penalize high power units less.
--Special case for flying deflect added to keep them expensive.
--Speed is now slightly more expensive.
--Cost exponent at levels 1, 2 and 3 changed to 1, to decrease spam cost. Base level costs adjusted accordingly.

--Non-Attrcombiner Changes:
--AA towers now have 30 sight radius and do 12.5 damage per tick for 4 ticks (up from 10).

--Let's go!

--deleteStart
function combine_creature()
--deleteEnd

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
popdivide = 150;

-----------------
--uncapped vars--
-----------------
---- Let's start by determining effective speed.

speed_max 	= getgameattribute( "speed_max" );
airspeed_max 	= getgameattribute( "airspeed_max" );
waterspeed_max 	= getgameattribute( "waterspeed_max" );

---- We raise the speed if it's below min caps.

if (speed_max < 15) then
	speed_max = 15;
end
	
if (waterspeed_max > 0 and waterspeed_max < 12) then
	waterspeed_max = 12;
end

---- Now we calculate effective speed.

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

--pairsBelow
for index, part in BodyPartsThatCanHaveRange do
--endPairs
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

speedCost = ((speed/22)^0.35);

sightCost = sight_radius1*0.4;

rank_cmp = { rank2pow, rank3pow, rank4pow, rank5pow };

CreatureRank = Rank( power, rank_cmp );

-----------------
-----------------
-------ui--------
-----------------
-----------------

--deleteStart
setgameattribute("powerdisplay", Power(damage, hitpoints, armour));

-- Attribute data.

-- Column ids.
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
	{ "speed_max", 1, 15, nil, {15.0, 21.0, 26.0, 31.0}, "landspeed", 1 },
	{ "waterspeed_max", 1, 12, nil, {12.0, 20.0, 25.0, 30.0}, "waterspeed", 1 },
	{ "airspeed_max", 1, 16, nil, {16.0, 20.0, 24.0, 28.0}, "airspeed", 1 },
	{ "sight_radius1", nil, nil, nil, {20.0, 30.0, 35.0, 45.0}, "sightradius", 1 },
	--{ "powerdisplay", nil, nil, nil, {rank2pow, rank3pow, rank4pow, rank5pow}, "sightradius", 1 },
	{ "size", nil, 1, nil, {0, 3, 6, 9}, "size", 1},

	{ "melee_damage", 1, 0, nil, {1.0, 10.0, 17.0, 26.0}, "damage", 1 },
	{ "range2_max", 1, 0, nil, {1.0, 8.0, 14.0, 21.0}, "range2_max", 1 },
	{ "range4_max", 1, 0, nil, {1.0, 8.0, 14.0, 21.0}, "range4_max", 1 },
	{ "range5_max", 1, 0, nil, {1.0, 8.0, 14.0, 21.0}, "range5_max", 1 },
	{ "range8_max", 1, 0, nil, {1.0, 8.0, 14.0, 21.0}, "range8_max", 1 },
	{ "range3_max", 1, 0, nil, {1.0, 8.0, 14.0, 21.0}, "range3_max", 1 },
};

-- Apply boundaries and rank attributes.

for k, at in AttributeData do

	local attribute = at[AT_Name];
	local val = 0;
	local rating = 1;

	if checkgameattribute( attribute ) == 1 then

		-- Boundary check and fix.
		val = getgameattribute( attribute );

		if not ( at[AT_ZeroOK] == 1 and val == 0 ) and at[AT_Min] and ( val < at[AT_Min] ) then
			setgameattribute( attribute, at[AT_Min] );
			val = at[AT_Min];
		end

		if at[AT_Max] and val > at[AT_Max] then
			setgameattribute( attribute, at[AT_Max] );
			val = at[AT_Max];
		end

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

if checkgameattribute( "range2_damage" ) == 1 then
val = getgameattribute( "range2_damage" );
if (val and val > -1) then
rating = Rank( val, {-1.0,12.0,20.0,26.0} );
setattribute( "range2_damage_rating", rating - 1 );
setattribute( "range2_damage_val", val );
end
end

if checkgameattribute( "range4_damage" ) == 1 then
val = getgameattribute( "range4_damage" );
if (val and val > -1) then
rating = Rank( val, {-1.0,12.0,20.0,26.0} );
setattribute( "range4_damage_rating", rating - 1 );
setattribute( "range4_damage_val", val );
end
end

if checkgameattribute( "range8_damage" ) >= 0 then
val = getgameattribute( "range8_damage" );
if (val and val > 0) then
rating = Rank( val, {-1,12.0,20.0,26.0} );
setattribute( "range8_damage_rating", rating - 1 );
setattribute( "range8_damage_val", val );
end
end

if checkgameattribute( "range8_damage" ) >= 0 then
val = getgameattribute( "range8_damage" );
if (val and val > 0) then
rating = Rank( val, {-1,12.0,20.0,26.0} );
setattribute( "range2_damage_rating", rating - 1 );
setattribute( "range2_damage_val", val );
end
end

if checkgameattribute( "range3_damage" ) == 1 then
val = getgameattribute( "range3_damage" );
if (val and val > -1) then
rating = Rank( val, {-1.0,12.0,20.0,26.0} );
setattribute( "range3_damage_rating", rating - 1 );
setattribute( "range3_damage_val", val );
end
end

if checkgameattribute( "range3_damage" ) == 1 then
val = getgameattribute( "range3_damage" );
if (val and val > -1) then
rating = Rank( val, {-1.0,12.0,20.0,26.0} );
setattribute( "range2_damage_rating", rating - 1 );
setattribute( "range2_damage_val", val );
end
end

if checkgameattribute( "range5_damage" ) == 1 then
val = getgameattribute( "range5_damage" );
if (val and val > -1) then
rating = Rank( val, {-1.0,12.0,20.0,26.0} );
setattribute( "range5_damage_rating", rating - 1 );
setattribute( "range5_damage_val", val );
end
end

if checkgameattribute( "range5_damage" ) == 1 then
val = getgameattribute( "range5_damage" );
if (val and val > -1) then
rating = Rank( val, {-1.0,12.0,20.0,26.0} );
setattribute( "range2_damage_rating", rating - 1 );
setattribute( "range2_damage_val", val );
end
end

--deleteEnd

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

--not unusable, but removing so that loner units won't be charged for herding or pack hunter
if (getgameattribute("loner") == 1) then
	setgameattribute("herding", 0);
	setgameattribute("pack_hunter", 0);
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
	{ ABT_Ability, 	"can_dig", 			0, 	0, 	0, 	0 },
	{ ABT_Ability, 	"sonar_pulse", 		0, 	15, 0, 	5 },
	{ ABT_Ability, 	"is_stealthy", 		0, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"stink_attack", 	0, 	50, 0, 	5 },
	{ ABT_Ability, 	"stink",			0, 	50, 0, 	5 },
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
	{ ABT_Ability, 	"can_SRF", 		2, 	25, 0, 	5 },
	{ ABT_Ability, 	"quill_burst", 		2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"leap_attack", 		2, 	10, 0, 	5 },
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
	{ ABT_Ability, 	"flash", 			0, 	35, 0, 	5 },
	{ ABT_Ability, 	"headflashdisplay", 0, 	35, 0, 	5 },

	{ ABT_Range, 	DT_Electric, 		2, 	0, 	0,	0 },
	{ ABT_Range, 	DT_Poison, 			3, 	0, 	0,	0 },
	{ ABT_Range, 	DT_Sonic, 			2, 	0, 	0,	0 },
	{ ABT_Range, 	DT_VenomSpray, 		3, 	0, 	0,	0 },

	{ ABT_Melee, 	DT_BarrierDestroy, 	0, 	0, 	0, 	0},     --special
	{ ABT_Melee, 	DT_HornNegateFull, 	2, 	30, 0, 	10 },
	{ ABT_Melee, 	DT_HornNegateArmour,2, 	0, 	0, 	0 },	--special
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
	{ ABT_Ability, 	"can_dig", 			150, 	60, 	210, 	3 },
	{ ABT_Ability, 	"regeneration", 	110, 	110, 	250,	3 },
	{ ABT_Ability, 	"frenzy_attack", 	170, 	120, 	400,	3 },	
	{ ABT_Ability, 	"ranged_piercing", 	80, 	50, 	200,	2 },

	{ ABT_Melee, 	DT_BarrierDestroy, 		140, 	60, 	400, 	1 },
	{ ABT_Melee, 	DT_HornNegateArmour, 	200, 	120, 	550,	1 },

};

-- Total the costs and find min rank for all abilities.
--pairsStart
for n, ab in AbilityData do
--pairsEnd
	-- If we have this ability...
	if ABT_CheckFunctions[ab[AB_AbilityType]]( ab[AB_Id] ) == 1 then
		-- add on the costs.
		if ab[AB_CostRenew] then
			CostRenew = CostRenew + ab[AB_CostRenew];
		end
		if ab[AB_CostGather] then
			CostGather = CostGather + ab[AB_CostGather];
		end
		-- add on the costs.
		if ab[AB_CostRenewIncrement] then
			CostRenew = CostRenew + ab[AB_CostRenewIncrement] * (CreatureRank - 1);
		end
		
		-- check for min rank.
		CreatureRank = max( CreatureRank, ab[AB_MinRank] );
	end
end


-- Total the costs for reference-point ability costs
--pairsStart
 for n, ab in AbilityRefPoints do
--pairsEnd
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
		CostGather = 100;
		elseif (CreatureRank == 3) then
		    max_power = rank4pow;
		    CostGather = 170;
		    elseif (CreatureRank == 4) then
		        max_power = rank5pow;
				CostGather = 240;
		        else
		    	max_power = 1000;
			    CostGather = 420;
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

--poplow cost
if (((getgameattribute("poplow") == 1) or (getgameattribute("poplowtorso") == 1)) and (power/popdivide >= 2)) then
	CostRenew = CostRenew + ((power-popdivide)/popdivide*20);
end

-- Deflect cost; the 0.88 here is a rank efficiency number. We may expand the attrcombiner
-- to use this number in more places in the future, in which case I'll declare it as a variable.
if getgameattribute("deflection_armour") == 1 then

	-- I've elected to set up a special case for flying deflect, because range does bonus damage for fliers (and therefore deflect
	-- does extra damage on the rebound hit). 
	if getgameattribute("is_flyer") == 1 then
		CostRenew = CostRenew + ((hitpoints/(1-armour))/3.8)*(0.88^(CreatureRank-3)) ;
	else
		CostRenew = CostRenew + ((hitpoints/(1-armour))/5.5)*(0.88^(CreatureRank-3)) ;
end
	
end

-- Overpopulation cost
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
-- Let's build a table of exponents to allow us to control cost curve per level:
CostExpo =
{
	1, -- Level 1 cost exponent
	1, -- Level 2 cost exponent
	1, -- Level 3 cost exponent
	0.8, -- Level 4 cost exponent
	0.7, -- Level 5 cost exponent
}
	
CostGather = (CostGather)*speedCost*((power*1.3/max_power)^(CostExpo[CreatureRank]))*1.1;
CostRenew = CostRenew*((power*1.3/max_power)^(CostExpo[CreatureRank]));


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
	Pop = (power/popdivide)/2;
else
	Pop = power/popdivide;
end


setattribute( "buildtime", 10 );

setgameattribute("constructionticks", build_time);

--Final Output
setattribute( "creature_rank", CreatureRank );
setattribute( "costrenew", CostRenew );
setattribute( "cost", CostGather );
setattribute( "popsize", Pop )

setgameattribute("Power", power);

--deleteStart
end
--deleteEnd