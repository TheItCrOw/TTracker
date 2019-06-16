using Prism.Commands;
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
    public class TimeEntryViewModel : ViewModelBase
    {
        private string _text;
        private float _startTime;
        private float _endTime;
        private string _projectName;
        private string _ticketName;
        public DelegateCommand DeleteCommand => new DelegateCommand(Delete);
        public TimeEntry Model { get; set; }

        public TimeEntryViewModel(TimeEntry timeEntry, ViewModelManagementBase currentBase)
        {
            Model = timeEntry;
            CurrentBase = currentBase;

            CreateViewModel();
            AfterSave();
        }

        private void CreateViewModel()
        {
            Id = Guid.NewGuid();
            StartTime = Model.StartTime;
            EndTime = Model.EndTime;
            Text = Model.Text;

            ProjectName = DataAccess.Instance.GetProjectById(Model.ProjectId).Name;
            TicketName = DataAccess.Instance.GetTaskTicketById(Model.TicketId).Name;
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

        public void Save()
        {
            if (!IsDirty)
                return;

            //Contains the property name and the changed value like:
            // Name/TTracker
            var changedPropertiesFullData = new List<string>();

            foreach (var p in ChangedProperties)
            {
                switch (p)
                {
                    case "Text":
                        Model.Text = this.Text;
                        changedPropertiesFullData.Add(("Text/" + Model.Text));
                        break;
                }
            }

            DataAccess.Instance.Save<TimeEntry>(this.Model, changedPropertiesFullData);
            AfterSave();
        }

        void Delete()
        {
            MarkAsDeletable();
            ((TimeEngineViewModel)CurrentBase).TimeEntries.Remove(this);
        }
    }
}
