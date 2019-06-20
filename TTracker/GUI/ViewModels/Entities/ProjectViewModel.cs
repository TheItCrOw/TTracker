using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.GUI.ViewModels.TicketManagementSubVms;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        private DateTime _created;
        private float _usedTime;
        private string _name;
        private string _text;
        private Guid _parentId;
        private bool _isSelected;

        public ObservableCollection<ProjectViewModel> Children { get; set; } = new ObservableCollection<ProjectViewModel>();
        public DelegateCommand SelectedCommand => new DelegateCommand(Selected);
        public DelegateCommand DeleteCommand => new DelegateCommand(Delete);

        #region Properties

        public DateTime Created { get { return _created; } set { SetProperty(ref _created, value); } }
        public float UsedTime { get { return _usedTime; } set { SetProperty(ref _usedTime, value); } }
        public Guid ParentId { get { return _parentId; } set { SetProperty(ref _parentId, value); } }


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

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetProperty(ref _isSelected, value);
            }
        }

        public Project Model { get; set; }
        #endregion

        void Selected()
        {
            ((ProjectFrameViewModel)CurrentBase).HandleSelectedProjects(this);
        }

        public ProjectViewModel(Project project, ViewModelManagementBase currentBase, bool @new)
        {
            CurrentBase = currentBase;
            Model = project;
            IsNew = @new;

            Id = Guid.NewGuid();
            ParentId = project.ParentId;
            Name = project.Name;
            Text = project.Text;
            Created = project.Created;
            ModelId = project.Id;

            if (!IsNew)
                AfterSave();

        }

        public void CalculateUsedTime()
        {
            var allTaskTickets = DataAccess.Instance.GetAll<TaskTicket>();

            if (ParentId == Guid.Empty)
            {
                foreach (var child in Children)
                {
                    UsedTime += child.UsedTime;
                    UsedTime = (float)(Math.Truncate((double)UsedTime * 100.0) / 100.0);
                }
                return;
            }

            foreach (var ticket in allTaskTickets)
            {
                if (ticket.ProjectId == this.ModelId)
                {
                    UsedTime += ticket.UsedTime;
                    UsedTime = (float)(Math.Truncate((double)UsedTime * 100.0) / 100.0);
                }
            }
        }

        public void Save()
        {
            if (!IsDirty)
                return;

            if (IsNew)
            {
                DataAccess.Instance.RegisterAndSaveNewProject(this.Model);
                AfterSave();
                return;
            }

            //Contains the property name and the changed value like:
            // Name/TTracker
            var changedPropertiesFullData = new List<string>();

            foreach (var p in ChangedProperties)
            {
                switch (p)
                {
                    case "Name":
                        Model.Name = this.Name;
                        changedPropertiesFullData.Add(("Name/" + Model.Name));
                        break;
                    case "Text":
                        Model.Text = this.Text;
                        changedPropertiesFullData.Add(("Text/" + Model.Text));
                        break;
                    case "UsedTime":
                        Model.UsedTime = this.UsedTime;
                        changedPropertiesFullData.Add(("UsedTime/" + Model.UsedTime));
                        break;
                }
            }

            DataAccess.Instance.Save<Project>(this.Model, changedPropertiesFullData);
            AfterSave();
        }
        void Delete()
        {
            MarkAsDeletable();
            var projectFrameBase = (ProjectFrameViewModel)CurrentBase;

            //When its a root project, add it as deletable
            if(this.ParentId == Guid.Empty)
            {
                projectFrameBase.Projects.Remove(this);
            }
            //when its a children, delete it from the children lsit of the root project
            else
            {
                foreach(var project in projectFrameBase.Projects)
                {
                    if (project.ModelId == this.ParentId)
                    {
                        project.Children.Remove(this);
                    }
                }

            }
        }

    }
}
