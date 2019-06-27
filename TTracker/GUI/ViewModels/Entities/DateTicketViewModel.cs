using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels.Entities
{
    public class DateTicketViewModel : ViewModelBase
    {
        private string _projectName;
        private DateTime _created;
        private string _name;
        private string _text;
        private DateTime _dateStart;
        private DateTime _dateEnd;

        public DateTicket Model { get; set; }

        #region Properties

        public string ProjectName { get { return _projectName; } set { SetProperty(ref _projectName, value); } }
        public DateTime Created { get { return _created; } set { SetProperty(ref _created, value); } }

        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                SetProperty(ref _text, value);
            }
        }

        public DateTime DateStart
        {
            get { return _dateStart; }
            set
            {
                SetProperty(ref _dateStart, value);
            }
        }
        public DateTime DateEnd
        {
            get { return _dateEnd; }
            set
            {
                SetProperty(ref _dateEnd, value);
            }
        }

        #endregion


        public DateTicketViewModel(DateTicket model, ViewModelManagementBase currentBase, bool @new)
        {
            CurrentBase = currentBase;
            Model = model;
            IsNew = @new;

            Id = Guid.NewGuid();
            Name = model.Name;
            Text = model.Text;
            ModelId = model.Id;
            ProjectName = DataAccess.Instance.GetProjectById(model.ProjectId).Name;
            Created = model.Created;
            DateStart = model.DateStart;
            DateEnd = model.DateEnd;

            if (!IsNew)
                AfterSave();
        }

    }
}
