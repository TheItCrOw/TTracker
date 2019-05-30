using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TTracker.GUI.Models;

namespace TTracker.Utility
{
    internal class CreateTFromXmlData
    {

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

        public List<TaskTicket> CreateTaskTicketFromXmlData(List<XDocument> ticketData)
        {
            var name = string.Empty;
            Guid Id = Guid.Empty;
            Guid userId = Guid.Empty;
            Guid projectId = Guid.Empty;
            var text = string.Empty;
            DateTime created = DateTime.Now;
            var projectName = string.Empty;
            float expectedTime = 0;
            float usedTime = 0;

            var allTaskTickets = new List<TaskTicket>();

            //Foreach XDocument in ticketData
            foreach (var doc in ticketData)
            {
                var docAllData = doc.Root.Value;
                var docElement = doc.Root.Elements();

                //Foreach element in XML FIle -> Id/432985094589 etc.
                foreach (var element in docElement)
                {
                    var data = element.ToString();

                    if (data != null)
                    {
                        string[] splitedData = data.Split(new char[] { '/' });

                        switch (splitedData[1])
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
                            case "ProjectName>":
                                projectName = (element.Value);
                                break;
                            case "ExpectedTime>":
                                expectedTime = float.Parse(element.Value);
                                break;
                            case "UsedTime>":
                                usedTime = float.Parse(element.Value);
                                break;
                        }
                    }
                }
                allTaskTickets.Add(new TaskTicket(name, Id, userId, projectId, text, created, projectName, expectedTime, usedTime));
            }
            return allTaskTickets;
        }
        public List<Project> CreateProjectFromXmlData(List<XDocument> projectData)
        {
            var name = string.Empty;
            Guid Id = Guid.Empty;
            Guid userId = Guid.Empty;
            Guid parentId = Guid.Empty;
            var text = string.Empty;
            DateTime created = DateTime.Now;
            float usedTime = 0;

            var allProjects = new List<Project>();

            //Foreach XDocument in projectData
            foreach (var doc in projectData)
            {
                var docAllData = doc.Root.Value;
                var docElement = doc.Root.Elements();

                //Foreach element in XML FIle -> Id/432985094589 etc.
                foreach (var element in docElement)
                {
                    var data = element.ToString();

                    if (data != null)
                    {
                        string[] splitedData = data.Split(new char[] { '/' });

                        switch (splitedData[1])
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
                allProjects.Add(new Project(name, Id, userId, parentId, text, created, usedTime));
            }
            return allProjects;
        }
    }
}
