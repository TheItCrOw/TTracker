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
using TTracker.GUI.ViewModels.Entities;
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
        private DateTime _selectedCalendarDate;
        private User _currentUser;
        private string _currentDescriptionText;

        public ObservableCollection<ProjectViewModel> Projects { get; set; } = new ObservableCollection<ProjectViewModel>();
        public ObservableCollection<TaskTicketViewModel> TaskTickets { get; set; } = new ObservableCollection<TaskTicketViewModel>();        
        public ObservableCollection<TimeEntryViewModel> TimeEntries{ get; set; } = new ObservableCollection<TimeEntryViewModel>();        
        public DelegateCommand SaveTimeCommand => new DelegateCommand(SaveTime);

        #region Properties
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
        public string CurrentDescriptionText
        {
            get { return _currentDescriptionText; }
            set
            {
                SetProperty(ref _currentDescriptionText, value);
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
        public DateTime SelectedCalendarDate
        {
            get { return _selectedCalendarDate; }
            set
            {
                SetProperty(ref _selectedCalendarDate, value);
                OnSelectedCalendarDateChanged();
            }
        }
        #endregion

        public TimeEngineViewModel()
        {
            CurrentContent = this;
            Setup();
            SelectedCalendarDate = DateTime.Now;


            //testing

            //TimeEntries.Add(new TimeEntryViewModel(new TimeEntry(Guid.Empty, Guid.Empty, Guid.Empty, new Guid("4ea89286-f047-44fd-9ad4-c9693dcb5863"), "This is a test Text haha la lorum ipsum dormus idit adat", 1000, 1100, DateTime.Now),this));
            //TimeEntries.Add(new TimeEntryViewModel(new TimeEntry(Guid.Empty, Guid.Empty, Guid.Empty, new Guid("4ea89286-f047-44fd-9ad4-c9693dcb5863"), "This is a test Text haha la lorum ipsum dormus idit adat", 1100, 1230, DateTime.Now),this));
            //TimeEntries.Add(new TimeEntryViewModel(new TimeEntry(Guid.Empty, Guid.Empty, Guid.Empty, new Guid("4ea89286-f047-44fd-9ad4-c9693dcb5863"), "This is a test Text haha la lorum ipsum dormus idit adat his is a test Text haha la lorum ipsum dormus idit adat his is a test Text haha la lorum ipsum dormus idit adat his is a test Text haha la lorum ipsum dormus idit adat his is a test Text haha la lorum ipsum dormus idit adat", 1500, 1600, DateTime.Now),this));
            //TimeEntries.Add(new TimeEntryViewModel(new TimeEntry(Guid.Empty, Guid.Empty, Guid.Empty, new Guid("4ea89286-f047-44fd-9ad4-c9693dcb5863"), "This is a test Text haha la lorum ipsum dormus idit adat", 1600, 1715, DateTime.Now),this));
            //TimeEntries.Add(new TimeEntryViewModel(new TimeEntry(Guid.Empty, Guid.Empty, Guid.Empty, new Guid("4ea89286-f047-44fd-9ad4-c9693dcb5863"), "This is a test Text haha la lorum ipsum dormus idit adat", 1715, 1815, DateTime.Now),this));







        }

        void Setup()
        {
            if(DataAccess.CurrentLoggedUser == null)
            {
                MessageBox.Show("You are not logged in. Please do so via the account View");
                return;
            }

            _currentUser = DataAccess.CurrentLoggedUser;
            IntroText = "Hello " + _currentUser.Name + "! What are you gonna do today, on the " + DateTime.Now.ToShortDateString() + "?";

            LoadProjects();
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
            if (TimeFrom >= TimeTo )
            {
                MessageBox.Show("Your 'worked until time' must always be later than your 'started from time'");
                return;
            }
            else if (SelectedProjectComboBoxItem == null || SelectedTaskTicketComboBoxItem == null)
            {
                return;
            }

            var currentTicket = SelectedTaskTicketComboBoxItem;
            var currentUsedTime = currentTicket.UsedTime;
            var workedTime= (TimeTo - TimeFrom) / 100;

            var workDay = 8;
            var usedTime = currentUsedTime + (workedTime / workDay);

            currentTicket.UsedTime = usedTime;
            currentTicket.Save();
            CreateTimeEntry();

            MessageBox.Show("Time has been saved.");
        }

        void CreateTimeEntry()
        {
            var timeEntry = new TimeEntry(
                Guid.NewGuid(),
                _currentUser.Id,
                SelectedProjectComboBoxItem.ModelId,
                SelectedTaskTicketComboBoxItem.ModelId,
                CurrentDescriptionText,
                TimeFrom,
                TimeTo,
                DateTime.Now);

            DataAccess.Instance.RegisterAndSaveNewTimeEntry(timeEntry);
            var timeEntryVm = new TimeEntryViewModel(timeEntry, this);

            TimeEntries.Add(timeEntryVm);
        }

        void OnSelectedCalendarDateChanged()
        {
            //Load all Time Entries
            var allTimeEntries = DataAccess.Instance.GetAll<TimeEntry>();
        }
    }
}
