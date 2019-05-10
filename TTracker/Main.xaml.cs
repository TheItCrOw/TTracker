using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TTracker.GUI.Views;

namespace TTracker
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {

        LoginView loginWindow = new LoginView();
        public Main()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            NavigationFrame.Content = new HomeView();
            loginWindow.Show();
            loginWindow.Topmost = true;
        }

        //Navigates to Home
        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationFrame.Content = new HomeView();
        }

        //Navigates to UserAccount
        private void AccountButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationFrame.Content = new AccountView();
        }

        private void TicketManagement_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationFrame.Content = new TicketManagementView();
        }
    }
}
