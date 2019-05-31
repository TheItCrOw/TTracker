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
            if (SelectedItem != null)
                SelectedItem.isSelected = false;

            SelectedItem = selectedProject;
            SelectedItem.isSelected = true;
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
            foreach (var project in Projects)
            {
                project.Save();

                foreach(var child in project.Children)
                {
                    child.Save();
                }
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
            //Get all project Models
            var allProjects = DataAccess.Instance.GetAll<Project>();

            //Check for nulls or not logged in user
            if (allProjects == null || DataAccess.CurrentLoggedUser == null)
                return;

            //All projects that belong to the user, craete a VM out of it
            var allProjectsVM = allProjects
                .Where(p => p.UserId == DataAccess.CurrentLoggedUser.Id)
                .Select(p => (new ProjectViewModel(p, this, false)))
                .ToList();

            //Create a lookup for the children 
            var allProjectsDic = allProjectsVM.ToLookup(p => p.ParentId, p => p);

            foreach (var project in allProjectsVM)
            {
                //Get the root projects
                if(project.ParentId == Guid.Empty)
                {
                    //When parentId matches ModelId, add as children!
                    var children = allProjectsDic[project.ModelId];

                    project.Children.AddRange(children);

                    Projects.Add(project);
                }

            }
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
