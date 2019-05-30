using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class CreateProjectViewModel : ViewModelManagementBase
    {
        private string _projectName;
        private string _projectText;
        private ProjectViewModel _selectedComboBoxItem;

        private ProjectFrameViewModel _allProjectsVm;
        public DelegateCommand CreateNewProjectCommand => new DelegateCommand(CreateNewProject);
        public ObservableCollection<ProjectViewModel> Projects { get; set; } = new ObservableCollection<ProjectViewModel>();

        public CreateProjectViewModel(ProjectFrameViewModel allProjectsVm)
        {
            _allProjectsVm = allProjectsVm;
            LoadRootProjects();
        }

        public string ProjectName
        {
            get { return _projectName;  }
            set
            {
                SetProperty(ref _projectName, value);
            }
        }
        public string ProjectText
        {
            get { return _projectText; }
            set
            {
                SetProperty(ref _projectText, value);
            }
        }
        public ProjectViewModel SelectedComboBoxItem
        {
            get { return _selectedComboBoxItem; }
            set
            {
                SetProperty(ref _selectedComboBoxItem, value);
            }
        }

        /// <summary>
        /// Load only the root project, so the ones that have an empty ParentId
        /// </summary>
        void LoadRootProjects()
        {
            Projects.Clear();
            var allProjects = DataAccess.Instance.GetAll<Project>();
            var allProjectsVm = new List<ProjectViewModel>();

            if (allProjects == null)
                return;

            foreach(var project in allProjects)
            {
                if(DataAccess.CurrentLoggedUser != null && project.UserId == DataAccess.CurrentLoggedUser.Id &&
                    project.ParentId == Guid.Empty)
                {
                    allProjectsVm.Add(new ProjectViewModel(project, (ProjectFrameViewModel)CurrentContent, false));
                }
            }
        }

        private void CreateNewProject()
        {
            if (DataAccess.CurrentLoggedUser == null)
            {
                MessageBox.Show("Please login as a User first.");
                return;
            }
            else if (ProjectName == string.Empty)
            {
                MessageBox.Show("Please name your Project.");
                return;
            }

            var rootProjectId = new Guid();
            if(SelectedComboBoxItem == null)
            {
                rootProjectId = Guid.Empty;
            }
            else
            {
                rootProjectId = SelectedComboBoxItem.ModelId;
            }


            var project = new Project(
                ProjectName,
                Guid.NewGuid(),
                DataAccess.CurrentLoggedUser.Id,
                rootProjectId,
                ProjectText,
                DateTime.Now,
                0);

            _allProjectsVm.Projects.Add(new ProjectViewModel(project, _allProjectsVm, true));
            MessageBox.Show("The project has been succesfully created!");

        }

    }
}
