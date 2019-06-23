using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using TTracker.GUI.Models;
using TTracker.BaseDataModules;
using TTracker.GUI.ViewModels.Entities;

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

        private CreateTFromData _createTFromXmlData = new CreateTFromData();
        public static User CurrentLoggedUser { get; set; }

        private string _saveDataPath = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Data\\";

        //Writes the given data into an xml file. Saves it that way
        private List<string> XmlWriteableDataList = new List<string>();

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
            XmlWriteableDataList.Add("Text/" + newTaskTicket.Text);
            XmlWriteableDataList.Add("Created/" + newTaskTicket.Created);
            XmlWriteableDataList.Add("ExpectedTime/" + newTaskTicket.ExpectedTime);
            XmlWriteableDataList.Add("UsedTime/" + newTaskTicket.UsedTime);
            XmlWriteableDataList.Add("Priority/" + newTaskTicket.Priority);
            XmlWriteableDataList.Add("Status/" + newTaskTicket.Status);
            _xmlReaderWriter.SaveNewToXml("TaskTickets", newTaskTicket.Id, XmlWriteableDataList);
        }
        public void RegisterAndSaveNewProject(Project newProject)
        {
            XmlWriteableDataList.Clear();
            XmlWriteableDataList.Add("Id/" + newProject.Id);
            XmlWriteableDataList.Add("Name/" + newProject.Name);
            XmlWriteableDataList.Add("UserId/" + newProject.UserId);
            XmlWriteableDataList.Add("ParentId/" + newProject.ParentId);
            XmlWriteableDataList.Add("Text/" + newProject.Text);
            XmlWriteableDataList.Add("Created/" + newProject.Created);
            XmlWriteableDataList.Add("UsedTime/" + newProject.UsedTime);
            _xmlReaderWriter.SaveNewToXml("Projects", newProject.Id, XmlWriteableDataList);
        }
        public void RegisterAndSaveNewTimeEntry(TimeEntry newTimeEntry)
        {
            XmlWriteableDataList.Clear();
            XmlWriteableDataList.Add("Id/" + newTimeEntry.Id);
            XmlWriteableDataList.Add("UserId/" + newTimeEntry.UserId);
            XmlWriteableDataList.Add("TicketId/" + newTimeEntry.TicketId);
            XmlWriteableDataList.Add("Text/" + newTimeEntry.Text);
            XmlWriteableDataList.Add("Created/" + newTimeEntry.Created);
            XmlWriteableDataList.Add("ProjectId/" + newTimeEntry.ProjectId);
            XmlWriteableDataList.Add("StartTime/" + newTimeEntry.StartTime);
            XmlWriteableDataList.Add("EndTime/" + newTimeEntry.EndTime);
            _xmlReaderWriter.SaveNewToXml("TimeEntrys", newTimeEntry.Id, XmlWriteableDataList);
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
                    return (IEnumerable<T>)(_createTFromXmlData.CreateTaskTicketListFromXmlData(allData));
                case "Project":
                    return (IEnumerable<T>)(_createTFromXmlData.CreateProjectListFromXmlData(allData));
                case "TimeEntry":
                    return (IEnumerable<T>)(_createTFromXmlData.CreateTimeEntryListFromXmlData(allData));
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
                var docId = doc.Root.FirstNode;

                if (docId.ToString().Contains(saveableObject.Id.ToString()))
                {
                    _xmlReaderWriter.OverwriteSaveToXml<T>(doc, changedProperties);
                }
            }
        }

        /// <summary>
        /// Deletes an Entity
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>        
        public void DeleteEntity<T>(ModelBase deletableObject) where T : ModelBase
        {
            var currentObject = typeof(T);
            var allXmlOfGivenType = _xmlReaderWriter.GetAllXmlFilesFromDirectory<T>();

            foreach (var doc in allXmlOfGivenType)
            {
                var docAllData = doc.Root.Value;
                var docElement = doc.Root.Elements();
                var docId = doc.Root.FirstNode;

                if (docId.ToString().Contains(deletableObject.Id.ToString()))
                {
                    _xmlReaderWriter.DeleteTFromXmlCache<T>(doc);
                }

            }
        }
        public void ExportEntity<T>(Guid Id, string exportPath)
        {
            var directoryPathFolder = Directory.GetFiles(_saveDataPath + typeof(T).Name + "s");

            foreach (var xmlFile in directoryPathFolder)
            {
                var doc = XDocument.Load(xmlFile);
                var docElement = doc.Root.Elements();

                foreach (var element in docElement)
                {
                    var nodeValue = element.Value;
                    if (nodeValue.ToString() == Id.ToString())
                    {
                        var xmlDataList = _xmlReaderWriter.GetXmlDataByXmlPath(xmlFile);
                        CustomBase64Writer.WriteDataAsEncodedBase64(exportPath, xmlDataList);
                    }
                }
            }
        }
        public void ImportEntity<T>(string fullPath)
        {
            var fileOnlyName = Path.GetFileName(fullPath);
            var encodedContent = File.ReadAllText(fullPath);
            var decodedContent = CustomBase64Writer.DecodeDataFromBase64(encodedContent);

            string[] splitedData = decodedContent.Split(new string[] { "~THISISANEXPORTEDFILEOFTTRACKER~" }, StringSplitOptions.None);
            var dataList = new List<string>();
            for (int i = 1; i < splitedData.Length; i++)
            {
                dataList.Add(splitedData[i]);
            }

            switch (typeof(T).Name)
            {
                case "TaskTicket":
                    var taskTicket = _createTFromXmlData.CreateTaskTicketFromDecodedData(dataList);
                    RegisterAndSaveNewTaskTicket(taskTicket);
                    break;
                default:
                    break;
            }
        }
        internal bool IsValidUser(string name, string password)
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
        public Project GetProjectById(Guid Id)
        {
            var directoryPathFolder = Directory.GetFiles(_saveDataPath + "Projects");

            foreach (var xmlFile in directoryPathFolder)
            {
                var doc = XDocument.Load(xmlFile);
                var docAllData = doc.Root.Value;
                var docElement = doc.Root.Elements();

                foreach (var element in docElement)

                {
                    var nodeValue = element.Value;
                    var nodeName = element.Name.LocalName;

                    if (nodeName == "Id" && nodeValue.ToString() == Id.ToString())
                    {
                        var desiredUserData = _xmlReaderWriter.GetXmlDataByXmlPath(xmlFile);
                        //Bit hacky here. The CreateProjectFromXmlData takes in a list of xDocument, not just one.
                        //So Create a list, add only one project and return the one project of that list.
                        var xDocList = new List<XDocument>();
                        xDocList.Add(doc);
                        var projects = _createTFromXmlData.CreateProjectListFromXmlData(xDocList);
                        Project project = projects.Find(p => p.ToString() != string.Empty);
                        return project;
                    }
                }
            }
            return null;
        }
        public TaskTicket GetTaskTicketById(Guid Id)
        {
            var directoryPathFolder = Directory.GetFiles(_saveDataPath + "TaskTickets");

            foreach (var xmlFile in directoryPathFolder)
            {
                var doc = XDocument.Load(xmlFile);
                var docAllData = doc.Root.Value;
                var docElement = doc.Root.Elements();

                foreach (var element in docElement)
                {
                    var nodeValue = element.Value;
                    if (nodeValue.ToString() == Id.ToString())
                    {
                        var desiredUserData = _xmlReaderWriter.GetXmlDataByXmlPath(xmlFile);
                        //Bit hacky here. The CreateProjectFromXmlData takes in a list of xDocument, not just one.
                        //So Create a list, add only one project and return the one project of that list.
                        var xDocList = new List<XDocument>();
                        xDocList.Add(doc);
                        var tickets = _createTFromXmlData.CreateTaskTicketListFromXmlData(xDocList);
                        TaskTicket taskTicket = tickets.Find(p => p.ToString() != string.Empty);
                        return taskTicket;
                    }
                }
            }

            return null;
        }

    }
}
