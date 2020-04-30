using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;

namespace Combiner
{
	public class ModManagerVM : BaseViewModel
	{
		private readonly string m_ModPath = "../../Mods";

		// C:\Program Files (x86)\Steam\steamapps\common\Impossible Creatures

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
			dialog.InitialDirectory = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Impossible Creatures";
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
			dialog.InitialDirectory = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Impossible Creatures";
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

		public void CreateMod(object o)
		{
			if (string.IsNullOrEmpty(CreateModName) 
				|| string.IsNullOrEmpty(AttrPath)
				|| string.IsNullOrEmpty(StockPath))
			{
				MessageBox.Show("Make sure to file out the Name, Attr, and Stock fields.");
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
				string convertedAttrPath = ConvertAttr(AttrPath, newAttrPath);
				File.Copy(convertedAttrPath, Path.Combine(newAttrPath, Path.GetFileName(AttrPath)), true);

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

				MessageBox.Show("Mod Created");
				CreateModName = string.Empty;
				AttrPath = string.Empty;
				StockPath = string.Empty;
			}
		}

		private string ConvertAttr(string src, string dest)
		{

			return src;
		}

	}
}
