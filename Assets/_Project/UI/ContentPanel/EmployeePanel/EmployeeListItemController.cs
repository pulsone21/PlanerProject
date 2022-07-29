using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine.UI;

namespace UISystem
{
    public class EmployeeListItemController : ListItemController<Employee>
    {
        [SerializeField] private TextMeshProUGUI employeeName;
        [SerializeField] private TextMeshProUGUI employeeAge;
        [SerializeField] private TextMeshProUGUI employeeJob;
        [SerializeField] private SVGImage icon;
        private EmployeeViewer EmployeeViewer;
        private void Start() => EmployeeViewer = EmployeeViewer.Instance;
        public override void Initialize(Employee employee)
        {
            if (Initialized) return;
            Initialized = true;
            item = employee;
            employeeName.text = item.Name.ToString();
            icon.sprite = item.Job.Icon;
            employeeAge.text = item.Age.ToString();
            employeeJob.text = item.Job.Name;
            gameObject.SetActive(true);
        }
        public override void SetContent() => EmployeeViewer.SetContent(item);
    }
}
