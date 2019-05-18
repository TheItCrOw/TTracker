using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTracker.GUI.Models
{
    public class TaskTicket : Ticket
    {
        public TaskTicket(string name, Guid id, Guid userId, Guid projectId, string text, DateTime created,
                          string projectName, float expectedTime, float usedTime)
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
            //  Progress = progress;
        }
    }
}
