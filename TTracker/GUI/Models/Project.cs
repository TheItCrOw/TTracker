using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;

namespace TTracker.GUI.Models
{
    public class Project : ModelBase
    {
        private Guid _userId;
        private Guid _parentId;
        private string _text;
        private DateTime _created;
        private string _name;
        private float _usedTime;
        private string _projectName;

        public float UsedTime { get { return _usedTime; } set => _usedTime = value; }
        public Guid UserId { get { return _userId; } set => _userId = value; }
        public Guid ParentId { get { return _parentId; } set => _parentId = value; }
        public string Text { get { return _text; } set => _text = value; }
        public DateTime Created { get { return _created; } set => _created = value; }
        public string Name { get { return _name; } set => _name = value; }
        public string ProjectName { get { return _projectName; } set => _projectName = value; }


        public Project(string name, Guid id, Guid userId, Guid parentId, string text, DateTime created, float usedTime)
        {
            Name = name;
            Id = id;
            UserId = userId;
            ParentId = parentId;
            Text = text;
            Created = created;
            UsedTime = usedTime;
        }
    }
}
