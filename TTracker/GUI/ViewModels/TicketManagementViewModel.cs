using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.GUI.Models;

namespace TTracker.GUI.ViewModels
{
    class TicketManagementViewModel
    {

        public ObservableCollection<TaskTicketViewModel> TaskTickets { get; set; } = new ObservableCollection<TaskTicketViewModel>();

        public TicketManagementViewModel()
        {
            testingOnly();
        }


        void testingOnly()
        {
            var testTicket1 = new TaskTicket("test1", new Guid(), new Guid(), new Guid(), "testText1testText1testText1testext1", DateTime.Now, "projectNameHere1", 5, 3);
            var testTicket2 = new TaskTicket("test2", new Guid(), new Guid(), new Guid(), "testText2", DateTime.Now, "projectNameHere2", 3, 1);
            var testTicket3 = new TaskTicket("test3", new Guid(), new Guid(), new Guid(), "testText3", DateTime.Now, "projectNameHere3", 2, 3);
            var testTicket4 = new TaskTicket("test4", new Guid(), new Guid(), new Guid(), "testText4", DateTime.Now, "projectNameHere4", 1, 5);
            var testTicket5 = new TaskTicket("test5", new Guid(), new Guid(), new Guid(), "testText5", DateTime.Now, "projectNameHere5", 6, 7); 



            var testTicket1VM = new TaskTicketViewModel(testTicket1);
            var testTicket2VM = new TaskTicketViewModel(testTicket2);
            var testTicket3VM = new TaskTicketViewModel(testTicket3);
            var testTicket4VM = new TaskTicketViewModel(testTicket4);
            var testTicket5VM = new TaskTicketViewModel(testTicket5);

            TaskTickets.Add(testTicket1VM);
            TaskTickets.Add(testTicket2VM);
            TaskTickets.Add(testTicket3VM);
            TaskTickets.Add(testTicket4VM);
            TaskTickets.Add(testTicket5VM);

            TaskTickets.Add(testTicket1VM);
            TaskTickets.Add(testTicket2VM);
            TaskTickets.Add(testTicket3VM);
            TaskTickets.Add(testTicket4VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket1VM);
            TaskTickets.Add(testTicket2VM);
            TaskTickets.Add(testTicket3VM);
            TaskTickets.Add(testTicket4VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);
            TaskTickets.Add(testTicket5VM);

        }

    }
}
