using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine.UI;

namespace UISystem
{
    public class EmployeeListItemController : ListItemController
    {
        private Employee _employee;
        [SerializeField] private TextMeshProUGUI employeeName;
        [SerializeField] private TextMeshProUGUI employeeAge;
        [SerializeField] private TextMeshProUGUI employeeJob;
        [SerializeField] private SVGImage icon;
        private EmployeeDetailController EmployeeDetailController;

        private void Start() => EmployeeDetailController = EmployeeDetailController.Instance;
        protected override void OnDestroy() => button.onClick.RemoveListener(SetEmployee);
        protected override void OnEnable() => button.onClick.AddListener(SetEmployee);
        private void SetEmployee() => EmployeeDetailController.SetEmployee(_employee);
        public void Initialize(Employee employee)
        {
            if (Initialized) return;
            Initialized = true;
            _employee = employee;
            employeeName.text = _employee.Name.ToString();
            icon.sprite = _employee.Job.Icon;
            employeeAge.text = _employee.Age.ToString();
            employeeJob.text = _employee.Job.Name;
            gameObject.SetActive(true);
        }
    }
}
