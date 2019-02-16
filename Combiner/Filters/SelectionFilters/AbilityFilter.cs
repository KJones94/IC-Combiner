using System.Collections.ObjectModel;
using System.Linq;

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
