using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class EmployeeManager
    {
        [SerializeField] private List<Employee> currentCanidates = new List<Employee>();

        public void AddEmployeeToList(Employee employee)
        {
            if (employee.EmplyoeeState == Employee.State.Canidate)
            {
                currentCanidates.Add(employee);
            }
        }
        public void AddEmployeeToList(List<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                AddEmployeeToList(employee);
            }
        }
        public List<Employee> GetAllEmployees() => currentCanidates;
    }
}
