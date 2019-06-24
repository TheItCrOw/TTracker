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
using TTracker.GUI.ViewModels.ManagamentBase;
using TTracker.GUI.Views.TimeEngineSubViews;
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
        public DelegateCommand SaveTimeEntriesCommand => new DelegateCommand(SaveAllTimeEntries);
        public DelegateCommand CreateDayStatisticCommand => new DelegateCommand(CreateDayStatistic);

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
            if (!IsValidTimeEntry())
                return;

            var currentTicket = SelectedTaskTicketComboBoxItem;
            var currentUsedTime = currentTicket.UsedTime;
            var workedTime= (TimeTo - TimeFrom) / 100;

            var workDay = 8;
            var usedTime = currentUsedTime + (workedTime / workDay);

            currentTicket.UsedTime = usedTime;
            currentTicket.Save();
            CreateTimeEntry();

        }

        bool IsValidTimeEntry()
        {
            var timeFromDecimal = TimeFrom / 100;
            var timeToDecimal = TimeTo / 100;
            var x = timeFromDecimal - Math.Truncate(timeFromDecimal);
            var y = timeToDecimal - Math.Truncate(timeToDecimal);

            if (TimeFrom >= TimeTo)
            {
                MessageBox.Show("Your 'worked until time' must always be later than your 'started from time'");
                return false;
            }
            else if (TimeFrom < 100 || TimeTo < 100)
            {
                MessageBox.Show("That is not a legit timespan. FYI: 00:00 is 24:00");
                return false;
            }
            else if (TimeTo > 2400 || TimeFrom > 2400)
            {
                MessageBox.Show("After 24:00 o'clock, the day is over. Please enter anything after that on the next day.");
                return false;
            }
            else if (x > 0.59 || y > 0.59)
            {
                MessageBox.Show("An hour only has 60 minutes. Please check your timespan again");
                return false;
            }
            else if (SelectedProjectComboBoxItem == null || SelectedTaskTicketComboBoxItem == null)
            {
                return false;
            }

            return true;
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
                SelectedCalendarDate);

            DataAccess.Instance.RegisterAndSaveNewTimeEntry(timeEntry);
            var timeEntryVm = new TimeEntryViewModel(timeEntry, this);

            TimeEntries.Add(timeEntryVm);
        }

        void OnSelectedCalendarDateChanged()
        {
            //Load all Time Entries
            var allTimeEntries = DataAccess.Instance.GetAll<TimeEntry>();
            TimeEntries.Clear();

            //Show only those that were created on SelectedCalendarDate
            var timeEntriesForSelectedDateVm = allTimeEntries
                .Where(t => t.Created.ToShortDateString() == SelectedCalendarDate.ToShortDateString() && t.UserId == DataAccess.CurrentLoggedUser.Id)
                .Select(t => new TimeEntryViewModel(t, this))
                .OrderBy(t => t.StartTime)
                .ToList();

            TimeEntries.AddRange(timeEntriesForSelectedDateVm);

        }

        void SaveAllTimeEntries()
        {
            DeleteTimeEntries();

            foreach (var timeEntry in TimeEntries)
            {
                timeEntry.Save();
            }
            HasUnsavedChanges = false;
        }

        private void DeleteTimeEntries()
        {
            foreach (var timeEntry in DeletableList)
            {
                //Substract the amount of time put into the timeEntry from the ticket.
                var timeEntryVm = (TimeEntryViewModel)timeEntry;
                var currentTicket = DataAccess.Instance.GetTaskTicketById(timeEntryVm.Model.TicketId);
                var currentTicketVm = new TaskTicketViewModel(currentTicket, this, false);
                var currentUsedTime = currentTicketVm.UsedTime;
                var workedTime = (timeEntryVm.EndTime - timeEntryVm.StartTime) / 100;

                var workDay = 8;
                var usedTime = currentUsedTime - (workedTime / workDay);
                //Save the ticket
                currentTicketVm.UsedTime = usedTime;
                currentTicketVm.Save();
                //Save the TimeEntry
                DataAccess.Instance.DeleteEntity<TimeEntry>(timeEntryVm.Model);
            }
        }

        void CreateDayStatistic()
        {
            var createDayStatisticView = new CreateDayStatisticView();
            createDayStatisticView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            createDayStatisticView.DataContext = new CreateDayStatisticViewModel((TimeEngineViewModel)CurrentContent, TimeEntries.ToList(), SelectedCalendarDate);
            createDayStatisticView.Show();
            createDayStatisticView.Topmost = true;
        }

    }
}
