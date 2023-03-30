using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeSystem;
using System;

namespace EmployeeSystem
{
    public class EmployeeAttribute : EmployeeStat
    {
        public int RateOfChange { get; protected set; }
        protected long _NextChangeTimeStamp;
        private int BaseRate;
        public EmployeeAttribute(int value, string name, int baseRate) : base(value, name)
        {
            BaseRate = baseRate;
        }
        protected virtual void ContinualChange() => ChangeValue(RateOfChange);
        public void InfluenceRateOfChange(int change) => RateOfChange += change;
        public void SetRateOfChange(int newRateOfChange) => RateOfChange = newRateOfChange;
    }
}
