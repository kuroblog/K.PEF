using System;

namespace K.PEF.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string AsFullTimeFormatString(this DateTime time) => time.AsFormatString("yyyyMMddHHmmssfffffff");

        public static string AsFormatString(this DateTime time, string format = "yyyy-MM-dd HH:mm:ss") =>
            time.ToString(format);
    }
}
