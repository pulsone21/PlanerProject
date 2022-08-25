using System;
using UnityEngine;

namespace TimeSystem
{
    public enum Season { Autum, Spring, Winter, Summer }
    public enum Month { January, February, March, April, May, June, July, August, September, October, November, December }
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance;
        private int minute;
        private int hour;
        private int day;
        private int month;
        private int year;
        private Season currentSeason;
        private bool fastForwardActive = false;
        private long fastForwardMinutes = long.MaxValue;

        [SerializeField, Tooltip("Speeding Up the time")] private int speedModifier;
        private const int DAY_IN_MINUTES = 1440;
        [SerializeField, Tooltip("How many real life minutes are one ingame day")] private float m_realLifeMinToIngameDay;
        private float m_timer => (m_realLifeMinToIngameDay / DAY_IN_MINUTES) * 60; //multiply by 60 to convert it to secounds
        private float timer;
        public static readonly int[] DayInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public enum SubscriptionType { Minute, Hour, Day, Month, Year, Season }

        private Action OnMinuteChange;
        private Action OnHourChange;
        private Action OnDayChange;
        private Action OnMonthChange;
        private Action OnYearChange;
        private Action OnSeasonChange;
        private Action<TimeStamp> OnAfterElapseTime;

        /// <summary>
        /// Register a Action for an specified subscription type
        /// </summary>
        /// <param name="action">The action you want to trigger</param>
        /// <param name="subType">On which time change the action should be triggered</param>
        public void RegisterForTimeUpdate(Action action, SubscriptionType subType)
        {
            Debug.Log($"Register for {subType}");
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
        public void UnregisterForTimeUpdate(Action<TimeStamp> action) => OnAfterElapseTime -= action;
        public TimeStamp CurrentTimeStamp => new TimeStamp(minute, hour, day, month, year, currentSeason);
        public static TimeStamp Now => Instance.CurrentTimeStamp;
        public int SpeedModifier => speedModifier;
        public void ChangeSpeedModifier(int newSpeed) => speedModifier = newSpeed;
        public void ResetSpeedModifier() => speedModifier = 1;
        public void PauseTime() => speedModifier = 0;

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

        private void Awake()
        {
            if (Instance == null) { Instance = this; } else { Destroy(this); }
            InitTime();
        }

        private void InitTime()
        {
            minute = TimeStamp.INITIAL_TIMESTAMP.Minute;
            hour = TimeStamp.INITIAL_TIMESTAMP.Hour;
            day = TimeStamp.INITIAL_TIMESTAMP.Day;
            month = TimeStamp.INITIAL_TIMESTAMP.Month;
            year = TimeStamp.INITIAL_TIMESTAMP.Year;
            timer = m_timer;
            OnMonthChange += UpdateSeason;
            speedModifier = speedModifier == 0 ? 1 : speedModifier;
            UpdateSeason();
        }

        // Update is called once per frame
        void Update()
        {
            ElapseTime();
        }

        private void ElapseTime()
        {
            timer -= (Time.fixedDeltaTime * speedModifier);
            if (timer <= 0)
            {
                timer = m_timer;
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

        public void SetYearDirty(int Year)
        {
            year = Year;
        }

        public void FastForwardToTimestamp(TimeStamp timeStamp)
        {
            fastForwardMinutes = timeStamp.InMinutes();
            fastForwardActive = true;
            ChangeSpeedModifier(1000);
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
    }
}
