using Microsoft.Win32;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TTracker.BaseDataModules;
using TTracker.GUI.ViewModels.Entities;
using TTracker.Utility;
using System.IO.Packaging;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Spire.Pdf.Graphics;
using Spire.Pdf;

namespace TTracker.GUI.ViewModels.ManagamentBase
{
    class CreateDayStatisticViewModel : ViewModelManagementBase
    {
        private string _selectedCalendarDate;
        private DateTime _currentDate;
        public ObservableCollection<TimeEntryViewModel> TimeEntries { get; set; } = new ObservableCollection<TimeEntryViewModel>();

        //This collection holds the data that is shown in the sub projects pie chart
        public ObservableCollection<ChartHelperModel> SubProjectsChart { get; set; } = new ObservableCollection<ChartHelperModel>();
        public DelegateCommand<FrameworkElement> SaveAsPdfCommand => new DelegateCommand<FrameworkElement>(SaveAsPdf);



        public string SelectedCalendarDate
        {
            get { return _selectedCalendarDate; }
            set
            {
                SetProperty(ref _selectedCalendarDate, value);
            }
        }

        public CreateDayStatisticViewModel(TimeEngineViewModel currentBase, List<TimeEntryViewModel> timeEntries, DateTime date)
        {
            SelectedCalendarDate = "Day summary of the " + date.ToShortDateString();
            _currentDate = date;
            TimeEntries.Clear();
            var sortedTimeEntries = timeEntries.OrderBy(t => t.StartTime);
            TimeEntries.AddRange(sortedTimeEntries);

            SubProjectsChart.AddRange(StatisticsHelperClass.CreateChartModelsOfTimeEntriesSubProjects(timeEntries));
        }

        /// <summary>
        /// Creates an image of the given FrameworkElement, then converts it into a PDF
        /// </summary>
        /// <param name="source"></param>
        void SaveAsPdf(FrameworkElement source)
        {            
            var dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.DefaultExt = "pdf";
            dialog.Filter = "PDF Document (*.pdf)|*.pdf";

            if (dialog.ShowDialog() == false)
                return;

            string path = dialog.FileName;

            try
            {
                var dir = Path.GetDirectoryName(path);
                if (dir != null && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)source.ActualWidth, (int)source.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                VisualBrush sourceBrush = new VisualBrush(source);

                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();

                using (drawingContext)
                {
                    drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(source.ActualWidth, source.ActualHeight)));
                }
                renderTarget.Render(drawingVisual);

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderTarget));

                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, System.IO.FileShare.Write))
                {
                    encoder.Save(stream);
                }

                CreatePdfFromImage(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private protected void CreatePdfFromImage(string path)
        {
            using (PdfDocument pdfDoc = new PdfDocument())
            {
                PdfImage pdfImg = PdfImage.FromFile(path);


                PdfPageBase page = pdfDoc.Pages.Add();
                float width = pdfImg.Width * 0.68f;
                float height = pdfImg.Height * 0.68f;
                float x = (page.Canvas.ClientSize.Width - width) / 5;

                page.Canvas.DrawImage(pdfImg, x, 0, width, height);

                string PdfFilename = path;
                pdfDoc.SaveToFile(PdfFilename);
                System.Diagnostics.Process.Start(PdfFilename);
            }
        }
    }


}
