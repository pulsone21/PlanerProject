using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TimeSystem;

namespace UISystem
{
    public class DispotableController : MonoBehaviour
    {
        [SerializeField] private Transform Content;
        [SerializeField] private ScheduleEntryController Prefab;
        [SerializeField] private List<ScheduleEntryController> ActiveRows = new List<ScheduleEntryController>();
        private string[] week = new string[7];

        private void Start()
        {
            SetCurrWeek();
        }

        [Button("Add new Row")]
        public void AddNewRow()
        {
            ScheduleEntryController newRow = Instantiate(Prefab);
            newRow.transform.SetParent(Content);
            newRow.transform.localScale = Vector3.one;
            ActiveRows.Add(newRow);
            newRow.SetWeek(week);
        }

        [Button("Set prev Week")]
        public void SetPrevWeek()
        {
            TimeStamp ts = TimeManager.Instance.CurrentTimeStamp;
            int currIndex = (int)ts.WeekDay();
            TimeStamp firstDayOfWeek = ts.AddDays(-(7 + currIndex));
            week = CalculateWeek(firstDayOfWeek);
            PushWeekToTable(week);
        }

        [Button("Set Curr Week")]
        public void SetCurrWeek()
        {
            TimeStamp ts = TimeManager.Instance.CurrentTimeStamp;
            Debug.Log(ts.ToString());
            int currIndex = (int)ts.WeekDay();
            Debug.Log("Current Index = " + currIndex);
            TimeStamp firstDayOfWeek = ts.AddDays(-currIndex);
            Debug.Log(firstDayOfWeek.ToString());
            week = CalculateWeek(firstDayOfWeek);
            PushWeekToTable(week);
        }

        [Button("Set next Week")]
        public void SetNextWeek()
        {
            TimeStamp ts = TimeManager.Instance.CurrentTimeStamp;
            int currIndex = (int)ts.WeekDay();
            TimeStamp firstDayOfWeek = ts.AddDays(7 - currIndex);
            week = CalculateWeek(firstDayOfWeek);
            PushWeekToTable(week);
        }

        private void PushWeekToTable(string[] week)
        {
            foreach (ScheduleEntryController row in ActiveRows)
            {
                row.SetWeek(week);
            }
        }

        private string[] CalculateWeek(TimeStamp firstDayOfWeek)
        {
            Debug.Log(firstDayOfWeek.ToString());
            string[] week = new string[7];
            week[0] = $"Sunday - {firstDayOfWeek.Day}.{firstDayOfWeek.Month}.{firstDayOfWeek.Year}";
            week[1] = "Monday - ";
            week[2] = "Tuseday - ";
            week[3] = "Wednesday - ";
            week[4] = "Thursday - ";
            week[5] = "Friday - ";
            week[6] = "Saturday - ";

            for (int i = 1; i < week.Length; i++)
            {
                TimeStamp currTs = firstDayOfWeek.AddDays(i);
                week[i] += $"{currTs.Day:00}.{currTs.Month:00}.{currTs.Year:0000}";
            }

            return week;
        }

    }
}
