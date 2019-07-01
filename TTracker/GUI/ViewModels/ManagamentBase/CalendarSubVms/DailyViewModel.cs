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
        private DateTime _currentDate;
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
            _currentDate = selectedDate;
            LoadDateTickets();
        }

        public void LoadDateTickets()
        {
            var allDateTicketsVm = new List<DateTicketViewModel>();
            foreach (var ticket in _dateTicketsVm)
            {
                allDateTicketsVm.Add(ticket);
            }
            CalculateHeightOfTickets(allDateTicketsVm);

            allDateTicketsVm = allDateTicketsVm.OrderBy(t => t.DateEnd.ToShortTimeString()).ToList();
            DateTickets.AddRange(allDateTicketsVm);
        }

        void CalculateHeightOfTickets(List<DateTicketViewModel> allDateTicketsVm)
        {
            //Get height of the itemscontrol with the currentContent property
            HeightOfCalendar = 1500;
            int i = 0;

            foreach (var ticket in allDateTicketsVm)
            {
                //Reset the ticket time, otherwise I corrupt the times in the ticket which I dont want.
                //I want them to be indiviudlally changed for the UI
                ticket.TimeStart = ticket.DateStart.ToShortTimeString();
                ticket.TimeEnd = ticket.DateEnd.ToShortTimeString();

                if (ticket.DateStart.ToShortDateString() != ticket.DateEnd.ToShortDateString())
                {
                    //When the Ticket starts Today:
                    if(ticket.DateStart.Date == _currentDate.Date)
                    {
                        ticket.TimeEnd = "24:00";
                        ticket.Height = (float)(HeightOfCalendar / 24) * (24 - (ticket.DateStart.Hour));
                    }
                    //When the ticket ends today
                    else if(ticket.DateEnd.Date == _currentDate.Date)
                    {
                        ticket.TimeStart = "00:00";
                        ticket.Height = (float)(HeightOfCalendar / 24) * ((ticket.DateEnd.Hour) - 0);
                    }
                    //When the ticket goes from 24-28.6 and curretnDate is the 26.06, then the ticket goes from 0-24
                    else if(ticket.DateStart.Date < _currentDate.Date && ticket.DateEnd.Date > _currentDate.Date)
                    {
                        ticket.TimeStart = "00:00";
                        ticket.TimeEnd= "24:00";
                        ticket.Height = 800;
                    }
                    ticket.BackgroundColor = CustomSolidColorBrushes.GetColorByIndex(i);
                    i++;
                    return;
                }
                
                var start = ticket.DateStart.Hour;
                var end = ticket.DateEnd.Hour;

                var height = (HeightOfCalendar / 24) * (end - start);
                ticket.Height = (float)height;
                ticket.BackgroundColor = CustomSolidColorBrushes.GetColorByIndex(i);
                i++;
            }
        }

    }
}
