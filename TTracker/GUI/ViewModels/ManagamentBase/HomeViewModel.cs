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
        public ObservableCollection<ChartHelperModel> RootProjectsChart { get; set; } = new ObservableCollection<ChartHelperModel>();
        public ObservableCollection<ChartHelperModel> SubProjectsChart { get; set; } = new ObservableCollection<ChartHelperModel>();

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
            Date = ($"Your To-Do´s for today, the {DateTime.Now.ToShortDateString()}");
            allTimeEntries.AddRange(DataAccess.Instance.GetAll<TimeEntry>());
            LoadTickets();
            CalculateRootProjectsChart();
            CalcluateSubProjects();
        }

        void LoadTickets()
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
                if ((ticket.Priority == PriorityLevel.High
                    || ticket.Priority == PriorityLevel.VeryHigh)
                    && ticket.Status != Status.Finished
                    && !(todoTickets.Contains(ticket)))
                {
                    highPrioTickets.Add(ticket);
                }
            }

            TaskTickets.AddRange(todoTickets.Select(t => new TaskTicketViewModel(t, this, false)));
            TaskTickets.AddRange(highPrioTickets.Select(t => new TaskTicketViewModel(t, this, false)));
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

        void CalcluateSubProjects()
        {
            Task.Run(() =>
            {
                DateTime dateOfLastMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                var desiredTimeEntriesVm = new List<TimeEntryViewModel>();

                foreach (var tE in allTimeEntries)
                {
                    if (tE.Created >= dateOfLastMonday)
                    {
                        desiredTimeEntriesVm.Add(new TimeEntryViewModel(tE, this));
                    }
                }
               
                Application.Current.Dispatcher.Invoke(() => SubProjectsChart.AddRange(StatisticsHelperClass.CreateChartModelsOfTimeEntriesSubProjects(desiredTimeEntriesVm)));

            });
        }
    }
}
