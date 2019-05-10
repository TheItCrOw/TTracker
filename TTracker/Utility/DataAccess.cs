using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using TTracker.GUI.Models;
using TTracker.GUI.ViewModels;

namespace TTracker.Utility
{
    public sealed class DataAccess
    {
        private static readonly Lazy<DataAccess>
            lazy =
            new Lazy<DataAccess>
            (() => new DataAccess());

        public static DataAccess Instance { get { return lazy.Value; } }

        public static User CurrentLoggedUser { get; set; }

        private string _saveDataPath = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Data\\";

        //Writes the given data into an xml file. Saves it that way
        private List<string> XmlWriteableDataList = new List<string>();

        private DataAccess()
        {

        }

        public void RegisterAndSaveNewUser(User newUser)
        {
            XmlWriteableDataList.Clear();
            XmlWriteableDataList.Add("Id/" + newUser.Id);
            XmlWriteableDataList.Add("Name/" + newUser.Name);
            XmlWriteableDataList.Add("Password/" + newUser.Password);
            XmlWriteableDataList.Add("Created/" + newUser.Created.ToString());
            SaveToXml("Users", newUser.Id, XmlWriteableDataList);
        }

        public bool IsValidUser(string name, string password)
        {
            var desiredUser = GetUserByNameAndPassword(name, password);

            if (desiredUser != null)
            {
                CurrentLoggedUser = desiredUser;
                return true;
            }
            return false;
        }


        private User GetUserByNameAndPassword(string name, string password)
        {
            var directoryPathFolder = Directory.GetFiles(_saveDataPath + "Users");

            foreach (var xmlFile in directoryPathFolder)
            {
                var doc = XDocument.Load(xmlFile);
                var docAllData = doc.Root.Value;
                var docElement = doc.Root.Elements();

                foreach (var element in docElement)
                {
                    var nodeValue = element.Value;
                    if (nodeValue.ToString() == name)
                    {
                        var desiredUserData = GetXmlDataByXmlPath(xmlFile);

                        if (desiredUserData[2] == "Password/" + password)
                        {
                            User desiredUser = CreateUserFromXmlData(desiredUserData);
                            return desiredUser;
                        }
                    }
                }
            }
            return null;
        }

        //Takes in the userData as a list<string> and returns an instance of the user class out of it.
        public User CreateUserFromXmlData(List<string> userData)
        {
            string name = string.Empty;
            string password = string.Empty;
            Guid Id = Guid.Empty;
            DateTime created = DateTime.Now;

            foreach (var data in userData)
            {
                if (data != null)
                {
                    string[] splitedData = data.Split(new char[] { '/' });

                    switch (splitedData[0])
                    {
                        case "Name":
                            name = splitedData[1];
                            break;
                        case "Password":
                            password = splitedData[1];
                            break;
                        case "Id":
                            Id = new Guid(splitedData[1]);
                            break;
                        case "Created":
                            created = DateTime.Parse(splitedData[1]);
                            break;
                    }
                }
            }
            return new User(name, password, Id, created);
        }

        /// <summary>
        /// This takes in a xmlFilePath and returns a List<string> that contains the data
        /// formatted like Name/Value
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        private List<string> GetXmlDataByXmlPath(string xmlFilePath)
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


        /// <summary>
        /// Writes the data that is given into the list into a xml file. 
        /// Split like this: "Name/" + user.Name
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
