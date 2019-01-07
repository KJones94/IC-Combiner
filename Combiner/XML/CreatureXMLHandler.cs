using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Combiner.XML
{
	public static class CreatureXMLHandler
	{
		private static readonly XNamespace ns = "IC";

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

		private static Dictionary<string, string> BuildBodyParts(XElement xmlBodyParts)
		{
			Dictionary<string, string> bodyParts = new Dictionary<string, string>();
			foreach (var item in xmlBodyParts.Descendants(ns + "item"))
			{
				bodyParts.Add(item.Element(ns + "key").Value, item.Element(ns + "value").Value);
			}
			return bodyParts;
		}

		public static void AddCreatures(IEnumerable<Creature> creatures)
		{
			foreach (var creature in creatures)
			{
				AddCreature(creature);
			}
		}

		public static void AddCreature(Creature creature)
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

			XmlSchemaSet schemas = new XmlSchemaSet();
			schemas.Add("IC", Path.Combine(directoryPath, "SavedCreatures.xsd"));

			if (ValidateWithSchema(xml, schemas))
			{
				XDocument doc = new XDocument(xml);
				string fileName = "SavedCreatures.xml";
				doc.Save(Path.Combine(directoryPath, fileName));
			}
		}

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
