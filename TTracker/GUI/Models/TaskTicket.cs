using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;

namespace TTracker.GUI.Models
{

    public class TaskTicket : Ticket, IPrioritable, IStatusable
    {

        private float _expectedTime;
        private float _usedTime;
        private PriorityLevel _priority;
        private Status _status;

        public float ExpectedTime { get { return _expectedTime; } set => _expectedTime = value; }
        public float UsedTime { get { return _usedTime; } set => _usedTime = value; }
        public PriorityLevel Priority { get { return _priority; } set => _priority = value; }
        public Status Status { get { return _status; } set => _status = value; }


        public TaskTicket(string name, Guid id, Guid userId, Guid projectId, string text, DateTime created,
                          float expectedTime, float usedTime, string priority, string status)
        {
            Name = name;
            Id = id;
            UserId = userId;
            ProjectId = projectId;
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

            switch (status)
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
