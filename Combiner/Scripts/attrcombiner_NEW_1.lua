-----------------
-----------------
--vars and shit--
-----------------
-----------------

-----------------
--uncapped vars--
-----------------

damagedif = 0;

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
range_max 	= max(range_max1, max(range_max2, max(range_max3, range_max4)));

damage2 	= getgameattribute( "range2_damage" );
damage4 	= getgameattribute( "range4_damage" );
damage5 	= getgameattribute( "range5_damage" );
damage8 	= getgameattribute( "range8_damage" );

RangeCostMult	= 1.0;


damager = max (damage2, max (damage4, max(damage5, damage8)));

if (range_max > 0) then
	damager = damager*(1+(range_max/35));
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

--Defence caps for hard shell + not hard shell
if getgameattribute("hard_shell") == 1 then
	maxArmour = 0.80;
	else
	maxArmour = 0.60;
end

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

--dont delete this for some fucking reason
function combine_creature()
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
function Power(dmg, hp, arm)
	return (dmg*hp/(1-arm))^(1/1.59);
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

--Actual ranking/cost done after abilities

-----------------
-----------------
-------ui--------
-----------------
-----------------

--that sum thing for melee damage int it nice
if damager < 1 then
	setgameattribute("total_damage", damage);
	else
	setgameattribute("total_damage", getgameattribute("melee_damage"));
end

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

	{ "hitpoints",  nil, 1, nil, {0.0, 171.0, 304.0, 456.0}, "health", 1 },
	{ "armour", 1, 0, nil, {0.0, 0.15, 0.20, 0.35}, "armour", 100 },
	{ "speed_max", 1, 10, nil, {0.0, 20.0, 30.0, 40.0}, "landspeed", 1 },
	{ "waterspeed_max", 1, 8, nil, {0.0, 4.0, 6.0, 8.5}, "waterspeed", 1 },
	{ "airspeed_max", 1, 16, nil, {0.0, 5.0, 15.0, 20.0}, "airspeed", 1 },
	{ "sight_radius1", nil, nil, nil, {0.0, 25.0, 35.0, 45.0}, "sightradius", 1 },
	{ "size", nil, 1, nil, {0, 3, 6, 9}, "size", 1},

	{ "total_damage", 1, 0, nil, {0.0, 5.0, 9.0, 11.5}, "damage", 1 },
	{ "range2_max", 1, 0, nil, {0.0, 5.0, 9.0, 16.0}, "range2_max", 1 },
	{ "range4_max", 1, 0, nil, {0.0, 5.0, 9.0, 16.0}, "range4_max", 1 },
	{ "range5_max", 1, 0, nil, {0.0, 5.0, 9.0, 16.0}, "range5_max", 1 },
	{ "range8_max", 1, 0, nil, {0.0, 5.0, 9.0, 16.0}, "range8_max", 1 },
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
	if (val and val > 0) then
			rating = Rank( val, {0.0,12.0,20.0,26.0} );
			setattribute( "range2_damage_rating", rating - 1 );
		setattribute( "range2_damage_val", val );
	end
end

if checkgameattribute( "range4_damage" ) == 1 then
	val = getgameattribute( "range4_damage" );
	if (val and val > 0) then
			rating = Rank( val, {0.0,12.0,20.0,26.0} );
			setattribute( "range4_damage_rating", rating - 1 );
		setattribute( "range4_damage_val", val );
	end
end

if checkgameattribute( "range5_damage" ) == 1 then
	val = getgameattribute( "range5_damage" );
	if (val and val > 0) then
			rating = Rank( val, {0.0,12.0,20.0,26.0} );
			setattribute( "range5_damage_rating", rating - 1 );
		setattribute( "range5_damage_val", val );
	end
end

if checkgameattribute( "range8_damage" ) == 1 then
	val = getgameattribute( "range8_damage" );
	if (val and val > 0) then
			rating = Rank( val, {0.0,12.0,20.0,26.0} );
			setattribute( "range8_damage_rating", rating - 1 );
		setattribute( "range8_damage_val", val );
	end
end



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

if (getgameattribute("hard_shell") == 1) then
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

AbilityData =
{
	--All special case abilities are charged after power discount is applied, this is presently only to abilities that already have
	--power as a factor in their cost and a number of special abilities either because the ability is potentially problematic with the power discount
	--Note that the special case ranks are still controlled in this array

	--{ ability_type, ability_id, minrank, costrenew, costgather, costrenew plus perrank, Rank that the increase starts at }

	{ ABT_Ability, 	"is_immune", 		0, 	10, 	0, 	5 },
	{ ABT_Ability, 	"poison_pincers", 	3, 	10, 	0, 	5 },
	{ ABT_Ability, 	"keen_sense", 		0, 	10, 	0, 	0 },
	{ ABT_Ability, 	"can_dig", 		0, 	10, 	0, 	10 },
	{ ABT_Ability, 	"sonar_pulse", 		0, 	15, 	0, 	5 },
	{ ABT_Ability, 	"is_stealthy", 		0, 	20, 	0, 	10 },
	{ ABT_Ability, 	"stink_attack", 	0, 	50, 	0, 	5 },
	{ ABT_Ability, 	"stink",		0, 	50, 	0, 	5 },
	{ ABT_Ability, 	"flash", 		0, 	0, 	0, 	0 },
	{ ABT_Ability, 	"end_bonus", 		0, 	10, 	0 },
 	{ ABT_Ability, 	"speed_boost", 		0, 	0, 	0, 	0 },
 	{ ABT_Ability, 	"overpopulation", 	2, 	0, 	0, 	0 },
	{ ABT_Ability, 	"poplow", 		1, 	3, 	0, 	3 },
	{ ABT_Ability, 	"herding", 		1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"pack_hunter", 		1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"regeneration", 	1, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"frenzy_attack", 	1, 	25, 	0, 	5 },
	{ ABT_Ability, 	"plague_attack", 	1, 	50, 	0, 	5 },
	{ ABT_Ability, 	"Autodefense", 		1, 	15, 	0, 	5 },
	{ ABT_Ability, 	"assassinate", 		1, 	10, 	10, 	20 },
	{ ABT_Ability, 	"can_SRF", 		1, 	15, 	0, 	10 },
	{ ABT_Ability, 	"quill_burst", 		2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"leap_attack", 		2, 	15, 	0, 	5 },
	{ ABT_Ability, 	"is_swimmer", 		2, 	0, 	0 },
	{ ABT_Ability, 	"deflection_armour", 	2, 	30, 	10, 	20 },
	{ ABT_Ability, 	"infestation", 		2, 	30, 	0, 	0 },
	{ ABT_Ability, 	"hard_shell", 		3, 	30, 	30, 	5 },	--special
	{ ABT_Ability, 	"charge_attack", 	3, 	15, 	0, 	5 },
	{ ABT_Ability, 	"is_flyer", 		3, 	25, 	0, 	15 },
	{ ABT_Ability, 	"electric_burst", 	2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"poison_touch", 	3, 	30, 	0, 	10 },
	{ ABT_Ability, 	"web_throw", 		3, 	0, 	0, 	0 },	--special
 	{ ABT_Ability, 	"poison_bite", 		3,  	0, 	0, 	0 },	--special
 	{ ABT_Ability, 	"poison_sting", 	3,  	0, 	0, 	10 },
	{ ABT_Ability, 	"loner", 		2, 	0, 	0, 	0 },	--special
	{ ABT_Ability, 	"soiled_land", 		4, 	0, 	0, 	0 },	--special

	{ ABT_Range, 	DT_Electric, 		2, 	0, 	0 },
	{ ABT_Range, 	DT_Poison, 		3, 	0, 	0 },
	{ ABT_Range, 	DT_Sonic, 		3, 	30, 	0 },
	{ ABT_Range, 	DT_VenomSpray, 		3, 	0, 	0 },

	{ ABT_Melee, 	DT_BarrierDestroy, 	0, 	20, 	0, 	5},
	{ ABT_Melee, 	DT_HornNegateFull, 	2, 	30, 	0, 	10 },
	{ ABT_Melee, 	DT_HornNegateArmour, 	3, 	30, 	0, 	10 },
	{ ABT_Melee, 	DT_Poison, 		3, 	0, 	0, 	0 },
};

-- Total the costs and find min rank for all abilities.
for n, ab in AbilityData do
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

BodyPartsThatCanHaveRange = { 2, 4, 5, 8 };

for index, part in BodyPartsThatCanHaveRange do
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
 for n, ab in AbilityData do
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
	CostGather = 60;
	elseif (CreatureRank == 2) then
		max_power = rank3pow;
		CostGather = 100;
		elseif (CreatureRank == 3) then
		    max_power = rank4pow;
		    CostGather = 160;
		    elseif (CreatureRank == 4) then
		        max_power = rank5pow;
				CostGather = 240;
		        else
		    	max_power = 800;
			    CostGather = 400;
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
if getgameattribute("herding") == 1 then
	CostRenew = CostRenew + ((Power(damage, hitpoints, min(armour*1.4, 0.6))/power)-1)*216*RangeCostMult;
end

if getgameattribute("hard_shell") == 1 then
	CostRenew = CostRenew + ((Power((damage+1), hitpoints, armour)/power)-1)*200*RangeCostMult;
	CostGather = CostGather + ((Power(damage+1, hitpoints, armour)/power)-1)*200*RangeCostMult;
end

if getgameattribute("pack_hunter") == 1 then
	CostRenew = CostRenew + ((Power(damage*1.3, hitpoints, armour)/power)-1)*180*RangeCostMult;
end

if getgameattribute("flash") == 1 then
	CostRenew = CostRenew + (50+(10*CreatureRank))*flCost;
end

if getgameattribute("headflashdisplay") == 1 then
	CostRenew = CostRenew + (50+(10*CreatureRank));
end

ActualHealth = hitpoints/(1-armour);
RegenHealth = (1 + 0.5*CreatureRank);

if getgameattribute("regeneration") == 1 then
	CostRenew = CostRenew+10+Power(damage, RegenHealth, armour)*6*ActualHealth/600;
end

if getgameattribute("poison_bite") == 1 or getgameattribute("poison_sting") == 1 then
    CostRenew = CostRenew + ((Power(damage*1.1, hitpoints/0.75, armour)/power) - 1)*200;
end

build_time = (8+24 * CreatureRank)*(power*1.1/max_power)*popMult;
CostGather = (CostGather + speedCost + sightCost)*(power*1.1/max_power)*RangeCostMult*1.1+(2/CreatureRank)*1.25;
CostRenew = CostRenew * (power*1.1/max_power)*1.2;

if getgameattribute("overpopulation") == 0 then
build_time = max(build_time, 16);
else
build_time = max(build_time, 8);
end

--no discount ability costs
if getgameattribute("quill_burst") == 1 then
	CostRenew = CostRenew + 20 + 10*CreatureRank;
end

if getgameattribute("electric_burst") == 1 then
	CostRenew = CostRenew + 10 + 10*CreatureRank;
end
                            
if getgameattribute("web_throw") == 1 then
   CostRenew = CostRenew + 80 + CreatureRank * 10;
   CostGather = CostGather * 1.3;
end

if getgameattribute("soiled_land") == 1 then
	CostRenew = CostRenew + 230 + 20 * CreatureRank;
end

if getgameattribute("loner") == 1 then
	CostRenew = CostRenew + ((Power(damage*6, hitpoints/0.25, armour)/power)-1)*15*CreatureRank;
	CostGather = CostGather + 3 * speedCost;
end



-----------------
-----------------
-----outputs-----
-----------------
-----------------

--Size fixes
if getgameattribute("size") == 10 then
	setgameattribute("size", 9);
end
if getgameattribute("size") == 11 then
	setgameattribute("size", 10);
end
if getgameattribute("size") >= 12 then
	setgameattribute("size", 10);
end

--Popspace calc
if (getgameattribute("poplow") == 1) or (getgameattribute("poplowtorso") == 1) then
	Pop = 1;
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

end