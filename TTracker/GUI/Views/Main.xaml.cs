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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TTracker.GUI.Views;
using TTracker.Utility;

namespace TTracker
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {

        public Main()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            NavigationFrame.Content = new HomeView();
            NavigateTo.MainFrame = this.NavigationFrame;
            LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage("de-DE")));
        }

        //Navigates to Home
        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigateTo.Home();
        }

        //Navigates to TicketManagement
        private void TicketManagement_OnClick(object sender, RoutedEventArgs e)
        {
            NavigateTo.TicketManagement();
        }

        //Naviates to the Calendar
        private void CalendarButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo.Calendar();
        }

        //Navigates to TimeEngine
        private void TimeEngine_OnClick(object sender, RoutedEventArgs e)
        {
            NavigateTo.TimeEngine();
        }

        //Navigates to the statistic
        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo.Statistics();
        }

        //Navigates to UserAccount
        private void AccountButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigateTo.Account();
        }


    }
}
