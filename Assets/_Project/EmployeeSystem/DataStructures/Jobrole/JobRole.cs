using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace EmployeeSystem
{
    [CreateAssetMenu(fileName = "Job", menuName = "SO/EmployeeSystem/Job", order = 0)]
    public class JobRole : ScriptableObject
    {
        [SerializeField] private new string name;
        [SerializeField] private Sprite icon;
        [SerializeField] private List<Skill> neededSkills = new List<Skill>();
        public string Name => name;
        public Sprite Icon => icon;
        public List<Skill> NeededSkills => neededSkills;
    }
}
