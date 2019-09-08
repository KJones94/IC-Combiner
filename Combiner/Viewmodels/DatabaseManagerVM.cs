using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Combiner
{
	public class DatabaseManagerVM : BaseViewModel
	{
		private readonly string m_CreaturesCollectionName = "creatures";

		public delegate void CollectionChangeHandler(string collection);
		public event CollectionChangeHandler CollectionChangedEvent;

		Database m_Database;

		public DatabaseManagerVM(Database database)
		{
			m_Database = database;
			ActiveCollection = m_CreaturesCollectionName;
		}

		private ObservableCollection<string> m_Collections;
		public ObservableCollection<string> Collections
		{
			get {
				return m_Collections ??
				  (m_Collections = new ObservableCollection<string>(m_Database.GetCollectionNames()));
			}
			set
			{
				if (value != m_Collections)
				{
					m_Collections = value;
					OnPropertyChanged(nameof(Collections));
				}
			}
		}

		private void UpdateCollections()
		{
			Collections = new ObservableCollection<string>(m_Database.GetCollectionNames());
		}

		private string m_ActiveCollection;
		public string ActiveCollection
		{
			get { return m_ActiveCollection; }
			set
			{
				if (value != m_ActiveCollection)
				{
					m_ActiveCollection = value;
					OnPropertyChanged(nameof(ActiveCollection));
				}
			}
		}

		private ICommand m_ActivateCollectionCommand;
		public ICommand ActivateCollectionCommand
		{
			get {
				return m_ActivateCollectionCommand ??
				  (m_ActivateCollectionCommand = new RelayCommand(ActivateCollection));
			}
			set
			{
				if (value != m_ActivateCollectionCommand)
				{
					m_ActivateCollectionCommand = value;
					OnPropertyChanged(nameof(ActivateCollection));
				}
			}
		}
		private void ActivateCollection(object o)
		{
			if (!string.IsNullOrEmpty(SelectedCollection))
			{
				ActiveCollection = SelectedCollection;
				CollectionChangedEvent?.Invoke(ActiveCollection);
			}
		}

		private string m_CreateCollectionName;
		public string CreateCollectionName
		{
			get { return m_CreateCollectionName; }
			set
			{
				if (value != m_CreateCollectionName)
				{
					m_CreateCollectionName = value;
					OnPropertyChanged(nameof(CreateCollectionName));
				}
			}
		}

		private ICommand m_CreateCollectionCommand;
		public ICommand CreateCollectionCommand
		{
			get {
				return m_CreateCollectionCommand ??
				  (m_CreateCollectionCommand = new RelayCommand(CreateCollection));
			}
			set
			{
				if (value != m_CreateCollectionCommand)
				{
					m_CreateCollectionCommand = value;
					OnPropertyChanged(nameof(CreateCollectionCommand));
				}
			}
		}
		private void CreateCollection(object o)
		{
			if (string.IsNullOrEmpty(CreateCollectionName))
			{
				// do something
			}
			else if (m_Database.CreateCollection(CreateCollectionName))
			{
				UpdateCollections();
			}
			else
			{
				// Failure explanation
			}
		}

		private string m_SelectedCollection;
		public string SelectedCollection
		{
			get { return m_SelectedCollection; }
			set
			{
				if (value != m_SelectedCollection)
				{
					m_SelectedCollection = value;
					OnPropertyChanged(nameof(SelectedCollection));
				}
			}
		}

		private ICommand m_DeleteCollectionCommand;
		public ICommand DeleteCollectionCommand
		{
			get {
				return m_DeleteCollectionCommand ??
				  (m_DeleteCollectionCommand = new RelayCommand(DeleteCollection));
			}
			set
			{
				if (value != m_DeleteCollectionCommand)
				{
					m_DeleteCollectionCommand = value;
					OnPropertyChanged(nameof(DeleteCollectionCommand));
				}
			}
		}
		private void DeleteCollection(object o)
		{
			// Are you sure window?
			if (!string.IsNullOrEmpty(SelectedCollection))
			{
				m_Database.DeleteCollection(SelectedCollection);
				UpdateCollections();
			}
		}

		// This should probably be a property so references will receive any changes..?
		public List<string> SaveableCollections()
		{
			return Collections.Where(s => s != m_CreaturesCollectionName && s != ActiveCollection).ToList();
		}

		public void SaveCreature(Creature creature, string collectionName)
		{

			m_Database.SaveCreature(creature, collectionName);
		}

		public void UnsaveCreature(Creature creature)
		{
			if (ActiveCollection != m_CreaturesCollectionName)
			{
				m_Database.UnsaveCreature(creature, ActiveCollection);
				CollectionChangedEvent?.Invoke(ActiveCollection);
			}
		}

	}
}
