using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Planing : SkillSet
    {
        public Planing(int skill) : base(skill)
        {
            _type = SkillType.Proficiency;
        }
    }
}
