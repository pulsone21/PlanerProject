using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public abstract class SkillSet : EmployeeStats
    {
        public enum SkillType { Mental, Proficiency }
        [SerializeField] protected SkillType _type;

        protected SkillSet(int value) : base(value)
        {
        }
        public SkillType Type => _type;


    }
}