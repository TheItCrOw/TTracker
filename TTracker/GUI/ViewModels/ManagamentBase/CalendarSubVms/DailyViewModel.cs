using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.GUI.ViewModels.Entities;
using TTracker.GUI.Views.CalendarSubViews;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels.ManagamentBase.CalendarSubVms
{
    public class DailyViewModel : ViewModelManagementBase
    {
        private List<DateTicketViewModel> _dateTicketsVm = new List<DateTicketViewModel>();
        private List<SolidColorBrush> _dateTicketsColors = new List<SolidColorBrush>();
        private CalendarViewModel _currentBase;
        private string _selectedDate;
        private double _heightOfCalendar;

        public ObservableCollection<DateTicketViewModel> DateTickets { get; set; } = new ObservableCollection<DateTicketViewModel>();

        public string SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                SetProperty(ref _selectedDate, value);
            }
        }

        public double HeightOfCalendar
        {
            get { return _heightOfCalendar; }
            set
            {
                SetProperty(ref _heightOfCalendar, value);
            }
        }

        public DailyViewModel(CalendarViewModel currentBase, DateTime selectedDate, List<DateTicketViewModel> currentDateTickets)
        {
            _currentBase = currentBase;
            SelectedDate = selectedDate.DayOfWeek + ", the " + selectedDate.ToShortDateString();
            _dateTicketsVm = currentDateTickets;
            FillColors();
            LoadDateTickets();
        }

        void FillColors()
        {
            _dateTicketsColors.Add(Brushes.LightBlue);
            _dateTicketsColors.Add(Brushes.LightGreen);
            _dateTicketsColors.Add(Brushes.LightYellow);
            _dateTicketsColors.Add(Brushes.LightGray);
            _dateTicketsColors.Add(Brushes.LightPink);
            _dateTicketsColors.Add(Brushes.LightGoldenrodYellow);
            _dateTicketsColors.Add(Brushes.LightSteelBlue);
        }

        public void LoadDateTickets()
        {
            var allDateTicketsVm = new List<DateTicketViewModel>();
            foreach (var ticket in _dateTicketsVm)
            {
                allDateTicketsVm.Add(ticket);
            }
            CalculateHeightOfTickets(allDateTicketsVm);
        }

        void CalculateHeightOfTickets(List<DateTicketViewModel> allDateTicketsVm)
        {
            //Get height of the itemscontrol with the currentContent property
            HeightOfCalendar = 1500;
            int i = 0;

            foreach (var ticket in allDateTicketsVm)
            {
                if (ticket.DateStart.ToShortDateString() != ticket.DateEnd.ToShortDateString())
                {
                    ticket.Height = (float)HeightOfCalendar;
                    return;
                }

                var start = ticket.DateStart.Hour + ticket.DateStart.Minute;
                var end = ticket.DateEnd.Hour + ticket.DateEnd.Minute;

                var height = (HeightOfCalendar / 24) * (end - start);
                ticket.Height = (float)height;
                ticket.BackgroundColor = _dateTicketsColors[i];
                i++;
            }

            allDateTicketsVm = allDateTicketsVm.OrderBy(t => t.DateStart.ToShortTimeString()).ToList();
            DateTickets.AddRange(allDateTicketsVm);
        }





        //                if (start != timeOfLastDate)
        //        {
        //            DateTime dateStart = new DateTime();
        //DateTime dateEnd = new DateTime();
        //dateStart += DateTimeExtensions.ConvertFloatToTimespan(timeOfLastDate);
        //            dateEnd += DateTimeExtensions.ConvertFloatToTimespan(start);
        //            var placeHolder = new DateTicket(Guid.Empty, "", Guid.NewGuid(), Guid.Empty, "", DateTime.Now, dateStart, dateEnd);
        //placeholderList.Add(new DateTicketViewModel(placeHolder, this, false));
        //            timeOfLastDate = dateEnd.Hour + dateEnd.Minute;
        //        }
    }
}
