using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTracker.GUI.Models
{
    class TaskTicket : ITicket
    {
        private Guid _userId;
        private string _text;
        private DateTime _created;
        private Guid _id;
        private string _name;
        private float _expectedTime;
        private float _usedTime;
        //private float _progress;
        private Guid _projectId;
        private string _projectName;

        public float ExpectedTime { get { return _expectedTime; } set => _expectedTime = value; }
        public float UsedTime { get { return _usedTime; } set => _usedTime = value; }
       // public float Progress { get { return _progress; } set => _progress = value; }
        public Guid UserId { get { return _userId; } set => _userId = value; }
        public string Text { get { return _text; } set => _text = value; }
        public DateTime Created { get { return _created; } set => _created = value; }
        public Guid Id { get { return _id; } set => _id = value; }
        public Guid ProjectId { get { return _projectId; } set => _projectId = value; }
        public string Name { get { return _name; } set => _name = value; }
        public string ProjectName { get { return _projectName; } set => _projectName = value; }



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
