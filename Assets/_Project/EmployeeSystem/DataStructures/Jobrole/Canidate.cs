using System.Collections;
using System.Collections.Generic;
using TimeSystem;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Canidate : Employee
    {
        public Canidate(Skills skills, EmployeeName name, TimeStamp birthday) : base(skills, name, birthday) { }
        public Canidate(Skill adaptability, Skill determination, Skill driving, Skill law, Skill leadership, Skill mechanic, Skill negotiation, Skill planing, EmployeeName name, TimeStamp birthday) : base(adaptability, determination, driving, law, leadership, mechanic, negotiation, planing, name, birthday) { }
        public Driver HireDriver() => new Driver(this);
        public Accountant HireAccountant() => new Accountant(this);
        public Dispatcher HireDispatcher() => new Dispatcher(this);
    }
}
