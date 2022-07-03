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
            string firstName = "";
            switch (sex)
            {
                case Sex.Female:
                    firstName = FirstnameFemale[Random.Range(0, FirstnameFemale.Length)];
                    break;
                case Sex.Male:
                    firstName = FirstnamesMale[Random.Range(0, FirstnamesMale.Length)];
                    break;
            }
            string lastName = Lastnames[Random.Range(0, Lastnames.Length)];
            return new Employee.EmployeeName(firstName, lastName);
        }
    }
}
