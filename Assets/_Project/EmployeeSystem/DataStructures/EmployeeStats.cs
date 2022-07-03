using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public abstract class EmployeeStats
    {
        [SerializeField] private int _value;
        protected EmployeeStats(int value)
        {
            _value = value;
        }

        public int Value => _value;

        public virtual void ChangeValue(int amount)
        {
            _value += amount;
            if (_value > 100) _value = 100;
            if (_value < 0) _value = 0;
        }
    }
}
