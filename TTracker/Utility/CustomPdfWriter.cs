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

namespace TTracker.Utility
{
    public class CustomPdfWriter
    {
        /// <summary>
        /// Creates an image of the given FrameworkElement, then converts it into a PDF
        /// </summary>
        /// <param name="source"></param>
        public void ExportAsPdf(FrameworkElement source, float widthMultiplier, float heightMultiplier, PdfPageOrientation pdfPageOrientation)
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

                CreatePdfFromImage(path, widthMultiplier, heightMultiplier, pdfPageOrientation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CreatePdfFromImage(string path, float widthMultiplier, float heightMultiplier, PdfPageOrientation pdfPageOrientation)
        {
            using (PdfDocument pdfDoc = new PdfDocument())
            {
                PdfImage pdfImg = PdfImage.FromFile(path);
                pdfDoc.PageSettings.Size = PdfPageSize.A4;
                pdfDoc.PageSettings.Orientation = pdfPageOrientation;

                PdfPageBase page = pdfDoc.Pages.Add();
                float width = pdfImg.Width * widthMultiplier;
                float height = pdfImg.Height * heightMultiplier;
                float x = (page.Canvas.ClientSize.Width - width) / 5;
                pdfDoc.CompressionLevel = PdfCompressionLevel.Best;

                page.Canvas.DrawImage(pdfImg, x, 0, width, height);

                string PdfFilename = path;
                pdfDoc.SaveToFile(PdfFilename);
                System.Diagnostics.Process.Start(PdfFilename);
            }
        }

    }
}
