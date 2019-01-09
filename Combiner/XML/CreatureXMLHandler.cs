using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Combiner
{
	public static class CreatureXMLHandler
	{
		private static readonly XNamespace ns = "IC";

		/// <summary>
		/// Adds the creatures from the XML to the database
		/// </summary>
		public static void LoadSavedCreaturesFromXML()
		{
			XElement xml = GetXML();
			if (xml == null)
			{
				return;
			}
			if (!ValidateWithSchema(xml, GetSchema()))
			{
				return;
			}
			XDocument doc = new XDocument(xml);

			IEnumerable<CreatureData> allCreatureData =
				from creature in doc.Descendants(ns + "Creature")
				select new CreatureData()
				{
					left = creature.Element(ns + "left").Value,
					right = creature.Element(ns + "right").Value,
					bodyParts = BuildBodyParts(creature.Element(ns + "bodyParts"))
				};

			List<Creature> creatures = new List<Creature>();
			foreach (var data in allCreatureData)
			{
				creatures.Add(Database.GetCreature(data.left, data.right, data.bodyParts));
			}
			Database.SaveCreatures(creatures);
		}

		/// <summary>
		/// Builds a dictionary of the body parts from the XML
		/// </summary>
		/// <param name="xmlBodyParts"></param>
		/// <returns></returns>
		private static Dictionary<string, string> BuildBodyParts(XElement xmlBodyParts)
		{
			Dictionary<string, string> bodyParts = new Dictionary<string, string>();
			foreach (var item in xmlBodyParts.Descendants(ns + "item"))
			{
				bodyParts.Add(item.Element(ns + "key").Value, item.Element(ns + "value").Value);
			}
			return bodyParts;
		}

		/// <summary>
		/// Adds creatures to saved creatures XML
		/// </summary>
		/// <param name="creatures"></param>
		public static void AddCreaturesToXML(IEnumerable<Creature> creatures)
		{
			foreach (var creature in creatures)
			{
				AddCreatureToXML(creature);
			}
		}

		/// <summary>
		/// Adds the creature to the saved creatures XML
		/// </summary>
		/// <param name="creature"></param>
		public static void AddCreatureToXML(Creature creature)
		{
			XElement xmlSavedCreatures = CreateXML();

			XElement xmlBodyParts = new XElement(ns + "bodyParts");
			foreach (var key in creature.BodyParts.Keys)
			{
				xmlBodyParts.Add(new XElement(ns + "item",
					new XElement(ns + "key", key),
					new XElement(ns + "value", creature.BodyParts[key])));
			}

			XElement xmlCreature = new XElement(ns + "Creature");
			xmlCreature.Add(new XElement(ns + "left", creature.Left),
				new XElement(ns + "right", creature.Right),
				xmlBodyParts);

			xmlSavedCreatures.Add(xmlCreature);
			SaveXml(xmlSavedCreatures);
		}

		/// <summary>
		/// Saves the given XML
		/// </summary>
		/// <param name="xml"></param>
		public static void SaveXml(XElement xml)
		{
			if (xml == null)
			{
				return;
			}

			string directoryPath = "../../XML";
			if (!Directory.Exists(directoryPath))
			{
				//Directory.CreateDirectory(directoryPath);
				return;
			}

			string filePath = Path.Combine(directoryPath, "SavedCreatures.xsd");
			//if (File.Exists(filePath) 
			//{

			//}

			XmlSchemaSet schemas = new XmlSchemaSet();
			schemas.Add("IC", filePath);

			if (ValidateWithSchema(xml, schemas))
			{
				XDocument doc = new XDocument(xml);
				string fileName = "SavedCreatures.xml";
				doc.Save(Path.Combine(directoryPath, fileName));
			}
		}

		/// <summary>
		/// Validates the XML using the given schema
		/// </summary>
		/// <param name="xElement"></param>
		/// <param name="schemas"></param>
		/// <returns></returns>
		public static bool ValidateWithSchema(XElement xElement, XmlSchemaSet schemas)
		{
			string errorMessage = string.Empty;
			bool result = false;

			try
			{
				if (xElement != null && schemas != null)
				{
					XDocument xmlDoc = new XDocument(xElement);
					xmlDoc.Validate(schemas, (sender, eventArgs) => { errorMessage = eventArgs.Message; });
				}
				else
				{
					errorMessage = "Null xml or schema";
				}

				if (string.IsNullOrWhiteSpace(errorMessage))
				{
					result = true;
				}
			}
			catch (XmlSchemaValidationException ex)
			{
				errorMessage = ex.Message;
			}

			Console.WriteLine(errorMessage);

			return result;
		}

		/// <summary>
		/// Creates the saved creatures XML file
		/// </summary>
		/// <returns></returns>
		public static XElement CreateXML()
		{
			string file = "../../XML/SavedCreatures.xml";
			if (!File.Exists(file))
			{
				return new XElement(ns + "SavedCreatures");
			}

			XElement xml;
			using (Stream sr = File.Open(file, FileMode.Open))
			{
				xml = XElement.Load(sr);
			}

			return xml;
		}

		/// <summary>
		/// Gets the XML from the saved creatures XML file
		/// </summary>
		/// <returns></returns>
		public static XElement GetXML()
		{
			string file = "../../XML/SavedCreatures.xml";
			if (!File.Exists(file))
			{
				return null;
			}

			XElement xml;
			using (Stream sr = File.Open(file, FileMode.Open))
			{
				xml = XElement.Load(sr);
			}

			return xml;
		}

		/// <summary>
		/// Gets the saved creatures XML schema
		/// </summary>
		/// <returns></returns>
		public static XmlSchemaSet GetSchema()
		{
			string directoryPath = "../../XML";
			if (!Directory.Exists(directoryPath))
			{
				//Directory.CreateDirectory(directoryPath);
				return null;
			}

			XmlSchemaSet schemas = new XmlSchemaSet();
			schemas.Add("IC", Path.Combine(directoryPath, "SavedCreatures.xsd"));
			return schemas;
		}

		internal class CreatureData
		{
			public string left;
			public string right;
			public Dictionary<string, string> bodyParts;
		}
	}
}
