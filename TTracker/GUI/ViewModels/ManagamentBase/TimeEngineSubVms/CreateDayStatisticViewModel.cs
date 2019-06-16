using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TTracker.BaseDataModules;
using TTracker.GUI.ViewModels.Entities;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels.ManagamentBase
{
    class CreateDayStatisticViewModel : ViewModelManagementBase
    {
        private string _selectedCalendarDate;
        public ObservableCollection<TimeEntryViewModel> TimeEntries { get; set; } = new ObservableCollection<TimeEntryViewModel>();

        //This collection holds the data that is shown in the sub projects pie chart
        public ObservableCollection<ChartHelperModel> SubProjectsChart { get; set; } = new ObservableCollection<ChartHelperModel>();


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

            SubProjectsChart.AddRange(StatisticsHelperClass.CreateChartModelsOfTimeEntriesSubProjects(timeEntries));
        }

    }
}
