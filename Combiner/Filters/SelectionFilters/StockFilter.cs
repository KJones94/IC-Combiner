namespace Combiner.Filters.SelectionFilters
{
	using System.Collections.ObjectModel;
	using System.IO;
	using System.Linq;

	using Combiner.Models;
	using Combiner.Utility;

	public class StockFilter : SelectionFilter
	{
		public StockFilter()
			: base("Stock") { }

		protected override bool FilterAnySelected(Creature creature)
		{
			return this.Selected.Contains(creature.Left)
				|| this.Selected.Contains(creature.Right);
		}

		protected override bool FilterOnlySelected(Creature creature)
		{
			return this.Selected.Contains(creature.Left)
				&& this.Selected.Contains(creature.Right);
		}

		protected override ObservableCollection<string> InitChoices()
		{
			ObservableCollection<string> choices = new ObservableCollection<string>();
			var stockNames = Directory.GetFiles(DirectoryConstants.StockDirectory).
				Select(s => s.Replace(".lua", string.Empty).Replace(DirectoryConstants.StockDirectory, string.Empty));
			foreach (string stock in stockNames)
			{
				choices.Add(StockNames.ProperStockNames[stock]);
			}

			return choices;
		}

		public override string ToString()
		{
			return nameof(StockFilter);
		}
	}
}
