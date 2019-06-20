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
    internal class CreateTFromXmlData
    {

        /// <summary>
        // Uses two methods to return a list of all Entities in XML Cache:
        //CreateTListFromXmlData takes in all XDocuments and trigger foreach doc the CreateTFromXmlData
        //Had to be seperated so that the variables will be reseted, if a doc.element.value is empty.
        /// </summary>


        public CreateTFromXmlData()
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
        internal protected TaskTicket CreateTaskTicketFromXmlData(XDocument doc)
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
        internal protected Project CreateProjectFromXmlData(XDocument doc)
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
        internal protected TimeEntry CreateTimeEntryFromXmlData(XDocument doc)
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




    }
}
