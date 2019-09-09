using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace Combiner
{
	public class DatabaseManagerVM : BaseViewModel
	{
		private readonly string m_CreaturesCollectionName = "creatures";

		public delegate void CollectionChangeHandler(string collection);
		public event CollectionChangeHandler CollectionChangedEvent;

		Database m_Database;
		private ImportExportHandler m_ImportExportHandler;

		public DatabaseManagerVM(Database database, ImportExportHandler importExportHandler)
		{
			m_Database = database;
			m_ImportExportHandler = importExportHandler;
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
			if (!IsCollectionNameValid(CreateCollectionName))
			{
				MessageBox.Show("Name must only contain numbers and letters.");
			}
			else if (m_Database.CreateCollection(CreateCollectionName))
			{
				CreateCollectionName = string.Empty;
				UpdateCollections();
			}
			else
			{
				MessageBox.Show("Name already exists. Keep in mind casing is insensitive.");
			}
		}

		private bool IsCollectionNameValid(string collectionName)
		{
			// '_' is valid but simpler to not allow unless asked for
			return !string.IsNullOrEmpty(collectionName) 
				&& collectionName.All(c => char.IsLetterOrDigit(c));
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
			if (!string.IsNullOrEmpty(SelectedCollection))
			{
				if (SelectedCollection == m_CreaturesCollectionName)
				{
					MessageBox.Show("Cannot delete the main creature collection");
				}
				else
				{
					MessageBoxResult result = 
						MessageBox.Show(
							"Are you sure you want to delete this collection?", 
							"Database Warning", 
							MessageBoxButton.YesNo, 
							MessageBoxImage.Warning);
					if (result == MessageBoxResult.Yes)
					{
						m_Database.DeleteCollection(SelectedCollection);
						UpdateCollections();
					}
				}
			}
		}

		private string m_RenameCollectionName;
		public string RenameCollectionName
		{
			get { return m_RenameCollectionName; }
			set
			{
				if (value != m_RenameCollectionName)
				{
					m_RenameCollectionName = value;
					OnPropertyChanged(nameof(RenameCollectionName));
				}
			}
		}

		private ICommand m_RenameCollectionCommand;
		public ICommand RenameCollectionCommand
		{
			get {
				return m_RenameCollectionCommand
				  ?? (m_RenameCollectionCommand = new RelayCommand(RenameCollection));
			}
			set
			{
				if (value != m_RenameCollectionCommand)
				{
					m_RenameCollectionCommand = value;
					OnPropertyChanged(nameof(RenameCollectionCommand));
				}
			}
		}
		private void RenameCollection(object o)
		{
			if (!string.IsNullOrEmpty(SelectedCollection))
			{
				if (SelectedCollection == m_CreaturesCollectionName)
				{
					MessageBox.Show("Cannot rename the main creature collection");
				}
				else
				{
					if (!IsCollectionNameValid(RenameCollectionName))
					{
						MessageBox.Show("Name must contain only numbers and letters.");
					}
					else if (m_Database.RenameCollection(SelectedCollection, RenameCollectionName))
					{
						RenameCollectionName = string.Empty;
						UpdateCollections();
					}
					else
					{
						MessageBox.Show("Name already exists. Keep in mind casing is insensitive.");
					}
				}
			}
		}

		private ICommand m_ImportCollectionCommand;
		public ICommand ImportCollectionCommand
		{
			get {
				return m_ImportCollectionCommand
				  ?? (m_ImportCollectionCommand = new RelayCommand(ImportCollection));
			}
		}
		private void ImportCollection(object o)
		{
			if (!string.IsNullOrEmpty(SelectedCollection))
			{
				if (SelectedCollection == m_CreaturesCollectionName)
				{
					MessageBox.Show("Cannot import into the main creature collection");
				}
				else
				{
					MessageBoxResult result =
						MessageBox.Show(
							"Are you sure you want to import into this collection?",
							"Database Warning",
							MessageBoxButton.YesNo,
							MessageBoxImage.Warning);
					if (result == MessageBoxResult.Yes)
					{
						m_ImportExportHandler.Import(SelectedCollection);
					}
				}
			}
		}

		private ICommand m_ExportCollectionCommand;
		public ICommand ExportCollectionCommand
		{
			get {
				return m_ExportCollectionCommand
				  ?? (m_ExportCollectionCommand = new RelayCommand(ExportCollection));
			}
		}
		private void ExportCollection(object o)
		{
			if (!string.IsNullOrEmpty(SelectedCollection))
			{
				if (SelectedCollection == m_CreaturesCollectionName)
				{
					MessageBox.Show("Cannot export the main creature collection");
				}
				else
				{
					m_ImportExportHandler.Export(SelectedCollection);
				}
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
