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
using TTracker.Utility;

namespace TTracker.GUI.ViewModels
{
    class TimeEngineViewModel : ViewModelManagementBase
    {
        private string _introText;
        private ProjectViewModel _selectedProjectComboBoxItem;
        private TaskTicketViewModel _selectedTaskTicketComboBoxItem;
        private float _timeFrom;
        private float _timeTo;

        public ObservableCollection<ProjectViewModel> Projects { get; set; } = new ObservableCollection<ProjectViewModel>();
        public ObservableCollection<TaskTicketViewModel> TaskTickets { get; set; } = new ObservableCollection<TaskTicketViewModel>();
        public DelegateCommand SaveTimeCommand => new DelegateCommand(SaveTime);

        public float TimeFrom
        {
            get { return _timeFrom; }
            set
            {
                SetProperty(ref _timeFrom, value);
            }
        }
        public float TimeTo
        {
            get { return _timeTo; }
            set
            {
                SetProperty(ref _timeTo, value);
            }
        }
        public string IntroText
        {
            get { return _introText; }
            set
            {
                SetProperty(ref _introText, value);
            }
        }

        public ProjectViewModel SelectedProjectComboBoxItem
        {
            get { return _selectedProjectComboBoxItem; }
            set
            {
                SetProperty(ref _selectedProjectComboBoxItem, value);
                LoadTicketsForProject();
            }
        }

        public TaskTicketViewModel SelectedTaskTicketComboBoxItem
        {
            get { return _selectedTaskTicketComboBoxItem; }
            set
            {
                SetProperty(ref _selectedTaskTicketComboBoxItem, value);
            }
        }

        public TimeEngineViewModel()
        {
            CurrentContent = this;
            GetCurrentUser();
            LoadProjects();
        }

        void GetCurrentUser()
        {
            if(DataAccess.CurrentLoggedUser == null)
            {
                MessageBox.Show("You are not logged in. Please do so via the account View");
                return;
            }

            IntroText = "Hello " + DataAccess.CurrentLoggedUser.Name + "! What are you gonna do today, on the " + DateTime.Now.ToShortDateString() + "?";
        }

        void LoadProjects()
        {
            //Get all projects that parentId isnt empty
            var allProjects = DataAccess.Instance.GetAll<Project>();

            foreach (var project in allProjects)
            {
                if(project != null &&
                    project.UserId == DataAccess.CurrentLoggedUser.Id &&
                    project.ParentId != Guid.Empty)
                {
                    var projectVm = new ProjectViewModel(project, this, false);
                    Projects.Add(projectVm);
                }
            }
        }

        void LoadTicketsForProject()
        {
            TaskTickets.Clear();

            var allTaskTickets = DataAccess.Instance.GetAll<TaskTicket>();

            foreach (var ticket in allTaskTickets)
            {
                if(ticket.ProjectId == SelectedProjectComboBoxItem.ModelId)
                {
                    var taskTicketVm = new TaskTicketViewModel(ticket, this, false);
                    TaskTickets.Add(taskTicketVm);
                }
            }
        }

        void SaveTime()
        {
            if(TimeFrom > TimeTo)
            {
                MessageBox.Show("Your 'worked until time' must always be later than your 'started from time'");
                return;
            }

            var currentTicket = SelectedTaskTicketComboBoxItem;
            var totalTime= (TimeTo - TimeFrom) / 100;

            var workDay = 8;
            var usedTime = totalTime / workDay;

            currentTicket.UsedTime = usedTime;
            currentTicket.Save();

            MessageBox.Show("Time has been saved.");
        }
    }
}
