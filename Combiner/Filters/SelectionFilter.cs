namespace Combiner.Filters
{
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Windows.Input;

	using Combiner.Base;
	using Combiner.Models;

	public abstract class SelectionFilter : CreatureFilter
	{
		public SelectionFilter(string name)
			: base(name)
		{
			this.Choices = this.InitChoices();
		}

		protected abstract ObservableCollection<string> InitChoices();
		protected abstract bool FilterOnlySelected(Creature creature);
		protected abstract bool FilterAnySelected(Creature creature);

		public override bool Filter(Creature creature)
		{
			if (this.Selected.Count == 0)
			{
				return true;
			}

			if (this.IsOnlySelectedFiltered)
			{
				return this.FilterOnlySelected(creature);
			}
			return this.FilterAnySelected(creature);
		}

		public override void ResetFilter()
		{
			this.RemoveAllSelected(null);
			this.IsOnlySelectedFiltered = false;
		}

		private ObservableCollection<string> m_Choices;
		public ObservableCollection<string> Choices
		{
			get
			{
				return this.m_Choices
					?? (this.m_Choices = new ObservableCollection<string>());
			}
			set
			{
				if (this.m_Choices != value)
				{
					this.m_Choices = value;
					this.OnPropertyChanged(nameof(this.Choices));
				}
			}
		}

		public string ChoiceItem { get; set; }

		private ObservableCollection<string> m_Selected;
		public ObservableCollection<string> Selected
		{
			get
			{
				return this.m_Selected
					?? (this.m_Selected = new ObservableCollection<string>());
			}
			set
			{
				if (this.m_Selected != value)
				{
					this.m_Selected = value;
					this.OnPropertyChanged(nameof(this.Selected));
				}
			}
		}

		public string SelectedItem { get; set; }

		private ICommand m_AddChoiceCommand;
		public ICommand AddChoiceCommand
		{
			get
			{
				return this.m_AddChoiceCommand ??
				  (this.m_AddChoiceCommand = new RelayCommand(this.AddChoice));
			}
			set
			{
				if (value != this.m_AddChoiceCommand)
				{
					this.m_AddChoiceCommand = value;
					this.OnPropertyChanged(nameof(this.AddChoiceCommand));
				}
			}
		}
		private void AddChoice(object obj)
		{
			if (!string.IsNullOrEmpty(this.ChoiceItem) && !this.Selected.Contains(this.ChoiceItem))
			{
				this.Selected.Add(this.ChoiceItem);
				this.Selected = new ObservableCollection<string>(this.Selected.OrderBy(s => s));
				this.Choices.Remove(this.ChoiceItem);
			}
		}

		private ICommand m_RemovedSelectedCommand;
		public ICommand RemoveSelectedCommand
		{
			get
			{
				return this.m_RemovedSelectedCommand ??
				  (this.m_RemovedSelectedCommand = new RelayCommand(this.RemoveSelected));
			}
			set
			{
				if (value != this.m_RemovedSelectedCommand)
				{
					this.m_RemovedSelectedCommand = value;
					this.OnPropertyChanged(nameof(this.RemoveSelectedCommand));
				}
			}
		}
		private void RemoveSelected(object obj)
		{
			if (!string.IsNullOrEmpty(this.SelectedItem))
			{
				this.Choices.Add(this.SelectedItem);
				this.Choices = new ObservableCollection<string>(this.Choices.OrderBy(s => s));
				this.Selected.Remove(this.SelectedItem);
			}
		}

		private ICommand m_RemoveAllSelectedCommand;
		public ICommand RemoveAllSelectedCommand
		{
			get
			{
				return this.m_RemoveAllSelectedCommand ??
				  (this.m_RemoveAllSelectedCommand = new RelayCommand(this.RemoveAllSelected));
			}
			set
			{
				if (value != this.m_RemoveAllSelectedCommand)
				{
					this.m_RemoveAllSelectedCommand = value;
					this.OnPropertyChanged(nameof(this.RemoveAllSelectedCommand));
				}
			}
		}
		private void RemoveAllSelected(object obj)
		{
			foreach (string ability in this.Selected)
			{
				this.Choices.Add(ability);
			}
			this.Selected = new ObservableCollection<string>();
			this.Choices = new ObservableCollection<string>(this.Choices.OrderBy(s => s));
		}

		private bool m_IsOnlySelectedFiltered;
		public bool IsOnlySelectedFiltered
		{
			get { return this.m_IsOnlySelectedFiltered; }
			set
			{
				if (this.m_IsOnlySelectedFiltered != value)
				{
					this.m_IsOnlySelectedFiltered = value;
					this.OnPropertyChanged(nameof(this.IsOnlySelectedFiltered));
				}
			}
		}

	}
}
