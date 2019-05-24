using System.Windows;
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

        private void SelectedRootProjectButton_Click(object sender, RoutedEventArgs e)
        {
            var blub = sender.GetType();
            ((ProjectFrameViewModel)DataContext).SelectedItemCommand.Execute(sender);
        }
    }
}
