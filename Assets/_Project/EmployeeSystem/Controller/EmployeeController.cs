using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    public class EmployeeController : MonoBehaviour
    {
        private bool Initialized = false;
        private Employee _employee;
        public Employee Employee => _employee;
        public void Initialize(Employee employee)
        {
            if (Initialized) return;
            Initialized = true;
            _employee = employee;
        }
    }
}
