using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Combiner
{
	public class ImportExportHandler
	{
		private string m_XMLDirectory = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) , "..\\..\\XML"));

		private Database m_Database;
		private CreatureXMLHandler m_CreatureXMLHandler;

		public ImportExportHandler(Database database)
		{
			m_Database = database;
			m_CreatureXMLHandler = new CreatureXMLHandler();
		}

		public void Import(ModCollection modCollection)
		{
			// Select file from dialog
			string filePath = string.Empty;
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = m_XMLDirectory;
				openFileDialog.Filter = "XML files (*.xml)|*.xml";
				openFileDialog.RestoreDirectory = true;

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					filePath = openFileDialog.FileName;
				}
			}

			if (string.IsNullOrEmpty(filePath))
			{
				return;
			}

			// Read XML and all that

			IEnumerable<CreatureQueryData> importedCreatureData = m_CreatureXMLHandler.GetCreatureDataFromXML(filePath);

			// Save creatures to database

			ModCollection mainMod = m_Database.getMainMod(modCollection.ModName);
			List<Creature> creatures = new List<Creature>();
			foreach (var data in importedCreatureData)
			{
				creatures.Add(m_Database.GetCreature(data.left, data.right, data.bodyParts, mainMod));
			}
			m_Database.SaveCreatures(creatures, modCollection);
		}

		public void Export(ModCollection modCollection)
		{
			// Select file to save as

			string filePath = string.Empty;
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.InitialDirectory = m_XMLDirectory;
				saveFileDialog.Filter = "XML files (*.xml)|*.xml";
				saveFileDialog.RestoreDirectory = true;

				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					filePath = saveFileDialog.FileName;
				}
			}

			if (string.IsNullOrEmpty(filePath))
			{
				return;
			}

			// Get creature data from database

			IEnumerable<Creature> savedCreatures = m_Database.GetAllCreatures(modCollection);

			// Write to XML

			m_CreatureXMLHandler.AddCreaturesToXML(savedCreatures, filePath);

		}
	}
}
