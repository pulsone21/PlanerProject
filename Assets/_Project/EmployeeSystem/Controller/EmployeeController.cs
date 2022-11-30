using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    public class EmployeeController : MonoBehaviour
    {
        private bool initialized = false;
        private Employee _employee;
        public Employee Employee => _employee;
        public bool Initialized => initialized;
        private void Awake()
        {
            if (!initialized) gameObject.SetActive(false);
        }
        public void Initialize(Employee employee)
        {
            if (initialized) return;
            initialized = true;
            gameObject.SetActive(true);
            _employee = employee;
        }
    }
}
