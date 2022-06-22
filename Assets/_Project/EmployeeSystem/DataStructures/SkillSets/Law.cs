using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Law : SkillSet
    {
        public Law(int skill) : base(skill)
        {
            _type = SkillType.Proficiency;
        }
    }
}
