using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TTracker.Interfaces;

namespace TTracker.Utility
{
    internal class XmlDataCache
    {

        private string _saveDataPath = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Data\\";

        /// <summary>
        /// Writes the data that is given into the list into a xml file. 
        /// Split like this: "Name/" + user.Name
        /// </summary>
        /// <param name="directoryName"></param>
        /// <param name="data"></param>
        public void SaveNewToXml(string directoryName, Guid Id, List<string> data)
        {
            Directory.CreateDirectory(_saveDataPath + directoryName);
            string xmlPath = _saveDataPath + directoryName + "\\";
            string xmlName = Id.ToString() + ".xml";
            string fullLocationPath = xmlPath + xmlName;

            //So that the xmlWriter makes breaks between lines
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.NewLineOnAttributes = true;
            xmlWriterSettings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(fullLocationPath, xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Data");

                foreach (var s in data)
                {
                    string[] splitedString = s.Split(new char[] { '/' });
                    string dataName = splitedString[0];
                    string dataValue = splitedString[1];

                    writer.WriteElementString(dataName, dataValue);
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();

            }

        }


        /// <summary>
        /// This takes in a XmlDocument and a list of changedProperties
        /// It overwrites to the current doc the new propertyValues
        /// </summary>
        /// <param name="saveableXmlDoc"></param>
        /// <param name="changedProperties"></param>
        public void OverwriteSaveToXml<T>(XDocument saveableXmlDoc, List<string> changedProperties)
        {
            var changedPropertyName = new List<string>();
            var changedPropertyValue = new List<string>();

            string docId = string.Empty;

            foreach(var data in changedProperties)
            {
                string[] splitedString = data.Split(new char[] { '/' });
                changedPropertyName.Add((splitedString[0] + ">"));
                changedPropertyValue.Add(splitedString[1]);
            }

            var docElements = saveableXmlDoc.Root.Elements();
            foreach(var element in docElements)
            {
                string[] splitedString = element.ToString().Split(new char[] { '/' });
                var elementName = splitedString[1];

                if (elementName == "Id>")
                {
                    docId = element.Value;
                }

                if (changedPropertyName.Contains(elementName))
                {
                    var index = changedPropertyName.IndexOf(elementName);
                    element.Value = changedPropertyValue.ElementAt(index);
                }
            }
            var fullSavePath = _saveDataPath + typeof(T).Name + "s" + "\\" + docId + ".xml";
            saveableXmlDoc.Save(fullSavePath);
        }

        /// <summary>
        /// This takes in a xmlFilePath and returns a List<string> that contains the data
        /// formatted like Name/Value
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        public List<string> GetXmlDataByXmlPath(string xmlFilePath)
        {
            var doc = XDocument.Load(xmlFilePath);
            var docElements = doc.Root.Elements();

            List<string> data = new List<string>();

            foreach (var element in docElements)
            {
                data.Add(element.Name + "/" + element.Value);
            }
            return data;
        }

        public List<XDocument> GetAllXmlFilesFromDirectory<T>()
        {
            //Gets the type of generic T
            var result = typeof(T);
            var directoryName = result.Name;
            //Path of directory all xml files should be loaded from
            string directoryXmlPath = _saveDataPath + directoryName + "s\\";

            //Stores all the file Names in an array
            string[] files = Directory.GetFiles(directoryXmlPath);
            //Saves all docs in direcotry
            var allDoc = new List<XDocument>();
           

            //Foreach file in files Directory
            foreach(var file in files)
            {
                //Read every file. File is the path
                var doc = XDocument.Load(file);
                allDoc.Add(doc);
            }

            return allDoc;
        }

    }
}
