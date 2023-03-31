using System.Collections;
using System.Collections.Generic;
using TimeSystem;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Stress : EmployeeAttribute
    {
        private const int NIGHT_BASE_RATE = 3;
        public Stress() : base(0, "Stress", 1)
        {
        }
        internal override void ContinualChange()
        {
            // every hour this function is called
            //? IDEA Increase Stress during the day, Decrease Stress during night. Decreasing should have an base Rate which is influenced by the rateOfChange
            int computedRate = RateOfChange;
            if (TimeManager.Instance.CurrentTimeStamp.IsNight) computedRate = NIGHT_BASE_RATE + RateOfChange;
            ChangeValue(computedRate);
        }

    }
}
