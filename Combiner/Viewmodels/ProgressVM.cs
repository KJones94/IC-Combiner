using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combiner
{
	public class ProgressVM : BaseViewModel
	{
		public ProgressVM()
		{
			m_IsIndeterminate = false;
		}

		private bool m_IsIndeterminate;
		public bool IsIndeterminate
		{
			get { return m_IsIndeterminate; }
			set
			{
				if (value != m_IsIndeterminate)
				{
					m_IsIndeterminate = value;
					OnPropertyChanged(nameof(IsIndeterminate));
				}
			}
		}
	}
}
