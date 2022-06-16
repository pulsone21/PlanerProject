using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Leadership : SkillSet
    {
        public Leadership(int skill) : base(skill)
        {
            _type = SkillType.Mental;
        }
    }
}
