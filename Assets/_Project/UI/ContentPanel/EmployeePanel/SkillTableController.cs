using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;
using Utilities;

namespace UISystem
{
    public class SkillTableController : MonoBehaviour
    {
        [SerializeField] private GameObject InfoPairPrefab;
        private Employee _employee;
        public void SetEmployee(Employee employee)
        {
            transform.ClearAllChildren();
            _employee = employee;
            foreach (EmployeeStat stats in _employee.Skills)
            {
                SpawnPrefab(stats);
            }
        }
        private void SpawnPrefab(EmployeeStat EmployeeStats)
        {
            GameObject go = Instantiate(InfoPairPrefab, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(transform);
            InfoPairController ipc = go.GetComponent<InfoPairController>();
            ipc.SetText(InfoPairController.TextType.label, EmployeeStats.Name);
            ipc.SetText(InfoPairController.TextType.value, EmployeeStats.Value + " / 100");
        }
        private void OnDisable() => transform.ClearAllChildren();
    }
}
