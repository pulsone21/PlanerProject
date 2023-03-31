using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [RequireComponent(typeof(EmployeeLearnController), typeof(EmployeeStatsController))]
    public class EmployeeController : MonoBehaviour
    {
        private bool initialized = false;
        [SerializeField] private Employee _employee;
        public Employee Employee => _employee;
        public bool Initialized => initialized;
        private EmployeeLearnController _employeeLearnController;
        private EmployeeStatsController _employeeStatsController;
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
            _employeeLearnController = GetComponent<EmployeeLearnController>();
            _employeeStatsController = GetComponent<EmployeeStatsController>();
        }

        public void Destroy()
        {
            if (!initialized) return;
            Destroy(gameObject);
        }
    }
}
