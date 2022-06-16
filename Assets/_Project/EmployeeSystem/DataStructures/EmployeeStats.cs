using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    public abstract class EmployeeStats
    {
        [SerializeField] private int _value;
        protected EmployeeStats(int value)
        {
            _value = value;
        }

        public int Value => _value;

        public void ChangeValue(int amount)
        {
            _value += amount;
            if (_value > 100) _value = 100;
            if (_value < 0) _value = 0;
        }
    }
}
