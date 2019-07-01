using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
using TTracker.GUI.ViewModels.ManagamentBase;
using TTracker.Utility;

namespace TTracker.GUI.Views
{
    /// <summary>
    /// Interaktionslogik für StatisticsManagementView.xaml
    /// </summary>
    public partial class StatisticsManagementView : Page
    {
        public StatisticsManagementView()
        {
            InitializeComponent();
            this.DataContext = new StatisticsManagementViewModel();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            CustomPrintHelper.PrintVisual(MainChart, PageOrientation.Landscape);
        }

        private void OpenCalendarButton_Click(object sender, RoutedEventArgs e)
        {
            CalendarViewBox.Visibility = Visibility.Visible;
        }
        private void CalendarCloseButton_Click(object sender, RoutedEventArgs e)
        {
            CalendarViewBox.Visibility = Visibility.Collapsed;
        }
    }
}
