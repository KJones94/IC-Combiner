using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Combiner
{
	// https://stackoverflow.com/questions/784726/how-can-i-paginate-a-wpf-datagrid
	public class PagingCollectionView : ListCollectionView
	{
		private readonly IList m_InnerList;
		private readonly int m_ItemsPerPage;

		private int m_CurrentPage;

		public PagingCollectionView(IList innerList, int itemsPerPage)
			: base(innerList)
		{
			m_InnerList = innerList;
			m_ItemsPerPage = itemsPerPage;
		}

		public override int Count
		{
			get
			{
				if (m_InnerList.Count == 0)
				{
					return 0;
				}
				if (m_CurrentPage < PageCount)
				{
					return m_ItemsPerPage;
				}
				else
				{
					var itemsLeft = m_InnerList.Count % m_ItemsPerPage;
					if (itemsLeft == 0)
					{
						return m_ItemsPerPage;
					}
					else
					{
						return itemsLeft;
					}
				}
			}
		}

		public int CurrentPage
		{
			get { return m_CurrentPage; }
			set
			{
				m_CurrentPage = value;
				OnPropertyChanged(new PropertyChangedEventArgs(nameof(CurrentPage)));
			}
		}

		public int ItemsPerPage
		{
			get { return m_ItemsPerPage; }
		}

		public int PageCount
		{
			get
			{
				return (m_InnerList.Count + m_ItemsPerPage - 1)
					/ m_ItemsPerPage;
			}
		}

		private int EndIndex
		{
			get
			{
				var end = m_CurrentPage * m_ItemsPerPage - 1;
				return (end > m_InnerList.Count) ? m_InnerList.Count : end;
			}
		}

		private int StartEndex
		{
			get { return (m_CurrentPage - 1) * m_ItemsPerPage; }
		}

		public override object GetItemAt(int index)
		{
			var offset = index % m_ItemsPerPage;
			return m_InnerList[StartEndex + offset];
		}

		public void MoveToNextPage()
		{
			if (m_CurrentPage < PageCount)
			{
				CurrentPage += 1;
			}
			Refresh();
		}

		public void MoveToPreviousPage()
		{
			if (m_CurrentPage > 1)
			{
				CurrentPage -= 1;
			}
			Refresh();
		}
	}
}
