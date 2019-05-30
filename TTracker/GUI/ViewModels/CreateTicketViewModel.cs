using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.GUI.ViewModels.TicketManagementSubVms;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels
{
    class CreateTicketViewModel : ViewModelManagementBase
    {
        private string _ticketName;
        private bool _isTaskTicket;
        private bool _isDateTicket;
        private string _ticketText;
        private float _expectedTicketTime;
        private ProjectViewModel _selectedComboBoxItem;

        private AllTicketsFrameViewModel _allTicketsVm;

        public DelegateCommand CreateNewTicketCommand => new DelegateCommand(CreateNewTicket);
        public ObservableCollection<ProjectViewModel> Projects { get; set; } = new ObservableCollection<ProjectViewModel>();

        public CreateTicketViewModel(AllTicketsFrameViewModel allTicketsVm)
        {
            _allTicketsVm = allTicketsVm;
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
                if (DataAccess.CurrentLoggedUser != null && project.UserId == DataAccess.CurrentLoggedUser.Id)
                    allProjectsVM.Add(new ProjectViewModel(project, this, false));
            }

            Projects.AddRange(allProjectsVM);
        }

        public bool isTaskTicket
        {
            get { return _isTaskTicket; }
            set
            {
                SetProperty(ref _isTaskTicket, value);
            }
        }
        public bool isDateTicket
        {
            get { return _isDateTicket; }
            set
            {
                SetProperty(ref _isDateTicket, value);
            }
        }
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
        public float ExpectedTicketTime
        {
            get { return _expectedTicketTime; }
            set
            {
                SetProperty(ref _expectedTicketTime, value);
            }
        }
        public ProjectViewModel SelectedComboBoxItem
        {
            get { return _selectedComboBoxItem; }
            set
            {
                SetProperty(ref _selectedComboBoxItem, value);
            }
        }

        private void CreateNewTicket()
        {
            if (DataAccess.CurrentLoggedUser == null)
            {
                MessageBox.Show("Please login as a User first.");
                return;
            }
            else if (TicketName == string.Empty)
            {
                MessageBox.Show("Please name your ticket.");
                return;
            }
            else if(SelectedComboBoxItem == null)
            {
                MessageBox.Show("Please choose a project.");
                return;
            }

            if (isTaskTicket && !isDateTicket)
            {
                CreateNewTaskTicket();
            }
            else if (isDateTicket && !isTaskTicket)
            {
                CreateNewDateTicket();
            }
            else if (isDateTicket && isTaskTicket)
            {
                MessageBox.Show("Only one checkboxes at the top may be checked.");
            }
            else
            {
                MessageBox.Show("Please make sure, that one and only one of the top Check Boxes is checked.");
            }
        }

        private void CreateNewTaskTicket()
        {
            var taskTicket = new TaskTicket(
                TicketName,
                Guid.NewGuid(),
                DataAccess.CurrentLoggedUser.Id,
                SelectedComboBoxItem.ModelId,
                TicketText,
                DateTime.Now,
                SelectedComboBoxItem.Name,
                ExpectedTicketTime,
                0
                );

            _allTicketsVm.TaskTickets.Add(new TaskTicketViewModel(taskTicket, _allTicketsVm, true));
            MessageBox.Show("The Ticket has been succesfully created!");
        }

        private void CreateNewDateTicket()
        {

        }
    }
}
