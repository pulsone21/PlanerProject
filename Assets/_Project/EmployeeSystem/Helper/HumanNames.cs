using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    public class HumanNames
    {
        public enum Sex { Female, Male }
        public string[] FirstnamesMale;
        public string[] FirstnameFemale;
        public string[] Lastnames;

        public HumanNames(string[] firstnamesMale, string[] firstnameFemale, string[] lastnames)
        {
            FirstnamesMale = firstnamesMale;
            FirstnameFemale = firstnameFemale;
            Lastnames = lastnames;
        }

        public Employee.EmployeeName GetRndEmplyoeeName(Sex sex)
        {
            string firstname = FirstnameFemale[Random.Range(0, FirstnameFemale.Length)];
            if (sex == Sex.Male) firstname = FirstnamesMale[Random.Range(0, FirstnamesMale.Length)];
            string lastName = Lastnames[Random.Range(0, Lastnames.Length)];
            return new Employee.EmployeeName(firstname, lastName);
        }
    }
}
