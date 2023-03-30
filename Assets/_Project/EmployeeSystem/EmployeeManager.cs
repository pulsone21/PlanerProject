using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class EmployeeManager
    {
        [SerializeField] private List<Driver> _drivers = new List<Driver>();
        [SerializeField] private List<Accountant> _accountants = new List<Accountant>();
        [SerializeField] private List<Dispatcher> _dispatchers = new List<Dispatcher>();
        [SerializeField] private List<Canidate> _canidates = new List<Canidate>();
        public void AddEmployeeToList(Canidate employee) => _canidates.Add(employee);
        public void AddEmployeeToList(Driver employee) => _drivers.Add(employee);
        public void AddEmployeeToList(Accountant employee) => _accountants.Add(employee);
        public void AddEmployeeToList(Dispatcher employee) => _dispatchers.Add(employee);
        public bool RemoveEmployeeFromList(Canidate employee) => _canidates.Remove(employee);
        public bool RemoveEmployeeFromList(Driver employee) => _drivers.Remove(employee);
        public bool RemoveEmployeeFromList(Accountant employee) => _accountants.Remove(employee);
        public bool RemoveEmployeeFromList(Dispatcher employee) => _dispatchers.Remove(employee);

        public List<Driver> Drivers => _drivers;
        public List<Canidate> Canidates => _canidates;
        public List<Accountant> Accountants => _accountants;
        public List<Dispatcher> Dispatchers => _dispatchers;

        public List<Employee> GetAllEmployees()
        {
            List<Employee> allEmployees = new List<Employee>();
            allEmployees.AddRange(_drivers);
            allEmployees.AddRange(_canidates);
            allEmployees.AddRange(_accountants);
            allEmployees.AddRange(_dispatchers);
            return allEmployees;
        }

        public Driver GetFreeDriver()
        {
            foreach (Driver driver in _drivers)
            {
                if (!driver.OnRoute) return driver;
            }
            return default;
        }
    }
}
