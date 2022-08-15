using System.Collections;
using System.Collections.Generic;
using TimeSystem;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Loyalty : EmployeeStatus
    {
        public Loyalty(int skill) : base(skill)
        {
            _rateOfChange = BaseRate();
            TimeManager.Instance.RegisterForTimeUpdate(ContinualChange, TimeManager.SubscriptionType.Month);
        }

        protected override int BaseRate() => 1;

        protected override void ContinualChange()
        {
            // every month this function is called
            ChangeValue(_rateOfChange);
            SetBaseRate();
        }
    }
}
