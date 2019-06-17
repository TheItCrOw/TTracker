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
                var rawTime = (tE.EndTime - tE.StartTime) / 100;
                var timeOfThatTicket = rawTime;

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
                var timeOfThatTicket = (tE.EndTime - tE.StartTime) / 366;
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

        public static float Convert2FloatsToTimeSpan(float start, float end)
        {
            float result = 0;

             

            return result;
        }
    }
}
