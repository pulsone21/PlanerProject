using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EmployeeSystem;
using System;
using Planer;

namespace UISystem
{
    public class ScheduleEntryController : MonoBehaviour
    {
        private bool initialized = false;
        [SerializeField] private List<Image> driverAttributes = new List<Image>();
        [SerializeField] private TMPro.TextMeshProUGUI driverName;
        [SerializeField] private List<TextSetter> dayContainer = new List<TextSetter>();
        private Employee driver;



        public void SetUp(Employee Driver)
        {
            if (initialized) return;
            initialized = true;
            driver = Driver;
            driverName.text = Driver.Name.ToString();
            ActivateAttributes();
            LoadDriverSchedule();
            return;
        }

        private void LoadDriverSchedule()
        {
            throw new NotImplementedException();
        }

        private void ActivateAttributes()
        {
            throw new NotImplementedException();
        }

        public void SetWeek(string[] week)
        {
            for (int i = 0; i < week.Length; i++)
            {
                dayContainer[i].SetText(week[i]);
            }
        }
    }
}
