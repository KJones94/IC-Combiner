namespace Combiner.Engine
{
	using System;

	using Combiner.Utility;

	using MoonSharp.Interpreter;
	using MoonSharp.Interpreter.Loaders;

	public class LuaCreatureProxy
	{
		private Script Attrcombiner { get; set; }
		private CreatureBuilder Creature { get; set; }

		public LuaCreatureProxy()
		{
			this.Attrcombiner = new Script();
			this.Attrcombiner.Options.ScriptLoader = new FileSystemScriptLoader();
			this.SetupGlobals();
		}

		public void LoadScript(CreatureBuilder creature)
		{
			this.Creature = creature;

			// Attrcombiner.DoFile(Attributes.Attrcombiner);
			this.Attrcombiner.DoFile(DirectoryConstants.Testcombiner);
		}

		private void SetupGlobals()
		{
			// TODO: should I use DynValue or regular types
			this.Attrcombiner.Globals["getgameattribute"] = (Func<string, double>)this.GetGameAttribute;
			this.Attrcombiner.Globals["checkgameattribute"] = (Func<string, double>)this.CheckGameAttribute;
			this.Attrcombiner.Globals["setgameattribute"] = (Action<string, double>)this.SetGameAttribute;
			this.Attrcombiner.Globals["setuiattribute"] = (Action<string, double>)this.SetUIAttribute;
			this.Attrcombiner.Globals["max"] = (Func<double, double, double>)this.Max;
			this.Attrcombiner.Globals["min"] = (Func<double, double, double>)this.Min;
			this.Attrcombiner.Globals["hasmeleedmgtype"] = (Func<double, double>)this.HasMeleeDmgType;
			this.Attrcombiner.Globals["hasrangedmgtype"] = (Func<double, double>)this.HasRangeDmgType;

			this.Attrcombiner.Globals["DT_BarrierDestroy"] = 4;
			this.Attrcombiner.Globals["DT_HornNegateFull"] = 2;
			this.Attrcombiner.Globals["DT_HornNegateArmour"] = 2;
			this.Attrcombiner.Globals["DT_Poison"] = 1; // Should this be 256?

			this.Attrcombiner.Globals["DT_Electric"] = 8;
			this.Attrcombiner.Globals["DT_Sonic"] = 16;
			this.Attrcombiner.Globals["DT_VenomSpray"] = 256; // Should this be 1?
		}

		private double GetGameAttribute(string key)
		{
			double value;
			if (this.Creature.GameAttributes.TryGetValue(key, out value))
			{
				return value;
			}

			return 0;
		}

		private double CheckGameAttribute(string key)
		{
			double value;
			if (this.Creature.GameAttributes.TryGetValue(key, out value))
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
			if (this.Creature.GameAttributes.ContainsKey(key))
			{
				this.Creature.GameAttributes[key] = value;
			}
		}

		private void SetUIAttribute(string key, double value)
		{
			// Lua needs to hook into this, but shouldn't do anything
		}

		private double HasMeleeDmgType(double value)
		{
			if (this.Creature.GameAttributes[Attributes.Melee2Type] == value)
			{
				return 1;
			}

			if (this.Creature.GameAttributes[Attributes.Melee3Type] == value)
			{
				return 1;
			}

			if (this.Creature.GameAttributes[Attributes.Melee4Type] == value)
			{
				return 1;
			}

			if (this.Creature.GameAttributes[Attributes.Melee5Type] == value)
			{
				return 1;
			}

			if (this.Creature.GameAttributes[Attributes.Melee8Type] == value)
			{
				return 1;
			}

			return 0;
		}

		private double HasRangeDmgType(double value)
		{
			if (this.Creature.GameAttributes[Attributes.Range2Type] == value)
			{
				return 1;
			}

			if (this.Creature.GameAttributes[Attributes.Range3Type] == value)
			{
				return 1;
			}

			if (this.Creature.GameAttributes[Attributes.Range4Type] == value)
			{
				return 1;
			}

			if (this.Creature.GameAttributes[Attributes.Range5Type] == value)
			{
				return 1;
			}

			if (this.Creature.GameAttributes[Attributes.Range8Type] == value)
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

			return y;
		}

		private double Min(double x, double y)
		{
			if (x <= y)
			{
				return x;
			}

			return y;
		}
	}
}
