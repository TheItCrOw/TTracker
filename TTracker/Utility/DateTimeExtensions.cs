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

        /// <summary>
        /// Returns the date of the start of the running month
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime StartOfMonth(DateTime dt)
        {
            var startOfMonth = dt;
            for (int i = 0; i < 32; i++)
            {
                if (startOfMonth.Month == dt.Month)
                    startOfMonth = startOfMonth.AddDays(-1);
            }
            return startOfMonth;
        }

        /// <summary>
        /// Returns the amount of days the month has as an int
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int AmountOfDaysOfMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return 31;
                case 2:
                    return 28;
                case 3:
                    return 31;
                case 4:
                    return 30;
                case 5:
                    return 31;
                case 6:
                    return 30;
                case 7:
                    return 31;
                case 8:
                    return 31;
                case 9:
                    return 30;
                case 10:
                    return 31;
                case 11:
                    return 30;
                case 12:
                    return 31;

            }
            return 0;
        }

        /// <summary>
        /// Takes in a float and returns a Timespan, that you can then add upon a Date
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static TimeSpan ConvertFloatToTimespan(float time)
        {
            //Make a decimal out of the start and end time
            var timeDecimal = time / 100;
            //Get the value right of the comma e.g. 9,45 => 45
            var decimalValue = timeDecimal - Math.Truncate(timeDecimal);
            //Get the value left of the comma e.g 9,45 => 9
            var numberValue = Math.Truncate(timeDecimal);

            //Make Timespans .FromMinutes and .FromHours of left and right value of comma
            TimeSpan timeInMinutes = TimeSpan.FromMinutes(decimalValue * 100);
            TimeSpan timeInHours = TimeSpan.FromHours(numberValue);

            //Calculate them together e.g. 9:45 now, but in Timespan
            TimeSpan result = timeInHours + timeInMinutes;

            return result;
        }

        /// <summary>
        /// Takes in 2 floats like: 900 for 9:00 o'clock and gives back a float, that has the Timespan in hours
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float CalculateTimespanOf2floats(float start, float end)
        {
            double result = 0;

            //Make a decimal out of the start and end time
            var startDecimal = start / 100;
            var endDecimal = end / 100;
            //Get the value right of the comma e.g. 9,45 => 45
            var decimalValue_Start = startDecimal - Math.Truncate(startDecimal);
            var decimalValue_End = endDecimal - Math.Truncate(endDecimal);
            //Get the value left of the comma e.g 9,45 => 9
            var numberValue_Start = Math.Truncate(startDecimal);
            var numberValue_End = Math.Truncate(endDecimal);

            //Make Timespans .FromMinutes and .FromHours of left and right value of comma
            TimeSpan startInMinutes = TimeSpan.FromMinutes(decimalValue_Start * 100);
            TimeSpan startInHours = TimeSpan.FromHours(numberValue_Start);
            TimeSpan endInMinutes = TimeSpan.FromMinutes(decimalValue_End * 100);
            TimeSpan endInHours = TimeSpan.FromHours(numberValue_End);

            //Calculate them together e.g. 9:45 now, but in Timespan
            TimeSpan startInTimespan = startInHours + startInMinutes;
            TimeSpan endInTimespan = endInHours + endInMinutes;

            //Calculate the Difference between the two Timespasn start and end
            TimeSpan calculatedDiff = endInTimespan - startInTimespan;
            result = calculatedDiff.TotalHours;

            //Roundn the result to hours -> 10:00-9:30 is 0,5 hours
            var roundedResult = (float)(Math.Truncate((double)result * 100) / 100);

            return roundedResult;
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
