using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Combiner
{
	public class CreatureXMLHandler
	{
		private static readonly XNamespace ns = "IC";
		private static readonly string m_SchemaDirectory = "../../XML/Schemas";
		private static readonly string m_SavedCreaturesSchema = "SavedCreatures.xsd";

		/// <summary>
		/// Adds the creatures from the XML to the database
		/// </summary>
		public IEnumerable<CreatureQueryData> GetCreatureDataFromXML(string filePath)
		{
			XElement xml = GetXML(filePath);
			if (xml == null)
			{
				return new List<CreatureQueryData>();
			}
			if (!ValidateWithSchema(xml, GetSchema()))
			{
				return new List<CreatureQueryData>();
			}
			XDocument doc = new XDocument(xml);

			IEnumerable<CreatureQueryData> allCreatureData =
				from creature in doc.Descendants(ns + "Creature")
				select new CreatureQueryData()
				{
					left = creature.Element(ns + "left").Value,
					right = creature.Element(ns + "right").Value,
					bodyParts = BuildBodyParts(creature.Element(ns + "bodyParts"))
				};

			return allCreatureData;
		}

		/// <summary>
		/// Builds a dictionary of the body parts from the XML
		/// </summary>
		/// <param name="xmlBodyParts"></param>
		/// <returns></returns>
		private Dictionary<string, string> BuildBodyParts(XElement xmlBodyParts)
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
		public void AddCreaturesToXML(IEnumerable<Creature> creatures, string filePath)
		{
			XElement xmlSavedCreatures;
			if (!File.Exists(filePath))
			{
				xmlSavedCreatures = CreateXML(filePath);
			}
			else
			{
				xmlSavedCreatures = GetXML(filePath);
			}

			if (xmlSavedCreatures == null)
			{
				return;
			}

			foreach (var creature in creatures)
			{
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
			}

			SaveXML(xmlSavedCreatures, filePath);
		}

		/// <summary>
		/// Saves the given XML to a new file or overwrites the given file
		/// </summary>
		/// <param name="xml"></param>
		private void SaveXML(XElement xml, string filePath)
		{
			if (xml == null)
			{
				return;
			}

			if (ValidateWithSchema(xml, GetSchema()))
			{
				XDocument doc = new XDocument(xml);
				doc.Save(filePath);
			}
		}


		/// <summary>
		/// Creates a saved creatures XML file
		/// </summary>
		/// <returns></returns>
		private XElement CreateXML(string filePath)
		{
			XElement newXML = new XElement(ns + "SavedCreatures");
			SaveXML(newXML, filePath);
			return GetXML(filePath);
		}

		/// <summary>
		/// Gets the saved creatures XML from the given file
		/// </summary>
		/// <returns></returns>
		private XElement GetXML(string filePath)
		{
			if (!File.Exists(filePath))
			{
				return null;
			}

			XElement xml;
			using (Stream sr = File.Open(filePath, FileMode.Open))
			{
				xml = XElement.Load(sr);
			}

			return xml;
		}

		/// <summary>
		/// Validates the XML using the given schema
		/// </summary>
		/// <param name="xElement"></param>
		/// <param name="schemas"></param>
		/// <returns></returns>
		private bool ValidateWithSchema(XElement xElement, XmlSchemaSet schemas)
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
		/// Gets the saved creatures XML schema
		/// </summary>
		/// <returns></returns>
		private XmlSchemaSet GetSchema()
		{
			if (!Directory.Exists(m_SchemaDirectory))
			{
				return null;
			}

			XmlSchemaSet schemas = new XmlSchemaSet();
			schemas.Add("IC", Path.Combine(m_SchemaDirectory, m_SavedCreaturesSchema));
			return schemas;
		}


	}
}
