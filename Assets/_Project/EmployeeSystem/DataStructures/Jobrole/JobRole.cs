using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    public abstract class JobRole : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private List<Planing> neededSkills = new List<Planing>();

        public string Name => name;
        public Sprite Icon => icon;
        public List<Planing> NeededSkills => neededSkills;
    }
}
