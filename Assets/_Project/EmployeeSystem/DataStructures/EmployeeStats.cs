using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class EmployeeStat
    {
        public readonly string Name;
        public int Value { get; protected set; }
        public int MaxValue { get; protected set; }
        protected EmployeeStat(int value, string name, int maxValue = 100)
        {
            Name = name;
            Value = value;
            MaxValue = maxValue;
        }
        public virtual void ChangeValue(int amount)
        {
            Value += amount;
            if (Value > MaxValue) Value = MaxValue;
            if (Value < 0) Value = 0;
        }
    }
}
