using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;

namespace TTracker.Utility
{
    internal class CreateTFromData
    {

        /// <summary>
        /// Uses two methods to return a list of all Entities in XML Cache:
        ///CreateTListFromXmlData takes in all XDocuments and trigger foreach doc the CreateTFromXmlData
        ///Had to be seperated so that the variables will be reseted, if a doc.element.value is empty.
        /// </summary>

        #region CreateTFromXmlData
        public CreateTFromData()
        {

        }
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
        public List<TaskTicket> CreateTaskTicketListFromXmlData(List<XDocument> ticketData)
        {
            var allTaskTickets = new List<TaskTicket>();

            //Foreach XDocument in ticketData
            foreach (var doc in ticketData)
            {
                allTaskTickets.Add(CreateTaskTicketFromXmlData(doc));
            }

            return allTaskTickets;
        }
        private protected TaskTicket CreateTaskTicketFromXmlData(XDocument doc)
        {
            var name = string.Empty;
            Guid Id = Guid.Empty;
            Guid userId = Guid.Empty;
            Guid projectId = Guid.Empty;
            var text = string.Empty;
            DateTime created = DateTime.Now;
            float expectedTime = 0;
            float usedTime = 0;
            string priority = string.Empty;
            string status = string.Empty;


            var docAllData = doc.Root.Value;
            var docElement = doc.Root.Elements();

            //Foreach element in XML FIle -> Id/432985094589 etc.
            foreach (var element in docElement)
            {
                var data = element.ToString();

                if (data != null)
                {
                    string[] splitedData = data.Split(new char[] { '/' });

                    switch (splitedData.Last())
                    {
                        case "Name>":
                            name = element.Value;
                            break;
                        case "Id>":
                            Id = new Guid(element.Value);
                            break;
                        case "UserId>":
                            userId = new Guid(element.Value);
                            break;
                        case "ProjectId>":
                            projectId = new Guid(element.Value);
                            break;
                        case "Text>":
                            text = element.Value;
                            break;
                        case "Created>":
                            created = DateTime.Parse(element.Value);
                            break;
                        case "ExpectedTime>":
                            expectedTime = float.Parse(element.Value);
                            break;
                        case "UsedTime>":
                            usedTime = float.Parse(element.Value);
                            break;
                        case "Priority>":
                            priority = element.Value;
                            break;
                        case "Status>":
                            status = element.Value;
                            break;
                    }
                }
            }
            return (new TaskTicket(name, Id, userId, projectId, text, created, expectedTime, usedTime, priority, status));
        }
        public List<Project> CreateProjectListFromXmlData(List<XDocument> projectData)
        {
            var allProjects = new List<Project>();
            //Foreach XDocument in projectData
            foreach (var doc in projectData)
            {
                allProjects.Add(CreateProjectFromXmlData(doc));
            }
            return allProjects;
        }
        private protected Project CreateProjectFromXmlData(XDocument doc)
        {
            var name = string.Empty;
            Guid Id = Guid.Empty;
            Guid userId = Guid.Empty;
            Guid parentId = Guid.Empty;
            var text = string.Empty;
            DateTime created = DateTime.Now;
            float usedTime = 0;

            var docAllData = doc.Root.Value;
            var docElement = doc.Root.Elements();

            //Foreach element in XML FIle -> Id/432985094589 etc.
            foreach (var element in docElement)
            {
                var data = element.ToString();

                if (data != null)
                {
                    string[] splitedData = data.Split(new char[] { '/' });

                    switch (splitedData.Last())
                    {
                        case "Name>":
                            name = element.Value;
                            break;
                        case "Id>":
                            Id = new Guid(element.Value);
                            break;
                        case "UserId>":
                            userId = new Guid(element.Value);
                            break;
                        case "ParentId>":
                            parentId = new Guid(element.Value);
                            break;
                        case "Text>":
                            text = element.Value;
                            break;
                        case "Created>":
                            created = DateTime.Parse(element.Value);
                            break;
                        case "UsedTime>":
                            usedTime = float.Parse(element.Value);
                            break;
                    }
                }
            }
            return new Project(name, Id, userId, parentId, text, created, usedTime);
        }
        public List<TimeEntry> CreateTimeEntryListFromXmlData(List<XDocument> timeEntryData)
        {
            var allTimeEntries = new List<TimeEntry>();

            //Foreach XDocument in allTimeEntries
            foreach (var doc in timeEntryData)
            {
                allTimeEntries.Add(CreateTimeEntryFromXmlData(doc));
            }
            return allTimeEntries;
        }
        private protected TimeEntry CreateTimeEntryFromXmlData(XDocument doc)
        {
            Guid Id = Guid.Empty;
            Guid userId = Guid.Empty;
            Guid projectId = Guid.Empty;
            Guid ticketId = Guid.Empty;
            var text = string.Empty;
            DateTime created = DateTime.Now;
            float startTime = 0;
            float endTime = 0;

            var docAllData = doc.Root.Value;
            var docElement = doc.Root.Elements();

            //Foreach element in XML FIle -> Id/432985094589 etc.
            foreach (var element in docElement)
            {
                var data = element.ToString();

                if (data != null)
                {
                    string[] splitedData = data.Split(new char[] { '/' });

                    switch (splitedData.Last())
                    {
                        case "Id>":
                            Id = new Guid(element.Value);
                            break;
                        case "UserId>":
                            userId = new Guid(element.Value);
                            break;
                        case "ProjectId>":
                            projectId = new Guid(element.Value);
                            break;
                        case "TicketId>":
                            ticketId = new Guid(element.Value);
                            break;
                        case "Text>":
                            text = element.Value;
                            break;
                        case "Created>":
                            created = DateTime.Parse(element.Value);
                            break;
                        case "StartTime>":
                            startTime = float.Parse(element.Value);
                            break;
                        case "EndTime>":
                            endTime = float.Parse(element.Value);
                            break;
                    }
                }
            }
            return (new TimeEntry(Id, userId, projectId, ticketId, text, startTime, endTime, created));
        }
        public List<MicroTask> CreateMicroTaskListFromXmlData(List<XDocument> microTaskData)
        {
            var allMicroTasks = new List<MicroTask>();

            //Foreach XDocument in allTimeEntries
            foreach (var doc in microTaskData)
            {
                allMicroTasks.Add(CreateMicroTaskFromXmlData(doc));
            }
            return allMicroTasks;
        }
        private protected MicroTask CreateMicroTaskFromXmlData(XDocument doc)
        {
            Guid Id = Guid.Empty;
            Guid userId = Guid.Empty;
            var text = string.Empty;
            DateTime created = DateTime.Now;
            float isChecked = 0;

            var docAllData = doc.Root.Value;
            var docElement = doc.Root.Elements();

            //Foreach element in XML FIle -> Id/432985094589 etc.
            foreach (var element in docElement)
            {
                var data = element.ToString();

                if (data != null)
                {
                    string[] splitedData = data.Split(new char[] { '/' });

                    switch (splitedData.Last())
                    {
                        case "Id>":
                            Id = new Guid(element.Value);
                            break;
                        case "UserId>":
                            userId = new Guid(element.Value);
                            break;
                        case "Text>":
                            text = element.Value;
                            break;
                        case "Created>":
                            created = DateTime.Parse(element.Value);
                            break;
                        case "IsChecked>":
                            isChecked = float.Parse(element.Value);
                            break;
                    }
                }
            }
            return (new MicroTask(Id, userId, text, created, isChecked));
        }

        #endregion

        #region CreateTFromDecodedData
        public TaskTicket CreateTaskTicketFromDecodedData(List<string> decodedData)
        {
            var name = string.Empty;
            Guid Id = Guid.Empty;
            Guid userId = Guid.Empty;
            Guid projectId = Guid.Empty;
            var text = string.Empty;
            DateTime created = DateTime.Now;
            float expectedTime = 0;
            float usedTime = 0;
            string priority = string.Empty;
            string status = string.Empty;

            if (decodedData != null)
            {
                foreach(var data in decodedData)
                {
                    string[] splitedData = data.Split(new char[] { '/' });
                    var dataName = splitedData[0];
                    var dataValue = string.Empty;
                    for (int i = 1; i < splitedData.Length; i++)
                    {
                        if(i == 1)
                        {
                            dataValue += splitedData[i];
                        }
                        else
                        {
                            dataValue += "/" + splitedData[i];
                        }
                    }

                    switch (dataName)
                    {
                        case "Name":
                            name = "(Imported) " + dataValue;
                            break;
                        case "Id":
                            Id = new Guid(dataValue);
                            break;
                        case "UserId":
                            userId = new Guid(dataValue);
                            break;
                        case "ProjectId":
                            projectId = new Guid(dataValue);
                            break;
                        case "Text":
                            text = dataValue;
                            break;
                        case "Created":
                            created = DateTime.Parse(dataValue);
                            break;
                        case "ExpectedTime":
                            expectedTime = float.Parse(dataValue);
                            break;
                        case "UsedTime":
                            usedTime = float.Parse(dataValue);
                            break;
                        case "Priority":
                            priority = dataValue;
                            break;
                        case "Status":
                            status = dataValue;
                            break;
                    }
                }
                
            }
            return (new TaskTicket(name, Id, userId, projectId, text, created, expectedTime, usedTime, priority, status));
        }

        #endregion
    }
}
