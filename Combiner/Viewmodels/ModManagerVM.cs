using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using UI = System.Windows.Forms;

namespace Combiner
{
	public class ModManagerVM : BaseViewModel
	{
		private readonly string m_ModPath = "../../Mods";

		// C:\Program Files (x86)\Steam\steamapps\common\Impossible Creatures

		private ProgressVM m_ProgressVM;
		private Database m_Database;

		public ModManagerVM(Database database,
			ProgressVM progressVM)
		{
			m_Database = database;
			m_ProgressVM = progressVM;

			//SetupExistingMods();
		}

		public void SetupExistingMods()
		{
			string[] modDirectories = Directory.GetDirectories(DirectoryConstants.ModDirectory);
			string[] modNames = modDirectories.Select(x => Path.GetFileName(x)).ToArray();
			string[] modsToAdd = modNames.Where(x => m_Database.getMainMod(x) == null).ToArray();
			
			if (modsToAdd.Length > 0)
			{
				UI.DialogResult dialogResult = UI.MessageBox.Show("There are mods in your Mods directory without a creature collection. Would you like to create them now?", 
					"Add Existing Mods", UI.MessageBoxButtons.YesNo);
				if (dialogResult == UI.DialogResult.Yes)
				{
					string modsPath = Path.GetFullPath(DirectoryConstants.ModDirectory);
					foreach (string mod in modsToAdd)
					{
						string attrPath = Path.Combine(modsPath, mod, "Script");
						string attrFilePath = Directory.GetFiles(attrPath, "*.lua")[0];
						string stockPath = Path.Combine(modsPath, mod, "Stock");

						CreateMod(mod, attrFilePath, stockPath);
					}
				}
				else if (dialogResult == UI.DialogResult.No)
				{
					//do something else
				}
			}
		}

		private ObservableCollection<string> m_Mods;
		public ObservableCollection<string> Mods
		{
			get
			{
				return m_Mods ??
				  (m_Mods = new ObservableCollection<string>(m_Database.GetMainModNames()));
			}
			set
			{
				if (value != m_Mods)
				{
					m_Mods = value;
					OnPropertyChanged(nameof(Mods));
				}
			}
		}

		private void UpdateMods()
		{
			Mods = new ObservableCollection<string>(m_Database.GetMainModNames());
		}

		private string m_AttrPath;
		public string AttrPath
		{
			get { return m_AttrPath; }
			set
			{
				if (value != m_AttrPath)
				{
					m_AttrPath = value;
					OnPropertyChanged(nameof(AttrPath));
				}
			}
		}

		private ICommand m_SelectAttrCommand;
		public ICommand SelectAttrCommand
		{
			get
			{
				return m_SelectAttrCommand ??
				  (m_SelectAttrCommand = new RelayCommand(SelectAttr));
			}
			set
			{
				if (value != m_SelectAttrCommand)
				{
					m_SelectAttrCommand = value;
					OnPropertyChanged(nameof(SelectAttrCommand));
				}
			}
		}
		public void SelectAttr(object o)
		{
			// https://stackoverflow.com/questions/34922735/commonopenfiledialog-put-my-window-behind-all-other-windows
			var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
			CommonOpenFileDialog dialog = new CommonOpenFileDialog();
			//dialog.InitialDirectory = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Impossible Creatures";
			if (dialog.ShowDialog(window) == CommonFileDialogResult.Ok)
			{
				AttrPath = dialog.FileName;
			}
		}

		private string m_StockPath;
		public string StockPath
		{
			get { return m_StockPath; }
			set
			{
				if (value != m_StockPath)
				{
					m_StockPath = value;
					OnPropertyChanged(nameof(StockPath));
				}
			}
		}

		private ICommand m_SelectStockCommand;
		public ICommand SelectStockCommand
		{
			get
			{
				return m_SelectStockCommand ??
				  (m_SelectStockCommand = new RelayCommand(SelectStock));
			}
			set
			{
				if (value != m_SelectStockCommand)
				{
					m_SelectStockCommand = value;
					OnPropertyChanged(nameof(SelectStockCommand));
				}
			}
		}
		public void SelectStock(object o)
		{
			// https://stackoverflow.com/questions/34922735/commonopenfiledialog-put-my-window-behind-all-other-windows
			var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
			CommonOpenFileDialog dialog = new CommonOpenFileDialog();
			//dialog.InitialDirectory = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Impossible Creatures";
			dialog.IsFolderPicker = true;
			if (dialog.ShowDialog(window) == CommonFileDialogResult.Ok)
			{
				StockPath = dialog.FileName;
			}
		}

		private string m_CreateModName;
		public string CreateModName
		{
			get { return m_CreateModName; }
			set
			{
				if (value != m_CreateModName)
				{
					m_CreateModName = value;
					OnPropertyChanged(nameof(CreateModName));
				}
			}
		}

		private ICommand m_CreateModCommand;
		public ICommand CreateModCommand
		{
			get
			{
				return m_CreateModCommand ??
				  (m_CreateModCommand = new RelayCommand(CreateMod));
			}
			set
			{
				if (value != m_CreateModCommand)
				{
					m_CreateModCommand = value;
					OnPropertyChanged(nameof(CreateModCommand));
				}
			}
		}

		private bool IsModNameValid(string modName)
		{
			return Regex.IsMatch(modName, "^(?!_)\\w+(?<!_main)$");
		}

		public void CreateModDirectory()
		{
			Directory.CreateDirectory(DirectoryConstants.ModDirectory);
		}

		public void CreateMod(object o)
		{
			if (string.IsNullOrEmpty(CreateModName)
				|| string.IsNullOrEmpty(AttrPath)
				|| string.IsNullOrEmpty(StockPath))
			{
				MessageBox.Show("Make sure to file out the Name, Attr, and Stock fields.");
			}
			else if (!IsModNameValid(CreateModName))
			{
				MessageBox.Show("Name must only contain numbers, letters, and _.");
			}
			else
			{
				string newPath = Path.Combine(m_ModPath, CreateModName);
				Directory.CreateDirectory(newPath);
				string newAttrPath = Path.Combine(newPath, "Script");
				Directory.CreateDirectory(newAttrPath);
				string newStockPath = Path.Combine(newPath, "Stock");
				Directory.CreateDirectory(newStockPath);

				// Not sure I need to do this testcombiner.lua...
				// can just use attrcombiner.lua as name
				string convertedAttrPath = ConvertAttr(AttrPath, Path.Combine(newAttrPath, Path.GetFileName(AttrPath)));
				//File.Copy(convertedAttrPath, Path.Combine(newAttrPath, Path.GetFileName(AttrPath)), true);

				if (Directory.Exists(StockPath))
				{
					string[] files = System.IO.Directory.GetFiles(StockPath);
					foreach (string s in files)
					{
						if (Path.GetExtension(s) == ".lua")
						{
							File.Copy(s, Path.Combine(newStockPath, Path.GetFileName(s)), true);
							Path.GetFileName(s);
						}
					}
				}

				MessageBox.Show("Starting creation of main creature collection for this mod...");

				m_Database.CreateMod(CreateModName, convertedAttrPath, newStockPath);
				UpdateMods();

				MessageBox.Show("Mod Created");
				CreateModName = string.Empty;
				AttrPath = string.Empty;
				StockPath = string.Empty;
			}
		}

		public void CreateMod(string modName, string attrPath, string stockPath)
		{
			m_Database.CreateMod(modName, attrPath, stockPath);
			UpdateMods();
		}

		private string ConvertAttr(string src, string dest)
		{
			using (StreamReader reader = new StreamReader(src))
			using (StreamWriter writer = new StreamWriter(dest))
			{
				bool delete = false;
				bool pairs = false;
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					if (line.Contains("deleteStart"))
					{
						delete = true;
						continue;
					}
					else if (line.Contains("deleteEnd"))
					{
						delete = false;
						continue;
					}
					if (delete) continue;

					if (line.Contains("pairsStart"))
					{
						pairs = true;
						continue;
					}
					else if (line.Contains("pairsEnd"))
					{
						pairs = false;
						continue;
					}

					if (pairs)
					{
						int start = line.IndexOf(" in ") + 4;
						line = line.Insert(start, "pairs(");
						int end = line.IndexOf(" do");
						line = line.Insert(end, ")");
						writer.WriteLine(line);
					}
					else
					{
						writer.WriteLine(line);
					}

				}
			}

			return dest;
		}

		//private ICommand m_CreateDatabaseCommand;
		//public ICommand CreateDatabaseCommand
		//{
		//	get
		//	{
		//		return m_CreateDatabaseCommand ??
		//		  (m_CreateDatabaseCommand = new RelayCommand(CreateDatabase));
		//	}
		//	set
		//	{
		//		if (value != m_CreateDatabaseCommand)
		//		{
		//			m_CreateDatabaseCommand = value;
		//			OnPropertyChanged(nameof(CreateDatabaseCommand));
		//		}
		//	}
		//}
		//private async void CreateDatabase(string name)
		//{
		//	string text = string.Empty;
		//	if (m_Database.Exists())
		//	{
		//		text = "The database has already been created. If you continue the database will be over written, including saved creatures. Would you like to continue?";
		//	}
		//	else
		//	{
		//		text = "Creating a new database will delete and replace your current database. This could take a while (around 20-30 minutes), but a dialog box will appear when it is finished. Would you like to continue?";
		//	}

		//	MessageBoxResult result = MessageBox.Show(text, "Database Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
		//	if (result == MessageBoxResult.Yes)
		//	{
		//		m_ProgressVM.StartWork();

		//		// TODO: How does this act if some other code tries to use the ProgressVM?
		//		await Task.Run(() => m_Database.CreateDB(name));

		//		m_ProgressVM.EndWork();

		//		MessageBox.Show("Finished creating the database.");
		//		// TODO: Need to update creature count somewhere
		//		//m_CreatureVM.UpdateTotalCreatureCount();


		//	}
		//}
	}
}
