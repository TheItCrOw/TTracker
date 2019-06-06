using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;

namespace TTracker.GUI.Models
{
    class TimeEntry : ModelBase
    {
        private string _text;
        private Guid _userId;
        private Guid _projectId;
        private Guid _ticketId;
        private DateTime _startTime;
        private DateTime _endTime;

        public Guid UserId { get { return _userId; } set => _userId = value; }
        public Guid ProjectId { get { return _projectId; } set => _projectId = value; }
        public Guid TicketId { get { return _ticketId; } set => _ticketId = value; }
        public string Text { get { return _text; } set => _text = value; }
        public DateTime StartTime { get { return _startTime; } set => _startTime = value; }
        public DateTime EndTime { get { return _endTime; } set => _endTime = value; }

        public TimeEntry(Guid userId, Guid projectId, Guid ticketId, string text, DateTime startTime, DateTime endTime)
        {
            UserId = userId;
            ProjectId = projectId;
            TicketId = ticketId;
            Text = text;
            StartTime = startTime;
            EndTime = endTime;
        }

    }
}
