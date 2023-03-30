using UnityEngine;

namespace EmployeeSystem
{
    public enum SkillType { Mental, Proficiency }
    [System.Serializable]
    public class Skill : EmployeeStat
    {
        public readonly SkillType Type;
        public Skill(int value, string name, SkillType type) : base(value, name)
        {
            Type = type;
        }
    }
}