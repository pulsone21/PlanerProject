using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TimeSystem;

namespace EmployeeSystem
{
    [Serializable]
    public class Employee
    {
        public enum State { Canidate, Employed, Vacation, Sickleave }
        [SerializeField] public EmployeeName Name;
        [SerializeField] private Skill _adaptability, _determination, _driving, _law, _leadership, _mechanic, _negotiation, _planing;
        [SerializeField] private Happines _happines;
        [SerializeField] private Loyalty _loyalty;
        [SerializeField] private Stress _stress;
        [SerializeField] private State _state;
        private List<Skill> _Skills;
        [SerializeField] public readonly TimeStamp Birthday;
        [SerializeField] private JobRole _job;
        public Skill Adaptability => _adaptability;
        public Skill Determination => _determination;
        public Skill Driving => _driving;
        public Happines Happines => _happines;
        public Skill Law => _law;
        public Skill Leadership => _leadership;
        public Loyalty Loyalty => _loyalty;
        public Skill Mechanic => _mechanic;
        public Skill Negotiation => _negotiation;
        public Skill Planing => _planing;
        public Stress Stress => _stress;
        public State EmplyoeeState => _state;
        public int Age => Birthday.DifferenceToNowInYears();
        public JobRole Job => _job;
        public List<Skill> Skills => _Skills;

        private Action OnStateChange;
        private List<Skill> skills;

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
            _Skills = InstantiateSkillList();
            Birthday = birthday;
        }
        public Employee(List<Skill> skills, Happines happy, Loyalty loyal, Stress stress, EmployeeName name, TimeStamp birthday)
        {
            _adaptability = skills[(int)SkillName.Adaptability];
            _determination = skills[(int)SkillName.Determination];
            _leadership = skills[(int)SkillName.Leadership];
            _driving = skills[(int)SkillName.Driving];
            _law = skills[(int)SkillName.Law];
            _mechanic = skills[(int)SkillName.Mechanic];
            _negotiation = skills[(int)SkillName.Negotiation];
            _planing = skills[(int)SkillName.Planing];
            _happines = happy;
            _loyalty = loyal;
            _stress = stress;
            Name = name;
            Birthday = birthday;
            _Skills = skills;
        }

        private List<Skill> InstantiateSkillList()
        {
            List<Skill> skills = new List<Skill>();
            skills[(int)SkillName.Adaptability] = _adaptability;
            skills[(int)SkillName.Determination] = _determination;
            skills[(int)SkillName.Leadership] = _leadership;
            skills[(int)SkillName.Driving] = _driving;
            skills[(int)SkillName.Law] = _law;
            skills[(int)SkillName.Mechanic] = _mechanic;
            skills[(int)SkillName.Negotiation] = _negotiation;
            skills[(int)SkillName.Planing] = _planing;
            return skills;
        }

        public void RegisterOnStateChange(Action action) => OnStateChange += action;
        public void UnregisterOnStateChange(Action action) => OnStateChange -= action;

        public void SetState(State state)
        {
            if (state != State.Canidate && _job == null)
            {
                Debug.LogError("Cant assign " + state + " because employee has no job, asign job first with AsignJob()");
                return;
            }
            _state = state;
            OnStateChange?.Invoke();
        }

        public void AsignJob(JobRole job)
        {
            _job = job;
            _state = State.Employed;
        }

        public void ChangeSkillSet<T>(T set, int amount) where T : EmployeeStat
        {
            foreach (EmployeeStat employeeStat in _Skills)
            {
                if (employeeStat.GetType() == set.GetType())
                {
                    employeeStat.ChangeValue(amount);
                    return;
                }
            }
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
