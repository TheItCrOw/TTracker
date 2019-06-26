using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTracker.GUI.Models
{
    public class DateTicket : Ticket
    {
        private DateTime _dateStart;
        private DateTime _dateEnd;

        public DateTime DateStart { get { return _dateStart; } set => _dateStart = value; }
        public DateTime DateEnd { get { return _dateEnd; } set => _dateEnd = value; }

        public DateTicket(Guid Id, string name, Guid userId, Guid projectId, string text, DateTime created,
                          DateTime dateStart, DateTime dateEnd)
        {
            this.Id = Id;
            Name = name;
            UserId = userId;
            ProjectId = projectId;
            Text = text;
            Created = created;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }
    }
}
