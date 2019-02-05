namespace Combiner.Engine
{
	using MoonSharp.Interpreter;
	using MoonSharp.Interpreter.Loaders;

	public class LuaStockProxy
	{
		private Script Script { get; set; }

		public LuaStockProxy()
		{
			this.Script = new Script();
			this.Script.Options.ScriptLoader = new FileSystemScriptLoader();
		}

		public Table GetLimbAttributes(string stockFile)
		{
			this.Script.DoFile(stockFile);
			Table table = this.Script.Globals["limbattributes"] as Table;
			return table;
		}
	}
}
