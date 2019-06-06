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
    class TimeEntryViewModel : ViewModelBase
    {
        private string _text;
        private float _startTime;
        private float _endTime;
        private string _projectName;
        private TimeEntry _model;
        private string _ticketName;

        public TimeEntryViewModel(TimeEntry timeEntry, ViewModelManagementBase currentBase)
        {
            _model = timeEntry;
            CurrentBase = currentBase;

            CreateViewModel();
        }

        private void CreateViewModel()
        {
            Id = Guid.NewGuid();
            StartTime = _model.StartTime;
            EndTime = _model.EndTime;
            Text = _model.Text;

            ProjectName = DataAccess.Instance.GetProjectById(_model.ProjectId).Name;
            TicketName = DataAccess.Instance.GetTaskTicketById(_model.TicketId).Name;
        }


        #region Properties
        public float StartTime { get { return _startTime; } set { SetProperty(ref _startTime, value); } }
        public float EndTime { get { return _endTime; } set { SetProperty(ref _endTime, value); } }
        public string ProjectName {  get { return _projectName; } set { SetProperty(ref _projectName, value); } }
        public string TicketName {  get { return _ticketName; } set { SetProperty(ref _ticketName, value); } }

        public string Text
        {
            get { return _text; }
            set
            {
                SetProperty(ref _text, value);
                SetIsDirty(nameof(Text));
            }
        }


        #endregion

    }
}
