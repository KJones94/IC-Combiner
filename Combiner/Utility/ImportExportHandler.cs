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

		public void Import()
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

			List<Creature> creatures = new List<Creature>();
			foreach (var data in importedCreatureData)
			{
				creatures.Add(m_Database.GetCreature(data.left, data.right, data.bodyParts));
			}
			m_Database.SaveCreatures(creatures);
		}

		public void Import(string collectionName)
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

			List<Creature> creatures = new List<Creature>();
			foreach (var data in importedCreatureData)
			{
				creatures.Add(m_Database.GetCreature(data.left, data.right, data.bodyParts));
			}
			m_Database.SaveCreatures(creatures, collectionName);
		}

		public void Export()
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

			List<Creature> savedCreatures = m_Database.GetSavedCreatures();

			// Write to XML

			m_CreatureXMLHandler.AddCreaturesToXML(savedCreatures, filePath);

		}

		public void Export(string collectionName)
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

			IEnumerable<Creature> savedCreatures = m_Database.GetAllCreatures(collectionName);

			// Write to XML

			m_CreatureXMLHandler.AddCreaturesToXML(savedCreatures, filePath);

		}
	}
}
