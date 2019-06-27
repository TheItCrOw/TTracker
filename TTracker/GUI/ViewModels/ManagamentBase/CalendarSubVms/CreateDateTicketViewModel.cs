using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels.ManagamentBase.CalendarSubVms
{
    public class CreateDateTicketViewModel : ViewModelManagementBase
    {
        private CalendarViewModel _currentBase;
        private string _ticketName;
        private string _ticketText;
        private ProjectViewModel _selectedProjectComboBox;
        private DateTime _startDate;
        private DateTime _endDate;
        private DateTime _fromSelectedCalendarDate;
        private DateTime _toSelectedCalendarDate;
        private float _fromSelectedCalendarTime;
        private float _toSelectedCalendarTime;

        public DelegateCommand CreateNewTicketCommand => new DelegateCommand(CreateNewDateTicket);
        public ObservableCollection<ProjectViewModel> Projects { get; set; } = new ObservableCollection<ProjectViewModel>();

        #region Properties
        public string TicketName
        {
            get { return _ticketName; }
            set
            {
                SetProperty(ref _ticketName, value);
            }
        }
        public string TicketText
        {
            get { return _ticketText; }
            set
            {
                SetProperty(ref _ticketText, value);
            }
        }

        public ProjectViewModel SelectedProjectComboBox
        {
            get { return _selectedProjectComboBox; }
            set
            {
                SetProperty(ref _selectedProjectComboBox, value);
            }
        }
        public DateTime FromSelectedCalendarDate
        {
            get { return _fromSelectedCalendarDate; }
            set
            {
                SetProperty(ref _fromSelectedCalendarDate, value);
                CalculateStartAndEndTime();
            }
        }
        public DateTime ToSelectedCalendarDate
        {
            get { return _toSelectedCalendarDate; }
            set
            {
                SetProperty(ref _toSelectedCalendarDate, value);
                CalculateStartAndEndTime();
            }
        }
        public float FromSelectedCalendarTime
        {
            get { return _fromSelectedCalendarTime; }
            set
            {
                SetProperty(ref _fromSelectedCalendarTime, value);
                CalculateStartAndEndTime();
            }
        }
        public float ToSelectedCalendarTime
        {
            get { return _toSelectedCalendarTime; }
            set
            {
                SetProperty(ref _toSelectedCalendarTime, value);
                CalculateStartAndEndTime();
            }
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                SetProperty(ref _startDate, value);
            }
        }
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                SetProperty(ref _endDate, value);
            }
        }
        #endregion

        public CreateDateTicketViewModel(CalendarViewModel currentBase)
        {
            _currentBase = currentBase;
            Setup();
        }

        void Setup()
        {
            LoadProjects();
        }

        private void LoadProjects()
        {
            Projects.Clear();
            var allProjects = DataAccess.Instance.GetAll<Project>();
            var allProjectsVM = new List<ProjectViewModel>();

            if (allProjects == null)
                return;

            foreach (var project in allProjects)
            {
                if (DataAccess.CurrentLoggedUser != null
                    && project.UserId == DataAccess.CurrentLoggedUser.Id
                    && project.ParentId != Guid.Empty)
                {
                    allProjectsVM.Add(new ProjectViewModel(project, this, false));
                }
            }

            Projects.AddRange(allProjectsVM);
        }

        private void CreateNewDateTicket()
        {
            CalculateStartAndEndTime();

            if (!CheckForFormalError())
                return;

            var dateTicket = new DateTicket(
                Guid.NewGuid(),
                TicketName,                
                DataAccess.CurrentLoggedUser.Id,
                SelectedProjectComboBox.ModelId,
                TicketText,
                DateTime.Now,
                StartDate,
                EndDate
                );

            DataAccess.Instance.RegisterAndSaveNewDateTicket(dateTicket);
            MessageBox.Show("The Ticket has been succesfully created!");
            _currentBase.Reload();
        }

        void CalculateStartAndEndTime()
        {
            var startTime = DateTimeExtensions.ConvertFloatToTimespan(FromSelectedCalendarTime);
            var endTime = DateTimeExtensions.ConvertFloatToTimespan(ToSelectedCalendarTime);

            StartDate = FromSelectedCalendarDate + startTime;
            EndDate = ToSelectedCalendarDate + endTime;
        }

        bool CheckForFormalError()
        {
            if (DataAccess.CurrentLoggedUser == null)
            {
                MessageBox.Show("Please login as a User first.");
                return false;
            }
            else if (TicketName == string.Empty)
            {
                MessageBox.Show("Please name your ticket.");
                return false;
            }
            else if (SelectedProjectComboBox == null)
            {
                MessageBox.Show("Please choose a project.");
                return false;
            }
            else if(StartDate.ToShortDateString() == "01.01.0001" || EndDate.ToShortDateString() == "01.01.0001")
            {
                MessageBox.Show("Please enter a valid Start- and End-Date");
                return false;
            }
            else if(StartDate > EndDate)
            {
                MessageBox.Show("Your End-Date must be later than your Start-Date");
                return false;
            }

            return true;
        }
    }

}

