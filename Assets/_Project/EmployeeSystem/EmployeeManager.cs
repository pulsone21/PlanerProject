using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class EmployeeManager
    {
        private List<Employee> currentCanidates = new List<Employee>();

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
        public List<Employee> GetEmployee(int amount)
        {
            List<Employee> outList = new List<Employee>();
            int i = amount;
            if (i > currentCanidates.Count) i = currentCanidates.Count;
            while (i > 0)
            {
                outList.Add(currentCanidates[i - 1]);
                i--;
            }
            return outList;
        }
        public List<Employee> GetAllEmployees() => currentCanidates;
    }
}
