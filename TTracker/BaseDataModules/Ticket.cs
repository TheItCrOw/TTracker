using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;
using TTracker.Interfaces;

namespace TTracker.GUI.Models
{
    public class Ticket : ModelBase
    {
        private Guid _userId;
        private string _text;
        private DateTime _created;
        private Guid _projectId;
        private string _name;

        public Guid UserId { get { return _userId; } set => _userId = value; }
        public string Text { get { return _text; } set => _text = value; }
        public DateTime Created { get { return _created; } set => _created = value; }
        public Guid ProjectId { get { return _projectId; } set => _projectId = value; }
        public string Name { get { return _name; } set => _name = value; }

    }
}
