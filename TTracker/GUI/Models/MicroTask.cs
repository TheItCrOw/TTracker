using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;

namespace TTracker.GUI.Models
{
    public class MicroTask : ModelBase
    {
        private string _text;
        private Guid _userId;
        private DateTime _created;
        private float _isChecked;

        public Guid UserId { get { return _userId; } set => _userId = value; }
        public string Text { get { return _text; } set => _text = value; }
        public DateTime Created { get { return _created; } set => _created = value; }
        public float IsChecked{ get { return _isChecked; } set => _isChecked = value; }

        public MicroTask(Guid Id, Guid userId, string text, DateTime created, float isChecked)
        {
            this.Id = Id;
            UserId = userId;
            Text = text;
            Created = created;
            IsChecked = isChecked;
        }

    }
}
