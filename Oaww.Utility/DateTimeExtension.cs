using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Oaww.Utility
{
    public static class DateTimeExtension
    {
        public static string Default(this DateTime? datetime, string defaultValue = "")
        {
            return datetime.HasValue ? datetime.Value.ToString("yyyy/MM/dd HH:mm:ss") : defaultValue;
        }

        public static string DefaultDate(this DateTime? datetime, string defaultValue = "")
        {
            return datetime.HasValue ? datetime.Value.ToString("yyyy/MM/dd") : defaultValue;
        }

        public static string ToTaiwanDateStringByFormat(this DateTime dateTime, string format)
        {
            CultureInfo info = new CultureInfo("zh-TW");
            TaiwanCalendar calendar = new TaiwanCalendar();
            info.DateTimeFormat.Calendar = calendar;
            return dateTime.ToString(format, info);
        }
    }
}
