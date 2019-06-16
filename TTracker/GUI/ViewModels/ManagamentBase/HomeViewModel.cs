using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.GUI.Views;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels
{
    class HomeViewModel : ViewModelManagementBase
    {
        private string _date;
        public ObservableCollection<TaskTicketViewModel> TaskTickets { get; set; } = new ObservableCollection<TaskTicketViewModel>();


        public HomeViewModel()
        {
            Date = ($"Your To-Do´s for today, the {DateTime.Now.ToShortDateString()}");
            LoadTickets();
        }

        void LoadTickets()
        {
            var allTickets = DataAccess.Instance.GetAll<TaskTicket>();
            var todoTickets = new List<TaskTicket>();
            var highPrioTickets = new List<TaskTicket>();

            foreach(var ticket in allTickets)
            {
                if (DataAccess.CurrentLoggedUser == null || ticket.UserId != DataAccess.CurrentLoggedUser.Id)
                    return;

                if(ticket.Status == Status.Working)
                {
                    todoTickets.Add(ticket);
                }
                if(ticket.Priority == PriorityLevel.High 
                    || ticket.Priority == PriorityLevel.VeryHigh
                    && !(todoTickets.Contains(ticket)))
                {
                    highPrioTickets.Add(ticket);
                }
            }

            TaskTickets.AddRange(todoTickets.Select(t => new TaskTicketViewModel(t, this, false)));
            TaskTickets.AddRange(highPrioTickets.Select(t => new TaskTicketViewModel(t, this, false)));
        }

        public string Date
        {
            get { return _date; }
            set
            {
                SetProperty(ref _date, value);
            }
        }

    }
}
