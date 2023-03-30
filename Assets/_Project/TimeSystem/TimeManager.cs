using System;
using UnityEngine;
using SLSystem;
namespace TimeSystem
{
    public enum Season { Autum, Spring, Winter, Summer }
    public enum Month { January = 1, February, March, April, May, June, July, August, September, October, November, December }
    public class TimeManager : MonoBehaviour, IPersistenceData
    {
        public static TimeManager Instance;
        [SerializeField] private TimeStamp initialTimestamp;
        public TimeStamp INITIAL_TIMESTAMP => initialTimestamp;
        private string _className;
        private int minute, hour, day, month, year;
        private Season currentSeason;
        private bool fastForwardActive = false;
        private long fastForwardMinutes = long.MaxValue;
        private const int DAY_IN_MINUTES = 1440;
        [SerializeField, Tooltip("How many real life minutes are one ingame day")] private float m_realLifeMinToIngameDay;
        private float timer;
        public static readonly int[] DayInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public enum SubscriptionType { Minute, Hour, Day, Month, Year, Season }
        private DateTime myEpoch;
        public DateTime MyEpoch => myEpoch;
        private Action OnMinuteChange, OnHourChange, OnDayChange, OnMonthChange, OnYearChange, OnSeasonChange;
        private Action<TimeStamp> OnAfterElapseTime;
        public TimeStamp CurrentTimeStamp => new TimeStamp(minute, hour, day, month, year, currentSeason);
        public int SpeedModifier => Mathf.FloorToInt(Time.timeScale);
        public GameObject This => gameObject;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            InitTime();
            _className = GetType().Name;
        }
        private void FixedUpdate() => ElapseTime();

        /// <summary>
        /// Register a Action for an specified subscription type
        /// </summary>
        /// <param name="action">The action you want to trigger</param>
        /// <param name="subType">On which time change the action should be triggered</param>
        public void RegisterForTimeUpdate(Action action, SubscriptionType subType)
        {
            switch (subType)
            {
                case SubscriptionType.Minute:
                    OnMinuteChange += action;
                    break;
                case SubscriptionType.Hour:
                    OnHourChange += action;
                    break;
                case SubscriptionType.Day:
                    OnDayChange += action;
                    break;
                case SubscriptionType.Month:
                    OnMonthChange += action;
                    break;
                case SubscriptionType.Year:
                    OnYearChange += action;
                    break;
                case SubscriptionType.Season:
                    OnSeasonChange += action;
                    break;
                default: throw new NotImplementedException();
            }
        }
        public void UnregisterForTimeUpdate(Action<TimeStamp> action) => OnAfterElapseTime -= action;
        public void ChangeSpeedModifier(int newSpeed)
        {
            Debug.Log("Setting timeScale to " + newSpeed);
            Time.timeScale = newSpeed;
            Debug.Log("Current TimeScale is " + Time.timeScale);
        }
        public void ResetSpeedModifier() => Time.timeScale = 1;
        public void PauseTime() => Time.timeScale = 0;
        public void RegisterForTimeUpdate(Action<TimeStamp> action) => OnAfterElapseTime += action;
        /// <summary>
        /// Unregister a Action for an specified subscription type
        /// </summary>
        /// <param name="action">The action which is registered</param>
        /// <param name="subType">On which time change the action is regisitered to</param>
        public void UnregisterForTimeUpdate(Action action, SubscriptionType subType)
        {
            switch (subType)
            {
                case SubscriptionType.Minute:
                    OnMinuteChange -= action;
                    break;
                case SubscriptionType.Hour:
                    OnHourChange -= action;
                    break;
                case SubscriptionType.Day:
                    OnDayChange -= action;
                    break;
                case SubscriptionType.Month:
                    OnMonthChange -= action;
                    break;
                case SubscriptionType.Year:
                    OnYearChange -= action;
                    break;
                case SubscriptionType.Season:
                    OnSeasonChange -= action;
                    break;
                default: throw new NotImplementedException();
            }
        }
        public static Season GetSeason(int month)
        {
            if (month >= 3 && month < 6)
            {
                return Season.Spring;
            }
            else if (month >= 6 && month < 9)
            {
                return Season.Summer;
            }
            else if (month >= 9 && month < 12)
            {
                return Season.Autum;
            }
            else
            {
                return Season.Winter;
            }
        }
        private void InitTime()
        {
            minute = initialTimestamp.Minute;
            hour = initialTimestamp.Hour;
            day = initialTimestamp.Day;
            month = initialTimestamp.Month;
            year = initialTimestamp.Year;
            myEpoch = new DateTime(initialTimestamp.Year, initialTimestamp.Month, initialTimestamp.Day, 0, 0, 0, 0, DateTimeKind.Utc);
            OnMonthChange += UpdateSeason;
            UpdateSeason();
        }
        private void InitTime(PersitenTime startingTime)
        {
            minute = startingTime.minute;
            hour = startingTime.hour;
            day = startingTime.day;
            month = startingTime.month;
            year = startingTime.year;
            m_realLifeMinToIngameDay = startingTime.realLifeMinToIngameDay;
            myEpoch = new DateTime(initialTimestamp.Year, initialTimestamp.Month, initialTimestamp.Day, 0, 0, 0, 0, DateTimeKind.Utc);
            OnMonthChange += UpdateSeason;
            UpdateSeason();
        }
        private void ElapseTime()
        {
            timer -= Time.fixedDeltaTime;
            if (timer <= 0)
            {
                timer = 60; // return it to 60 seconds since we increment every minute
                minute++;//increment min
                OnMinuteChange?.Invoke();
                if (minute == 60)  //increment hour
                {
                    hour++;
                    OnHourChange?.Invoke();
                    minute = 0;
                    if (hour == 24)  //increment day
                    {
                        day++;
                        OnDayChange?.Invoke();
                        hour = 0;
                        if (day > DayInMonth[month - 1])  //increment month
                        {
                            day = 1;
                            month++;
                            OnMonthChange?.Invoke();
                            if (month == 13)  //increment year
                            {
                                year++;
                                OnYearChange?.Invoke();
                                month = 1;
                            }
                        }
                    }
                }
                OnAfterElapseTime?.Invoke(CurrentTimeStamp);
            }
        }
        private void UpdateSeason()
        {
            if (month >= 3 && month < 6)
            {
                currentSeason = Season.Spring;
            }
            else if (month >= 6 && month < 9)
            {
                currentSeason = Season.Summer;
            }
            else if (month >= 9 && month < 12)
            {
                currentSeason = Season.Autum;
            }
            else
            {
                currentSeason = Season.Winter;
            }
            OnSeasonChange?.Invoke();
        }
        public void SetYearDirty(int Year) => year = Year;
        public void FastForwardToTimestamp(TimeStamp timeStamp)
        {
            fastForwardMinutes = timeStamp.InMinutes();
            fastForwardActive = true;
            ChangeSpeedModifier(100);
            RegisterForTimeUpdate(CheckForReachedTimeStamp);
        }
        private void CheckForReachedTimeStamp(TimeStamp currTimeStamp)
        {
            if (!fastForwardActive) return;
            if (fastForwardMinutes <= currTimeStamp.InMinutes())
            {
                ResetSpeedModifier();
                fastForwardActive = false;
                fastForwardMinutes = long.MaxValue;
                UnregisterForTimeUpdate(CheckForReachedTimeStamp);
            }
        }
        public void Load(GameData gameData)
        {
            if (gameData.Data.ContainsKey(_className))
            {
                PersitenTime persitenTime = JsonUtility.FromJson<PersitenTime>(gameData.Data[_className]);
                InitTime(persitenTime);
            }
        }
        public void Save(ref GameData gameData)
        {
            PersitenTime persitenTime = new PersitenTime(minute, hour, day, month, year, SpeedModifier, m_realLifeMinToIngameDay);
            gameData.Data[_className] = persitenTime.ToString();
        }
        private class PersitenTime
        {
            public int minute, hour, day, month, year, speedModifier;
            public float realLifeMinToIngameDay;
            public PersitenTime(int minute, int hour, int day, int month, int year, int speedModifier, float realLifeMinToIngameDay)
            {
                this.minute = minute;
                this.hour = hour;
                this.day = day;
                this.month = month;
                this.year = year;
                this.speedModifier = speedModifier;
                this.realLifeMinToIngameDay = realLifeMinToIngameDay;
            }
            public override string ToString() => JsonUtility.ToJson(this);
        }
    }
}
