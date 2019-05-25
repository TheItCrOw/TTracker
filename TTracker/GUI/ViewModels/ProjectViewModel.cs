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

namespace TTracker.GUI.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        private DateTime _created;
        private float _usedTime;
        private string _name;
        private string _text;
        private Project _model;
        private Guid _modelId;
        private Guid _parentId;

        public DateTime Created { get { return _created; } set { SetProperty(ref _created, value); } }
        public float UsedTime { get { return _usedTime; } set { SetProperty(ref _usedTime, value); } }
        public Guid ModelId { get { return _modelId; } set { SetProperty(ref _modelId, value); } }
        public Guid ParentId { get { return _parentId; } set { SetProperty(ref _parentId, value); } }
        public ObservableCollection<ProjectViewModel> Children { get; set; } = new ObservableCollection<ProjectViewModel>();
        public DelegateCommand SelectedCommand => new DelegateCommand(Selected);

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

        void Selected()
        {
            ((ProjectFrameViewModel)CurrentBase).SelectedItem = this;
        }

        public ProjectViewModel(Project project, ViewModelManagementBase currentBase, bool @new)
        {
            CurrentBase = currentBase;
            _model = project;
            isNew = @new;

            Id = Guid.NewGuid();
            ParentId = project.ParentId;
            Name = project.Name;
            Text = project.Text;
            Created = project.Created;
            UsedTime = project.UsedTime;
            ModelId = project.Id;

            if (!isNew)
                AfterSave();

        }

        public void Save()
        {
            if (!isDirty)
                return;

            if (isNew)
            {

            }


        }

    }
}
