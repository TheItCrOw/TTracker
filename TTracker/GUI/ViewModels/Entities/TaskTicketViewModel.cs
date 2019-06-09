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
        private TaskTicket _model;
        private PriorityLevel _priority;

        #region Properties

        public string ProjectName { get { return _projectName; } set { SetProperty(ref _projectName, value); } }
        public DateTime Created { get { return _created; } set { SetProperty(ref _created, value); } }
        public string Progress { get { return _progress; } set { SetProperty(ref _progress, value); }}
        public Guid ModelId { get { return _modelId; } set { SetProperty(ref _modelId, value); } }

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

        #endregion

        public TaskTicketViewModel(TaskTicket taskTicket, ViewModelManagementBase currentBase, bool @new)
        {
            CurrentBase = currentBase;
            _model = taskTicket;
            IsNew = @new;

            Name = taskTicket.Name;
            Id = Guid.NewGuid();
            ModelId = taskTicket.Id;
            ProjectName = taskTicket.ProjectName;
            Text = taskTicket.Text;
            Created = DateTime.Now;
            ExpectedTime = taskTicket.ExpectedTime;
            UsedTime = taskTicket.UsedTime;
            Priority = taskTicket.Priority;
            Progress = (float)(Math.Truncate((double)UsedTime * 100.0) / 100.0) + " / " + ExpectedTime + " Days";

            if(!IsNew)
            {
                AfterSave();
            }          
        }

        public void Save()
        {
            if (!IsDirty)
                return;

            if(IsNew)
            {
                DataAccess.Instance.RegisterAndSaveNewTaskTicket(this._model);
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
                        _model.Name = this.Name;
                        changedPropertiesFullData.Add(("Name/" + _model.Name).ToString());
                        break;
                    case "Text":
                        _model.Text = this.Text;
                        changedPropertiesFullData.Add(("Text/" + _model.Text).ToString());
                        break;
                    case "ExpectedTime":
                        _model.ExpectedTime = this.ExpectedTime;
                        changedPropertiesFullData.Add(("ExpectedTime/" + _model.ExpectedTime).ToString());
                        break;
                    case "UsedTime":
                        _model.UsedTime = this.UsedTime;
                        changedPropertiesFullData.Add(("UsedTime/" + _model.UsedTime).ToString());
                        break;
                    case "Priority":
                        _model.Priority = this.Priority;
                        changedPropertiesFullData.Add(("Priority/" + _model.Priority).ToString());
                        break;

                }
            }
            DataAccess.Instance.Save<TaskTicket>(this._model, changedPropertiesFullData);
            changedPropertiesFullData.Clear();
            AfterSave();
        }


    }
}
