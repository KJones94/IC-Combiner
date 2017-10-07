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

        public LuaHandler()
        {
            Attrcombiner = new Script();
            Attrcombiner.Options.ScriptLoader = new FileSystemScriptLoader();
            SetupGlobals();
            
        }

        public void LoadScript()
        {
            //Attrcombiner.DoFile("../../Scripts/littlecombiner.lua");
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

        double MoonSharpFactorial()
        {
            string script = @"    
		-- defines a factorial function
		function fact (n)
			if (n == 0) then
				return 1
			else
				return n*fact(n - 1)
			end
		end

		return fact(5)";

            DynValue res = Script.RunString(script);
            return res.Number;
        }
    }
}
