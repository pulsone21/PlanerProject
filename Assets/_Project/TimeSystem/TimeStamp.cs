using System;
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


        /// <summary>
        /// Returns the Current Timestamp in Minutes
        /// </summary>
        /// <returns></returns>
        public long InMinutes() => GetTimeStampInMinutes(this);
        //public string GetDayOfWeekAsString() => TimeManager.Instance.MyEpoch.AddMinutes(this.InMinutes()).DayOfWeek.ToString();
        //public DateTime GetDateTime() => TimeManager.Instance.MyEpoch.AddMinutes(this.InMinutes());

        public override string ToString() => $"{hour:00}:{minute:00} / {day:00}.{month:00}.{year:0000}";

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
            return TimeStamp.GetTimeStampFromTotalMinutes(birthDay); ;
        }

        /// <summary>
        /// Creates an Timestamp based of the given Minutes
        /// </summary>
        /// <param name="totalMinutes">The amount of minutes, since game epoch</param>
        /// <returns>Returns the timestamp based of the ingame epoch</returns>
        public static TimeStamp GetTimeStampFromTotalMinutes(long totalMinutes)
        {
            TimeManager tm = TimeManager.Instance;
            DateTime dtDateTime = new DateTime(tm.INITIAL_TIMESTAMP.Year, tm.INITIAL_TIMESTAMP.Month, tm.INITIAL_TIMESTAMP.Day, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMinutes(totalMinutes);
            return new TimeStamp(dtDateTime.Minute, dtDateTime.Hour, dtDateTime.Day, dtDateTime.Month, dtDateTime.Year);
        }

        public static DateTime GetDateTimeFromTimeStamp(TimeStamp timeStamp)
        {
            DateTime dtDateTime = new DateTime(timeStamp.Year, timeStamp.Month, timeStamp.Day, 0, 0, 0, 0, DateTimeKind.Utc);
            return dtDateTime;
        }
    }
}