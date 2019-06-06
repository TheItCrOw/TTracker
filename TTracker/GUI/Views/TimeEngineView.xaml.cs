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

namespace TTracker.GUI.Views
{
    /// <summary>
    /// Interaktionslogik für TimeEngineView.xaml
    /// </summary>
    public partial class TimeEngineView : Page
    {
        public TimeEngineView()
        {
            InitializeComponent();
            this.DataContext = new TimeEngineViewModel();
        }

        private void ProjectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ((ComboBox)sender).SelectedItem;
            ((TimeEngineViewModel)DataContext).SelectedProjectComboBoxItem = (ProjectViewModel)item;
        }

        private void TaskTicketsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ((ComboBox)sender).SelectedItem;
            ((TimeEngineViewModel)DataContext).SelectedTaskTicketComboBoxItem = (TaskTicketViewModel)item;
        }


        //When the time boxes are selected, select everything in that box
        private void FromTimeBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            FromTimeBox.SelectAll();
        }
        private void FromTimeBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            FromTimeBox.SelectAll();
        }
        private void ToTimeBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ToTimeBox.SelectAll();
        }
        private void ToTimeBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            ToTimeBox.SelectAll();
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
