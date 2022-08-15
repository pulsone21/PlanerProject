using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeSystem;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Happines : EmployeeStatus
    {

        public Happines(int skill) : base(skill)
        {
            _rateOfChange = BaseRate();
            TimeManager.Instance.RegisterForTimeUpdate(ContinualChange, TimeManager.SubscriptionType.Day);
        }
        protected override int BaseRate() => 24;
        // every day this function is triggered
        protected override void ContinualChange() => ChangeValue(_rateOfChange);

    }
}
