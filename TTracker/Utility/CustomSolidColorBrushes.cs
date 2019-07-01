using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TTracker.Utility
{
    public static class CustomSolidColorBrushes
    {

        public static SolidColorBrush GetColorByIndex(int i)
        {
            var colors = new List<SolidColorBrush>();

            colors.Add(Brushes.LightBlue);
            colors.Add(Brushes.LightGreen);
            colors.Add(Brushes.LightYellow);
            colors.Add(Brushes.LightGray);
            colors.Add(Brushes.LightPink);
            colors.Add(Brushes.LightGoldenrodYellow);
            colors.Add(Brushes.LightSteelBlue);
            colors.Add(Brushes.LightCoral);
            colors.Add(Brushes.LightSalmon);
            colors.Add(Brushes.LightSlateGray);

            var color = colors[i];

            return color;
        }

    }
}
