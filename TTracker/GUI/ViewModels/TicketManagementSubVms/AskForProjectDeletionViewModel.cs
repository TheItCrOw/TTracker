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
using TTracker.GUI.Views.TicketManagementSubViews;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels.TicketManagementSubVms
{
    class AskForProjectDeletionViewModel : ViewModelManagementBase
    {

        private ProjectFrameViewModel _projectFrameViewModel;

        public ObservableCollection<ProjectViewModel> RootProjects { get; set; } = new ObservableCollection<ProjectViewModel>();
        public ObservableCollection<ProjectViewModel> SubProjects { get; set; } = new ObservableCollection<ProjectViewModel>();
        public ObservableCollection<TaskTicketViewModel> TaskTickets { get; set; } = new ObservableCollection<TaskTicketViewModel>();
        public DelegateCommand DeleteAllListedEntitiesCommand => new DelegateCommand(DeleteAllListedEntities);


        public AskForProjectDeletionViewModel(ProjectFrameViewModel projectFrameViewModel, List<ViewModelBase> deletableList)
        {
            CurrentContent = this;
            _projectFrameViewModel = projectFrameViewModel;
            DeletableList = deletableList;
            LoadProjectsAndTickets();
        }

        void LoadProjectsAndTickets()
        {
            var allTickets = DataAccess.Instance.GetAll<TaskTicket>();
            var allProjects = DataAccess.Instance.GetAll<Project>();

            foreach (var project in DeletableList)
            {
                var projectVm = (ProjectViewModel)project;
                var allRootProjects = new List<ProjectViewModel>();
                var allSubProjects = new List<ProjectViewModel>();
                var allTicketsOfProject = new List<TaskTicketViewModel>();

                //If the project has subProjects
                if (projectVm.ParentId == Guid.Empty)
                {
                    allSubProjects.AddRange(allProjects
                        .Where(p => p.ParentId == projectVm.ModelId)
                        .Select(p => new ProjectViewModel(p, this, false))
                        .ToList());

                    allRootProjects.Add(projectVm);
                }
                else
                {
                    allSubProjects.Add(projectVm);
                }

                foreach (var subProject in allSubProjects)
                {
                    //Add the tickets of project to List
                    allTicketsOfProject.AddRange(allTickets
                        .Where(t => t.ProjectId == subProject.ModelId)
                        .Select(t => new TaskTicketViewModel(t, this, false))
                        .ToList());
                }

                RootProjects.AddRange(allRootProjects);
                SubProjects.AddRange(allSubProjects);
                TaskTickets.AddRange(allTicketsOfProject);
            }
        }

        void DeleteAllListedEntities()
        {
            var allDeletableProjects = new List<ProjectViewModel>();
            var alldeleteableTaskTickets = new List<TaskTicketViewModel>();

            allDeletableProjects.AddRange(RootProjects);
            allDeletableProjects.AddRange(SubProjects);

            alldeleteableTaskTickets.AddRange(TaskTickets);

            _projectFrameViewModel.DeleteProjects(allDeletableProjects, alldeleteableTaskTickets);

        }
    }
}
