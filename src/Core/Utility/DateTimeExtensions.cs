using Core.Enums;

namespace Core.Utility;

public static class DateTimeExtensions
{
    public static string Minutes(this DateTime dateTime, DateTime nowTime = default)
    {
        if (nowTime == default) nowTime = DateTime.UtcNow;

        if (dateTime.Year == nowTime.Year &&
            dateTime.Month == nowTime.Month &&
            dateTime.Day == nowTime.Day &&
            dateTime.Hour == nowTime.Hour &&
            dateTime.Minute == nowTime.Minute &&
            dateTime.Minute != nowTime.Second)
        {
            return string.Concat(Math.Abs(dateTime.Second - nowTime.Second), " secs");
        }
        else if (dateTime.Year == nowTime.Year &&
                dateTime.Month == nowTime.Month &&
                dateTime.Day == nowTime.Day &&
                dateTime.Hour == nowTime.Hour &&
                dateTime.Minute != nowTime.Minute)
        {
            return string.Concat(Math.Abs(dateTime.Minute - nowTime.Minute), " mins");
        }
        else if (dateTime.Year == nowTime.Year &&
                dateTime.Month == nowTime.Month &&
                dateTime.Day == nowTime.Day &&
                dateTime.Hour != nowTime.Hour)
        {
            return string.Concat(dateTime.Hour, ":", dateTime.Minute);
        }
        else if (dateTime.Year == nowTime.Year &&
                dateTime.Month == nowTime.Month &&
                dateTime.Day != nowTime.Day)
        {
            return string.Concat((Months)(dateTime.Month - 1), " ", dateTime.Day);
        }
        else
        {
            return string.Concat((Months)(dateTime.Month - 1), " ", dateTime.Day, " '", (dateTime.Year % 100));
        }
    }
}
