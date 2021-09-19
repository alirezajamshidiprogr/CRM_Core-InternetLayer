using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

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

        public static string ToPersianDateTime(DateTime dt)
        {
            PersianCalendar PersianCalendar1 = new PersianCalendar();
            return string.Format(@"{0}-{1}-{2}_{3}-{4}-{5}",
                         PersianCalendar1.GetYear(dt),
                         PersianCalendar1.GetMonth(dt),
                         PersianCalendar1.GetDayOfMonth(dt),
                         PersianCalendar1.GetHour(dt),
                         PersianCalendar1.GetMonth(dt),
                         PersianCalendar1.GetMinute(dt));
        }

        public static string ToPersianDate(DateTime dt)
        {
            PersianCalendar PersianCalendar1 = new PersianCalendar();
            return string.Format(@"{0}-{1}-{2}",
                         PersianCalendar1.GetYear(dt),
                         PersianCalendar1.GetMonth(dt),
                         PersianCalendar1.GetDayOfMonth(dt));
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData )
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static void RegisterErrorLog(Exception ex , string userName)
        { 
            var filePath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            filePath += @"\wwwroot\Logs\";
            string fileName = "log_" +ToPersianDateTime(DateTime.Now) + ".txt";
            string fullPath = filePath + fileName;
            try
            {
                // Create a new file     
                using (FileStream fs = File.Create(fullPath))
                {
                    StringBuilder sbuilder = new StringBuilder();
                    using (StringWriter sw = new StringWriter(sbuilder))
                    {
                        using (XmlTextWriter xmlWriter = new XmlTextWriter(sw))
                        {
                            xmlWriter.Formatting = Formatting.Indented;
                            xmlWriter.Indentation = 4;

                            xmlWriter.WriteStartElement("LogInfo");
                            xmlWriter.WriteElementString("Time", ToPersianDateTime(DateTime.Now));
                            xmlWriter.WriteElementString("UserInfo", userName);
                            xmlWriter.WriteElementString("Target", ex.StackTrace);
                            xmlWriter.WriteElementString("ErrorMessage", ex.Message);
                            xmlWriter.WriteEndElement();
                        }
                    }

                    //// Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes(sbuilder.ToString());
                    fs.Write(title, 0, title.Length);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
