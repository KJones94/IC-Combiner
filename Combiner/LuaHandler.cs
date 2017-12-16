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
        Script Attrcombiner { get; set; }
		Creature Creature { get; set; }

        public LuaHandler()
        {
            Attrcombiner = new Script();
            Attrcombiner.Options.ScriptLoader = new FileSystemScriptLoader();
            SetupGlobals();
        }

        public void LoadScript()
        {
            Attrcombiner.DoFile("../../Scripts/attrcombiner.lua");
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
            string getGameAttribute = @"
            function getgameattribute(s)
                return 0
            end";

            string setGameAttribute = @"
function setgameattribute(s, d)
    return 0
end";

            string max = @"
function max(d1, d2)
    return 1
end";
            string min = @"
function min(d1, d2)
    return 0
end";
            //Attrcombiner.RequireModule("math");
            Attrcombiner.DoString(getGameAttribute);
            Attrcombiner.DoString(setGameAttribute);
            Attrcombiner.DoString(max);
            Attrcombiner.DoString(min);
        
        }

        private DynValue GetGameAttribute(string s)
		{

			return new DynValue();
		} 

		private void SetGameAttribute(string s)
		{

		}

		private DynValue Max(DynValue x, DynValue y)
		{
			if (x.Number >= y.Number)
			{
				return x;
			}
			else
			{
				return y;
			}
		}

		private DynValue Min(DynValue x, DynValue y)
		{
			if (x.Number <= y.Number)
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
