﻿using System;
using System.Globalization;

namespace NetCoreWithRedis.Core.Log.Helper
{
    public static class DateTimeHelper
    {
        public static string TodayStr
        {
            get
            {
                var currDate = DateTime.Now;
                return currDate.Day + "" + currDate.Month + "" + currDate.Year;
            }
        }
        public static long EndOfDay(this long longDate)
        {
            var date = longDate.ToDateTime();
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Kind).ToLong();
        }
        public static long BeginningOfDay(this long longDate)
        {
            var date = longDate.ToDateTime();
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Kind).ToLong();
        }
        public static long BeginningOfHour(this long longDate)
        {
            var date = longDate.ToDateTime();
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0, date.Kind).ToLong();
        }
        public static DateTime ToDateTime(this long numberDate)
        {
            DateTime result;
            if (numberDate == 0)
            {
                return DateTime.MinValue;
            }

            if (DateTime.TryParseExact(numberDate.ToString(), new[] { "yyyyMMddHHmmss", "yyyyMMddHHmm" }, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out result))
            {
                return result;
            }
            throw new Exception("Date Parse Error" + numberDate);
        }
        public static long MinValue => DateTime.MinValue.ToLong();
        public static long MaxValue => DateTime.MaxValue.ToLong();
        public static long Now => DateTime.Now.ToLong();
        public static long Today => DateTime.Today.ToLong();
        public static TimeSpan Subtract(this long source, long target) => source.ToDateTime().Subtract(target.ToDateTime());
        public static long GenerateDate(int year, int month, int day) => new DateTime(year, month, day).ToLong();
        public static long GenerateDate(int year, int month, int day, int hour, int minute, int second) => new DateTime(year, month, day, hour, minute, second).ToLong();
        public static long GenerateDate(int year, int month, int day, int hour, int minute, int second, int miliSecond) => new DateTime(year, month, day, hour, minute, second, miliSecond).ToLong();
        public static long AddSeconds(this long dt, double value) => dt.ToDateTime().AddSeconds(value).ToLong();
        public static long AddMinutes(this long dt, double value) => dt.ToDateTime().AddMinutes(value).ToLong();
        public static long AddHours(this long dt, double value) => dt.ToDateTime().AddHours(value).ToLong();
        public static long AddDays(this long dt, double value) => dt.ToDateTime().AddDays(value).ToLong();
        public static long AddMonths(this long dt, int value) => dt.ToDateTime().AddMonths(value).ToLong();
        public static long AddYears(this long dt, int value) => dt.ToDateTime().AddYears(value).ToLong();
        public static int GetYear(this long dt) => dt.ToDateTime().Year;
        public static int GetMonth(this long dt) => dt.ToDateTime().Month;
        public static int GetDay(this long dt) => dt.ToDateTime().Day;
        public static int GetHour(this long dt) => dt.ToDateTime().Hour;
        public static int GetMinute(this long dt) => dt.ToDateTime().Minute;
        public static int GetSecond(this long dt) => dt.ToDateTime().Second;
        public static long EndOfDay(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Kind).ToLong();
        public static long BeginningOfDay(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Kind).ToLong();
        public static long EndOfHour(this long longDate) => longDate.BeginningOfHour().AddHours(1);
        public static long BeginningOfMonth(this long longDate) => GenerateDate(longDate.GetYear(), longDate.GetMonth(), 1);
        public static long EndOfMonth(this long longDate) => longDate.BeginningOfMonth().AddMonths(1).AddSeconds(-1);
        public static long BeginningOfYear(this long longDate) => GenerateDate(longDate.GetYear(), 1, 1);
        public static int DayInMonth(this long date) => DateTime.DaysInMonth(date.GetYear(), date.GetMonth());
        public static int DaysInMonth(int year, int month) => DateTime.DaysInMonth(year, month);
        private static long ToLong(this DateTime dt, bool withSeconds = true) => Int64.Parse(withSeconds ? dt.ToString("yyyyMMddHHmmss") : dt.ToString("yyyyMMddHHmm"));
    }
}
