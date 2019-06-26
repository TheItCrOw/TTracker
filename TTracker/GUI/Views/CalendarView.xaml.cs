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
using TTracker.GUI.ViewModels.ManagamentBase;

namespace TTracker.GUI.Views
{
    /// <summary>
    /// Interaktionslogik für CalendarView.xaml
    /// </summary>
    public partial class CalendarView : Page
    {
        public CalendarView()
        {
            InitializeComponent();
            this.DataContext = new CalendarViewModel(MainContentFrame);
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
