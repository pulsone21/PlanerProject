using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeSystem;
using System;

namespace EmployeeSystem
{
    public abstract class EmployeeStatus : EmployeeStat
    {
        protected int _rateOfChange;
        public int RateOfChange => _rateOfChange;
        protected long _NextChangeTimeStamp;
        public EmployeeStatus(int skill) : base(skill)
        {
        }

        protected abstract void ContinualChange(TimeStamp timeStamp);
        protected void SetBaseRate() => _rateOfChange = BaseRate();
        protected abstract int BaseRate();
        public void InfluenceRateOfChange(int change) => _rateOfChange += change;
        public void SetRangeOfChange(int newRateOfChange) => _rateOfChange = newRateOfChange;
    }
}
