using System;

namespace EmployeeSystem
{
    [Serializable]
    public abstract class EmployeeAttribute : EmployeeStat
    {
        public int RateOfChange { get; protected set; }
        protected long _NextChangeTimeStamp;
        private int BaseRate;
        public EmployeeAttribute(int value, string name, int baseRate) : base(value, name)
        {
            BaseRate = baseRate;
        }
        internal virtual void ContinualChange() => ChangeValue(RateOfChange);
        public void InfluenceRateOfChange(int change) => RateOfChange += change;
        public void SetRateOfChange(int newRateOfChange) => RateOfChange = newRateOfChange;
    }
}
