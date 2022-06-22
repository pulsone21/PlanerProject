using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Driving : SkillSet
    {
        public Driving(int skill) : base(skill)
        {
            _type = SkillType.Proficiency;
        }
    }
}
