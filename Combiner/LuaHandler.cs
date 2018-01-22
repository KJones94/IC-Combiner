using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combiner
{
    class LuaHandler
    {
        private Script Attrcombiner { get; set; }
		private Creature Creature { get; set; }

        public LuaHandler()
        {
            Attrcombiner = new Script();
            Attrcombiner.Options.ScriptLoader = new FileSystemScriptLoader();
            SetupGlobals();
        }

        public void LoadScript(Creature creature)
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
			Attrcombiner.Globals["setgameattribute"] = (Action<string, double>)SetGameAttribute;
			Attrcombiner.Globals["max"] = (Func<double, double, double>)Max;
			Attrcombiner.Globals["min"] = (Func<double, double, double>)Min;
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

		private void SetGameAttribute(string key, double value)
		{
			if (Creature.GameAttributes.ContainsKey(key))
			{
				Creature.GameAttributes[key] = value;
			}
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
