using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Combiner
{
	public class CsvWriterVM : BaseViewModel
	{
		private CreatureDataVM m_CreatureVM;
		private CreatureCsvWriter m_CreatureCsvWriter;

		public CsvWriterVM(CreatureDataVM newCreatureVM, 
			CreatureCsvWriter creatureCsvWriter)
		{
			m_CreatureCsvWriter = creatureCsvWriter;
		}

		private ICommand m_ExportToCsvCommand;
		public ICommand ExportToCsvCommand
		{
			get
			{
				return m_ExportToCsvCommand ??
					(m_ExportToCsvCommand = new RelayCommand(ExportToCsv));
			}
			set
			{
				if (value != m_ExportToCsvCommand)
				{
					m_ExportToCsvCommand = value;
					OnPropertyChanged(nameof(ExportToCsvCommand));
				}
			}
		}
		public void ExportToCsv(object obj)
		{
			if (m_CreatureVM.Creatures != null && m_CreatureVM.Creatures.Count > 0)
			{
				m_CreatureCsvWriter.WriteFile(m_CreatureVM.Creatures);
			}
		}
	}
}
