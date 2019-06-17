using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.GUI.Models;
using TTracker.GUI.ViewModels.Entities;

namespace TTracker.Utility
{
    public static class StatisticsHelperClass
    {
        /// <summary>
        /// Takes in a list of TimeEntries, calculates the time that have been spent on the containg SUB-project
        /// and returns a list of ChartHelperModels to visualize in a chart.
        /// </summary>
        /// <param name="timeEntries"></param>
        /// <returns></returns>
        public static List<ChartHelperModel> CreateChartModelsOfTimeEntriesSubProjects(List<TimeEntryViewModel> timeEntries)
        {
            var allSubProjectsName = new List<string>();
            var allSubProjectsTime = new List<float>();
            var allChartModels = new List<ChartHelperModel>();

            foreach (var tE in timeEntries)
            {
                //Calculate the usedTime of the ticket
                var timeOfThatTicket = CalculateTimespanOf2floats(tE.StartTime, tE.EndTime);

                //When its the first time the project came up, add to the allProjectsName List and also allProjectsTime,
                // but at the same index
                if (!(allSubProjectsName.Contains(tE.ProjectName)))
                {
                    allSubProjectsName.Add(tE.ProjectName);
                    var index = allSubProjectsName.IndexOf(tE.ProjectName);
                    allSubProjectsTime.Insert(index, timeOfThatTicket);
                }
                //If the project has already been registered, only add the time to the allProjectsTime list at the index
                // of the project
                else
                {
                    var index = allSubProjectsName.IndexOf(tE.ProjectName);
                    allSubProjectsTime[index] += timeOfThatTicket;
                }
            }

            //Sum everything up and create a PieChartHelperClass foreach project
            foreach (var project in allSubProjectsName)
            {
                var index = allSubProjectsName.IndexOf(project);
                var name = project;
                var share = allSubProjectsTime.ElementAt(index);

                allChartModels.Add(new ChartHelperModel(name, share));
            }
            return allChartModels;
        }

        /// <summary>
        /// Takes in a list of TimeEntries, calculates the time that have been spent on the containg ROOT-project
        /// and returns a list of ChartHelperModels to visualize in a chart.
        /// </summary>
        /// <param name="timeEntries"></param>
        /// <returns></returns>
        public static List<ChartHelperModel> CreateChartModelsOfTimeEntriesRootProjects(List<TimeEntry> timeEntries)
        {
            var allRootProjectsName = new List<string>();
            var allRootProjectsTime = new List<float>();
            var allChartModels = new List<ChartHelperModel>();

            foreach (var tE in timeEntries)
            {
                //Calculate the usedTime of the ticket
                var timeOfThatTicket = CalculateTimespanOf2floats(tE.StartTime, tE.EndTime);
                var subProject = DataAccess.Instance.GetProjectById(tE.ProjectId);
                var rootProject = DataAccess.Instance.GetProjectById(subProject.ParentId);

                //When its the first time the project came up, add to the allProjectsName List and also allProjectsTime,
                // but at the same index
                if (!(allRootProjectsName.Contains(rootProject.Name)))
                {
                    allRootProjectsName.Add(rootProject.Name);
                    var index = allRootProjectsName.IndexOf(rootProject.Name);
                    allRootProjectsTime.Insert(index, timeOfThatTicket);
                }
                //If the project has already been registered, only add the time to the allProjectsTime list at the index
                // of the project
                else
                {
                    var index = allRootProjectsName.IndexOf(rootProject.Name);
                    allRootProjectsTime[index] += timeOfThatTicket;
                }
            }

            //Sum everything up and create a PieChartHelperClass foreach project
            foreach (var project in allRootProjectsName)
            {
                var index = allRootProjectsName.IndexOf(project);
                var name = project;
                var share = allRootProjectsTime.ElementAt(index);

                allChartModels.Add(new ChartHelperModel(name, share));
            }
            return allChartModels;
        }

        /// <summary>
        /// Takes in 2 floats like: 900 for 9:00 o'clock and gives back a float, that has the Timespan in hours
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float CalculateTimespanOf2floats(float start, float end)
        {
            double result = 0;

            //Make a decimal out of the start and end time
            var startDecimal = start / 100;
            var endDecimal = end / 100;
            //Get the value right of the comma e.g. 9,45 => 45
            var decimalValue_Start = startDecimal - Math.Truncate(startDecimal);
            var decimalValue_End = endDecimal - Math.Truncate(endDecimal);
            //Get the value left of the comma e.g 9,45 => 9
            var numberValue_Start = Math.Truncate(startDecimal);
            var numberValue_End = Math.Truncate(endDecimal);

            //Make Timespans .FromMinutes and .FromHours of left and right value of comma
            TimeSpan startInMinutes = TimeSpan.FromMinutes(decimalValue_Start * 100);
            TimeSpan startInHours = TimeSpan.FromHours(numberValue_Start);
            TimeSpan endInMinutes = TimeSpan.FromMinutes(decimalValue_End * 100);
            TimeSpan endInHours = TimeSpan.FromHours(numberValue_End);

            //Calculate them together e.g. 9:45 now, but in Timespan
            TimeSpan startInTimespan = startInHours + startInMinutes;
            TimeSpan endInTimespan = endInHours + endInMinutes;

            //Calculate the Difference between the two Timespasn start and end
            TimeSpan calculatedDiff = endInTimespan - startInTimespan;
            result = calculatedDiff.TotalHours;

            //Roundn the result to hours -> 10:00-9:30 is 0,5 hours
            var roundedResult = (float)(Math.Truncate((double)result * 100) / 100);

            return roundedResult;
        }
    }
}
