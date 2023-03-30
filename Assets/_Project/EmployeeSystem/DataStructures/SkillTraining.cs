using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace EmployeeSystem
{
    [CreateAssetMenu(fileName = "Training", menuName = "SO/EmployeeSystem/Training", order = 0)]
    public class SkillTraining : ScriptableObject
    {
#if UNITY_EDITOR
#pragma warning disable 
        private static List<string> SkillNames = new List<string>() { "Adaptability", "Determination", "Driving", "Law", "Leadership", "Mechanic", "Negotiation", "Planing" };
#endif
        public string TitelID;
        [ValueDropdown("SkillNames")] public string SkillName;
        [Tooltip("Duration of the training in days")] public float Duration;
        public float SkillIncrease;
    }
}