using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TTracker.GUI.Models;
using TTracker.GUI.ViewModels.TicketManagementSubVms;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels
{
    class CreateTicketViewModel : BindableBase, INotifyPropertyChanged
    {
        private string _ticketName;
        private bool _isTaskTicket;
        private bool _isDateTicket;
        private string _ticketText;
        private float _expectedTicketTime;

        private AllTicketsFrameViewModel _allTicketsVm;
        public DelegateCommand CreateNewTicketCommand => new DelegateCommand(CreateNewTicket);


        //Adding Projectslist with observable Collection

        public CreateTicketViewModel(AllTicketsFrameViewModel allTicketsVm)
        {
            _allTicketsVm = allTicketsVm;
        }

        public bool isTaskTicket
        {
            get
            {
                return _isTaskTicket;
            }
            set
            {
                SetProperty(ref _isTaskTicket, value);
            }
        }
        public bool isDateTicket
        {
            get
            {
                return _isDateTicket;
            }
            set
            {
                SetProperty(ref _isDateTicket, value);
            }
        }
        public string TicketName
        {
            get
            {
                return _ticketName;
            }
            set
            {
                SetProperty(ref _ticketName, value);
            }
        }
        public string TicketText
        {
            get
            {
                return _ticketText;
            }
            set
            {
                SetProperty(ref _ticketText, value);
            }
        }
        public float ExpectedTicketTime
        {
            get
            {
                return _expectedTicketTime;
            }
            set
            {
                SetProperty(ref _expectedTicketTime, value);
            }
        }

        //Projects Property has to be added here

        private void CreateNewTicket()
        {
            if(DataAccess.CurrentLoggedUser == null)
            {
                MessageBox.Show("Please login as a User first.");
                return;
            }

            if(isTaskTicket && !isDateTicket)
            {
                CreateNewTaskTicket();
            }
            else if(isDateTicket && !isTaskTicket)
            {
                CreateNewDateTicket();
            }
            else if(isDateTicket && isTaskTicket)
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
                Guid.Empty,
                TicketText,
                DateTime.Now,
                "IHaveNoProjectsYet",
                ExpectedTicketTime,
                0
                );

           // DataAccess.Instance.RegisterAndSaveNewTaskTicket(taskTicket);
            _allTicketsVm.TaskTickets.Add(new TaskTicketViewModel(taskTicket, _allTicketsVm));
            MessageBox.Show("The Ticket has been succesfully created!");
        }

        private void CreateNewDateTicket()
        {

        }

    }
}
