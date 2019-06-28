using System;

namespace Lab2.Services
{
    public class DateTimeUtils
    {
        public static int GetMonthDifference(DateTime start, DateTime end)
        {
            int monthsApart = 12 * (start.Year - end.Year) + start.Month - end.Month;
            return Math.Abs(monthsApart);
        }
    }
}