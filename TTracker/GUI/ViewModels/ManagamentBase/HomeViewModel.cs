using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.GUI.ViewModels.Entities;
using TTracker.GUI.Views;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels
{
    class HomeViewModel : ViewModelManagementBase
    {
        private string _date;
        private List<TimeEntry> allTimeEntries = new List<TimeEntry>();
        public ObservableCollection<TaskTicketViewModel> TaskTickets { get; set; } = new ObservableCollection<TaskTicketViewModel>();
        public ObservableCollection<UtilityViewModel<DateTicketViewModel>> DateTickets { get; set; } = new ObservableCollection<UtilityViewModel<DateTicketViewModel>>();
        public ObservableCollection<ChartHelperModel> RootProjectsChart { get; set; } = new ObservableCollection<ChartHelperModel>();

        //public ObservableCollection<ChartHelperModel> SubProjectsChart { get; set; } = new ObservableCollection<ChartHelperModel>();
        public ObservableCollection<MicroTaskViewModel> MicroTasks { get; set; } = new ObservableCollection<MicroTaskViewModel>();
        public DelegateCommand CreateMicroTaskCommand => new DelegateCommand(CreateMicroTask);

        public string Date
        {
            get { return _date; }
            set
            {
                SetProperty(ref _date, value);
            }
        }
        public HomeViewModel()
        {
            if (DataAccess.CurrentLoggedUser == null)
                return;

            Date = ($"Your To-Do´s for today, the {DateTime.Now.ToShortDateString()}");
            allTimeEntries.AddRange(DataAccess.Instance.GetAll<TimeEntry>());
            LoadTaskTickets();
            LoadDateTickets();
            LoadMicroTasks();
            CalculateRootProjectsChart();
            //CalcluateSubProjects();
        }

        void LoadTaskTickets()
        {
            var allTickets = DataAccess.Instance.GetAll<TaskTicket>();
            var todoTickets = new List<TaskTicket>();
            var highPrioTickets = new List<TaskTicket>(); 

            foreach (var ticket in allTickets)
            {
                if (DataAccess.CurrentLoggedUser == null || ticket.UserId != DataAccess.CurrentLoggedUser.Id)
                    return;

                if (ticket.Status == Status.Working)
                {
                    todoTickets.Add(ticket);
                }
                //if ((ticket.Priority == PriorityLevel.High
                //    || ticket.Priority == PriorityLevel.VeryHigh)
                //    && ticket.Status != Status.Finished
                //    && !(todoTickets.Contains(ticket)))
                //{
                //    highPrioTickets.Add(ticket);
                //}
            }

            TaskTickets.AddRange(todoTickets.Select(t => new TaskTicketViewModel(t, this, false)));
            //TaskTickets.AddRange(highPrioTickets.Select(t => new TaskTicketViewModel(t, this, false)));
        }

        void LoadDateTickets()
        {
            var allDateTickets = DataAccess.Instance.GetAll<DateTicket>();
            var neededDateTicektsVm = new List<DateTicketViewModel>();
            var allUtilityVms = new List<UtilityViewModel<DateTicketViewModel>>();

            foreach (var ticket in allDateTickets)
            {
                if(ticket.DateStart.Date == DateTime.Now.Date || ticket.DateEnd.Date == DateTime.Now.Date
                    || (ticket.DateStart.Date < DateTime.Now.Date && ticket.DateEnd.Date > DateTime.Now.Date))
                {
                    neededDateTicektsVm.Add(new DateTicketViewModel(ticket, this, false));
                }
            }

            int i = 0;
            foreach(var ticket in neededDateTicektsVm)
            {
                //Reset the ticket time, otherwise I corrupt the times in the ticket which I dont want.
                //I want them to be indiviudlally changed for the UI
                ticket.TimeStart = ticket.DateStart.ToShortTimeString();
                ticket.TimeEnd = ticket.DateEnd.ToShortTimeString();

                if (ticket.DateEnd.Date != DateTime.Now.Date)
                {
                    ticket.TimeEnd = "24:00";
                }
                if(ticket.DateStart.Date != DateTime.Now.Date)
                {
                    ticket.TimeStart = "00:00";
                }
                var background = CustomSolidColorBrushes.GetColorByIndex(i);
                ticket.BackgroundColor = background;
                i++;

                allUtilityVms.Add(new UtilityViewModel<DateTicketViewModel>(ticket, this));
            }

            DateTickets.Clear();
            DateTickets.AddRange(allUtilityVms);
        }

        void LoadMicroTasks()
        {
            var allMicroTasks = DataAccess.Instance.GetAll<MicroTask>();
            var allMicroTasksVm = allMicroTasks
                .Where(mT => mT.UserId == DataAccess.CurrentLoggedUser.Id)
                .Select(mT => new MicroTaskViewModel(mT, this));

            MicroTasks.AddRange(allMicroTasksVm);
        }
        void CalculateRootProjectsChart()
        {
            Task.Run(() =>
            {
                DateTime dateOfLastMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                var desiredTimeEntries = new List<TimeEntry>();

                foreach (var tE in allTimeEntries)
                {
                    if (tE.Created >= dateOfLastMonday)
                    {
                        desiredTimeEntries.Add(tE);
                    }
                }

                Application.Current.Dispatcher.Invoke(() => RootProjectsChart.AddRange(StatisticsHelperClass.CreateChartModelsOfTimeEntriesRootProjects(desiredTimeEntries)));
            });
        }

        //void CalcluateSubProjects()
        //{
        //    Task.Run(() =>
        //    {
        //        DateTime dateOfLastMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
        //        var desiredTimeEntriesVm = new List<TimeEntryViewModel>();

        //        foreach (var tE in allTimeEntries)
        //        {
        //            if (tE.Created >= dateOfLastMonday)
        //            {
        //                desiredTimeEntriesVm.Add(new TimeEntryViewModel(tE, this));
        //            }
        //        }
               
        //        Application.Current.Dispatcher.Invoke(() => SubProjectsChart.AddRange(StatisticsHelperClass.CreateChartModelsOfTimeEntriesSubProjects(desiredTimeEntriesVm)));

        //    });
        //}
        void CreateMicroTask()
        {
            var microTask = new MicroTask(Guid.NewGuid(), DataAccess.CurrentLoggedUser.Id, "", DateTime.Now, 0);
            var microTaskVm = new MicroTaskViewModel(microTask, this);

            DataAccess.Instance.RegisterAndSaveNewMicroTask(microTask);

            MicroTasks.Add(microTaskVm);
        }
        public override void OnVmGotSelected()
        {
            if (SelectedVm is DateTicketViewModel)
            {
                NavigateTo.Calendar();
            }
        }
    }
}
