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
    class MonthlyViewModel : ViewModelManagementBase
    {
        private CalendarViewModel _currentBase;
        private string _selectedDate;
        private List<DateTicketViewModel> _allDateTickets = new List<DateTicketViewModel>();

        public ObservableCollection<object> FirstDays { get; set; } = new ObservableCollection<object>();
        public ObservableCollection<object> SecondDays { get; set; } = new ObservableCollection<object>();
        public ObservableCollection<object> ThirdDays { get; set; } = new ObservableCollection<object>();
        public ObservableCollection<object> ForthDays { get; set; } = new ObservableCollection<object>();

        public string SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                SetProperty(ref _selectedDate, value);
            }
        }

        public MonthlyViewModel(CalendarViewModel currentBase, List<DateTime> dates, List<DateTicketViewModel> dateTickets)
        {
            _currentBase = currentBase;
            SelectedDate = $"Month from: {dates[0].ToShortDateString()} to {dates[dates.Count - 1].ToShortDateString()}";
            _allDateTickets.AddRange(dateTickets);
            LoadDays(dates);
        }

        public override void OnVmGotSelected()
        {
            _currentBase.UpdateCalendar((DateTime)SelectedVm, Utility.CustomCalendarMode.Day);
        }

        void LoadDays(List<DateTime> dates)
        {
            dates = dates.OrderBy(d => d.Date).ToList();

            foreach (var date in dates)
            {
                var utilityVm = new UtilityViewModel<DateTime>(date, this);
                LoadChildrenOfDay(utilityVm, dates);
                if(date.Day < 8)
                {
                    FirstDays.Add(utilityVm);
                }
                else if (date.Day < 15)
                {
                    SecondDays.Add(utilityVm);
                }
                else if(date.Day < 22)
                {
                    ThirdDays.Add(utilityVm);
                }
                else if (date.Day < 32)
                {
                    ForthDays.Add(utilityVm);
                }
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
