using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combiner
{
	/// <summary>
	/// Base class for all filters
	/// </summary>
	public abstract class CreatureFilter : BaseViewModel
	{
		public abstract bool Filter(Creature creature);

		public abstract void ResetFilter();

		public virtual BsonExpression BuildQuery()
		{
			return Query.All().Select;
		}

		public string Name { get; private set; }

		public event IsActiveChangedEventHandler IsActiveChanged;

		private bool m_IsActive;
		/// <summary>
		/// The filter's active state. Invokes the IsActiveChanged event.
		/// </summary>
		public bool IsActive
		{
			get { return m_IsActive; }
			set
			{
				if (m_IsActive != value)
				{
					m_IsActive = value;
					IsActiveChanged?.Invoke(this, new IsActiveArgs(m_IsActive));
					OnPropertyChanged(nameof(IsActive));
				}
			}
		}

		public CreatureFilter(string name)
		{
			Name = name;
		}
	}

	public delegate void IsActiveChangedEventHandler(CreatureFilter filter, IsActiveArgs args);

	public class IsActiveArgs : EventArgs
	{
		public bool IsActive { get; private set; }
		public IsActiveArgs(bool isActive)
		{
			IsActive = isActive;
		}
	}
}
