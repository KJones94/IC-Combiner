using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combiner
{
	public class ModCollection
	{
		public int Id { get; set; }

		public string ModName { get; set; }
		public bool IsMain { get; set; }
		public string CollectionName { get; set; }
		public string AttrPath { get; set; }
		public string StockPath { get; set; }

		public override string ToString()
		{
			return CollectionName;
		}
	}
}
