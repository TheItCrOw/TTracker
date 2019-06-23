using Microsoft.Win32;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
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
        public DelegateCommand ExportSelectedTicketsCommand => new DelegateCommand(ExportSelectedTickets);

        public FinishedTicketsViewModel(AllTicketsFrameViewModel currentBase, List<TaskTicketViewModel> currentTaskTickets)
        {
            CurrentContent = this;
            LoadFinishedTaskTickets(currentTaskTickets);
        }

        void LoadFinishedTaskTickets(List<TaskTicketViewModel> currentTaskTickets)
        {
            if (currentTaskTickets.Count == 0)
            {
                System.Windows.MessageBox.Show("There are no tickets currently available. Make sure you are logged in or try to restart the program.");
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
                System.Windows.MessageBox.Show("There are no finished Tickets currently.");
                System.Windows.Application.Current.MainWindow.Close();
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
                System.Windows.MessageBox.Show("Please select at least one ticket");
                return;
            }

            FinishedTaskTickets.Clear();
            FinishedTaskTickets.AddRange(leftOverTickets);

            System.Windows.MessageBox.Show("Selected Tickets have been deleted.");
        }

        void ExportSelectedTickets()
        {
            var leftOverTickets = new List<object>();
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();

            if (folderBrowserDialog.SelectedPath == string.Empty)
                return;

            foreach (var utilityVm in FinishedTaskTickets)
            {
                var ticket = (UtilityViewModel<TaskTicketViewModel>)utilityVm;
                if(ticket.IsSelected == true)
                {
                    var Id = ticket.CurrentViewModel.ModelId;
                    var name = ticket.CurrentViewModel.Name;
                    DataAccess.Instance.ExportEntity<TaskTicket>(Id, folderBrowserDialog.SelectedPath + "\\" + Id + ".tt");
                    DataAccess.Instance.DeleteEntity<TaskTicket>(ticket.CurrentViewModel.Model);                    
                }
                else
                {
                    leftOverTickets.Add(ticket);
                }
            }

            FinishedTaskTickets.Clear();
            FinishedTaskTickets.AddRange(leftOverTickets);
        }
    }
}
