using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Combiner
{
	public class MainVM : BaseViewModel
	{

		public CreatureVM CreatureVM { get; set; }

		public MainVM()
		{
			CreatureVM = new CreatureVM();
			DatabasePrototype.TestDB();
		}

		
	}
}
