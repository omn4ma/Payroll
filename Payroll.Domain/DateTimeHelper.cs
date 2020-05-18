using System;

namespace Payroll.Domain
{
    internal static class DateTimeHelper
    {
        public static int TotalYears(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) +
                (((end.Month > start.Month) ||
                ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }
    }
}
