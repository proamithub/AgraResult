using System;

namespace DbrauResultUI.Helpers
{
    public static class DateTimeZoneExtension
    {
        public static string ToCustomeTimeZone(this DateTime dt, string timeOffSet)
        {
            if (timeOffSet != null)
            {
                var k = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, TimeZoneInfo.Utc.Id, timeOffSet);
                return k.ToString();
            }
            return dt.ToLocalTime().ToString();
        }

        public static DateTime ToCustomeTimeZone(this DateTime dt, string timeOffSet, int addHours)
        {
            try
            {
                TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timeOffSet);
                if (timeOffSet != null)
                {
                    bool isDayLight = zone.IsDaylightSavingTime(dt);
                    if (isDayLight)
                    {
                        var offset = zone.GetUtcOffset(dt);
                        var localTime = dt.AddHours(offset.Hours);
                        return localTime;
                    }
                    DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(dt, zone);
                    return localDateTime;
                }
            }
            catch (Exception)
            {
                return dt.ToLocalTime();
            }
            return dt.ToLocalTime();
        }

    }
}