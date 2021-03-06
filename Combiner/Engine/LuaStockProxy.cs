﻿using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class LuaStockProxy
	{
		private Script Script { get; set; }

		public LuaStockProxy()
		{
			Script = new Script();
			Script.Options.ScriptLoader = new FileSystemScriptLoader();
		}

		public Table GetLimbAttributes(string stockFile)
		{
			Script.DoFile(stockFile);
			Table table = Script.Globals["limbattributes"] as Table;
			return table;
		}
	}
}
