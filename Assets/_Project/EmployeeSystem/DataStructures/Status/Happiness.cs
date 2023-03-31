using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeSystem;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Happines : EmployeeAttribute
    {
        public Happines() : base(100, "Happines", 24)
        { }
    }
}
