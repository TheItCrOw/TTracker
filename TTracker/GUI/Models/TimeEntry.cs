using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;

namespace TTracker.GUI.Models
{
    public class TimeEntry : ModelBase
    {
        private string _text;
        private Guid _userId;
        private Guid _projectId;
        private Guid _ticketId;
        private float _startTime;
        private float _endTime;
        private DateTime _created;

        public Guid UserId { get { return _userId; } set => _userId = value; }
        public Guid ProjectId { get { return _projectId; } set => _projectId = value; }
        public Guid TicketId { get { return _ticketId; } set => _ticketId = value; }
        public string Text { get { return _text; } set => _text = value; }
        public DateTime Created { get { return _created; } set => _created = value; }
        public float StartTime { get { return _startTime; } set => _startTime = value; }
        public float EndTime { get { return _endTime; } set => _endTime = value; }

        public TimeEntry(Guid Id, Guid userId, Guid projectId, Guid ticketId, string text, float startTime, float endTime, DateTime created)
        {
            this.Id = Id;
            UserId = userId;
            ProjectId = projectId;
            TicketId = ticketId;
            Text = text;
            StartTime = startTime;
            EndTime = endTime;
            Created = created;
        }

    }
}
