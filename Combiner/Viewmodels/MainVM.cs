﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Combiner
{
	public class MainVM : BaseViewModel
	{

		private CreatureDataVM m_CreatureDataVM;
		public CreatureDataVM CreatureDataVM
		{
			get
			{
				return m_CreatureDataVM;
			}
			set
			{
				if (value != m_CreatureDataVM)
				{
					m_CreatureDataVM = value;
					OnPropertyChanged(nameof(CreatureDataVM));
				}
			}
		}

		private DatabaseVM m_DatabaseVM;
		public DatabaseVM DatabaseVM
		{
			get
			{
				return m_DatabaseVM;
			}
			set
			{
				if (value != m_DatabaseVM)
				{
					m_DatabaseVM = value;
					OnPropertyChanged(nameof(DatabaseVM));
				}
			}
		}

		private FiltersVM m_FiltersVM;
		public FiltersVM FiltersVM
		{
			get
			{
				return m_FiltersVM;
			}
			set
			{
				if (value != m_FiltersVM)
				{
					m_FiltersVM = value;
					OnPropertyChanged(nameof(FiltersVM));
				}
			}
		}

		public MainVM()
		{
			CreatureDataVM = new CreatureDataVM();
			FiltersVM = new FiltersVM(CreatureDataVM);
			DatabaseVM = new DatabaseVM(CreatureDataVM, FiltersVM);
		}

		
	}
}
