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
using TTracker.GUI.Models;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels
{
    class TicketManagementViewModel : BindableBase, INotifyPropertyChanged
    {
        private bool _hasUnsavedChanges;

        public ObservableCollection<TaskTicketViewModel> TaskTickets { get; set; } = new ObservableCollection<TaskTicketViewModel>();
        public DelegateCommand SaveAllTicketsCommand => new DelegateCommand(SaveAllTickets);


        public TicketManagementViewModel()
        {
            LoadTaskTickets();
            TaskTickets.CollectionChanged += this.OnCollectionChanged;
        }

        private void SaveAllTickets()
        {
            foreach(var ticket in TaskTickets)
            {
                ticket.Save();
            }
            HasUnsavedChanges = false;
        }


        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HasUnsavedChanges = true;
        }

        private void LoadTaskTickets()
        {
            var allTaskTickets = DataAccess.Instance.GetAll<TaskTicket>();
            var allTaskTicketsVM = new List<TaskTicketViewModel>();

            foreach (var ticket in allTaskTickets)
            {
                if (DataAccess.CurrentLoggedUser != null && ticket.UserId == DataAccess.CurrentLoggedUser.Id)
                    allTaskTicketsVM.Add(new TaskTicketViewModel(ticket));
            }

            TaskTickets.AddRange(allTaskTicketsVM);
        }


        public bool HasUnsavedChanges
        {
            get
            {
                return _hasUnsavedChanges;
            }
            set
            {
                SetProperty(ref _hasUnsavedChanges, value);
            }
        }

    }
}
