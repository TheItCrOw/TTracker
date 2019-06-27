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
using System.Windows.Shapes;

namespace TTracker.GUI.Views.CalendarSubViews
{
    /// <summary>
    /// Interaktionslogik für CreateDateTicketView.xaml
    /// </summary>
    public partial class CreateDateTicketView : Window
    {
        public CreateDateTicketView()
        {
            InitializeComponent();
        }

        private void FromOpenCalendarButton_Click(object sender, RoutedEventArgs e)
        {
            FromCalendarViewBox.Visibility = Visibility.Visible;
        }
        private void FromCalendarCloseButton(object sender, RoutedEventArgs e)
        {
            FromCalendarViewBox.Visibility = Visibility.Collapsed;
        }

        private void ToOpenCalendarButton_Click(object sender, RoutedEventArgs e)
        {
            ToCalendarViewBox.Visibility = Visibility.Visible;
        }
        private void ToCalendarCloseButton(object sender, RoutedEventArgs e)
        {
            ToCalendarViewBox.Visibility = Visibility.Collapsed;
        }
    }
}
