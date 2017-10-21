----------------------------------------
-- ALL Variables
----------------------------------------

DamageDiff = 0;

MaxSpeed = 0;
MaxAirSpeed = 0;
MaxWaterSpeed = 0;
Speed = 0;
Size = 0;
Hitpoints = 0

TotalDamage = 0; -- not sure if this messed up UI stuff switching from "damage"
MeleeDamage = 0;
RangeDamage = 0;

MaxRange1 = 0;
MaxRange2 = 0;
MaxRange3 = 0;
MaxRange4 = 0;
MaxRange5 = 0;
MaxRange = 0;

Damage2 = 0;
Damage3 = 0;
Damage4 = 0;
Damage5 = 0;
Damage8 = 0;

-- What are those??

RangeCostMult   = 0;	-- used

LonerCost = 0;			-- used
MaxAirSpeedMod = 0;				-- used for flyers
FlyerDamageMod = 0;		-- set to 1.0 so does nothing, used a lot

RangeDamageMod = 0;			-- used, not sure what this stands for
MeleeRamageMod = 0;			-- used, not sure what this stands for

PopMod = 0;			-- used

MaxArmour = 0			-- used

SightRadius = 0;		-- used, gameattribute is also referenced multiple times

Armour = 0;				-- used, also appears in functions

CostRenew = 0;			-- used
CostGather = 0;			-- used
CreatureRank = 0;		-- used a lot

max_power = 0;

FlyerRankMod = 0;		-- set to 1.1 to adjust flyer ranking

Rank2Power = 0;			-- used
Rank3Power = 0;			-- used
Rank4Power = 0;			-- used
Rank5Power = 0;			-- used

Power = 0;				-- used

SpeedCost = 0;			-- used
SightCost = 0;			-- used

PowerRankThresholds = {};			-- used

Rank1HealthLim = 0;			--used
Rank2HealthLim = 0;			--used
Rank3HealthLim = 0;			--used
Rank4HealthLim = 0;			--used
Rank5HealthLim = 0;			--used

AT_Name = 1;			--used
AT_ZeroOK = 2;			--used
AT_Min = 3;				--used
AT_Max = 4;				--used
AT_RankList = 5;		--used
AT_UIName = 6;			--used
AT_UIScale = 7;			--used

AttributeData = {};		-- used

val = 0;				-- used, also used as local var in loop, should be in a function
rating = 0;				-- used, also used as local var in loop, should be in a function

EnduranceCost = 0;			-- used

ABT_Ability = 1;		-- used
ABT_Melee = 2;			-- used
ABT_Range = 3;			-- used

ABT_CheckFunctions = {};	-- used

AB_AbilityType = 1;						-- used
AB_Id = 2;								-- used
AB_MinRank = 3;							-- used
AB_CostRenew = 4;						-- used
AB_CostGather = 5;						-- used
AB_CostRenewIncrement = 6;				-- used
AB_CostRenewIncrementStartRank = 7;		-- used

AbilityData = {};		-- used but weird

HasDirectRange = nil;		-- used
HasArtillery = nil;	-- used

BodyPartsThatCanHaveRange = {};			-- used

ArtPureSwimmerCostMod = 0;	-- used
RancePureSwimmerCostMod = 0;		-- used
MeleePureSwimmerCostMod = 0;		-- used

ActualHealth = 0;		-- used
MaxHealth = 0;			-- used
RegenHealth = 0;		-- used

BuildTime = 0;			-- used

Pop = 0;				-- used


----------------------------------------
-- ALL Functions
----------------------------------------

-- Used for ui game attributes
function setattribute( attribute_string, value )
  setgameattribute(attribute_string,value);
  setuiattribute(attribute_string,value);
end

function Rank( x, rank_upper_bounds )
  -- Find where x falls in the array of ranges.
  local i = 1;
  while rank_upper_bounds[i] do
    if x <= rank_upper_bounds[i] then
      return i;
    end
    i = i + 1;
  end
  return i;
end

-- Calculates power based on damage, health, and armour
function Power(damage, health, armour)
  return (damage * health / (1 - armour)) ^ (1 / 1.59);
end

function EffectualHealth(health, armour)
	return health / (1 - armour)
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


----------------------------------------
--Uncapped Major Variables
----------------------------------------

DamageDiff = 0;

MaxSpeed   = getgameattribute( "speed_max" );
MaxAirSpeed   = getgameattribute( "airspeed_max" );
MaxWaterSpeed   = getgameattribute( "waterspeed_max" );
Speed     = max( MaxSpeed, max( MaxAirSpeed, MaxWaterSpeed ) );

Size     = getgameattribute( "size" );
Hitpoints   = getgameattribute( "hitpoints" );
MeleeDamage   = getgameattribute( "melee_damage" );

MaxRange1   = getgameattribute("range4_max");
MaxRange2  = getgameattribute("range2_max");
MaxRange3   = getgameattribute("range5_max");
MaxRange4   = getgameattribute("range8_max");
MaxRange5   = getgameattribute("range3_max");
MaxRange   = max(MaxRange1, max(MaxRange2, max(MaxRange3, max(MaxRange4, MaxRange5))));

Damage2   = getgameattribute( "range2_damage" );
Damage3   = getgameattribute( "range3_damage" );
Damage4   = getgameattribute( "range4_damage" );
Damage5   = getgameattribute( "range5_damage" );
Damage8   = getgameattribute( "range8_damage" );


RangeDamage = max (Damage2, max (Damage4, max(Damage5, max(Damage3, Damage8))));

if (MaxRange > 0) then
  RangeDamage = RangeDamage*(1+(MaxRange/35));
end


if MaxAirSpeed > 0 and RangeDamage < 1 then
  FlyerDamageMod = 1.0;
  else
  FlyerDamageMod = 1.0
end


if RangeDamage > MeleeDamage then
  RangeDamageMod = 0.8;
  MeleeRamageMod = 0.2;
  else
  RangeDamageMod = 0.2;
  MeleeRamageMod = 0.8;
end

if RangeDamage > 0 then
  TotalDamage = MeleeDamage * MeleeRamageMod + RangeDamage * RangeDamageMod;
  else
  TotalDamage = MeleeDamage;
end

----------------------------------------
--Nonstandard additions
----------------------------------------

if (getgameattribute("fly") == 1) then
  MaxAirSpeedMod = 1.4;
  else
  MaxAirSpeedMod = 1.0;
end

setgameattribute("airspeed_max", MaxAirSpeed*MaxAirSpeedMod);



PopMod = 1.0;
if getgameattribute("overpopulation") == 1 then
PopMod = 0.5;
end

--if getgameattribute("SightRadius") < MaxRange then
--    setgameattribute("SightRadius", MaxRange);
--end

setgameattribute("sight_radius1", max(getgameattribute("sight_radius1"), 20));

------------------------------------------
--Capped Major Variables
------------------------------------------
if getgameattribute("sight_radius1") > 50 then
    setgameattribute("sight_radius1", 50);
end

if getgameattribute("hard_shell") == 1 then
  MaxArmour = 0.99;
  else
  MaxArmour = 0.80;
end

setgameattribute("armour", min(getgameattribute("armour"), MaxArmour)*0.8);

SightRadius = getgameattribute("sight_radius1" );
Armour = min(getgameattribute( "armour" ), 0.85);

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
  MaxRange = min(MaxRange, 40);
end

--armour = getgameattribute("armour");

----------------------------------------
--Utility functions
----------------------------------------



function combine_creature()
if   getgameattribute("speed_max") == 0 and
  getgameattribute("waterspeed_max") == 0 and
  getgameattribute("airspeed_max") == 0 then
  setgameattribute("speed_max",8.0)
end

function setattribute( attribute_string, value )
  setgameattribute(attribute_string,value);
  setuiattribute(attribute_string,value);
end

function Rank( x, rank_upper_bounds )
  -- Find where x falls in the array of ranges.
  local i = 1;
  while rank_upper_bounds[i] do
    if x <= rank_upper_bounds[i] then
      return i;
    end
    i = i + 1;
  end
  return i;
end

function Power(d, h, a)
  return (d*h/(1-a))^(1/1.59);
end



------------------------------------------
----Ranking and Power
------------------------------------------

FlyerRankMod = 1.1

Rank2Power = 60;
Rank3Power = 120;
Rank4Power = 220;
Rank5Power = 400;
if getgameattribute("is_flyer") == 1 then
  Rank2Power = Rank2Power * FlyerRankMod;
  Rank3Power = Rank3Power * FlyerRankMod;
  Rank4Power = Rank4Power * FlyerRankMod;
  Rank5Power = Rank5Power * FlyerRankMod;
end

Power = Power(TotalDamage*FlyerDamageMod, Hitpoints, Armour);

PowerRankThresholds = { Rank2Power, Rank3Power, Rank4Power, Rank5Power };
--61.5, 104.3, 180.1, 346.5

-- Final rank of creature.  0 = I, 1 = II, 2 = III, 3 = IV, 4 = V
CreatureRank = Rank( Power, PowerRankThresholds );



--Arrays in lua suck...



------------------------------------------
--Health Limits
------------------------------------------

if RangeDamage < 1 then
  setgameattribute("melee4_damage", getgameattribute("melee4_damage")+DamageDiff);
  else
  setgameattribute("melee4_damage", getgameattribute("melee4_damage")+DamageDiff*MeleeRamageMod);
  if Damage4 > Damage5 and Damage4 > Damage2 and Damage4 > Damage8 and Damage4 > Damage3 then
    setgameattribute("range4_damage", getgameattribute("range4_damage")+ DamageDiff*RangeDamageMod/(1+(MaxRange/35)));
    elseif Damage5 > Damage4 and Damage5 > Damage2 and Damage5 > Damage8 and Damage5 > Damage3 then
      setgameattribute("range5_damage", getgameattribute("range5_damage")+ DamageDiff*RangeDamageMod/(1+(MaxRange/35)));
      elseif Damage2 > Damage4 and Damage2 > Damage5 and Damage2 > Damage8 and Damage2 > Damage3 then
      setgameattribute("range2_damage", getgameattribute("range2_damage")+ DamageDiff*RangeDamageMod/(1+(MaxRange/35)));
      elseif Damage8 > Damage4 and Damage8 > Damage5 and Damage8 > Damage2 and Damage8 > Damage3 then
      setgameattribute("range8_damage", getgameattribute("range8_damage")+ DamageDiff*RangeDamageMod/(1+(MaxRange/35)));
      else
      setgameattribute("range3_damage", getgameattribute("range3_damage")+ DamageDiff*RangeDamageMod/(1+(MaxRange/35)));
  end
end

if RangeDamage < 1 then
  setgameattribute("total_damage", TotalDamage);
  else
  setgameattribute("total_damage", getgameattribute("melee_damage"));
end

setgameattribute("Power", Power(TotalDamage*FlyerDamageMod, Hitpoints, Armour));

----------------------------------------
--UI stuff
----------------------------------------

--setgameattribute("DamagePerHit", DamagePerHit);


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
  { "range3_max", 1, 0, nil, {0.0, 5.0, 9.0, 16.0}, "range3_max", 1 },
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

-- Tail ranged attacks (chem spray)
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

if checkgameattribute( "range3_damage" ) == 1 then
  val = getgameattribute( "range3_damage" );
  if (val and val > 0) then
      rating = Rank( val, {0.0,12.0,20.0,26.0} );
      setattribute( "range3_damage_rating", rating - 1 );
    setattribute( "range3_damage_val", val );
  end
end

----------------------------------------
-- Cost Calculations
----------------------------------------

-- Total renewable resource cost of creature.
CostRenew = 0;
-- Total gatherable resource cost of creature.
CostGather = 0;

--Rank based costs are based on physical rank.  Minimum ranks from abilities are NOT taken into account.
if (CreatureRank == 1) then
  max_power = Rank2Power;
  CostGather = 70;
  elseif (CreatureRank == 2) then
    max_power = Rank3Power;
    CostGather = 110;
    elseif (CreatureRank == 3) then
        max_power = Rank4Power;
        CostGather = 170;
        elseif (CreatureRank == 4) then
            max_power = Rank5Power;
        CostGather = 320;
            else
          max_power = 960;
          CostGather = 500;
end

----------------------------------------
--Ability Cost and Min Rank
----------------------------------------
--Ability cost variables

if (  getgameattribute("stink_attack")==1 or
  getgameattribute("electric_burst")==1 or
  getgameattribute("quill_burst")==1 or
  getgameattribute("frenzy_attack")==1 or
  getgameattribute("plague_attack")==1 or
  getgameattribute("web_throw")==1 or
  getgameattribute("assassinate")==1 or
  getgameattribute("soiled_land")==1 or
  getgameattribute("flash")==1) then
    EnduranceCost = 9 * CreatureRank;
    else
    Triggered_ability = 0;
    EnduranceCost = 5;
end
-- Unusable abilities removed
if (Damage2>0) or (Damage4>0) or (Damage5>0) or (Damage8>0) or (Damage3>0) then
  setgameattribute( "charge_attack", 0 );
  setgameattribute( "leap_attack", 0 );
  --setgameattribute( "speed_boost", 0 );
end

-- make sure flyers and swimmers don't have and are not charged for certain abilities
if ( getgameattribute("is_flyer") == 1 ) then

  setgameattribute( "can_dig", 0 );
  setgameattribute( "leap_attack", 0 );
  setgameattribute( "charge_attack", 0 );
  setgameattribute( "can_SRF", 0);
end

if ( getgameattribute("is_swimmer") == 1 and getgameattribute("is_land") == 0) then
  setgameattribute( "can_dig", 0 );
end

--if getgameattribute("loner") == 1 then
--  setgameattribute("herding", 0);
--  setgameattribute("pack_hunter", 0);
--end

if getgameattribute("hard_shell") == 1 then
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
-- { ability_type, ability_id, minrank, costrenew, costgather, costrenew plus perrank, Rank that the increase starts at }
  --All special case abilities are charged after Power discount is applied, this is presently only to abilities that already have
  --Power as a factor in their cost and a number of special abilities either because the ability is potentially problematic with the Power discount
  --Note that the special case ranks are still controlled in this array
  --MinRank 0
  { ABT_Ability, "is_immune", 0, 10, 0, 5 },

  { ABT_Ability, "poison_pincers", 3, 10, 0, 5 },

  { ABT_Ability, "keen_sense", 0, 10, 0, 0 },

  { ABT_Ability, "can_dig", 0, 10, 0, 10 },

  { ABT_Ability, "sonar_pulse", 0, 15, 0, 5 },

  { ABT_Ability, "is_stealthy", 0, 20, 0, 10 },

  { ABT_Ability, "stink_attack", 0, 50, 0, 5 },

  { ABT_Ability, "stink",0, 50, 0, 5 },

  { ABT_Ability, "flash", 0, 0, 0, 0 },

  { ABT_Ability, "end_bonus", 0, EnduranceCost, 0 },

   { ABT_Ability, "speed_boost", 0, 0, 0, 0},

   { ABT_Ability, "overpopulation", 2, 0, 0, 0},

  { ABT_Ability, "poplow", 1, 3, 0, 3},

  --MinRank 1
  { ABT_Ability, "herding", 1, 0, 0, 0 },       --Special Case

  { ABT_Ability, "pack_hunter", 1, 0, 0, 0 }, --Special Case

  { ABT_Ability, "regeneration", 1, 0, 0, 0 }, --Special Case

  { ABT_Ability, "frenzy_attack", 1, 25, 0, 5 },

  { ABT_Ability, "plague_attack", 1, 50, 0, 5 },

  { ABT_Ability, "Autodefense", 1, 15, 0, 5},

  { ABT_Ability, "assassinate", 1, 10, 10, 20},

  { ABT_Ability, "can_SRF", 1, 15, 0, 10},

  --MinRank 2
  { ABT_Ability, "quill_burst", 2, 0, 0, 0 },   --Special Case

  { ABT_Ability, "leap_attack", 2, 15, 0, 5 },

  { ABT_Ability, "is_swimmer", 2, 0, 0 },

  { ABT_Ability, "deflection_armour", 2, 30, 10, 20 },

  { ABT_Ability, "infestation", 2, 30, 0, 0 },

  --MinRank 3
  { ABT_Ability, "hard_shell", 3, 30, 30, 5 }, --Special Case

  { ABT_Ability, "charge_attack", 3, 15, 0, 5 },

  { ABT_Ability, "is_flyer", 3, 25, 0, 15 },

  { ABT_Ability, "electric_burst", 2, 0, 0, 0 }, --Special Case

  { ABT_Ability, "poison_touch", 3, 30, 0, 10 },

  { ABT_Ability, "tiny", 3, 18, 0, 4 },

  { ABT_Ability, "web_throw", 3, 0, 0, 0 },  --Special Case

   { ABT_Ability, "poison_bite", 3,  0, 0, 0}, --Special Case

   { ABT_Ability, "poison_sting", 3,  0, 0, 10},    --Special Case

  --MinRank 4
  { ABT_Ability, "loner", 2, 0, 0, 0 },   --Special Case

  { ABT_Ability, "soiled_land", 3, 0, 0, 0},  --Special Case

  --MinRank 2
  { ABT_Range, DT_Electric, 2, 0, 0 },

  { ABT_Range, DT_Poison, 3, 0, 0 },

  --MinRank 3
  { ABT_Range, DT_Sonic, 3, 30, 0 },

  { ABT_Range, DT_VenomSpray, 3, 0, 0 },


  --MinRank 0
  { ABT_Melee, DT_BarrierDestroy, 0, 20, 0, 5},

  --MinRank 2
  { ABT_Melee, DT_HornNegateFull, 2, 30, 0, 10 },

  --MinRank 3
  { ABT_Melee, DT_HornNegateArmour, 3, 30, 0, 10 },

  { ABT_Melee, DT_Poison, 3, 0, 0, 0 },
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




-- Total the costs and find min rank for all abilities.
 for n, ab in AbilityData do
  -- If we have this ability...
  if ABT_CheckFunctions[ab[AB_AbilityType]]( ab[AB_Id] ) == 1 then
    -- figure out what rank we should start increasing at
    -- 1  cost_renew_inc_start_rank = ab[AB_MinRank];
    -- 1  if ab[AB_CostRenewIncrementStartRank] then
    -- 1  cost_renew_inc_start_rank = ab[AB_CostRenewIncrementStartRank];
     -- 1  end

    -- add on the costs.
    if ab[AB_CostRenewIncrement] then
      CostRenew = CostRenew + ab[AB_CostRenewIncrement] * (CreatureRank - 1);
    end

  end
end


----------------------------------------
--Cost Modifiers.
----------------------------------------



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
HasDirectRange = nil;
HasArtillery = nil;

--Ranged unit minimum rank
if RangeDamage > 0 then
  CreatureRank = max(CreatureRank, 2);
end

BodyPartsThatCanHaveRange = { 2, 3, 4, 5, 8 };
RangeCostMult   = 1.0;

for index, part in BodyPartsThatCanHaveRange do
  part_damage = getgameattribute( "range" .. part .. "_damage" );
  if ( part_damage > 0 ) then
    -- if not artillery range
    if ( range_artillerytype( part ) == 0 ) then
      HasDirectRange = 1;
      RangeCostMult = 1.15;
      CostRenew = CostRenew + 30 + 10*(CreatureRank - 1);
    else
      HasArtillery = 1;
      RangeCostMult = 1.2;
      CostRenew = CostRenew + 30 + 10*(CreatureRank - 1);
    end
  end
end

if (getgameattribute("loner") == 1) then
  LonerCost = 1.2;
  else
  LonerCost = 1.0;
end

SpeedCost = LonerCost*Speed*0.75;
SightCost = 0.4 * LonerCost * SightRadius;

-- Dedicated swimmer cost modifiers
ArtPureSwimmerCostMod = 0.85;
RancePureSwimmerCostMod = 0.75;
MeleePureSwimmerCostMod = 0.4;


-- Reduce cost if unit is a dedicated swimmer
if   getgameattribute("speed_max") == 0 and
  getgameattribute("waterspeed_max") > 0 and
  getgameattribute("airspeed_max") == 0 then

  if HasDirectRange == 1 then
    CostGather = CostGather * RancePureSwimmerCostMod;
  end
  if HasArtillery == 1 then
    CostGather = CostGather * ArtPureSwimmerCostMod;
  end
  if HasDirectRange == nil and HasArtillery == nil then
    CostGather = CostGather * MeleePureSwimmerCostMod;
  end

  if HasDirectRange == 1 and HasArtillery == 1 then
    CostGather = CostGather/RancePureSwimmerCostMod;
  end
end
if   getgameattribute("speed_max") == 0 and
  getgameattribute("waterspeed_max") > 0 and
  getgameattribute("airspeed_max") == 0 then

  if HasDirectRange == 1 then
    CostRenew = CostRenew * RancePureSwimmerCostMod;
  end
  if HasArtillery == 1 then
    CostRenew = CostRenew * ArtPureSwimmerCostMod;
  end
  if HasDirectRange == nil and HasArtillery == nil then
    CostRenew = CostRenew * MeleePureSwimmerCostMod;
  end

  if HasDirectRange == 1 and HasArtillery == 1 then
    CostRenew = CostRenew/RancePureSwimmerCostMod;
  end
end

--more ability costs
if getgameattribute("herding") == 1 then
  CostRenew = CostRenew + ((Power(TotalDamage * FlyerDamageMod, Hitpoints, min(Armour * 1.5, 0.75)) / Power) -1) * 180 * RangeCostMult * 1.2;
end

if getgameattribute("hard_shell") == 1 then
  CostRenew = CostRenew + ((Power((TotalDamage+1)*FlyerDamageMod, Hitpoints, getgameattribute("armour"))/Power)-1)*200*RangeCostMult;
end

if getgameattribute("hard_shell") == 1 then
  CostGather = CostGather + ((Power((TotalDamage+1)*FlyerDamageMod, Hitpoints, getgameattribute("armour"))/Power)-1)*200*RangeCostMult;
end

if getgameattribute("pack_hunter") == 1 then
  CostRenew = CostRenew + ((Power(TotalDamage*1.4*FlyerDamageMod, Hitpoints, Armour)/Power)-1)*180*RangeCostMult;
end

if getgameattribute("flash") == 1 then
  CostRenew = CostRenew + (50+(10*CreatureRank));
end
if getgameattribute("headflashdisplay") == 1 then
  CostRenew = CostRenew + (50+(10*CreatureRank));
end

if (CreatureRank == 3) and (getgameattribute("is_lioness") == 1) and (getgameattribute("pack_hunter") == 1) and (getgameattribute("range4_damage") > 0) then
  CostGather = CostGather*1.2;
end


Rank1HealthLim = 1000;
Rank2HealthLim = 2500;
Rank3HealthLim = 7500;
Rank4HealthLim = 12000;
Rank5HealthLim = 35000;

if getgameattribute("hard_shell") == 1 then
  Rank1HealthLim = 1000000;
  Rank2HealthLim = 1000000;
  Rank3HealthLim = 1000000;
  Rank4HealthLim = 1000000;
  Rank5HealthLim = 1000000;
end


ActualHealth = Hitpoints/(1-Armour);
if CreatureRank == 1 then
  MaxHealth = Rank1HealthLim;
end
if CreatureRank == 2 then
  MaxHealth = Rank2HealthLim;
end
if CreatureRank == 3 then
  MaxHealth = Rank3HealthLim;
end
if CreatureRank == 4 then
  MaxHealth = Rank4HealthLim;
end
if CreatureRank == 5 then
  MaxHealth = Rank5HealthLim*3;
end

RegenHealth = (1 + 0.5*CreatureRank);
if getgameattribute("regeneration") == 1 then
  CostRenew = CostRenew +10+ Power(TotalDamage*FlyerDamageMod, RegenHealth, Armour)*6*ActualHealth/MaxHealth;
end


if getgameattribute("poison_bite") == 1 or getgameattribute("poison_sting") == 1 then
    CostRenew = CostRenew + ((Power(TotalDamage*1.1*FlyerDamageMod, Hitpoints/0.75, Armour)/Power) - 1)*200;
end

BuildTime = (8+24 * CreatureRank)*(Power*1.1/max_power)*PopMod;
CostGather = (CostGather + SpeedCost + SightCost)*(Power*1.1/max_power)*RangeCostMult*1.1+(2/CreatureRank)*1.25;
CostRenew = CostRenew * (Power*1.1/max_power)*1.2;

if getgameattribute("overpopulation") == 0 then
BuildTime = max(BuildTime, 16);
else
BuildTime = max(BuildTime, 8);
end

--no discount ability costs

if getgameattribute("quill_burst") == 1 then
  CostRenew = CostRenew + 20 + 10*CreatureRank;
end

if getgameattribute("electric_burst") == 1 then
  CostRenew = CostRenew + 10 + 10*CreatureRank;
end

if getgameattribute("web_throw") == 1 then
   CostRenew = CostRenew + 120/(LonerCost^4) + CreatureRank * 20/(LonerCost^4);
   CostGather = CostGather * 1.3;
end

if getgameattribute("soiled_land") == 1 then
  CostRenew = CostRenew + 100;
end

if getgameattribute("loner") == 1 then
  CostRenew = CostRenew + ((Power(TotalDamage*6*FlyerDamageMod, Hitpoints/0.25, Armour)/Power)-1)*15*CreatureRank;
  CostGather = CostGather + 3 * SpeedCost;
end


----------------------------------------
--Post ui tricks
----------------------------------------
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



----------------------------------------
--Diagnostic Code
----------------------------------------
--CostGather = Power*10;
--CostRenew = BuildTime/8;
----------------------------------------
--Final Code
----------------------------------------


----------------------------------------
--TELLURIAN CHANGES
----------------------------------------

--Health Cap for Level 3

if Hitpoints/(1-Armour) > 750 and CreatureRank == 2 then
  CreatureRank = 3;
end

--Minimum Cost for Fliers

if getgameattribute("is_flyer") == 1 then
  CostGather = CostGather+30;
  CostRenew = CostRenew+30;
end


if HasArtillery == 1 and getgameattribute("is_flyer") == 1 then
  CostRenew = CostRenew+50;
  CostGather = CostGather+40;
end

--if getgameattribute("bobcost") == 1 then
--  CostRenew = CostRenew+20;
--end


if getgameattribute("wtf") == 1 then
  CostRenew = 0;
end
if getgameattribute("wtf") == 1 then
  CostGather = 0;
end
if getgameattribute("wtf") == 1 then
  BuildTime = 0;
end
if getgameattribute("wtf") == 1 then
  CreatureRank = 1;
end


if CreatureRank == 3 then
  CostRenew = CostRenew*0.75
end

if CreatureRank == 4 then
  CostGather = CostGather*0.85
end

if CreatureRank == 4 then
  CostRenew = CostRenew*0.85
end

if CreatureRank == 5 then
  CostRenew = CostRenew*0.95
end

if CreatureRank == 5 then
  CostGather = CostGather*0.95
end

if CreatureRank == 1 then
  BuildTime = BuildTime*1.2
end

if getgameattribute("hard_shell") == 1 and getgameattribute("regeneration") == 1 then
  CostRenew = CostRenew+40;
end


if getgameattribute ("armour") >= 0.6 and getgameattribute("regeneration") == 1 then
  CostRenew = CostRenew+30;
end

if CreatureRank <= 2 and getgameattribute ("armour") >= 0.5 then
  CostRenew = CostRenew+15;
end
if CreatureRank <= 2 and getgameattribute ("damage") >= 16 then
  CostRenew = CostRenew+20;
end
if CreatureRank <= 2 and getgameattribute ("damage") >= 12 and getgameattribute("pack_hunter") == 1 then
  CostRenew = CostRenew+20;
end

if HasArtillery == 1 then
  CostRenew = CostRenew*1.0;
end

if HasArtillery == 1 then
  CostGather = CostGather*1.0;
end

if getgameattribute("is_flyer") == 0 and getgameattribute("soiled_land") == 1 and getgameattribute("can_SRF") == 0 then
  CostRenew = CostRenew*0.75;
end

if getgameattribute("is_flyer") == 0 and getgameattribute("soiled_land") == 1 and getgameattribute("can_SRF") == 1 then
  CostRenew = CostRenew*0.875;
end

if getgameattribute("end_bonus") == 1 and getgameattribute("frenzy_attack") == 1 then
  CostRenew = CostRenew*1.25;
end

if getgameattribute("herding") == 1 and getgameattribute("armour") >= 0.4 then
  CostRenew = CostRenew*1.05;
end

if getgameattribute("herding") == 1 and getgameattribute("armour") >= 0.59 then
  CostGather = CostGather*1.05;
end


------------------------------------------
--Population Calculator
------------------------------------------

if (getgameattribute("is_flyer") == 1) then
  if (getgameattribute("poplow") == 1) or (getgameattribute("poplowtorso") == 1) then
  Pop = 1;

  else

--  Pop = (Power/150)*((-0.4*CreatureRank)+3.2);
  Pop = (CreatureRank-2)*((-0.2*CreatureRank)+3.2);

  end


  else
    if (getgameattribute("poplow") == 1) or (getgameattribute("poplowtorso") == 1) then
    Pop = 1;

    else

    Pop = Power/150;

    end
end

----------------------------------------
--End of Ripping off SMod
----------------------------------------

setattribute( "buildtime", 10 );

setgameattribute("constructionticks", BuildTime);

--Final Output
setattribute( "creature_rank", CreatureRank );
setattribute( "costrenew", CostRenew );
setattribute( "cost", CostGather );
setattribute( "popsize", Pop )

end
