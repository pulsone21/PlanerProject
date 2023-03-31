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
        public readonly EmployeeName Name;
        public Skills Skills => _Skills;
        [SerializeField] private Skills _Skills;
        public readonly TimeStamp Birthday;
        public int Age => Birthday.DifferenceToNowInYears();
        [SerializeField] private Happines _Happines;
        public Happines Happines => _Happines;
        [SerializeField] private Loyalty _Loyalty;
        public Loyalty Loyalty => _Loyalty;
        [SerializeField] private Stress _Stress;
        public Stress Stress => _Stress;

        public Employee(Skill adaptability,
                        Skill determination,
                        Skill driving,
                        Skill law,
                        Skill leadership,
                        Skill mechanic,
                        Skill negotiation,
                        Skill planing,
                        EmployeeName name,
                        TimeStamp birthday)
        {
            _Skills = new Skills(adaptability, determination, driving, law, leadership, mechanic, negotiation, planing);
            Name = name;
            Birthday = birthday;
            _Happines = new Happines();
            _Loyalty = new Loyalty();
            _Stress = new Stress();
        }
        public Employee(Skills skills, EmployeeName name, TimeStamp birthday)
        {
            _Skills = skills;
            Name = name;
            Birthday = birthday;
            _Happines = new Happines();
            _Loyalty = new Loyalty();
            _Stress = new Stress();
        }

        public override string ToString()
        {
            return Name.ToString();
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
