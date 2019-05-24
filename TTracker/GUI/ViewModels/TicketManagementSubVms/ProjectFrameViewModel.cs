using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels.TicketManagementSubVms
{
    public class ProjectFrameViewModel : ViewModelManagementBase, INotifyPropertyChanged
    { 
        public ObservableCollection<ProjectViewModel> Projects { get; set; } = new ObservableCollection<ProjectViewModel>();
        public DelegateCommand<object> SelectedItemCommand => new DelegateCommand<object>(SelectedItem);

        public ProjectFrameViewModel()
        {
            CurrentContent = this;
            LoadProjects();
            HandleCollectionChanges();
            testing();
        }
        private void HandleCollectionChanges()
        {
            Projects.CollectionChanged += this.OnCollectionChanged;
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HasUnsavedChanges = true;
            RaisePropertyChanged(nameof(Projects));
        }
        
        void SelectedItem(object selectedObject)
        {
            MessageBox.Show("Test2");
           if(selectedObject is ProjectViewModel selectedProject)
            {

            }

        }

        private void LoadProjects()
        {
            Projects.Clear();
            var allProjects = DataAccess.Instance.GetAll<Project>();
            var allProjectsVM = new List<ProjectViewModel>();

            if (allProjects == null)
                return;

            foreach (var project in allProjects)
            {
                if (DataAccess.CurrentLoggedUser != null && project.UserId == DataAccess.CurrentLoggedUser.Id)
                    allProjectsVM.Add(new ProjectViewModel(project, (AllTicketsFrameViewModel)CurrentContent, false));
            }
        }



        void testing()
        {
            var projects = new List<Project>();


            projects.Add(new Project("Projects1", Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "Text", DateTime.Now, 2));
            projects.Add(new Project("Projects2", Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "Text", DateTime.Now, 2));
            projects.Add(new Project("Projects3", Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "Text", DateTime.Now, 2));
            projects.Add(new Project("Projects4", Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "Text", DateTime.Now, 2));
            projects.Add(new Project("Projects5", Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "Text", DateTime.Now, 2));
            projects.Add(new Project("Projects5", Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "Text", DateTime.Now, 2));
            projects.Add(new Project("Projects5", Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "Text", DateTime.Now, 2));
            projects.Add(new Project("Projects5", Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "Text", DateTime.Now, 2));
            projects.Add(new Project("Projects5", Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "Text", DateTime.Now, 2));
            projects.Add(new Project("Projects5", Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "Text", DateTime.Now, 2));
            projects.Add(new Project("Projects5", Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "Text", DateTime.Now, 2));
            projects.Add(new Project("Projects5", Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "Text", DateTime.Now, 2));
            projects.Add(new Project("Projects5", Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "Text", DateTime.Now, 2));

            Projects.AddRange(projects.Select(x => new ProjectViewModel(x, this, true)));

        }



    }
}
