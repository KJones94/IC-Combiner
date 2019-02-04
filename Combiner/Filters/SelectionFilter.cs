using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Combiner
{
	public abstract class SelectionFilter : CreatureFilter
	{
		public SelectionFilter(string name)
			: base(name)
		{
			Choices = InitChoices();
		}

		protected abstract ObservableCollection<string> InitChoices();
		protected abstract bool FilterOnlySelected(Creature creature);
		protected abstract bool FilterAnySelected(Creature creature);

		public override bool Filter(Creature creature)
		{
			if (Selected.Count == 0)
			{
				return true;
			}

			if (IsOnlySelectedFiltered)
			{
				return FilterOnlySelected(creature);
			}
			return FilterAnySelected(creature);
		}

		public override void ResetFilter()
		{
			RemoveAllSelected(null);
			IsOnlySelectedFiltered = false;
		}

		private ObservableCollection<string> m_Choices;
		public ObservableCollection<string> Choices
		{
			get
			{
				return m_Choices
					?? (m_Choices = new ObservableCollection<string>());
			}
			set
			{
				if (m_Choices != value)
				{
					m_Choices = value;
					OnPropertyChanged(nameof(Choices));
				}
			}
		}

		public string ChoiceItem { get; set; }

		private ObservableCollection<string> m_Selected;
		public ObservableCollection<string> Selected
		{
			get
			{
				return m_Selected
					?? (m_Selected = new ObservableCollection<string>());
			}
			set
			{
				if (m_Selected != value)
				{
					m_Selected = value;
					OnPropertyChanged(nameof(Selected));
				}
			}
		}

		public string SelectedItem { get; set; }

		private ICommand m_AddChoiceCommand;
		public ICommand AddChoiceCommand
		{
			get
			{
				return m_AddChoiceCommand ??
				  (m_AddChoiceCommand = new RelayCommand(AddChoice));
			}
			set
			{
				if (value != m_AddChoiceCommand)
				{
					m_AddChoiceCommand = value;
					OnPropertyChanged(nameof(AddChoiceCommand));
				}
			}
		}
		private void AddChoice(object obj)
		{
			if (!string.IsNullOrEmpty(ChoiceItem) && !Selected.Contains(ChoiceItem))
			{
				Selected.Add(ChoiceItem);
				Selected = new ObservableCollection<string>(Selected.OrderBy(s => s));
				Choices.Remove(ChoiceItem);
			}
		}

		private ICommand m_RemovedSelectedCommand;
		public ICommand RemoveSelectedCommand
		{
			get
			{
				return m_RemovedSelectedCommand ??
				  (m_RemovedSelectedCommand = new RelayCommand(RemoveSelected));
			}
			set
			{
				if (value != m_RemovedSelectedCommand)
				{
					m_RemovedSelectedCommand = value;
					OnPropertyChanged(nameof(RemoveSelectedCommand));
				}
			}
		}
		private void RemoveSelected(object obj)
		{
			if (!string.IsNullOrEmpty(SelectedItem))
			{
				Choices.Add(SelectedItem);
				Choices = new ObservableCollection<string>(Choices.OrderBy(s => s));
				Selected.Remove(SelectedItem);
			}
		}

		private ICommand m_RemoveAllSelectedCommand;
		public ICommand RemoveAllSelectedCommand
		{
			get
			{
				return m_RemoveAllSelectedCommand ??
				  (m_RemoveAllSelectedCommand = new RelayCommand(RemoveAllSelected));
			}
			set
			{
				if (value != m_RemoveAllSelectedCommand)
				{
					m_RemoveAllSelectedCommand = value;
					OnPropertyChanged(nameof(RemoveAllSelectedCommand));
				}
			}
		}
		private void RemoveAllSelected(object obj)
		{
			foreach (string ability in Selected)
			{
				Choices.Add(ability);
			}
			Selected = new ObservableCollection<string>();
			Choices = new ObservableCollection<string>(Choices.OrderBy(s => s));
		}

		private bool m_IsOnlySelectedFiltered;
		public bool IsOnlySelectedFiltered
		{
			get { return m_IsOnlySelectedFiltered; }
			set
			{
				if (m_IsOnlySelectedFiltered != value)
				{
					m_IsOnlySelectedFiltered = value;
					OnPropertyChanged(nameof(IsOnlySelectedFiltered));
				}
			}
		}

	}
}
