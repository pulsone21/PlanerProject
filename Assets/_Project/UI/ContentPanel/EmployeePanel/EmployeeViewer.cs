using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EmployeeSystem;
using DG.Tweening;
using System.Linq;
namespace UISystem
{
    public class EmployeeViewer : DetailViewer<EmployeeViewer, Employee>
    {
        [SerializeField] private SkillTableController skillTable;
        [SerializeField] private TextMeshProUGUI employeeNameText;
        [SerializeField] private TextMeshProUGUI employeeJobText;
        [SerializeField] private TextMeshProUGUI employeeBirthDayText;
        [SerializeField] private TextMeshProUGUI employedSinceText;

        private void UpdateUI(Employee employee)
        {
            employeeNameText.text = employee.Name.ToString();
            employeeBirthDayText.text = employee.Birthday.ToString().Split("/")[1] + " (" + employee.Age.ToString() + ")";
            employedSinceText.text = "TODO!! this data isnt implemented yet"; //TODO
            employeeJobText.text = JobRoleManager.GetJobRoleByName(employee.GetType().ToString().Split(".").Last()).Name;
            ShowDetails(true);
        }
        private void OnDisable()
        {
            employeeNameText.text = "";
            employeeBirthDayText.text = "";
            employeeJobText.text = "";
        }

        public override void SetContent(Employee Item)
        {
            currentContent = Item;
            skillTable.SetEmployee(currentContent);
            UpdateUI(currentContent);
        }
    }
}
