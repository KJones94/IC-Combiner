namespace Combiner.Utility
{
	using System.Collections.Generic;
	using System.IO;
	using System.Windows.Forms;

	using Combiner.Models;
	using Combiner.XML;

	public class ImportExportHandler
	{
		private string m_XMLDirectory = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) , "..\\..\\XML"));

		private Database m_Database;
		private CreatureXMLHandler m_CreatureXMLHandler;

		public ImportExportHandler(Database database)
		{
			this.m_Database = database;
			this.m_CreatureXMLHandler = new CreatureXMLHandler();
		}

		public void Import()
		{
			// Select file from dialog
			string filePath = string.Empty;
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = this.m_XMLDirectory;
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
			IEnumerable<CreatureQueryData> importedCreatureData = this.m_CreatureXMLHandler.GetCreatureDataFromXML(filePath);

			// Save creatures to database
			List<Creature> creatures = new List<Creature>();
			foreach (var data in importedCreatureData)
			{
				creatures.Add(this.m_Database.GetCreature(data.left, data.right, data.bodyParts));
			}

			this.m_Database.SaveCreatures(creatures);
		}

		public void Export()
		{
			// Select file to save as
			string filePath = string.Empty;
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.InitialDirectory = this.m_XMLDirectory;
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
			List<Creature> savedCreatures = this.m_Database.GetSavedCreatures();

			// Write to XML
			this.m_CreatureXMLHandler.AddCreaturesToXML(savedCreatures, filePath);

		}
	}
}
