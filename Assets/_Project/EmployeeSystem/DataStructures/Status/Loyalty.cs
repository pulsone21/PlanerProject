using System.Collections;
using System.Collections.Generic;
using TimeSystem;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Loyalty : EmployeeAttribute
    {
        private int _baseRate;
        public Loyalty() : base(100, "Loyalty", 1)
        {
            _baseRate = 1;
#if !UNITY_EDITOR
            TimeManager.Instance.RegisterForTimeUpdate(ContinualChange, TimeManager.SubscriptionType.Month);
#endif
        }
        protected override void ContinualChange()
        {
            // every month this function is called
            ChangeValue(RateOfChange);
            RateOfChange = _baseRate;
        }
    }
}
