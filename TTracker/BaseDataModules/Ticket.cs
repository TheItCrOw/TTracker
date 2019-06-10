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
    public class Ticket : ModelBase, IPrioritable, IStatusable
    {

        private Guid _userId;
        private string _text;
        private DateTime _created;
        private string _name;
        private float _expectedTime;
        private float _usedTime;
        private Guid _projectId;
        private string _projectName;
        private PriorityLevel _priority;
        private Status _status;

        public float ExpectedTime { get { return _expectedTime; } set => _expectedTime = value; }
        public float UsedTime { get { return _usedTime; } set => _usedTime = value; }
        public Guid UserId { get { return _userId; } set => _userId = value; }
        public string Text { get { return _text; } set => _text = value; }
        public DateTime Created { get { return _created; } set => _created = value; }
        public Guid ProjectId { get { return _projectId; } set => _projectId = value; }
        public string Name { get { return _name; } set => _name = value; }
        public string ProjectName { get { return _projectName; } set => _projectName = value; }
        public PriorityLevel Priority { get { return _priority; } set => _priority = value; }
        public Status Status{ get { return _status; } set => _status = value; }


    }
}
