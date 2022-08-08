using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [CreateAssetMenu(fileName = "Training", menuName = "SO/EmployeeSystem/Training", order = 0)]
    public class SkillTraining : ScriptableObject
    {
        public string TitelID;
        public SkillName Skill;
        [Tooltip("Duration of the training in days")] public float Duration;
        public float SkillIncrease;
    }
}
