using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Adaptability : SkillSet
    {
        public Adaptability(int skill) : base(skill)
        {
            _type = SkillType.Mental;
        }
    }
}
