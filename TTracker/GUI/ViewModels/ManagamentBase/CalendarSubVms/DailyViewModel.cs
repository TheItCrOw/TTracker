using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTracker.GUI.ViewModels.ManagamentBase.CalendarSubVms
{
    public class DailyViewModel : BindableBase
    {

        private CalendarViewModel _currentBase;
        private string _selectedDate;

        public string SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                SetProperty(ref _selectedDate, value);
            }
        }

        public DailyViewModel(CalendarViewModel currentBase, DateTime selectedDate)
        {
            _currentBase = currentBase;
            SelectedDate = selectedDate.DayOfWeek + ", the " + selectedDate.ToShortDateString();
        }
    }
}
