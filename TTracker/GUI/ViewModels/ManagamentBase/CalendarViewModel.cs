using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TTracker.BaseDataModules;
using TTracker.GUI.ViewModels.ManagamentBase.CalendarSubVms;
using TTracker.GUI.Views.CalendarSubViews;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels.ManagamentBase
{
    public class CalendarViewModel : ViewModelManagementBase
    {
        private DateTime _selectedCalendarDate;
        private Frame _mainContentFrame;
        private List<DateTime> _currentlyShownDates = new List<DateTime>();
        private int _currentlyAddedAmountOfDays;
        private CustomCalendarMode _currentCalendarMode;

        public DelegateCommand<string> GoNextOrPreviousCommand => new DelegateCommand<string>(GoNextOrPrevious);
        public DelegateCommand<string> ChangeCustomCalendarModeCommand => new DelegateCommand<string>(ChangeCustomCalendarMode);
        public DelegateCommand CreateNewDateTicketCommand => new DelegateCommand(CreateNewDateTicket);

        public Frame MainContentFrame
        {
            get { return _mainContentFrame; }
            set
            {
                SetProperty(ref _mainContentFrame, value);
            }
        }
        public DateTime SelectedCalendarDate
        {
            get { return _selectedCalendarDate; }
            set
            {
                SetProperty(ref _selectedCalendarDate, value);
                ChangeMainContentFrame(CustomCalendarMode.Day);
            }
        }

        public CalendarViewModel(Frame mainContentFrame)
        {
            CurrentContent = this;
            MainContentFrame = mainContentFrame;
            SelectedCalendarDate = DateTime.Now;
            _currentCalendarMode = CustomCalendarMode.Day;
            Setup();
        }

        void Setup()
        {
            UpdateCalendar(SelectedCalendarDate, _currentCalendarMode);
        }
        void CreateNewDateTicket()
        {
            var createDateTicketView = new CreateDateTicketView();
            createDateTicketView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            createDateTicketView.DataContext = new CreateDateTicketViewModel((CalendarViewModel)CurrentContent);
            createDateTicketView.Show();
            createDateTicketView.Topmost = true;
        }
        void GoNextOrPrevious(string senderName)
        {
            if (senderName == "GoPrevious")
            {
                var newDate = SelectedCalendarDate.AddDays(-_currentlyAddedAmountOfDays);
                UpdateCalendar(newDate, _currentCalendarMode);
            }
            if (senderName == "GoNext")
            {
                var newDate = SelectedCalendarDate.AddDays(_currentlyAddedAmountOfDays);
                UpdateCalendar(newDate, _currentCalendarMode);
            }
        }
        void ChangeCustomCalendarMode(string senderName)
        {
            switch (senderName)
            {
                case "DayButton":
                    _currentCalendarMode = CustomCalendarMode.Day;
                    UpdateCalendar(SelectedCalendarDate, _currentCalendarMode);
                    break;
                case "WeekButton":
                    _currentCalendarMode = CustomCalendarMode.Week;
                    UpdateCalendar(SelectedCalendarDate, _currentCalendarMode);
                    break;
                case "MonthButton":
                    _currentCalendarMode = CustomCalendarMode.Month;
                    UpdateCalendar(SelectedCalendarDate, _currentCalendarMode);
                    break;
                case "YearButton":
                    _currentCalendarMode = CustomCalendarMode.Year;
                    UpdateCalendar(SelectedCalendarDate, _currentCalendarMode);
                    break;
                default:
                    break;
            }
        }
        public void UpdateCalendar(DateTime currentDay, CustomCalendarMode mode)
        {
            CalculateCurrentlyNeededDates(currentDay);
            ChangeMainContentFrame(mode);
        }
        protected void CalculateCurrentlyNeededDates(DateTime currentDay)
        {
            switch (_currentCalendarMode)
            {
                case CustomCalendarMode.Day:
                    SelectedCalendarDate = currentDay;
                    _currentlyAddedAmountOfDays = 1;
                    break;

                case CustomCalendarMode.Week:
                    _currentlyShownDates.Clear();
                    SelectedCalendarDate = currentDay;
                    var startOfWeek = DateTimeExtensions.StartOfWeek(currentDay, DayOfWeek.Monday);
                    _currentlyShownDates.Add(startOfWeek);
                    for (int i = 1; i < 7; i++)
                    {
                        var date = startOfWeek.AddDays(i);
                        _currentlyShownDates.Add(date);
                    }
                    _currentlyAddedAmountOfDays = 7;
                    break;

                case CustomCalendarMode.Month:
                    break;
                case CustomCalendarMode.Year:
                    break;
                default:
                    break;
            }
        }
        protected void ChangeMainContentFrame(CustomCalendarMode mode)
        {
            switch (mode)
            {
                case CustomCalendarMode.Day:
                    var newDailyView = new DailyView();
                    newDailyView.DataContext = new DailyViewModel(this, SelectedCalendarDate);
                    MainContentFrame.Content = null;
                    MainContentFrame.Content = newDailyView;
                    _currentCalendarMode = CustomCalendarMode.Day;
                    break;
                case CustomCalendarMode.Week:
                    var newWeeklyView = new WeeklyView();
                    newWeeklyView.DataContext = new WeeklyViewModel(this, _currentlyShownDates);
                    MainContentFrame.Content = null;
                    MainContentFrame.Content = newWeeklyView;
                    _currentCalendarMode = CustomCalendarMode.Week;
                    break;
                case CustomCalendarMode.Month:
                    break;
                case CustomCalendarMode.Year:
                    break;
                default:
                    break;
            }
        }
    }
}
