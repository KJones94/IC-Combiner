using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combiner
{
    public class LuaHandler
    {
        private Script Attrcombiner { get; set; }
		private CreatureBuilder Creature { get; set; }

        public LuaHandler()
        {
            Attrcombiner = new Script();
            Attrcombiner.Options.ScriptLoader = new FileSystemScriptLoader();
            SetupGlobals();
        }

        public void LoadScript(CreatureBuilder creature)
        {
			Creature = creature;
			//Attrcombiner.DoFile(Utility.Attrcombiner);
			Attrcombiner.DoFile(Utility.Testcombiner);
		}

        public Table GetLimbAttributes(string stockFile)
        {
            Attrcombiner.DoFile(stockFile);
            Table table = Attrcombiner.Globals["limbattributes"] as Table;
            return table;
        }

        public Table GetGlobals()
        {
            return Attrcombiner.Globals as Table;
        }

        private void SetupGlobals()
        {
			// TODO: should I use DynValue or regular types
			Attrcombiner.Globals["getgameattribute"] = (Func<string, double>)GetGameAttribute;
			Attrcombiner.Globals["checkgameattribute"] = (Func<string, double>)CheckGameAttribute;
			Attrcombiner.Globals["setgameattribute"] = (Action<string, double>)SetGameAttribute;
			Attrcombiner.Globals["setuiattribute"] = (Action<string, double>)SetUIAttribute;
			Attrcombiner.Globals["max"] = (Func<double, double, double>)Max;
			Attrcombiner.Globals["min"] = (Func<double, double, double>)Min;
			Attrcombiner.Globals["hasmeleedmgtype"] = (Func<double, double>)HasMeleeDmgType;
			Attrcombiner.Globals["hasrangedmgtype"] = (Func<double, double>)HasRangeDmgType;

			Attrcombiner.Globals["DT_BarrierDestroy"] = 4;
			Attrcombiner.Globals["DT_HornNegateFull"] = 2;
			Attrcombiner.Globals["DT_HornNegateArmour"] = 2;
			Attrcombiner.Globals["DT_Poison"] = 1; // Should this be 256?

			Attrcombiner.Globals["DT_Electric"] = 8;
			Attrcombiner.Globals["DT_Sonic"] = 16;
			Attrcombiner.Globals["DT_VenomSpray"] = 256; // Should this be 1?
		}

        private double GetGameAttribute(string key)
		{
			double value;
			if (Creature.GameAttributes.TryGetValue(key, out value))
			{
				return value;
			}
			return 0;
		}

		private double CheckGameAttribute(string key)
		{
			double value;
			if (Creature.GameAttributes.TryGetValue(key, out value))
			{
				if (value > 0)
				{
					return 1;
				}
			}
			return 0;
		}

		private void SetGameAttribute(string key, double value)
		{
			if (Creature.GameAttributes.ContainsKey(key))
			{
				Creature.GameAttributes[key] = value;
			}
		}

		private void SetUIAttribute(string key, double value)
		{
			// Lua needs to hook into this, but shouldn't do anything
		}

		private double HasMeleeDmgType(double value)
		{
			if (Creature.GameAttributes[Utility.Melee2Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Utility.Melee3Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Utility.Melee4Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Utility.Melee5Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Utility.Melee8Type] == value)
			{
				return 1;
			}
			return 0;
		}

		private double HasRangeDmgType(double value)
		{
			if (Creature.GameAttributes[Utility.Range2Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Utility.Range3Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Utility.Range4Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Utility.Range5Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Utility.Range8Type] == value)
			{
				return 1;
			}
			return 0;
		}

		private double Max(double x, double y)
		{
			if (x >= y)
			{
				return x;
			}
			else
			{
				return y;
			}
		}

		private double Min(double x, double y)
		{
			if (x <= y)
			{
				return x;
			}
			else
			{
				return y;
			}
		}
	}
}
