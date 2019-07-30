using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class AbilityFilter : SelectionFilter
	{
		public AbilityFilter()
			: base("Abilities") { }

		protected override bool FilterAnySelected(Creature creature)
		{
			foreach (string ability in Selected)
			{
				if (creature.Abilities.ContainsKey(ability))
				{
					if (creature.Abilities[ability])
					{
						return true;
					}
				}
			}
			return false;
		}

		protected override bool FilterOnlySelected(Creature creature)
		{
			bool hasAbilities = true;
			foreach (string ability in Selected)
			{
				if (creature.Abilities.ContainsKey(ability))
				{
					hasAbilities = hasAbilities && (creature.Abilities[ability]);
				}
			}
			return hasAbilities;
		}

		protected override Query QueryAnySelected()
		{
			List<Query> queries = BuildQueryList();
			if (queries.Count == 1)
			{
				return queries.First();
			}
			return Query.Or(queries.ToArray());
		}

		protected override Query QueryOnlySelected()
		{
			List<Query> queries = BuildQueryList();
			if (queries.Count == 1)
			{
				return queries.First();
			}
			return Query.And(queries.ToArray());
		}

		private List<Query> BuildQueryList()
		{
			List<Query> queries = new List<Query>();
			foreach (string ability in Selected)
			{
				queries.Add(Query.EQ("Abilities." + ability, true));
			}
			return queries;
		}

		

		protected override ObservableCollection<string> InitChoices()
		{
			ObservableCollection<string> choices = new ObservableCollection<string>();
			foreach (string ability in AbilityNames.Abilities)
			{
				choices.Add(AbilityNames.ProperAbilityNames[ability]);
			}
			choices = new ObservableCollection<string>(choices.OrderBy(s => s));
			return choices;
		}

		public override string ToString()
		{
			return nameof(AbilityFilter);
		}
	}
}
