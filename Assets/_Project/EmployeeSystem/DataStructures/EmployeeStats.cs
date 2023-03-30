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
        protected EmployeeStat(int value, string name)
        {
            Name = name;
            Value = value;
        }
        public virtual void ChangeValue(int amount)
        {
            Value += amount;
            if (Value > 100) Value = 100;
            if (Value < 0) Value = 0;
        }
    }
}
