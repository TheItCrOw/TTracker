using System;
using System.Printing;
using System.Windows.Controls;
using System.Windows.Media;

namespace TTracker.Utility
{
    public static class CustomPrintHelper
    {

        public static void PrintVisual(Visual printableVisual, PageOrientation pageOrientation)
        {
            PrintDialog printDialog = new PrintDialog();

            System.Windows.FrameworkElement printingElement = printableVisual as System.Windows.FrameworkElement;

            if (printingElement == null)
                return;

            if (printDialog.ShowDialog() == true)
            {
                //store original scale
                Transform originalScale = printingElement.LayoutTransform;
                //get selected printer capabilities
                System.Printing.PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);

                //get scale of the print wrt to screen of WPF visual
                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / printingElement.ActualWidth, capabilities.PageImageableArea.ExtentHeight /
                               printingElement.ActualHeight);

                //Transform the Visual to scale
                printingElement.LayoutTransform = new ScaleTransform(scale, scale);

                //get the size of the printer page
                System.Windows.Size sz = new System.Windows.Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

                //update the layout of the visual to the printer page size.
                printingElement.Measure(sz);
                printingElement.Arrange(new System.Windows.Rect(new System.Windows.Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

                //now print the visual to printer to fit on the one page.
                printDialog.PrintVisual(printingElement, "Statistics");

                //apply the original transform.
                printingElement.LayoutTransform = originalScale;
                printDialog.PrintTicket.PageOrientation = pageOrientation;

            }
        }

    }
}
