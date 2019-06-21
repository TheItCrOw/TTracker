using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.GUI.ViewModels.TicketManagementSubVms;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels.ManagamentBase.TicketManagementSubVms.AllTicketsFrameSubVms
{
    class FinishedTicketsViewModel : ViewModelManagementBase
    {
        public ObservableCollection<object> FinishedTaskTickets { get; set; } = new ObservableCollection<object>();
        public DelegateCommand DeleteSelectedTicketsCommand => new DelegateCommand(DeleteSelectedTickets);

        public FinishedTicketsViewModel(AllTicketsFrameViewModel currentBase, List<TaskTicketViewModel> currentTaskTickets)
        {
            CurrentContent = this;
            LoadFinishedTaskTickets(currentTaskTickets);
        }

        void LoadFinishedTaskTickets(List<TaskTicketViewModel> currentTaskTickets)
        {
            if (currentTaskTickets.Count == 0)
            {
                MessageBox.Show("There are no tickets currently available. Make sure you are logged in or try to restart the program.");
            }

            foreach (var ticket in currentTaskTickets)
            {
                if (ticket.Status == Status.Finished)
                {
                    var utilModel = new UtilityViewModel<TaskTicketViewModel>(ticket);
                    FinishedTaskTickets.Add(utilModel);
                }
            }

            if(FinishedTaskTickets.Count == 0)
            {
                MessageBox.Show("There are no finished Tickets currently.");
                Application.Current.MainWindow.Close();
            }
        }

        void DeleteSelectedTickets()
        {
            var leftOverTickets = new List<object>();

            foreach(var utilityVm in FinishedTaskTickets)
            {
                var ticket = (UtilityViewModel<TaskTicketViewModel>)utilityVm;
                if(ticket.IsSelected == true)
                {
                    DataAccess.Instance.DeleteEntity<TaskTicket>(ticket.CurrentViewModel.Model);
                }
                else
                {
                    leftOverTickets.Add(utilityVm);
                }
            }

            if(FinishedTaskTickets.Count == leftOverTickets.Count)
            {
                MessageBox.Show("Please select at least one ticket");
                return;
            }

            FinishedTaskTickets.Clear();
            FinishedTaskTickets.AddRange(leftOverTickets);

            MessageBox.Show("Selected Tickets have been deleted.");
        }
    }
}
