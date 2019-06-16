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
        public ObservableCollection<ChartHelperClass> SubProjectsChart { get; set; } = new ObservableCollection<ChartHelperClass>();


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

            FillSubProjectPieChart(timeEntries);
        }

        private void FillSubProjectPieChart(List<TimeEntryViewModel> timeEntries)
        {
            var allProjectsName = new List<string>();
            var allProjectsTime = new List<float>();

            foreach(var tE in timeEntries)
            {
                //Calculate the usedTime of the ticket
                var timeOfThatTicket = (tE.EndTime - tE.StartTime) / 100;

                //When its the first time the project came up, add to the allProjectsName List and also allProjectsTime,
                // but at the same index
                if (!(allProjectsName.Contains(tE.ProjectName)))
                {
                    allProjectsName.Add(tE.ProjectName);
                    var index = allProjectsName.IndexOf(tE.ProjectName);
                    allProjectsTime.Insert(index, timeOfThatTicket);
                }
                //If the project has already been registered, only add the time to the allProjectsTime list at the index
                // of the project
                else
                {
                    var index = allProjectsName.IndexOf(tE.ProjectName);
                    allProjectsTime[index] += timeOfThatTicket;
                }
            }

            //Sum everything up and create a PieChartHelperClass foreach project
            foreach(var project in allProjectsName)
            {
                var index = allProjectsName.IndexOf(project);
                var name = project;
                var share = allProjectsTime.ElementAt(index);

                SubProjectsChart.Add(new ChartHelperClass(name, share));
            }

        }
    }
}
