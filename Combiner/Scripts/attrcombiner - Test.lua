----------------------------------------
--Uncapped Major Variables
----------------------------------------

damagedif = 0;

speed_max   = getgameattribute( "speed_max" );
airspeed_max   = getgameattribute( "airspeed_max" );
waterspeed_max   = getgameattribute( "waterspeed_max" );
speed     = max( speed_max, max( airspeed_max, waterspeed_max ) );

size     = getgameattribute( "size" );
hitpoints   = getgameattribute( "hitpoints" );
damagem   = getgameattribute( "melee_damage" );

range_max1   = getgameattribute("range4_max");
range_max2  = getgameattribute("range2_max");
range_max3   = getgameattribute("range5_max");
range_max4   = getgameattribute("range8_max");
range_max5   = getgameattribute("range3_max");
range_max   = max(range_max1, max(range_max2, max(range_max3, max(range_max4, range_max5))));

damage2   = getgameattribute( "range2_damage" );
damage3   = getgameattribute( "range3_damage" );
damage4   = getgameattribute( "range4_damage" );
damage5   = getgameattribute( "range5_damage" );
damage8   = getgameattribute( "range8_damage" );


GlobalCostMod   = 1.0;
sonicRankMod  = 1.0;
RangeCostMult   = 1.0;
--flyerMod   = 0.75;

if (getgameattribute("range4_dmgtype") == 16) then
  damage4 = damage4 * sonicRankMod;
end

damager = max (damage2, max (damage4, max(damage5, max(damage3, damage8))));

if (range_max > 0) then
  damager = damager*(1+(range_max/35));
end

if (getgameattribute("loner") == 1) then
  LonerCost = 1.2;
  else
  LonerCost = 1.0;
end

if (getgameattribute("fml") == 1) then
  blehCost = 0.0;
  else
  blehCost = 1.0;
end

if (getgameattribute("headflashdisplay") == 1) then
  bmCost = 0.0;
  else
  bmCost = 1.0;
end
if (getgameattribute("fly") == 1) then
  cheese = 1.4;
  else
  cheese = 1.0;
end


if airspeed_max > 0 and damager < 1 then
  mflyerrankmod = 1.0;
  else
  mflyerrankmod = 1.0
end

--if airspeed_max > 0 then
--  if getgameattribute("melee4_damage") > 0 then
--    setgameattribute( "melee4_damage", getgameattribute("melee4_damage")*flyerMod );
--  end
--  if getgameattribute("melee2_damage") > 0 then
--    setgameattribute( "melee2_damage", getgameattribute("melee2_damage")*flyerMod );
--  end
--  if getgameattribute("melee3_damage") > 0 then
--    setgameattribute( "melee3_damage", getgameattribute("melee3_damage")*flyerMod );
--  end
--  if getgameattribute("melee5_damage") > 0 then
--    setgameattribute( "melee5_damage", getgameattribute("melee5_damage")*flyerMod );
--  end
--  if getgameattribute("melee8_damage") > 0 then
--    setgameattribute( "melee8_damage", getgameattribute("melee8_damage")*flyerMod );
--  end
--  if getgameattribute("melee6_damage") > 0 then
--    setgameattribute( "melee6_damage", getgameattribute("melee6_damage")*flyerMod );
--  end
--  setgameattribute("melee_damage", damagem*flyerMod);
--end
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

----------------------------------------
--Nonstandard additions
----------------------------------------

--function checkInt(i)
--while i > 0 do
--  i = i - 1;
--end
--return i == 0;
--end
--
--
--setgameattribute("speed_max", speed_max/(getgameattribute("size")^(-0.18))*(getgameattribute("size")^(0.1));

if (range_max3 > 0) then
   AttackRateMain = 80/speed;
   else
   AttackRateMain = 40/speed;
end

--if getgameattribute("loner") == 0 then
--  setgameattribute ("speed_boost", 0);
--end

---if getgameattribute("speed_boost") == 1 then
--  AttackRateMain  = 0.01;
--end

if airspeed_max > 0 then
  AttackRateMain = 4;
end

setgameattribute("airspeed_max", airspeed_max*cheese);

--setgameattribute( "melee4_rate", AttackRateMain );
--setgameattribute( "melee2_rate", AttackRateMain );
--setgameattribute( "melee3_rate", AttackRateMain );
--setgameattribute( "melee5_rate", AttackRateMain );
--setgameattribute( "melee8_rate", AttackRateMain );
--setgameattribute( "melee6_rate", AttackRateMain );
--setgameattribute( "range4_rate", AttackRateMain );
--setgameattribute( "range2_rate", AttackRateMain );
--setgameattribute( "range5_rate", AttackRateMain );

Popmult = 1.0;
if getgameattribute("overpopulation") == 1 then
Popmult = 0.5;
end

--if getgameattribute("sight_radius1") < range_max then
--    setgameattribute("sight_radius1", range_max);
--end

setgameattribute("sight_radius1", max(getgameattribute("sight_radius1"), 20));

------------------------------------------
--Capped Major Variables
------------------------------------------
if getgameattribute("sight_radius1") > 50 then
    setgameattribute("sight_radius1", 50);
end

if getgameattribute("hard_shell") == 1 then
  maxArmour = 0.99;
  else
  maxArmour = 0.80;
end

setgameattribute("armour", min(getgameattribute("armour"), maxArmour)*0.8);

sight_radius1 = getgameattribute("sight_radius1" );
armour = min(getgameattribute( "armour" ), 0.85);

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
  range_max = min(range_max, 40);
end

--armour = getgameattribute("armour");

----------------------------------------
--Utility functions
----------------------------------------
-- Total renewable resource cost of creature.
CostRenew = 0;
-- Total gatherable resource cost of creature.
CostGather = 0;
-- Final rank of creature.  0 = I, 1 = II, 2 = III, 3 = IV, 4 = V
CreatureRank = 0;
-- Minimum rank of creature, based on abilities.
MinRank = 0;

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

function HealthLimit(n)
  dhproduct = damage * hitpoints;
  if hitpoints < n and armour > 0.5 then
    rawpower = damage*hitpoints/(1-armour)
    armourdif = hitpoints/n - 1 + armour;
    armour = armour - armourdif;
    setgameattribute("armour", armour);
    damagedif = rawpower*(1-armour)/hitpoints - damage;
    damage = damage + damagedif;
    else
    healthdif = hitpoints - n*(1-armour);
    hitpoints = hitpoints - healthdif;
    setgameattribute("hitpoints", hitpoints);
    damagedif = dhproduct/hitpoints - damage;
    damage = damage + damagedif;
  end
end

------------------------------------------
----Ranking and Coal Cost
------------------------------------------

flyerrankmod = 1.1

rank2pow = 60;
rank3pow = 120;
rank4pow = 220;
rank5pow = 400;
if getgameattribute("is_flyer") == 1 then
  rank2pow = rank2pow * flyerrankmod;
  rank3pow = rank3pow * flyerrankmod;
  rank4pow = rank4pow * flyerrankmod;
  rank5pow = rank5pow * flyerrankmod;
end

power = Power(damage*mflyerrankmod, hitpoints, armour);

speedCost = LonerCost*speed*0.75;
sightCost = 0.4 * LonerCost * sight_radius1;

rank_cmp = { rank2pow, rank3pow, rank4pow, rank5pow };
--61.5, 104.3, 180.1, 346.5

CreatureRank = Rank( power, rank_cmp );
PhysRank = CreatureRank;
--Rank based costs are based on physical rank.  Minimum ranks from abilities are NOT taken into account.
if (CreatureRank == 1) then
  max_power = rank2pow;
  CostGather = 70;
  elseif (CreatureRank == 2) then
    max_power = rank3pow;
    CostGather = 110;
    elseif (CreatureRank == 3) then
        max_power = rank4pow;
        CostGather = 170;
        elseif (CreatureRank == 4) then
            max_power = rank5pow;
        CostGather = 320;
            else
          max_power = 960;
          CostGather = 500;
end


--Arrays in lua suck...



------------------------------------------
--Health Limits
------------------------------------------
rank1lim = 1000;
rank2lim = 2500;
rank3lim = 7500;
rank4lim = 12000;
rank5lim = 35000;

if getgameattribute("hard_shell") == 1 then
  rank1lim = 1000000;
  rank2lim = 1000000;
  rank3lim = 1000000;
  rank4lim = 1000000;
  rank5lim = 1000000;
end

if hitpoints/(1-armour) > rank1lim and CreatureRank == 1 then
  HealthLimit(rank1lim);
end

if hitpoints/(1-armour) > rank2lim and CreatureRank == 2 then
  HealthLimit(rank2lim);
end

if hitpoints/(1-armour) > rank3lim and CreatureRank == 3 then
  HealthLimit(rank3lim);
end

--
--if hitpoints/(1-armour) > 1000 and CreatureRank == 4 then
--  CreatureRank = 5;
--end

--if hitpoints/(1-armour) > 1000 and CreatureRank == 5 then
--  setgameattribute(CreatureRank , 5);
--end

if damager < 1 then
  setgameattribute("melee4_damage", getgameattribute("melee4_damage")+damagedif);
  else
  setgameattribute("melee4_damage", getgameattribute("melee4_damage")+damagedif*rangedmmod);
  if damage4 > damage5 and damage4 > damage2 and damage4 > damage8 and damage4 > damage3 then
    setgameattribute("range4_damage", getgameattribute("range4_damage")+ damagedif*rangedrmod/(1+(range_max/35)));
    elseif damage5 > damage4 and damage5 > damage2 and damage5 > damage8 and damage5 > damage3 then
      setgameattribute("range5_damage", getgameattribute("range5_damage")+ damagedif*rangedrmod/(1+(range_max/35)));
      elseif damage2 > damage4 and damage2 > damage5 and damage2 > damage8 and damage2 > damage3 then
      setgameattribute("range2_damage", getgameattribute("range2_damage")+ damagedif*rangedrmod/(1+(range_max/35)));
      elseif damage8 > damage4 and damage8 > damage5 and damage8 > damage2 and damage8 > damage3 then
      setgameattribute("range8_damage", getgameattribute("range8_damage")+ damagedif*rangedrmod/(1+(range_max/35)));
      else
      setgameattribute("range3_damage", getgameattribute("range3_damage")+ damagedif*rangedrmod/(1+(range_max/35)));
  end
end

----------------------------------------
--UI stuff
----------------------------------------

--DamagePerHit = max((max(getgameattribute("range2_damage"), max(getgameattribute("range4_damage"), getgameattribute("range5_damage"))) * AttackRateMain), (getgameattribute("melee_damage") * AttackRateMain));
--setgameattribute("DamagePerHit", DamagePerHit);

if damager < 1 then
  setgameattribute("total_damage", damage);
  else
  setgameattribute("total_damage", getgameattribute("melee_damage"));
end

setgameattribute("Power", Power(damage*mflyerrankmod, hitpoints, armour));
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






































end

