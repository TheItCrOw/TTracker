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
using TTracker.GUI.Views.TicketManagementSubViews;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels.TicketManagementSubVms
{
    public class ProjectFrameViewModel : ViewModelManagementBase, INotifyPropertyChanged
    {
        public ObservableCollection<ProjectViewModel> Projects { get; set; } = new ObservableCollection<ProjectViewModel>();
        public DelegateCommand SaveAllProjectsCommand => new DelegateCommand(SaveAllProjects);
        public DelegateCommand CreateNewProjectCommand => new DelegateCommand(CreateNewProject);
        public float UsedProjectTime { get { return _usedProjectTime; } set { SetProperty(ref _usedProjectTime, value); } }

        public ProjectViewModel SelectedItem;
        private string _projectName;
        private string _projectText;
        private float _usedProjectTime;

        public ProjectFrameViewModel()
        {
            CurrentContent = this;
            LoadProjects();
            HandleCollectionChanges();

        }
        private void HandleCollectionChanges()
        {
            Projects.CollectionChanged += this.OnCollectionChanged;
        }

        public void HandleSelectedProjects(ProjectViewModel selectedProject)
        {
            SelectedItem = selectedProject;
            ProjectName = selectedProject.Name;
            ProjectText = selectedProject.Text;
            UsedProjectTime = selectedProject.UsedTime;
        }

        private void HandlePropertyBase2Project(string property)
        {
            switch (property)
            {
                case "ProjectName":
                    if (SelectedItem.Name != ProjectName)
                        SelectedItem.Name = ProjectName;
                    break;
                case "ProjectText":
                    if (SelectedItem.Text != ProjectText)
                        SelectedItem.Text = ProjectText;
                    break;
            }
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HasUnsavedChanges = true;
            RaisePropertyChanged(nameof(Projects));
        }

        void SaveAllProjects()
        {
            foreach(var project in Projects)
            {
                project.Save();
            }
            HasUnsavedChanges = false;
        }

        void CreateNewProject()
        {
            var createNewProjectView = new CreateProjectView();
            createNewProjectView.DataContext = new CreateProjectViewModel((ProjectFrameViewModel)CurrentContent);
            createNewProjectView.Show();
            createNewProjectView.Topmost = true;
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
                    allProjectsVM.Add(new ProjectViewModel(project, (ProjectFrameViewModel)CurrentContent, false));
            }

            Projects.AddRange(allProjectsVM);
        }

        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                SetProperty(ref _projectName, value);
                HandlePropertyBase2Project(nameof(ProjectName));
            }
        }
        public string ProjectText
        {
            get { return _projectText; }
            set
            {
                SetProperty(ref _projectText, value);
                HandlePropertyBase2Project(nameof(ProjectText));
            }
        }
    }
}
