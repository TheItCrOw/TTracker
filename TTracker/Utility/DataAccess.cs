using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using TTracker.GUI.Models;

namespace TTracker.Utility
{
    public sealed class DataAccess
    {
        private static readonly Lazy<DataAccess>
            lazy =
            new Lazy<DataAccess>
            (() => new DataAccess());

        public static DataAccess Instance { get { return lazy.Value; } }

        private string _saveDataPath = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Data\\";

        //Keeps track of all the registeredUsers
        private List<string> SaveableDataList = new List<string>();

        private DataAccess()
        {

        }

        public void RegisterUser(Object newUser)
        {
            var _newUser = (User)newUser;
            SaveableDataList.Clear();
            SaveableDataList.Add("Id/" + _newUser.Id);
            SaveableDataList.Add("Name/" + _newUser.Name);
            SaveableDataList.Add("Password/" + _newUser.Password);
            SaveableDataList.Add("Created/" + _newUser.Created.ToString());
            SaveToXml("Users", _newUser.Id, SaveableDataList);
        }

        public void LoginUser(string name, string password)
        {
            //Check if the user even exists
            if(ReadFromXml("Users", "name") == name)
            {
                
            }
        }

        //This should take in the ID and returns the the User.
        private User GetUserById(Guid id)
        {
            return null;
        }

        /// <summary>
        /// This takes in the directoryName in which it should search and the desiredNodeName, for the value you want
        /// E.g. desiredNodeName = Name, returns Hans
        /// </summary>
        /// <param name="directoryName"></param>
        /// <param name="desiredNodeName"></param>
        /// <returns></returns>
        private string ReadFromXml(string directoryName, string desiredNodeName)
        {
            var directoryPathFolder = Directory.GetFiles(_saveDataPath + directoryName);

            foreach(var xmlFile in directoryPathFolder)
            {
                var doc = XDocument.Load(xmlFile.ToString());
                var docAllData = doc.Root.Value;
                var docElement = doc.Root.Elements();

                foreach (var element in docElement)
                {
                    //This contains the value of only one node and ONLY the value
                    var node = element.Name;

                    if(node.ToString().ToLower().Contains(desiredNodeName))
                    {
                        return element.Value;
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Writes the data that is given into the list into a xml file. 
        /// Split like this: Name/paul
        /// </summary>
        /// <param name="directoryName"></param>
        /// <param name="data"></param>
        private void SaveToXml(string directoryName, Guid Id, List<string> data)
        {
            Directory.CreateDirectory(_saveDataPath + directoryName);
            string xmlPath = _saveDataPath + directoryName + "\\";
            string xmlName = Id.ToString() + ".xml";
            string fullLocationPath = xmlPath + xmlName;

            using (XmlWriter writer = XmlWriter.Create(fullLocationPath))
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

    }
}
