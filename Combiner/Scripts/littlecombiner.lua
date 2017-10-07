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
