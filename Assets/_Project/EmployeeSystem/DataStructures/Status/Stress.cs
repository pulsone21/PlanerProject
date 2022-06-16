using System.Collections;
using System.Collections.Generic;
using TimeSystem;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Stress : EmployeeStatus
    {
        private const int NIGHT_BASE_RATE = 3;
        public Stress(int skill) : base(skill)
        {
            _rateOfChange = BaseRate();
            TimeManager.Instance.RegisterForTimeUpdate(ContinualChange, TimeManager.SubscriptionType.Hour);
        }

        protected override int BaseRate() => 1;
        protected override void ContinualChange(TimeStamp timeStamp)
        {
            // every hour this function is called
            //? IDEA Increase Stress during the day, Decrease Stress during night. Decreasing should have an base Rate which is influenced by the rateOfChange
            int computedRate = _rateOfChange;
            if (timeStamp.IsNight) computedRate = NIGHT_BASE_RATE + _rateOfChange;
            ChangeValue(computedRate);
        }

    }
}
