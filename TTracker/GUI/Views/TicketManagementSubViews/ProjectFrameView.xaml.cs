using System.Windows.Controls;
using TTracker.GUI.ViewModels.TicketManagementSubVms;

namespace TTracker.GUI.Views.TicketManagementSubViews
{
    /// <summary>
    /// Interaktionslogik für ProjectFrameView.xaml
    /// </summary>
    public partial class ProjectFrameView : Page
    {
        public ProjectFrameView()
        {
            InitializeComponent();
            this.DataContext = new ProjectFrameViewModel();
        }
    }
}
