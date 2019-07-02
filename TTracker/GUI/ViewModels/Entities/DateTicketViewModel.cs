using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.GUI.ViewModels.ManagamentBase;
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
        private string _timeStart;
        private string _timeEnd;
        private float _height;
        private SolidColorBrush _backgroundColor;

        public DateTicket Model { get; set; }
        public DelegateCommand DeleteCommand => new DelegateCommand(Delete);


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
        public string TimeStart
        {
            get { return _timeStart; }
            set
            {
                SetProperty(ref _timeStart, value);
            }
        }
        public string TimeEnd
        {
            get { return _timeEnd; }
            set
            {
                SetProperty(ref _timeEnd, value);
            }
        }
        public float Height
        {
            get { return _height; }
            set
            {
                SetProperty(ref _height, value);
            }
        }
        public SolidColorBrush BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                SetProperty(ref _backgroundColor, value);
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
            TimeStart = DateStart.ToShortTimeString();
            TimeEnd = DateEnd.ToShortTimeString();
     
            if (!IsNew)
                AfterSave();
        }

        void Delete()
        {
            if (MessageBox.Show("Do you really want to delete this ticket for good?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                DataAccess.Instance.DeleteEntity<DateTicket>(this.Model);
                ((CalendarViewModel)CurrentBase).Reload();
            }

        }
    }
}
