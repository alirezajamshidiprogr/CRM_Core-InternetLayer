using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CRM_Core.Infrastructure
{
    public static class Utility
    {
        public static DateTime ToDataTime(string persianData)
        {
            PersianCalendar pc = new PersianCalendar();
            int year = Convert.ToInt32(persianData.Substring(0, 4)); 
            int month = Convert.ToInt32(persianData.Substring(5, 2)); 
            int day = Convert.ToInt32(persianData.Substring(8, 2)); 
            DateTime dt = new DateTime(year, month, day, pc);

            return dt;
        }
    }
}
