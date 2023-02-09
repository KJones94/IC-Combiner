using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using MoonSharp.Interpreter.Debugging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Combiner
{
	public class LuaCreatureProxy
	{
		private Script AttrcombinerScript { get; set; }
		private CreatureBuilder Creature { get; set; }

		private string attrPath;

		private DynValue AttrcombinerFunc { get; set; }
		private DynValue CombineCreaturesFunc { get; set; }

		public LuaCreatureProxy(string attrPath)
		{
			AttrcombinerScript = new Script();
			AttrcombinerScript.Options.ScriptLoader = new FileSystemScriptLoader();
			SetupGlobals();
			this.attrPath = attrPath;
			AttrcombinerFunc = AttrcombinerScript.LoadFile(attrPath);
			AttrcombinerScript.Call(AttrcombinerFunc);
			CombineCreaturesFunc = AttrcombinerScript.LoadString("combine_creature();"); ;
		}

		public void LoadScript(CreatureBuilder creature)
		{ 
			Creature = creature;
			//AttrcombinerFunc = AttrcombinerScript.LoadFile(attrPath);
			AttrcombinerScript.Call(CombineCreaturesFunc);
		}

		private void SetupGlobals()
		{
			AttrcombinerScript.Globals["dofilepath"] = (Func<string, string>)LoadExtraFile;

			// TODO: should I use DynValue or regular types
			AttrcombinerScript.Globals["getgameattribute"] = (Func<string, double>)GetGameAttribute;
			AttrcombinerScript.Globals["checkgameattribute"] = (Func<string, double>)CheckGameAttribute;
			AttrcombinerScript.Globals["setgameattribute"] = (Action<string, double>)SetGameAttribute;
			AttrcombinerScript.Globals["setuiattribute"] = (Action<string, double>)SetUIAttribute;
			AttrcombinerScript.Globals["max"] = (Func<double, double, double>)Max;
			AttrcombinerScript.Globals["min"] = (Func<double, double, double>)Min;
			AttrcombinerScript.Globals["floor"] = (Func<double, double>)Floor;
			AttrcombinerScript.Globals["ceil"] = (Func<double, double>)Ceil;
			AttrcombinerScript.Globals["hasmeleedmgtype"] = (Func<double, double>)HasMeleeDmgType;
			AttrcombinerScript.Globals["hasrangedmgtype"] = (Func<double, double>)HasRangeDmgType;

			AttrcombinerScript.Globals["DT_BarrierDestroy"] = 4;
			AttrcombinerScript.Globals["DT_HornNegateFull"] = 4092; // Matt said this
			AttrcombinerScript.Globals["DT_HornNegateArmour"] = 2;
			AttrcombinerScript.Globals["DT_Poison"] = 1; // Should this be 256?

			AttrcombinerScript.Globals["DT_Electric"] = 8;
			AttrcombinerScript.Globals["DT_Sonic"] = 16;
			AttrcombinerScript.Globals["DT_VenomSpray"] = 256; // Should this be 1?
		}

		private string LoadExtraFile(string fileName)
		{
			fileName = fileName.Replace("data:", "");
			var scriptDir = Path.GetDirectoryName(attrPath);
			var fullPath = Path.Combine(scriptDir, fileName);
			var fileFunc = AttrcombinerScript.LoadFile(fullPath);
			AttrcombinerScript.Call(fileFunc);
			return fileName;
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
			else
			{
				Creature.GameAttributes.Add(key, value);
			}
		}

		private void SetUIAttribute(string key, double value)
		{
			// Lua needs to hook into this, but shouldn't do anything
		}

		private double HasMeleeDmgType(double value)
		{
			if (Creature.GameAttributes[Attributes.Melee2Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Attributes.Melee3Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Attributes.Melee4Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Attributes.Melee5Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Attributes.Melee8Type] == value)
			{
				return 1;
			}
			return 0;
		}

		private double HasRangeDmgType(double value)
		{
			if (Creature.GameAttributes[Attributes.Range2Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Attributes.Range3Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Attributes.Range4Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Attributes.Range5Type] == value)
			{
				return 1;
			}
			else if (Creature.GameAttributes[Attributes.Range8Type] == value)
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

		private double Floor(double x)
		{
			return Math.Floor(x);
		}
		private double Ceil(double x)
		{
			return Math.Ceiling(x);
		}
	}
}
