using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TTracker.GUI.Views;

namespace TTracker.Utility
{
    public static class NavigateTo
    {
        public static Frame MainFrame { get; set; }

        public static void Home()
        {
            MainFrame.Content = null;
            MainFrame.Content = new HomeView();
        }

        public static void TicketManagement()
        {
            MainFrame.Content = null;
            MainFrame.Content = new TicketManagementView();
        }

        public static void Calendar()
        {
            MainFrame.Content = null;
            MainFrame.Content = new CalendarView();
        }

        public static void TimeEngine()
        {
            MainFrame.Content = null;
            MainFrame.Content = new TimeEngineView();
        }

        public static void Statistics()
        {
            MainFrame.Content = null;
            MainFrame.Content = new StatisticsManagementView();
        }
        public static void Account()
        {
            MainFrame.Content = null;
            MainFrame.Content = new AccountView();
        }

    }
}
