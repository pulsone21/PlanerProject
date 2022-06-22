using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TimeSystem;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Employee
    {
        public enum State { Canidate, Employed, Vacation, Sickleave }
        [SerializeField] public EmployeeName Name;
        [SerializeField] private Adaptability _adaptability;
        [SerializeField] private Determination _determination;
        [SerializeField] private Driving _driving;
        [SerializeField] private Happines _happines;
        [SerializeField] private Law _law;
        [SerializeField] private Leadership _leadership;
        [SerializeField] private Loyalty _loyalty;
        [SerializeField] private Mechanic _mechanic;
        [SerializeField] private Negotiation _negotiation;
        [SerializeField] private Planing _planing;
        [SerializeField] private Stress _stress;
        [SerializeField] private State _state;
        private List<EmployeeStats> _employeeStats;
        [SerializeField] public readonly TimeSystem.TimeStamp Birthday;

        public Adaptability Adaptability => _adaptability;
        public Determination Determination => _determination;
        public Driving Driving => _driving;
        public Happines Happines => _happines;
        public Law Law => _law;
        public Leadership Leadership => _leadership;
        public Loyalty Loyalty => _loyalty;
        public Mechanic Mechanic => _mechanic;
        public Negotiation Negotiation => _negotiation;
        public Planing Planing => _planing;
        public Stress Stress => _stress;
        public Employee.State EmplyoeeState => _state;
        public int Age => Birthday.DifferenceToNowInYears();
        private Action OnStateChange;

        public Employee(Adaptability adaptability,
                        Determination determination,
                        Driving driving,
                        Happines happines,
                        Law law,
                        Leadership leadership,
                        Loyalty loyalty,
                        Mechanic mechanic,
                        Negotiation negotiation,
                        Planing planing,
                        Stress stress,
                        EmployeeName name,
                        TimeStamp birthday)
        {
            _adaptability = adaptability;
            _determination = determination;
            _driving = driving;
            _happines = happines;
            _law = law;
            _leadership = leadership;
            _loyalty = loyalty;
            _mechanic = mechanic;
            _negotiation = negotiation;
            _planing = planing;
            _stress = stress;
            _state = State.Canidate;
            Name = name;
            _employeeStats = InstantiateSkillList();
            Birthday = birthday;
        }

        private List<EmployeeStats> InstantiateSkillList()
        {
            List<EmployeeStats> skills = new List<EmployeeStats>();
            skills.Add(_adaptability);
            skills.Add(_determination);
            skills.Add(_driving);
            skills.Add(_happines);
            skills.Add(_law);
            skills.Add(_leadership);
            skills.Add(_loyalty);
            skills.Add(_mechanic);
            skills.Add(_negotiation);
            skills.Add(_planing);
            skills.Add(_stress);
            return skills;
        }

        public void RegisterOnStateChange(Action action) => OnStateChange += action;
        public void UnregisterOnStateChange(Action action) => OnStateChange -= action;

        public void SetState(State state)
        {
            _state = state;
            OnStateChange?.Invoke();
        }

        public void ChangeSkillSet<T>(T set, int amount) where T : EmployeeStats
        {
            foreach (EmployeeStats employeeStat in _employeeStats)
            {
                if (employeeStat.GetType() == set.GetType())
                {
                    employeeStat.ChangeValue(amount);
                    return;
                }
            }
        }

        [System.Serializable]
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
