using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Mechanic : SkillSet
    {
        public Mechanic(int skill) : base(skill)
        {
            _type = SkillType.Proficiency;
        }
    }
}
