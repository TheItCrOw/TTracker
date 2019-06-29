using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.GUI.ViewModels.Entities;

namespace TTracker.GUI.ViewModels.ManagamentBase.CalendarSubVms
{
    public class WeeklyViewModel : ViewModelManagementBase
    {
        private CalendarViewModel _currentBase;
        private List<DateTicketViewModel> _allDateTickets = new List<DateTicketViewModel>();
        private string _selectedDate;

        public ObservableCollection<object> Days { get; set; } = new ObservableCollection<object>();


        public string SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                SetProperty(ref _selectedDate, value);
            }
        }

        public override void OnVmGotSelected()
        {
            _currentBase.UpdateCalendar((DateTime)SelectedVm, Utility.CustomCalendarMode.Day);
        }

        public WeeklyViewModel(CalendarViewModel currentBase, List<DateTime> dates, List<DateTicketViewModel> dateTickets)
        {
            _currentBase = currentBase;
            _allDateTickets.AddRange(dateTickets);
            SelectedDate = $"Week from: {dates[0].ToShortDateString()} to {dates[dates.Count - 1].ToShortDateString()}";
            LoadDays(dates);
        }

        void LoadDays(List<DateTime> dates)
        {
            dates = dates.OrderBy(d => d.Date).ToList();

            foreach (var date in dates)
            {
                var utilityVm = new UtilityViewModel<DateTime>(date, this);
                LoadChildrenOfDay(utilityVm, dates);
                Days.Add(utilityVm);
            }
        }

        void LoadChildrenOfDay(UtilityViewModel<DateTime> utilityVm, List<DateTime> dates)
        {
            DateTime currentDate = new DateTime();
            foreach (var date in dates)
            {
                if (date == utilityVm.CurrentViewModel.Date)
                {
                    currentDate = date;
                }
            }

            foreach (var ticket in _allDateTickets)
            {
                if (ticket.DateStart.Date == currentDate.Date
                    || ticket.DateEnd.Date == currentDate.Date)
                {
                    utilityVm.Children.Add(ticket);
                }
                if (ticket.DateStart < currentDate.Date && ticket.DateEnd.Date > currentDate.Date)
                {
                    if (!(utilityVm.Children.Contains(ticket)))
                        utilityVm.Children.Add(ticket);
                }
            }
        }

    }
}
