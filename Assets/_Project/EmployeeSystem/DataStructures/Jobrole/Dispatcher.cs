using System.Collections;
using System.Collections.Generic;
using TimeSystem;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Dispatcher : Employee
    {
        public Dispatcher(Canidate canidate) : base(canidate.Skills, canidate.Name, canidate.Birthday)
        {

        }
    }
}
