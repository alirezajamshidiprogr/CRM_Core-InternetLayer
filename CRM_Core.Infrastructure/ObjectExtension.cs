using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CRM_Core.Infrastructure
{
   public static class ObjectExtension
    {
        public static DateTime ToDateTime(this string input)
        {
            PersianCalendar pc = new PersianCalendar();

            int year = Convert.ToInt32(input.Substring(0, 4));
            int month = Convert.ToInt32(input.Substring(5, 2));
            int day = Convert.ToInt32(input.Substring(8, 2));

            DateTime dt = pc.ToDateTime(year, month, day,DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            DateTime date = new DateTime(dt.Year, dt.Month, dt.Day, pc);

            DateTime longDateTime = new DateTime(date.Year, date.Month, date.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            return longDateTime;
        }
    }
}
