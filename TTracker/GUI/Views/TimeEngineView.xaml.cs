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
    }
}
