using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using TTracker.GUI.Models;
using TTracker.BaseDataModules;

namespace TTracker.Utility
{
    public sealed class DataAccess
    {
        private static readonly Lazy<DataAccess>
            lazy =
            new Lazy<DataAccess>
            (() => new DataAccess());

        public static DataAccess Instance { get { return lazy.Value; } }

        private XmlDataCache _xmlReaderWriter = new XmlDataCache();

        private CreateTFromXmlData _createTFromXmlData = new CreateTFromXmlData();
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
            _xmlReaderWriter.SaveNewToXml("Users", newUser.Id, XmlWriteableDataList);
        }
        public void RegisterAndSaveNewTaskTicket(TaskTicket newTaskTicket)
        {
            XmlWriteableDataList.Clear();
            XmlWriteableDataList.Add("Id/" + newTaskTicket.Id);
            XmlWriteableDataList.Add("Name/" + newTaskTicket.Name);
            XmlWriteableDataList.Add("UserId/" + newTaskTicket.UserId);
            XmlWriteableDataList.Add("ProjectId/" + newTaskTicket.ProjectId);
            XmlWriteableDataList.Add("ProjectName/" + newTaskTicket.ProjectName);
            XmlWriteableDataList.Add("Text/" + newTaskTicket.Text);
            XmlWriteableDataList.Add("Created/" + newTaskTicket.Created);
            XmlWriteableDataList.Add("ExpectedTime/" + newTaskTicket.ExpectedTime);
            XmlWriteableDataList.Add("UsedTime/" + newTaskTicket.UsedTime);
            _xmlReaderWriter.SaveNewToXml("TaskTickets", newTaskTicket.Id, XmlWriteableDataList);
        }

        /// <summary>
        /// Takes in a type and returns a List of all found Types that is found in the xmlCache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>() where T : Object
        {
            var allData = _xmlReaderWriter.GetAllXmlFilesFromDirectory<T>();

            //Keep adding Entites that can be saved here.
            //Also add a CreateTFromXmlData Method foreach
            switch (typeof(T).Name)
            {
                case "TaskTicket":
                    return (IEnumerable<T>)(_createTFromXmlData.CreateTaskTicketFromXmlData(allData));
                case "":
                    ;
                    break;
            }

            return null;
        }

        /// <summary>
        /// Takes in a ModelBase and a list of changedData, overwrites the xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="saveableObject"></param>
        /// <param name="changedProperties"></param>
        public void Save<T>(ModelBase saveableObject, List<string> changedProperties) where T : ModelBase
        {
            var currentObject = typeof(T);
            var allXmlOfGivenType = _xmlReaderWriter.GetAllXmlFilesFromDirectory<T>();

            foreach (var doc in allXmlOfGivenType)
            {
                var docAllData = doc.Root.Value;
                var docElement = doc.Root.Elements();              

                if(docAllData.ToString().Contains(saveableObject.Id.ToString()))
                {
                    _xmlReaderWriter.OverwriteSaveToXml<T>(doc, changedProperties);
                }
            }

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
                        var desiredUserData = _xmlReaderWriter.GetXmlDataByXmlPath(xmlFile);

                        if (desiredUserData[2] == "Password/" + password)
                        {
                            User desiredUser = _createTFromXmlData.CreateUserFromXmlData(desiredUserData);
                            return desiredUser;
                        }
                    }
                }
            }
            return null;
        }

    }
}
