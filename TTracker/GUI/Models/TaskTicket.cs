using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;

namespace TTracker.GUI.Models
{
    public class TaskTicket : Ticket
    {
        public TaskTicket(string name, Guid id, Guid userId, Guid projectId, string text, DateTime created,
                          string projectName, float expectedTime, float usedTime, string priority, string status)
        {
            Name = name;
            Id = id;
            UserId = userId;
            ProjectId = projectId;
            ProjectName = projectName;
            Text = text;
            Created = created;
            ExpectedTime = expectedTime;
            UsedTime = usedTime;

            switch (priority)
            {
                case "VeryLow":
                    Priority = PriorityLevel.VeryLow;
                    break;
                case "Low":
                    Priority = PriorityLevel.Low;
                    break;
                case "Normal":
                    Priority = PriorityLevel.Normal;
                    break;
                case "High":
                    Priority = PriorityLevel.High;
                    break;
                case "VeryHigh":
                    Priority = PriorityLevel.VeryHigh;
                    break;
                default:
                    Priority = PriorityLevel.Normal;
                    break;
            }

            switch (priority)
            {
                case "Static":
                    Status = Status.Static;
                    break;
                case "Todo":
                    Status = Status.Todo;
                    break;
                case "Working":
                    Status = Status.Working;
                    break;
                case "Finished":
                    Status = Status.Finished;
                    break;
                case "Blocked":
                    Status = Status.Blocked;
                    break;
                default:
                    Status = Status.Todo;
                    break;
            }

        }
    }
}
