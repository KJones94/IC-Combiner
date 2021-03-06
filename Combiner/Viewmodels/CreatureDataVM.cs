﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Combiner
{
	public class CreatureDataVM : BaseViewModel
	{
		private const int m_PageSize = 1000;
		private Database m_Database;
		private DatabaseManagerVM m_DatabaseManagerVM;

		public CreatureDataVM(Database database, DatabaseManagerVM databaseManagerVM)
		{
			m_Database = database;
			m_DatabaseManagerVM = databaseManagerVM;
			m_DatabaseManagerVM.CollectionChangedEvent += UpdateTotalCreatureCount;
		}

		private ObservableCollection<Creature> m_Creatures;
		public ObservableCollection<Creature> Creatures
		{
			get
			{
				return m_Creatures ?? (m_Creatures = new ObservableCollection<Creature>());
			}
			set
			{
				if (value != m_Creatures)
				{
					m_Creatures = value;
					CreaturesView = (ListCollectionView)CollectionViewSource.GetDefaultView(m_Creatures);

					//Pager = new PagingController(m_Creatures.Count, m_PageSize);
					//Pager.CurrentPageChanged += (s, e) => UpdateData();
					//UpdateData();

					OnPropertyChanged(nameof(Creatures));
				}
			}
		}

		private ListCollectionView m_CreaturesView;
		public ListCollectionView CreaturesView
		{
			get
			{
				return m_CreaturesView ?? (m_CreaturesView = (ListCollectionView)CollectionViewSource.GetDefaultView(Creatures));
			}
			set
			{
				if (value != m_CreaturesView)
				{
					m_CreaturesView = value;
					OnPropertyChanged(nameof(CreaturesView));
				}
			}
		}

		private int m_TotalCreatureCount;
		public int TotalCreatureCount
		{
			get { return m_TotalCreatureCount; }
			set
			{
				if (m_TotalCreatureCount != value)
				{
					m_TotalCreatureCount = value;
					OnPropertyChanged(nameof(TotalCreatureCount));
				}
			}
		}

		public void UpdateTotalCreatureCount(ModCollection collection)
		{
			TotalCreatureCount = m_Database.GetTotalCreatureCount(collection);
		}

		private PagingController m_Pager;
		public PagingController Pager
		{
			get
			{
				return m_Pager ?? (m_Pager = new PagingController(0, m_PageSize));
			}
			private set
			{
				m_Pager = value;
				OnPropertyChanged(nameof(Pager));
			}
		}

		private void UpdateData()
		{
			ObservableCollection<Creature> data = new ObservableCollection<Creature>();
			foreach (var creature in m_Creatures.Skip(Pager.CurrentPageStartIndex).Take(Pager.PageSize))
			{
				data.Add(creature);
			}
			CreaturesView = (ListCollectionView)CollectionViewSource.GetDefaultView(data);
		}

		private Creature m_SelectedCreature;
		public Creature SelectedCreature
		{
			get
			{
				return m_SelectedCreature;
			}
			set
			{
				if (value != m_SelectedCreature)
				{
					m_SelectedCreature = value;
					OnPropertyChanged(nameof(SelectedCreature));
				}
			}
		}

		private ICommand m_SaveCreatureCommand;
		public ICommand SaveCreatureCommand
		{
			get
			{
				return m_SaveCreatureCommand ??
					(m_SaveCreatureCommand = new RelayCommand(SaveCreature));
			}
			set
			{
				if (value != m_SaveCreatureCommand)
				{
					m_SaveCreatureCommand = value;
					OnPropertyChanged(nameof(SaveCreatureCommand));
				}
			}
		}
		public void SaveCreature(object obj)
		{
			if (SelectedCreature != null)
			{
				var window = new SaveCollectionWindow();
				window.DataContext = new SaveCollectionVM(SelectedCreature, m_DatabaseManagerVM);
				window.ShowDialog();
			}
		}

		private ICommand m_UnsaveCreatureCommand;
		public ICommand UnsaveCreatureCommand
		{
			get
			{
				return m_UnsaveCreatureCommand ??
					(m_UnsaveCreatureCommand = new RelayCommand(UnSaveCreature));
			}
			set
			{
				if (value != m_UnsaveCreatureCommand)
				{
					m_UnsaveCreatureCommand = value;
					OnPropertyChanged(nameof(UnsaveCreatureCommand));
				}
			}
		}
		public void UnSaveCreature(object obj)
		{
			if (SelectedCreature != null)
			{
				m_DatabaseManagerVM.UnsaveCreature(SelectedCreature);
			}
		}
	}
}
