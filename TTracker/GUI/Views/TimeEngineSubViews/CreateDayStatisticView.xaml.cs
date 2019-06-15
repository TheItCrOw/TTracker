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

namespace TTracker.GUI.Views.TimeEngineSubViews
{
    /// <summary>
    /// Interaktionslogik für CreateDayStatisticView.xaml
    /// </summary>
    public partial class CreateDayStatisticView : Window
    {
        public CreateDayStatisticView()
        {
            InitializeComponent();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(MainGrid, "Day Statistic");
            }
        }
    }
}
