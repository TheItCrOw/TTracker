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

namespace TTracker.GUI.ViewModels
{
    public class TicketManagementViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private bool _hasUnsavedChanges;
        private BindableBase _currentContent;

        public ObservableCollection<TaskTicketViewModel> TaskTickets { get; set; } = new ObservableCollection<TaskTicketViewModel>();
        public DelegateCommand SaveAllTicketsCommand => new DelegateCommand(SaveAllTickets);
        public DelegateCommand CreateNewTicketCommand => new DelegateCommand(CreateNewTicket);


        public TicketManagementViewModel()
        {
            LoadTaskTickets();
            TaskTickets.CollectionChanged += this.OnCollectionChanged;
        }

        void CreateNewTicket()
        {
            var createNewTicketView = new CreateTicketView();
            createNewTicketView.DataContext = new CreateTicketViewModel((TicketManagementViewModel)CurrentContent);
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

        public BindableBase CurrentContent
        {
            get
            {
                return _currentContent;
            }
            set
            {
                SetProperty(ref _currentContent, value);
            }
        }

    }
}
