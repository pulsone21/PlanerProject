using System.Collections;
using System.Collections.Generic;
using TimeSystem;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Accountant : Employee
    {
        public Accountant(Canidate canidate) : base(canidate.Skills, canidate.Name, canidate.Birthday)
        {

        }
    }
}
