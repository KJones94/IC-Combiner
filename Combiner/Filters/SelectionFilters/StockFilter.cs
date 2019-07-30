using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class StockFilter : SelectionFilter
	{
		public StockFilter()
			: base("Stock") { }

		protected override bool FilterAnySelected(Creature creature)
		{
			return Selected.Contains(creature.Left)
				|| Selected.Contains(creature.Right);
		}

		protected override bool FilterOnlySelected(Creature creature)
		{
			return Selected.Contains(creature.Left)
				&& Selected.Contains(creature.Right);
		}

		protected override Query QueryAnySelected()
		{
			List<Query> queries = new List<Query>();

			foreach (string stock in Selected)
			{
				queries.Add(Query.Or(
					Query.EQ("Left", stock),
					Query.EQ("Right", stock)));
			}

			if (queries.Count == 1)
			{
				return queries.First();
			}
			return Query.Or(queries.ToArray());
		}

		protected override Query QueryOnlySelected()
		{
			List<Query> queries = new List<Query>();

			if (Selected.Count == 1)
			{
				return Query.Or(
					Query.EQ("Left", Selected.First()),
					Query.EQ("Right", Selected.First()));
			}

			for (int i = 0; i < Selected.Count; i++)
			{
				for (int j = i + 1; j < Selected.Count; j++)
				{
					queries.Add(Query.Or(
						Query.And(
							Query.EQ("Left", Selected[i]),
							Query.EQ("Right", Selected[j])),
						Query.And(
							Query.EQ("Left", Selected[j]),
							Query.EQ("Right", Selected[i]))));
				}
			}

			if (queries.Count == 1)
			{
				return queries.First();
			}
			return Query.Or(queries.ToArray());
		}

		//public override Query BuildQuery()
		//{
		//	List<Query> queries = new List<Query>();
			

		//	if (IsOnlySelectedFiltered)
		//	{
		//		if (Selected.Count == 1)
		//		{
		//			return Query.Or(
		//				Query.EQ("Left", Selected.First()),
		//				Query.EQ("Right", Selected.First()));
		//		}

		//		for (int i = 0; i < Selected.Count; i++)
		//		{
		//			for (int j = i + 1; j < Selected.Count; j++)
		//			{
		//				queries.Add(Query.Or(
		//					Query.And(
		//						Query.EQ("Left", Selected[i]),
		//						Query.EQ("Right", Selected[j])),
		//					Query.And(
		//						Query.EQ("Left", Selected[j]),
		//						Query.EQ("Right", Selected[i]))));
		//			}
		//		}
		//	}
		//	else
		//	{
		//		foreach (string stock in Selected)
		//		{
		//			queries.Add(Query.Or(
		//				Query.EQ("Left", stock),
		//				Query.EQ("Right", stock)));
		//		}
		//	}

		//	if (queries.Count == 1)
		//	{
		//		return queries.First();
		//	}
		//	return Query.Or(queries.ToArray());
		//}

		protected override ObservableCollection<string> InitChoices()
		{
			ObservableCollection<string> choices = new ObservableCollection<string>();
			var stockNames = Directory.GetFiles(DirectoryConstants.StockDirectory).
				Select(s => s.Replace(".lua", "").Replace(DirectoryConstants.StockDirectory, ""));
			foreach (string stock in stockNames)
			{
				choices.Add(StockNames.ProperStockNames[stock]);
			}
			choices = new ObservableCollection<string>(choices.OrderBy(s => s));
			return choices;
		}

		public override string ToString()
		{
			return nameof(StockFilter);
		}
	}
}
