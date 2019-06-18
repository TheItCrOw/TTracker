using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.GUI.ViewModels.TicketManagementSubVms;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels
{
    public class TaskTicketViewModel : ViewModelBase
    {
        private string _name;
        private string _text;
        private DateTime _created;
        private string _projectName;
        private float _expectedTime;
        private float _usedTime;
        private string _progress;
        private Guid _modelId;
        private PriorityLevel _priority;
        private Status _status;
        private Visibility _visibility;

        public DelegateCommand DeleteCommand => new DelegateCommand(Delete);

        #region Properties

        public string ProjectName { get { return _projectName; } set { SetProperty(ref _projectName, value); } }
        public DateTime Created { get { return _created; } set { SetProperty(ref _created, value); } }
        public string Progress { get { return _progress; } set { SetProperty(ref _progress, value); }}
        public Guid ModelId { get { return _modelId; } set { SetProperty(ref _modelId, value); } }

        public TaskTicket Model { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
                SetIsDirty(nameof(Name));
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                SetProperty(ref _text, value);
                SetIsDirty(nameof(Text));
            }
        }
        public float ExpectedTime
        {
            get { return _expectedTime; }
            set
            {
                SetProperty(ref _expectedTime, value);
                SetIsDirty(nameof(ExpectedTime));
            }
        }
        public float UsedTime
        {
            get { return _usedTime; }
            set
            {
                SetProperty(ref _usedTime, value);
                SetIsDirty(nameof(UsedTime));
            }
        }

        public PriorityLevel Priority
        {
            get { return _priority; }
            set
            {
                SetProperty(ref _priority, value);
                SetIsDirty(nameof(Priority));
            }
        }

        public Status Status
        {
            get { return _status; }
            set
            {
                SetProperty(ref _status, value);
                SetIsDirty(nameof(Status));
            }
        }

        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                SetProperty(ref _visibility, value);
            }
        }
        #endregion

        public TaskTicketViewModel(TaskTicket taskTicket, ViewModelManagementBase currentBase, bool @new)
        {
            CurrentBase = currentBase;
            Model = taskTicket;
            IsNew = @new;

            Name = taskTicket.Name;
            Id = Guid.NewGuid();
            ModelId = taskTicket.Id;
            var project = DataAccess.Instance.GetProjectById(taskTicket.ProjectId);
            ProjectName = project.Name;
            Text = taskTicket.Text;
            Created = DateTime.Now;
            ExpectedTime = taskTicket.ExpectedTime;
            UsedTime = taskTicket.UsedTime;
            Priority = taskTicket.Priority;
            Status = taskTicket.Status;
            Progress = (float)(Math.Truncate((double)UsedTime * 100.0) / 100.0) + " / " + ExpectedTime + " Days";

            if(!IsNew)
            {
                AfterSave();
            }          
        }


        public void Save()
        {
                        //return
            if (!IsDirty)
            {
                return;
            }
            //Create first when new
            else if (IsNew)
            {
                DataAccess.Instance.RegisterAndSaveNewTaskTicket(this.Model);
                AfterSave();
                return;
            }
          
            //Contains the property name and the changed value like:
            // Name/Ttrackerr
            var changedPropertiesFullData = new List<string>();

            foreach (var p in ChangedProperties)
            {
                switch (p)
                {
                    case "Name":
                        Model.Name = this.Name;
                        changedPropertiesFullData.Add(("Name/" + Model.Name).ToString());
                        break;
                    case "Text":
                        Model.Text = this.Text;
                        changedPropertiesFullData.Add(("Text/" + Model.Text).ToString());
                        break;
                    case "ExpectedTime":
                        Model.ExpectedTime = this.ExpectedTime;
                        changedPropertiesFullData.Add(("ExpectedTime/" + Model.ExpectedTime).ToString());
                        break;
                    case "UsedTime":
                        Model.UsedTime = this.UsedTime;
                        changedPropertiesFullData.Add(("UsedTime/" + Model.UsedTime).ToString());
                        break;
                    case "Priority":
                        Model.Priority = this.Priority;
                        changedPropertiesFullData.Add(("Priority/" + Model.Priority).ToString());
                        break;
                    case "Status":
                        Model.Status = this.Status;
                        changedPropertiesFullData.Add(("Status/" + Model.Status).ToString());
                        break;
                }
            }
            DataAccess.Instance.Save<TaskTicket>(this.Model, changedPropertiesFullData);
            changedPropertiesFullData.Clear();
            AfterSave();
        }

        void Delete()
        {
            MarkAsDeletable();
            ((AllTicketsFrameViewModel)CurrentBase).TaskTickets.Remove(this);
        }

    }
}
