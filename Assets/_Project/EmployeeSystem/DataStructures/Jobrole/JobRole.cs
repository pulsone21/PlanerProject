using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    public abstract class JobRole : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private List<Skill> neededSkills = new List<Skill>();

        public string Name => name;
        public Sprite Icon => icon;
        public List<Skill> NeededSkills => neededSkills;
    }
}
