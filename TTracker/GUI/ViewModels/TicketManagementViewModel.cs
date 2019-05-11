using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.GUI.Models;

namespace TTracker.GUI.ViewModels
{
    class TicketManagementViewModel : BindableBase
    {

        public ObservableCollection<TaskTicketViewModel> TaskTickets { get; set; } = new ObservableCollection<TaskTicketViewModel>();

        public TicketManagementViewModel()
        {
            testingOnly();
        }

















        void testingOnly()
        {
            var testTicket1 = new TaskTicket("Adding legit test data", new Guid(), new Guid(), new Guid(), "Since I want to see the full design of the extender and ticketmanaagement window in generel, I need to add some legit data I would type in. Adding micro tasks for better overview might alos help.", DateTime.Now, "TTracker", 5, 3);
            var testTicket2 = new TaskTicket("Coming up with senseless stuff", new Guid(), new Guid(), new Guid(), "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.", DateTime.Now, "Testing TTracker", 3, 1);
            var testTicket3 = new TaskTicket("Come up with a long ass text jesus", new Guid(), new Guid(), new Guid(), "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?", DateTime.Now, "testing TTracker", 2, 3);
            var testTicket4 = new TaskTicket("test4", new Guid(), new Guid(), new Guid(), "testText4", DateTime.Now, "projectNameHere4", 6, 1);
            var testTicket5 = new TaskTicket("test5", new Guid(), new Guid(), new Guid(), "testText5", DateTime.Now, "projectNameHere5", 60, 7); 



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
