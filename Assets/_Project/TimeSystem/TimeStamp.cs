using System;
using UnityEngine;
namespace TimeSystem
{
    public class TimeStamp
    {
        public int Minute { get; protected set; }
        public int Hour { get; protected set; }
        public int Day { get; protected set; }
        public int Month { get; protected set; }
        public int Year { get; protected set; }
        public Season Season { get; protected set; }

        public static TimeStamp INITIAL_TIMESTAMP = new TimeStamp(0, 0, 1, 1, 1950);
        private const int MIN_PER_YEAR = 525600;
        private const int _19Year_IN_MIN = YEAR_IN_MIN * 19;
        public const int YEAR_IN_MIN = 525600;
        public const int MONTH_IN_MIN = 43800;
        public const int DAY_IN_MIN = 1440;
        public const int HOUR_IN_MIN = 60;

        public bool IsNight => Hour > 6 && Hour < 18;

        public TimeStamp(int _minute, int _hour, int _day, int _month, int _year, Season _season)
        {
            Minute = _minute;
            Hour = _hour;
            Day = _day;
            Month = _month;
            Year = _year;
            Season = _season;
        }

        public TimeStamp(int _minute, int _hour, int _day, int _month, int _year)
        {
            Minute = _minute;
            Hour = _hour;
            Day = _day;
            Month = _month;
            Year = _year;
            Season = TimeManager.GetSeason(_month);
        }

        /// <summary>
        /// Returns the Current Timestamp in Minutes
        /// </summary>
        /// <returns></returns>
        public long InMinutes() => GetTimeStampInMinutes(this);

        public override string ToString() => $"{Hour:00}:{Minute:00} / {Day:00}.{Month:00}.{Year:0000}";

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
            Debug.Log($"Minutes: {minutes} - In Years: {Mathf.FloorToInt(minutes / MIN_PER_YEAR)}");
            return Mathf.Abs(Mathf.FloorToInt(minutes / MIN_PER_YEAR));
        }

        public static TimeStamp GetRndBirthday(int maxAge = 65, long minAge = 18)
        {
            int rndMinute = Mathf.FloorToInt(UnityEngine.Random.Range(minAge * MIN_PER_YEAR, (maxAge + 1) * MIN_PER_YEAR));
            long normalizedMinutes = TimeManager.Instance.CurrentTimeStamp.InMinutes() - TimeStamp.INITIAL_TIMESTAMP.InMinutes();
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
            //Creating my own epoch
            DateTime dtDateTime = new DateTime(INITIAL_TIMESTAMP.Year, INITIAL_TIMESTAMP.Month, INITIAL_TIMESTAMP.Day, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMinutes(totalMinutes);
            return new TimeStamp(dtDateTime.Minute, dtDateTime.Hour, dtDateTime.Day, dtDateTime.Month, dtDateTime.Year);
        }
    }
}