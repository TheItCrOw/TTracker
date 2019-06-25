using Microsoft.Win32;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TTracker.BaseDataModules;
using TTracker.GUI.ViewModels.Entities;
using TTracker.Utility;
using System.IO.Packaging;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Spire.Pdf.Graphics;
using Spire.Pdf;

namespace TTracker.GUI.ViewModels.ManagamentBase
{
    class CreateDayStatisticViewModel : ViewModelManagementBase
    {
        private string _selectedCalendarDate;
        private DateTime _currentDate;
        public ObservableCollection<TimeEntryViewModel> TimeEntries { get; set; } = new ObservableCollection<TimeEntryViewModel>();

        //This collection holds the data that is shown in the sub projects pie chart
        public ObservableCollection<ChartHelperModel> SubProjectsChart { get; set; } = new ObservableCollection<ChartHelperModel>();
        public DelegateCommand<FrameworkElement> SaveAsPdfCommand => new DelegateCommand<FrameworkElement>(SaveAsPdf);

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
            _currentDate = date;
            TimeEntries.Clear();
            var sortedTimeEntries = timeEntries.OrderBy(t => t.StartTime);
            TimeEntries.AddRange(sortedTimeEntries);

            SubProjectsChart.AddRange(StatisticsHelperClass.CreateChartModelsOfTimeEntriesSubProjects(timeEntries));
        }

        void SaveAsPdf(FrameworkElement source)
        {
            var pdfWriter = new CustomPdfWriter();
            pdfWriter.ExportAsPdf(source, 0.68f, 0.68f, PdfPageOrientation.Portrait);
        }
    }


}
