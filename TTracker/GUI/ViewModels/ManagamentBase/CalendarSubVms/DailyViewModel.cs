using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.GUI.ViewModels.Entities;

namespace TTracker.GUI.ViewModels.ManagamentBase.CalendarSubVms
{
    public class DailyViewModel : BindableBase
    {
        private CalendarViewModel _currentBase;
        private string _selectedDate;
        public ObservableCollection<DateTicketViewModel> DateTickets { get; set; } = new ObservableCollection<DateTicketViewModel>();

        public string SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                SetProperty(ref _selectedDate, value);
            }
        }

        public DailyViewModel(CalendarViewModel currentBase, DateTime selectedDate, List<DateTicketViewModel> currentDateTickets)
        {
            _currentBase = currentBase;
            SelectedDate = selectedDate.DayOfWeek + ", the " + selectedDate.ToShortDateString();
            LoadDateTickets(currentDateTickets);
        }

        void LoadDateTickets(List<DateTicketViewModel> dateTickets)
        {
            foreach(var ticket in dateTickets)
            {
                DateTickets.Add(ticket);
            }
        }
    }
}
