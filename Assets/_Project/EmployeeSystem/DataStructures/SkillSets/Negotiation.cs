using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Negotiation : SkillSet
    {
        public Negotiation(int skill) : base(skill)
        {
            _type = SkillType.Proficiency;
        }
    }
}
