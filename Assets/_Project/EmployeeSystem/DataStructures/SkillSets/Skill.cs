using UnityEngine;

namespace EmployeeSystem
{
    public enum SkillType { Mental, Proficiency }
    public enum SkillName { Adaptability, Determination, Leadership, Driving, Law, Mechanic, Negotiation, Planing }
    [System.Serializable]
    public class Skill : EmployeeStat
    {
        public readonly SkillType Type;
        public readonly SkillName Name;
        public Skill(int value, SkillName name) : base(value)
        {
            Name = name;
            Type = SkillType.Mental;
            if ((int)name > 2) Type = SkillType.Proficiency;
        }
    }
}