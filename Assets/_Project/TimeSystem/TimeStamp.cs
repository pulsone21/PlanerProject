using System;
using System.Globalization;
using UnityEngine;
namespace TimeSystem
{
    [Serializable]
    public class TimeStamp
    {
        [SerializeField] private int minute, hour, day, month, year;
        public int Minute => minute;
        public int Hour => hour;
        public int Day => day;
        public int Month => month;
        public int Year => year;
        public Season Season { get; protected set; }
        private const int MIN_PER_YEAR = 525600;
        private const int _19Year_IN_MIN = YEAR_IN_MIN * 19;
        public const int YEAR_IN_MIN = 525600;
        public const int MONTH_IN_MIN = 43800;
        public const int DAY_IN_MIN = 1440;
        public const int HOUR_IN_MIN = 60;
        public bool IsNight => Hour > 6 && Hour < 18;

        public TimeStamp(int _minute, int _hour, int _day, int _month, int _year, Season _season)
        {
            minute = _minute;
            hour = _hour;
            day = _day;
            month = _month;
            year = _year;
            Season = _season;
        }

        public TimeStamp(int _minute, int _hour, int _day, int _month, int _year) => new TimeStamp(_minute, _hour, _day, _month, _year, TimeManager.GetSeason(_month));

        public TimeStamp(DateTime dateTime)
        {
            minute = dateTime.Minute;
            hour = dateTime.Hour;
            day = dateTime.Day;
            month = dateTime.Month;
            year = dateTime.Year;
            Season = TimeManager.GetSeason(month);
        }

        public TimeStamp(long minutes) => GetTimeStampFromTotalMinutes(minutes);


        /// <summary>
        /// Returns the Current Timestamp in Minutes
        /// </summary>
        /// <returns></returns>
        public long InMinutes() => GetTimeStampInMinutes(this);
        //public string GetDayOfWeekAsString() => TimeManager.Instance.MyEpoch.AddMinutes(this.InMinutes()).DayOfWeek.ToString();
        //public DateTime GetDateTime() => TimeManager.Instance.MyEpoch.AddMinutes(this.InMinutes());

        /// <summary>
        /// Returns Timestamp as a string
        /// </summary>
        /// <returns>Format hh:MM / dd.mm.yyyy</returns>
        public override string ToString() => $"{hour:00}:{minute:00} / {day:00}.{month:00}.{year:0000}";

        /// <summary>
        /// Returns Timestamp as a string
        /// </summary>
        /// <returns>string format dd.mm.yyyy</returns>
        public string ToDateString() => $"{day:00}.{month:00}.{year:0000}";

        /// <summary>
        /// Returns a TimeStamp in Minutes
        /// </summary>
        /// <param name="_timeStamp">The Timestamp you want in minutes</param>
        /// <returns>TimeStamp in Minutes</returns>
        public static int GetTimeStampInMinutes(TimeStamp _timeStamp)
        {
            int timeStampInMinutes = _timeStamp.Year * YEAR_IN_MIN;
            timeStampInMinutes += _timeStamp.Month * MONTH_IN_MIN;
            timeStampInMinutes += _timeStamp.Day * DAY_IN_MIN;
            timeStampInMinutes += _timeStamp.Hour * HOUR_IN_MIN;
            timeStampInMinutes += _timeStamp.Minute;
            return timeStampInMinutes;
        }

        public int DifferenceToNowInYears()
        {
            long minutes = InMinutes() - TimeManager.Instance.CurrentTimeStamp.InMinutes();
            return Mathf.Abs(Mathf.FloorToInt(minutes / MIN_PER_YEAR));
        }

        public static TimeStamp GetRndBirthday(int maxAge = 65, long minAge = 18)
        {
            int rndMinute = Mathf.FloorToInt(UnityEngine.Random.Range(minAge * MIN_PER_YEAR, (maxAge + 1) * MIN_PER_YEAR));
            long normalizedMinutes = TimeManager.Instance.CurrentTimeStamp.InMinutes() - TimeManager.Instance.INITIAL_TIMESTAMP.InMinutes();
            long birthDay = normalizedMinutes - (long)rndMinute;
            return GetTimeStampFromTotalMinutes(birthDay); ;
        }

        public TimeStamp AddDays(int amount)
        {
            DateTime dtDateTime = new DateTime(year, month, day, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeStamp ts = new TimeStamp(dtDateTime.AddDays(amount));
            long normalizedMinutes = ts.InMinutes() - TimeManager.Instance.INITIAL_TIMESTAMP.InMinutes();
            return GetTimeStampFromTotalMinutes(normalizedMinutes);
        }

        /// <summary>
        /// Creates an Timestamp based of the given Minutes
        /// </summary>
        /// <param name="totalMinutes">The amount of minutes, since game epoch</param>
        /// <returns>Returns the timestamp based of the ingame epoch</returns>
        public static TimeStamp GetTimeStampFromTotalMinutes(long totalMinutes)
        {
            TimeStamp initTS = TimeManager.Instance.INITIAL_TIMESTAMP;
            DateTime dtDateTime = new DateTime(initTS.Year, initTS.Month, initTS.Day, 0, 0, 0, 0, DateTimeKind.Utc);
            return new TimeStamp(dtDateTime.AddMinutes(totalMinutes));
        }

        public static DateTime GetDateTimeFromTimeStamp(TimeStamp timeStamp) => new DateTime(timeStamp.Year, timeStamp.Month, timeStamp.Day, 0, 0, 0, 0, DateTimeKind.Utc);
        public DayOfWeek WeekDay()
        {
            DateTime dtDateTime = new DateTime(year, month, day, 0, 0, 0, 0, DateTimeKind.Utc);
            return dtDateTime.DayOfWeek;
        }

        public int GetWeekOfYear()
        {
            DateTime dtDateTime = new DateTime(Year, Month, Day, 0, 0, 0, 0, DateTimeKind.Utc);
            CultureInfo info = CultureInfo.CurrentCulture;
            System.Globalization.Calendar cal = info.Calendar;
            return cal.GetWeekOfYear(dtDateTime, info.DateTimeFormat.CalendarWeekRule, info.DateTimeFormat.FirstDayOfWeek);
        }

        public static TimeStamp operator +(TimeStamp a, TimeStamp b)
        {
            long minutes = a.InMinutes() + b.InMinutes();
            return new TimeStamp(minutes);
        }

        public static TimeStamp operator -(TimeStamp a, TimeStamp b)
        {
            long minutes = a.InMinutes() - b.InMinutes();
            return new TimeStamp(minutes);
        }
    }
}