using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Utilities;

namespace TimeSystem
{
    public class Calendar : MonoBehaviour
    {
        public class Day
        {
            private int Number;
            private Color BackgroundColor;
            private GameObject gameObject;
            public Day(int number, Color color, GameObject go)
            {
                Number = number;
                BackgroundColor = color;
                gameObject = go;
                UpdateColor(BackgroundColor);
                go.GetComponentInChildren<TextMeshProUGUI>().text = number.ToString();
            }

            public void UpdateColor(Color col)
            {
                BackgroundColor = col;
                gameObject.GetComponent<Image>().color = col;
            }
            public void UpdateDay(int newNumber)
            {
                Number = newNumber;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = newNumber.ToString();
            }
        }
        [SerializeField] private int CurrentMonth = 0; //January is 0
        [SerializeField] private int Year;
        [SerializeField] private TextMeshProUGUI HeaderText;
        [SerializeField] private List<Color> BackgroundColors = new List<Color>();
        /// <summary>
        /// All the days in the month. After we make our first calendar we store these days in this list so we do not have to recreate them every time.
        /// </summary>
        private List<Day> days = new List<Day>();

        /// <summary>
        /// Setup in editor since there will always be six weeks. 
        /// Try to figure out why it must be six weeks even though at most there are only 31 days in a month
        /// </summary>
        public Transform[] weeks;

        /// <summary>
        /// this currDate is the date our Calendar is currently on. The year and month are based on the calendar, 
        /// while the day itself is almost always just 1
        /// If you have some option to select a day in the calendar, you would want the change this objects day value to the last selected day
        /// </summary>
        public DateTime currDate;

        private void Awake()
        {
            for (int w = 0; w < 6; w++)
            {
                for (int d = 0; d < 7; d++)
                {
                    days.Add(new Day(w * d, BackgroundColors[0], weeks[w].GetChild(d).gameObject));
                }
            }
        }

        private void OnEnable() => UpdateCalendar(TimeManager.Instance.CurrentTimeStamp.Year, TimeManager.Instance.CurrentTimeStamp.Month);

        /// <summary>
        /// Anytime the Calendar is changed we call this to make sure we have the right days for the right month/year
        /// </summary>
        void UpdateCalendar(int year, int month)
        {
            Debug.Log($"Updating Calendar with Year: {year} and Month: {month}");
            Year = year;
            CurrentMonth = month;
            TimeStamp firstDay = new TimeStamp(0, 0, 1, month, year, TimeManager.GetSeason(month));
            Debug.Log($"firstDat {firstDay.Month}-{firstDay.Year}");
            DateTime temp = TimeStamp.GetDateTimeFromTimeStamp(firstDay);
            currDate = temp;
            UpdateHeaderText();
            int startDay = GetMonthStartDay(year, month);
            int endDay = GetTotalNumberOfDays(year, month);

            Debug.Log(startDay);
            Debug.Log(endDay);

            for (int i = 0; i < 42; i++)
            {
                if (i < startDay) // Show Previos Month
                {
                    days[i].UpdateColor(BackgroundColors[0]);
                    int tempYear = year;
                    int tmpMonth = month - 1;
                    if (month == 0)
                    {
                        tmpMonth = 12;
                        tempYear = tempYear - 1;
                    }
                    int daysPrevMonth = GetTotalNumberOfDays(tempYear, tmpMonth);
                    days[i].UpdateDay((daysPrevMonth - (startDay - i)) + 1);
                }
                else if (i - startDay >= endDay) // Show next Month
                {
                    days[i].UpdateColor(BackgroundColors[0]);
                    days[i].UpdateDay((i - endDay) + 1);
                }
                else
                {
                    days[i].UpdateColor(BackgroundColors[1]);
                    days[i].UpdateDay((i - startDay) + 1);
                }
            }


            //This just checks if today is on our calendar. If so, we highlight it in green
            if (TimeManager.Instance.CurrentTimeStamp.Year == year && TimeManager.Instance.CurrentTimeStamp.Month == month)
            {
                days[(TimeManager.Instance.CurrentTimeStamp.Day - 1) + startDay].UpdateColor(BackgroundColors[2]);
            }

        }

        /// <summary>
        /// This returns which day of the week the month is starting on
        /// </summary>
        int GetMonthStartDay(int year, int month)
        {
            DateTime temp = new DateTime(year, month, 1);
            //DayOfWeek Sunday == 0, Saturday == 6 etc.
            Debug.Log(temp.DayOfWeek);
            return (int)temp.DayOfWeek;
        }

        /// <summary>
        /// Gets the number of days in the given month.
        /// </summary>
        int GetTotalNumberOfDays(int year, int month) => DateTime.DaysInMonth(year, month);

        /// <summary>
        /// This either adds or subtracts one month from our currDate.
        /// The arrows will use this function to switch to past or future months
        /// </summary>
        public void SwitchMonth(int direction)
        {
            Debug.Log("Changing Month in Direction: " + direction);
            currDate = currDate.AddMonths(direction);
            Debug.Log($"{currDate} -> Month: {currDate.Month}");
            UpdateCalendar(currDate.Year, currDate.Month);
        }
        public void UpdateHeaderText() => HeaderText.text = $"{(Month)CurrentMonth} {Year}";

    }
}
