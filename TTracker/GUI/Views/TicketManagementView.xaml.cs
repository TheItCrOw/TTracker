using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TTracker.GUI.ViewModels;
using TTracker.GUI.Views.TicketManagementSubViews;

namespace TTracker.GUI.Views
{
    /// <summary>
    /// Interaktionslogik für TicketManagementView.xaml
    /// </summary>
    public partial class TicketManagementView : Page
    {
        public TicketManagementView()
        {
            InitializeComponent();
            this.DataContext = new TicketManagementViewModel();
            NavigationFrame.Content = null;
            NavigationFrame.Content = new AllTicketsFrameView();
        }

        private void NewTicketButton_Click(object sender, RoutedEventArgs e)
        {
            var createNewTicketView = new CreateTicketView();
            createNewTicketView.Show();
            createNewTicketView.Topmost = true;
        }

        private void AllTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationFrame.Content = null;
            NavigationFrame.Content = new AllTicketsFrameView();
        }
    }
}
