using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EmployeeSystem;
using DG.Tweening;

namespace UISystem
{
    public class EmployeeDetailController : MonoBehaviour
    {
        public static EmployeeDetailController Instance;
        [SerializeField] private GameObject defaultText;
        [SerializeField] private GameObject detailContainer;
        [SerializeField] private SkillTableController skillTable;
        [SerializeField] private TextMeshProUGUI employeeNameText;
        [SerializeField] private TextMeshProUGUI employeeJobText;
        [SerializeField] private TextMeshProUGUI employeeBirthDayText;
        private Employee employee;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            defaultText.SetActive(true);
            detailContainer.SetActive(false);
        }

        public void SetEmployee(Employee Employee)
        {
            employee = Employee;
            skillTable.SetEmployee(employee);
            UpdateUI(employee);
        }

        private void UpdateUI(Employee employee)
        {
            employeeNameText.text = employee.Name.ToString();
            employeeBirthDayText.text = employee.Birthday.ToString().Split("/")[1] + " (" + employee.Age.ToString() + ")";
            employeeJobText.text = employee.Job.Name;
            ShowDetails(true);
        }

        private void ShowDetails(bool state)
        {
            defaultText.SetActive(!state);
            detailContainer.SetActive(state);
        }

        private void OnDisable()
        {
            employeeNameText.text = "";
            employeeBirthDayText.text = "";
            employeeJobText.text = "";
        }
    }
}
