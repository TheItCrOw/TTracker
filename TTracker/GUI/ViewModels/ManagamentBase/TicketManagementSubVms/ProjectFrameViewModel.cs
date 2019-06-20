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
        public DelegateCommand SaveAllProjectsCommand => new DelegateCommand(OnSaveButtonExecute);
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
                SelectedItem.IsSelected = false;

            SelectedItem = selectedProject;
            SelectedItem.IsSelected = true;
            ProjectName = selectedProject.Name;
            ProjectText = selectedProject.Text;
            UsedProjectTime = selectedProject.UsedTime;
        }

        private void HandlePropertyBase2Project(string property)
        {
            switch (property)
            {
                case "ProjectName":
                    if (SelectedItem.Name != ProjectName && SelectedItem != null)
                        SelectedItem.Name = ProjectName;
                    break;
                case "ProjectText":
                    if (SelectedItem.Text != ProjectText && SelectedItem != null)
                        SelectedItem.Text = ProjectText;
                    break;
            }
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HasUnsavedChanges = true;
            RaisePropertyChanged(nameof(Projects));
        }

        void OnSaveButtonExecute()
        {
            //The order is as follows:
            // - AskForProjectsDeletion opens new dialog. In there, the projects and tickets are loaded that are in the DeletableList
            // - When agreed to deletion, DeleteProjects is called from AskForDeletionViewModel, deleting every project and tickets
            // - DeleteProjects then calls SaveProjects and that saves all left projects.
            if(DeletableList.Count > 0)
            {
                AskForProjectsDeletion();
            }
            else
            {
                SaveProjects();
            }
        }

        void AskForProjectsDeletion()
        {
            var askForProjectDeletionView = new AskForProjectDeletionView();
            askForProjectDeletionView.DataContext = new AskForProjectDeletionViewModel((ProjectFrameViewModel)CurrentContent, DeletableList);
            askForProjectDeletionView.Show();
            askForProjectDeletionView.Topmost = true;
        }

        public void DeleteProjects(List<ProjectViewModel> deleteProjectsList, List<TaskTicketViewModel> deleteTaskTicketsList)
        {
            foreach (var project in deleteProjectsList)
            {
                DataAccess.Instance.DeleteEntity<Project>(project.Model);
            }

            foreach (var ticket in deleteTaskTicketsList)
            {
                DataAccess.Instance.DeleteEntity<TaskTicket>(ticket.Model);
            }

            DeletableList.Clear();

            SaveProjects();

            MessageBox.Show("Deletion And saving succesfull");
        }

        private void SaveProjects()
        {
            //Save the rest
            foreach (var project in Projects)
            {
                project.Save();

                foreach (var child in project.Children)
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
                if (project.ParentId == Guid.Empty)
                {
                    //When parentId matches ModelId, add as children!
                    var children = allProjectsDic[project.ModelId];

                    project.Children.AddRange(children);

                    Projects.Add(project);
                }
            }
            //Calculate the time of projects here, cause the Childrens list has to be filled first.
            foreach (var project in Projects)
            {
                foreach (var child in project.Children)
                {
                    child.CalculateUsedTime();
                }
                project.CalculateUsedTime();
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
