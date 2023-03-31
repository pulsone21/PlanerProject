using System;
using System.Collections.Generic;
using CompanySystem;
using UnityEngine;
namespace EmployeeSystem
{
    [Serializable]
    public class EmployeeManager
    {
        public List<Driver> Drivers { get; protected set; }
        public List<Accountant> Accountants { get; protected set; }
        public List<Dispatcher> Dispatchers { get; protected set; }
        public List<Canidate> Canidates { get; protected set; }
        [SerializeField] private List<EmployeeController> ecS;

        public EmployeeManager()
        {
            Drivers = new List<Driver>();
            Accountants = new List<Accountant>();
            Dispatchers = new List<Dispatcher>();
            ecS = new List<EmployeeController>();
        }
        public void AddEmployeeToList<T>(T employee) where T : Employee
        {
            CreateEmployeeController(employee);
            if (employee.GetType() == typeof(Driver)) AddEmployeeToList(employee as Driver);
            if (employee.GetType() == typeof(Accountant)) AddEmployeeToList(employee as Accountant);
            if (employee.GetType() == typeof(Dispatcher)) AddEmployeeToList(employee as Dispatcher);
        }
        private void CreateEmployeeController<T>(T employee) where T : Employee
        {
            GameObject go = new GameObject(employee.ToString());
            go.transform.SetParent(PlayerCompanyController.Instance.transform);
            EmployeeController ec = go.AddComponent<EmployeeController>();
            ec.Initialize(employee);
            ecS.Add(ec);
        }
        private void AddEmployeeToList(Driver employee) => Drivers.Add(employee);
        private void AddEmployeeToList(Accountant employee) => Accountants.Add(employee);
        private void AddEmployeeToList(Dispatcher employee) => Dispatchers.Add(employee);

        public bool RemoveEmployeeFromList<T>(T employee) where T : Employee
        {
            bool removed = RemoveDeleteEmployeeController(employee);
            if (removed)
            {
                if (employee.GetType() == typeof(Driver)) return RemoveEmployeeFromList(employee as Driver);
                if (employee.GetType() == typeof(Accountant)) return RemoveEmployeeFromList(employee as Accountant);
                if (employee.GetType() == typeof(Dispatcher)) return RemoveEmployeeFromList(employee as Dispatcher);
            }
            return false;
        }
        private bool RemoveDeleteEmployeeController<T>(T emplyoee) where T : Employee
        {
            foreach (EmployeeController ec in ecS)
            {
                if (ec.Employee == emplyoee)
                {
                    ec.Destroy();
                    return true;
                }
            }
            return false;
        }
        private bool RemoveEmployeeFromList(Driver employee) => Drivers.Remove(employee);
        private bool RemoveEmployeeFromList(Accountant employee) => Accountants.Remove(employee);
        private bool RemoveEmployeeFromList(Dispatcher employee) => Dispatchers.Remove(employee);
        public List<Employee> GetAllEmployees()
        {
            List<Employee> allEmployees = new List<Employee>();
            allEmployees.AddRange(Drivers);
            allEmployees.AddRange(Accountants);
            allEmployees.AddRange(Dispatchers);
            return allEmployees;
        }
        public Driver GetFreeDriver()
        {
            foreach (Driver driver in Drivers)
            {
                if (!driver.OnRoute) return driver;
            }
            return default;
        }
    }
}
