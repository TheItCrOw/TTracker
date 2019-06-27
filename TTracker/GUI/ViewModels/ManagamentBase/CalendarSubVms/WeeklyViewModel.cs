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

namespace TTracker.GUI.ViewModels.ManagamentBase.CalendarSubVms
{
    public class WeeklyViewModel : ViewModelManagementBase
    {
        private CalendarViewModel _currentBase;
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

        public WeeklyViewModel(CalendarViewModel currentBase, List<DateTime> dates)
        {
            _currentBase = currentBase;
            SelectedDate = $"Week from: {dates[0].ToShortDateString()} to {dates[dates.Count-1].ToShortDateString()}";
            LoadDays(dates);
        }

        void LoadDays(List<DateTime> dates)
        {         
            foreach(var date in dates)
            {
                var utilityVm = new UtilityViewModel<DateTime>(date, this);
                Days.Add(utilityVm);
            }
        }

    }
}
