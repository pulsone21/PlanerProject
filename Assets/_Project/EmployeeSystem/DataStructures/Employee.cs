using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TimeSystem;

namespace EmployeeSystem
{
    [Serializable]
    public abstract class Employee
    {
        [SerializeField] public EmployeeName Name;
        public Skills Skills { get; protected set; }
        [SerializeField] public readonly TimeStamp Birthday;
        public int Age => Birthday.DifferenceToNowInYears();

        public Employee(Skill adaptability,
                        Skill determination,
                        Skill driving,
                        Happines happines,
                        Skill law,
                        Skill leadership,
                        Loyalty loyalty,
                        Skill mechanic,
                        Skill negotiation,
                        Skill planing,
                        Stress stress,
                        EmployeeName name,
                        TimeStamp birthday)
        {
            Skills = new Skills(adaptability, determination, driving, happines, law, leadership, loyalty, mechanic, negotiation, planing, stress);
            Name = name;
            Birthday = birthday;
        }
        public Employee(Skills skills, EmployeeName name, TimeStamp birthday)
        {
            Skills = skills;
            Name = name;
            Birthday = birthday;
        }

        [Serializable]
        public class EmployeeName
        {
            public string Firstname;
            public string Lastname;

            public EmployeeName(string firstname, string lastname)
            {
                Firstname = firstname;
                Lastname = lastname;
            }

            public override string ToString() => $"{Firstname} {Lastname}";
        }
    }
}
