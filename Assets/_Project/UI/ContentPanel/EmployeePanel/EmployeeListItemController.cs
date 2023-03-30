using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine.UI;
using System;
using System.Linq;
namespace UISystem
{
    public class EmployeeListItemController : ListItemController<Employee>
    {

        [SerializeField] private TextMeshProUGUI employeeName;
        [SerializeField] private TextMeshProUGUI employeeAge;
        [SerializeField] private TextMeshProUGUI employeeJob;
        [SerializeField] private SVGImage icon;
        private EmployeeViewer EmployeeViewer;
        protected virtual void Start() => EmployeeViewer = EmployeeViewer.Instance;
        public override void Initialize(Employee employee)
        {
            if (Initialized) return;
            Initialized = true;
            item = employee;
            employeeName.text = item.Name.ToString();
            employeeAge.text = item.Age.ToString();
            JobRole jobRole = JobRoleManager.GetJobRoleByName(employee.GetType().ToString().Split(".").Last());
            icon.sprite = jobRole.Icon;
            employeeJob.text = jobRole.Name;
            gameObject.SetActive(true);
        }

        public override void SetContent() => EmployeeViewer.SetContent(item);
    }
}
