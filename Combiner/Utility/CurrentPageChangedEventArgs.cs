using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combiner
{
	public class CurrentPageChangedEventArgs : EventArgs
	{
		public CurrentPageChangedEventArgs(int startIndex, int itemCount)
		{
			StartIndex = startIndex;
			ItemCount = itemCount;
		}

		public int StartIndex { get; private set; }

		public int ItemCount { get; private set; }
	}
}
