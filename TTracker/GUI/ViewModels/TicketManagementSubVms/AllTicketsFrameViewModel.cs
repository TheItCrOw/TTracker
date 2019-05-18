using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.GUI.Views;
using TTracker.Utility;
namespace TTracker.GUI.ViewModels.TicketManagementSubVms
{
    public class AllTicketsFrameViewModel : ViewModelManagementBase, INotifyPropertyChanged
    {

        public ObservableCollection<TaskTicketViewModel> TaskTickets { get; set; } = new ObservableCollection<TaskTicketViewModel>();
        public DelegateCommand SaveAllTicketsCommand => new DelegateCommand(SaveAllTickets);
        public DelegateCommand CreateNewTicketCommand => new DelegateCommand(CreateNewTicket);


        public AllTicketsFrameViewModel()
        {
            CurrentContent = this;
            LoadTaskTickets();
            TaskTickets.CollectionChanged += this.OnCollectionChanged;
        }

        void CreateNewTicket()
        {
            var createNewTicketView = new CreateTicketView();
            createNewTicketView.DataContext = new CreateTicketViewModel((AllTicketsFrameViewModel)CurrentContent);
            createNewTicketView.Show();
            createNewTicketView.Topmost = true;
        }

        private void SaveAllTickets()
        {
            foreach (var ticket in TaskTickets)
            {
                ticket.Save();
            }
            HasUnsavedChanges = false;
        }


        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HasUnsavedChanges = true;
            RaisePropertyChanged(nameof(TaskTickets));
        }

        private void LoadTaskTickets()
        {
            TaskTickets.Clear();
            var allTaskTickets = DataAccess.Instance.GetAll<TaskTicket>();
            var allTaskTicketsVM = new List<TaskTicketViewModel>();

            foreach (var ticket in allTaskTickets)
            {
                if (DataAccess.CurrentLoggedUser != null && ticket.UserId == DataAccess.CurrentLoggedUser.Id)
                    allTaskTicketsVM.Add(new TaskTicketViewModel(ticket, (AllTicketsFrameViewModel)CurrentContent));
            }

            TaskTickets.AddRange(allTaskTicketsVM);
        }

    }
}
