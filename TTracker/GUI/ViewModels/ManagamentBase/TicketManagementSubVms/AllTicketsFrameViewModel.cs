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
using TTracker.GUI.ViewModels.ManagamentBase.TicketManagementSubVms.AllTicketsFrameSubVms;
using TTracker.GUI.Views;
using TTracker.GUI.Views.TicketManagementSubViews.AllTicketsFrameSubViews;
using TTracker.Utility;
namespace TTracker.GUI.ViewModels.TicketManagementSubVms
{
    public class AllTicketsFrameViewModel : ViewModelManagementBase, INotifyPropertyChanged
    {
        private bool _saveDirtyStateOfBase;
        private bool _showingAllTickets;

        public ObservableCollection<TaskTicketViewModel> TaskTickets { get; set; } = new ObservableCollection<TaskTicketViewModel>();
        public DelegateCommand SaveAllTicketsCommand => new DelegateCommand(SaveAllTickets);
        public DelegateCommand CreateNewTicketCommand => new DelegateCommand(CreateNewTicket);
        public DelegateCommand HideShowStaticTicketsCommand => new DelegateCommand(HideShowStaticTickets);
        public DelegateCommand<string> SortTaskTicketsCommand => new DelegateCommand<string>(SortTaskTickets);
        public DelegateCommand<string> SearchForTicketsCommand => new DelegateCommand<string>(SearchForTickets);
        public DelegateCommand FinishedTicketsCommand => new DelegateCommand(FinishedTickets);

        public AllTicketsFrameViewModel()
        {
            Setup();
        }

        void Setup()
        {
            CurrentContent = this;
            LoadTaskTickets();
            HandleCollectionChanges();
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
            //Delete first, then save the remaining
            DeleteTickets();

            foreach (var ticket in TaskTickets)
            {
                ticket.Save();
            }
            HasUnsavedChanges = false;
        }

        private void DeleteTickets()
        {
            foreach (var ticket in DeletableList)
            {
                var ticketVm = (TaskTicketViewModel)ticket;
                DataAccess.Instance.DeleteEntity<TaskTicket>(ticketVm.Model);
            }
        }

        private void HandleCollectionChanges()
        {
            TaskTickets.CollectionChanged += this.OnCollectionChanged;
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

            if (allTaskTickets == null)
                return;

            foreach (var ticket in allTaskTickets)
            {
                if (DataAccess.CurrentLoggedUser != null && ticket.UserId == DataAccess.CurrentLoggedUser.Id)
                    allTaskTicketsVM.Add(new TaskTicketViewModel(ticket, (AllTicketsFrameViewModel)CurrentContent, false));
            }
;
            TaskTickets.AddRange(allTaskTicketsVM);
            HasUnsavedChanges = false;
            _showingAllTickets = true;
        }

        private void HideShowStaticTickets()
        {
            //If there are unsaved changes, handle them first...There are prolly better ways, but for now this works
            if (HasUnsavedChanges)
            {
                MessageBox.Show("There are currently unsaved changes. Please save or reload them first");
                return;
            }

            _saveDirtyStateOfBase = HasUnsavedChanges;

            if (!_showingAllTickets)
            {
                LoadTaskTickets();
                return;
            }

            var currentListedTickets = new List<TaskTicketViewModel>();
            currentListedTickets.AddRange(TaskTickets);
            TaskTickets.Clear();

            foreach (var ticket in currentListedTickets)
            {
                if (!(ticket.Status == Status.Static))
                {
                    TaskTickets.Add(ticket);
                }
            }

            HasUnsavedChanges = _saveDirtyStateOfBase;
            _showingAllTickets = false;
        }

        private void SortTaskTickets(string buttonName)
        {
            //This makes the current base dirty, although it shouldnt. So I save the state of it before the method and set it afterwards again
            _saveDirtyStateOfBase = HasUnsavedChanges;
            var sortedTaskTickets = new List<TaskTicketViewModel>();

            switch (buttonName)
            {
                case "SortForProjects":
                    sortedTaskTickets = TaskTickets.OrderByDescending(t => t.ProjectName).ToList();
                    break;
                case "SortForName":
                    sortedTaskTickets = TaskTickets.OrderByDescending(t => t.Name).ToList();
                    break;
                case "SortForPriority":
                    sortedTaskTickets = TaskTickets.OrderByDescending(t => t.Priority).ToList();
                    break;
                case "SortForStatus":
                    sortedTaskTickets = TaskTickets.OrderBy(t => t.Status).ToList();
                    break;
                case "SortForProgress":
                    sortedTaskTickets = TaskTickets.OrderByDescending(t => t.Progress).ToList();
                    break;
            }

            TaskTickets.Clear();
            TaskTickets.AddRange(sortedTaskTickets);
            HasUnsavedChanges = _saveDirtyStateOfBase;
        }

        void SearchForTickets(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            Task.Run(() =>
            {
                if (searchTerm == string.Empty)
                {
                    foreach (var ticket in TaskTickets)
                        ticket.Visibility = Visibility.Visible;
                }

                foreach (var ticket in TaskTickets)
                {
                    if (ticket.Name.ToLower().Contains(searchTerm) 
                    || ticket.ProjectName.ToLower().Contains(searchTerm))
                    {
                        ticket.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ticket.Visibility = Visibility.Collapsed;
                    }
                }
            });
        }

        void FinishedTickets()
        {
            var finishedTicketsView = new FinishedTicketsView();
            finishedTicketsView.DataContext = new FinishedTicketsViewModel((AllTicketsFrameViewModel)CurrentContent, TaskTickets.ToList());
            finishedTicketsView.Show();
            finishedTicketsView.Topmost = true;
        }
    }
}
