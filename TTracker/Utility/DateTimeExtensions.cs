using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTracker.Utility
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns the Date of the start of the running week
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="startOfWeek"></param>
        /// <returns></returns>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

    }

    public enum CustomCalendarMode
    {
        [Description("Day")]
        Day,
        [Description("Week")]
        Week,
        [Description("Month")]
        Month,
        [Description("Year")]
        Year
    }
}
