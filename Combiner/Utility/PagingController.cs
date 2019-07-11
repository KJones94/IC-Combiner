using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Combiner
{
	// https://stackoverflow.com/questions/5305883/mvvm-paging-sorting
	public class PagingController : BaseViewModel
	{
		private int m_ItemCount;
		private int m_CurrentPage;
		private int m_PageSize;

		public PagingController(int itemCount, int pageSize)
		{
			m_ItemCount = itemCount;
			m_PageSize = pageSize;
			m_CurrentPage = m_ItemCount == 0 ? 0 : 1;


		}

		public event EventHandler<CurrentPageChangedEventArgs> CurrentPageChanged;

		private ICommand m_GoToFirstPageCommand;
		public ICommand GoToFirstPageCommand
		{
			get
			{
				return m_GoToFirstPageCommand ??
					(m_GoToFirstPageCommand = new RelayCommand(GoToFirstPage, GoToFirstPageCanExecute));
			}
		}
		private void GoToFirstPage(object obj)
		{
			CurrentPage = 1;
		}
		private bool GoToFirstPageCanExecute(object obj)
		{
			return ItemCount != 0 && CurrentPage > 1;
		}

		private ICommand m_GoToLastPageCommand;
		public ICommand GoToLastPageCommand
		{
			get
			{
				return m_GoToLastPageCommand ??
					(m_GoToLastPageCommand = new RelayCommand(GoToLastPage, GoToFirstLastCanExecute));
			}
		}
		private void GoToLastPage(object obj)
		{
			CurrentPage = CurrentPage = PageCount;
		}
		private bool GoToFirstLastCanExecute(object obj)
		{
			return ItemCount != 0 && CurrentPage < PageCount;
		}

		private ICommand m_GoToNextPageCommand;
		public ICommand GoToNextPageCommand
		{
			get
			{
				return m_GoToNextPageCommand ??
					(m_GoToNextPageCommand = new RelayCommand(GoToNextPage, GoToNextPageCanExecute));
			}
		}
		private void GoToNextPage(object obj)
		{
			CurrentPage++;
		}
		private bool GoToNextPageCanExecute(object obj)
		{
			return ItemCount != 0 && CurrentPage < PageCount;
		}

		private ICommand m_GoToPreviousPageCommand;
		public ICommand GoToPreviousPageCommand
		{
			get
			{
				return m_GoToPreviousPageCommand ??
					(m_GoToPreviousPageCommand = new RelayCommand(GoToPreviousPage, GoToPreviousPageCanExecute));
			}
		}
		private void GoToPreviousPage(object obj)
		{
			CurrentPage--;
		}
		private bool GoToPreviousPageCanExecute(object obj)
		{
			return ItemCount != 0 && CurrentPage > 1;
		}



		public int ItemCount
		{
			get
			{
				return m_ItemCount;
			}

			set
			{
				m_ItemCount = value;
				OnPropertyChanged(nameof(ItemCount));
				OnPropertyChanged(nameof(PageCount));
				// RaiseCanExecuteChanged stuff

				if (CurrentPage > PageCount)
				{
					CurrentPage = PageCount;
				}
			}
		}


		public int PageSize
		{
			get { return m_PageSize; }
			set
			{
				var oldStartIndex = CurrentPageStartIndex;
				m_PageSize = value;
				OnPropertyChanged(nameof(PageSize));
				OnPropertyChanged(nameof(PageCount));
				OnPropertyChanged(nameof(CurrentPageStartIndex));
				// RaiseCanExecuteChanged stuff

				if (oldStartIndex >= 0)
				{
					CurrentPage = GetPageFromIndex(oldStartIndex);
				}
			}
		}

		public int PageCount
		{
			get
			{
				if (m_ItemCount == 0)
				{
					return 0;
				}

				var ceil = (int)Math.Ceiling((double)m_ItemCount / m_PageSize);
				return ceil;
			}
		}

		public int CurrentPage
		{
			get
			{
				return m_CurrentPage;
			}
			set
			{
				m_CurrentPage = value;
				OnPropertyChanged(nameof(CurrentPage));
				OnPropertyChanged(nameof(CurrentPageStartIndex));
				// RaiseCanExecuteChanged stuff

				CurrentPageChanged?.Invoke(this, new CurrentPageChangedEventArgs(CurrentPageStartIndex, PageSize));
			}
		}

		public int CurrentPageStartIndex
		{
			get
			{
				return PageCount == 0 ? -1 : (CurrentPage - 1) * PageSize;
			}
		}

		private int GetPageFromIndex(int itemIndex)
		{
			var result = (int)Math.Floor((double)itemIndex / PageSize) + 1;
			return result;
		}
		
	}
}
