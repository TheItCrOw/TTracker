﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TTracker.GUI.Converters
{
    public class IsEqualOrGreaterThanConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
               if (values.Length < 2)
                     return false;

            //   return values[0].Equals(values[1]);

            if ((float)values[0] > (float)values[1])
                return true;

            return false;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
