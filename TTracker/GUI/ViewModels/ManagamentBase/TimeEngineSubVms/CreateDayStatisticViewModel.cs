﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;
using TTracker.GUI.ViewModels.Entities;

namespace TTracker.GUI.ViewModels.ManagamentBase
{
    class CreateDayStatisticViewModel : ViewModelManagementBase
    {
        private string _selectedCalendarDate;
        public ObservableCollection<TimeEntryViewModel> TimeEntries { get; set; } = new ObservableCollection<TimeEntryViewModel>();


        public string SelectedCalendarDate
        {
            get { return _selectedCalendarDate; }
            set
            {
                SetProperty(ref _selectedCalendarDate, value);
            }
        }

        public CreateDayStatisticViewModel(TimeEngineViewModel currentBase, List<TimeEntryViewModel> timeEntries, DateTime date)
        {
            SelectedCalendarDate = "Day summary of the " + date.ToShortDateString();
            TimeEntries.Clear();
            var sortedTimeEntries = timeEntries.OrderBy(t => t.StartTime);
            TimeEntries.AddRange(sortedTimeEntries);
        }


    }
}
