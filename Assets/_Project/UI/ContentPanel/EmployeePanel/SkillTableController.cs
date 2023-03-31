using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;
using Utilities;

namespace UISystem
{
    public class SkillTableController : MonoBehaviour
    {
        [SerializeField] private SkillItemController SkillItem;
        private Employee _employee;
        public void SetEmployee(Employee employee)
        {
            transform.ClearAllChildren();
            _employee = employee;
            foreach (Skill skill in _employee.Skills)
            {
                SpawnPrefab(skill);
            }
        }
        private void SpawnPrefab(Skill skill)
        {
            SkillItemController go = Instantiate(SkillItem);
            go.transform.SetParent(transform);
            go.SetContent(skill);
        }
        private void OnDisable() => transform.ClearAllChildren();
    }
}
