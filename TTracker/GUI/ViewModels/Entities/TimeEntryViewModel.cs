﻿using System;
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
        private DateTime _startTime;
        private DateTime _endTime;
        private string _projectName;
        private TimeEntry _model;

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

        }


        #region Properties
        public DateTime StartTime { get { return _startTime; } set { SetProperty(ref _startTime, value); } }
        public DateTime EndTime { get { return _endTime; } set { SetProperty(ref _endTime, value); } }
        public string ProjectName {  get { return _projectName; } set { SetProperty(ref _projectName, value); } }

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