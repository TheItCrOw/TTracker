using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.GUI.ViewModels.Entities;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels.ManagamentBase.CalendarSubVms
{
    class YearlyViewModel : ViewModelManagementBase
    {
        private CalendarViewModel _currentBase;
        private string _selectedDate;
        private List<DateTicketViewModel> _allDateTickets = new List<DateTicketViewModel>();

        public ObservableCollection<object> Months { get; set; } = new ObservableCollection<object>();

        public string SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                SetProperty(ref _selectedDate, value);
            }
        }

        public YearlyViewModel(CalendarViewModel currentBase, List<DateTime> dates, List<DateTicketViewModel> dateTickets)
        {
            _currentBase = currentBase;
            SelectedDate = $"Year from: {dates[0].ToShortDateString()} to {dates[dates.Count - 1].ToShortDateString()}";
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
            var addedMonths = new List<string>();

            foreach (var date in dates)
            {
                if (!(addedMonths.Contains(DateTimeExtensions.ConvertMonthToString(date.Month))))
                {
                    var month = DateTimeExtensions.ConvertMonthToString(date.Month);
                    var utilityVm = new UtilityViewModel<string>(month, this);
                    addedMonths.Add(month);
                    Months.Add(utilityVm);
                }
            }
        }

    }
}
