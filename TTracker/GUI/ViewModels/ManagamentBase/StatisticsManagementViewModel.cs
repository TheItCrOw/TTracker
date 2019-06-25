using Prism.Commands;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.GUI.ViewModels.Entities;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels.ManagamentBase
{
    class StatisticsManagementViewModel : ViewModelManagementBase
    {
        private List<TimeEntry> _allTimeEntries = new List<TimeEntry>();
        private List<TimeEntryViewModel> _currentTimeEntriesVm = new List<TimeEntryViewModel>();
        private List<TimeEntry> _currentTimeEntries = new List<TimeEntry>();
        private string _currentTopic;
        private Task _mainTask;
        private bool _isLoading;

        public ObservableCollection<ChartHelperModel> AllTimesChart { get; set; } = new ObservableCollection<ChartHelperModel>();
        public DelegateCommand<string> ChangeRootSubProjectsCommand => new DelegateCommand<string>(ChangeRootSubProjects);
        public DelegateCommand<string> ChangeTimeSpanCommand => new DelegateCommand<string>(ChangeTimeSpan);
        public DelegateCommand<FrameworkElement> SaveAsPdfCommand => new DelegateCommand<FrameworkElement>(SaveAsPdf);


        public string CurrentTopic
        {
            get { return _currentTopic; }
            set
            {
                SetProperty(ref _currentTopic, value);
            }
        }
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                SetProperty(ref _isLoading, value);
            }
        }

        public StatisticsManagementViewModel()
        {
            CurrentContent = this;
            Setup();
        }

        void Setup()
        {
            IsLoading = true;
            _mainTask = Task.Run(() =>
            {
                _allTimeEntries.AddRange(DataAccess.Instance.GetAll<TimeEntry>());
                _currentTimeEntries = _allTimeEntries;
                _currentTimeEntriesVm = _allTimeEntries.Select(tE => new TimeEntryViewModel(tE, this)).ToList();
                ChangeTimeSpan("AllTimeButton");
            });
            while (!_mainTask.IsCompleted)
            {
                CurrentTopic = "Loading...";
                IsLoading = true;
                return;
            }
        }

        /// <summary>
        /// This loads in the currentTimeEntries and takes the rootRrojects 
        /// </summary>
        void LoadAllTimesChartRoot()
        {
            _mainTask = Task.Run(() =>
            {
                var helperModels = StatisticsHelperClass.CreateChartModelsOfTimeEntriesRootProjects(_currentTimeEntries);
                Application.Current.Dispatcher.Invoke(() => AllTimesChart.Clear());
                Application.Current.Dispatcher.Invoke(() => AllTimesChart.AddRange(helperModels));
                IsLoading = false;
            });
            while (!_mainTask.IsCompleted)
            {
                CurrentTopic = "Loading...";
                IsLoading = true;
                return;
            }

        }

        /// <summary>
        /// This loads in the currentTimeEntries and takes the subProjects
        /// </summary>
        void LoadAllTimesChartSub()
        {
            _mainTask = Task.Run(() =>
            {
                var helperModels = StatisticsHelperClass.CreateChartModelsOfTimeEntriesSubProjects(_currentTimeEntriesVm);
                Application.Current.Dispatcher.Invoke(() => AllTimesChart.Clear());
                Application.Current.Dispatcher.Invoke(() => AllTimesChart.AddRange(helperModels));
                IsLoading = false;
            });

            while (!_mainTask.IsCompleted)
            {
                CurrentTopic = "Loading...";
                IsLoading = true;
                return;
            }
        }

        void ChangeRootSubProjects(string senderName)
        {
            if (IsLoading)
                return;

            if (senderName == "RootProjectsButton")
            {
                LoadAllTimesChartRoot();
            }
            else if (senderName == "SubProjectsButton")
            {
                LoadAllTimesChartSub();
            }
        }
        void ChangeTimeSpan(string senderName)
        {
            _mainTask = Task.Run(() =>
            {
                switch (senderName)
                {
                    case "AllTimeButton":
                        _currentTimeEntries = _allTimeEntries;
                        _currentTimeEntriesVm = _currentTimeEntries.Select(tE => new TimeEntryViewModel(tE, this)).ToList();
                        LoadAllTimesChartRoot();
                        CurrentTopic = "All time statistic";
                        break;
                    case "CurrentYearButton":
                        _currentTimeEntries = _allTimeEntries.Select(tE => tE).Where(tE => tE.Created.Year == DateTime.Now.Year).ToList();
                        _currentTimeEntriesVm = _currentTimeEntries.Select(tE => new TimeEntryViewModel(tE, this)).ToList();
                        LoadAllTimesChartRoot();
                        CurrentTopic = "Current year statistic";
                        break;
                    case "CurrentMonthButton":
                        _currentTimeEntries = _allTimeEntries.Select(tE => tE).Where(tE => tE.Created.Month == DateTime.Now.Month).ToList();
                        _currentTimeEntriesVm = _currentTimeEntries.Select(tE => new TimeEntryViewModel(tE, this)).ToList();
                        LoadAllTimesChartRoot();
                        CurrentTopic = "Current month statistic";
                        break;
                    case "CurrentWeekButton":
                        DateTime dateOfLastMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                        _currentTimeEntries = _allTimeEntries.Select(tE => tE).Where(tE => tE.Created > dateOfLastMonday).ToList();
                        _currentTimeEntriesVm = _currentTimeEntries.Select(tE => new TimeEntryViewModel(tE, this)).ToList();
                        LoadAllTimesChartRoot();
                        CurrentTopic = "Current week statistic";
                        break;
                    default:
                        break;
                }
            });

            while (!_mainTask.IsCompleted)
            {
                CurrentTopic = "Loading...";
                IsLoading = true;
                return;
            }
        }

        void SaveAsPdf(FrameworkElement source)
        {
            var pdfWriter = new CustomPdfWriter();
            pdfWriter.ExportAsPdf(source, 0.42f, 0.42f, PdfPageOrientation.Landscape);
        }
    }
}
