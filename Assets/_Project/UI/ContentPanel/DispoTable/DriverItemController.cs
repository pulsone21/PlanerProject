using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;
using TMPro;
using UnityEngine.UI;

namespace UISystem
{
    public class DriverItemController : ListItemController<Driver>
    {
        private DispoDetailPage _detailPage;
        private Driver _driver;
        protected void Start() => _detailPage = DispoDetailPage.Instance;
        [SerializeField] private TextMeshProUGUI employeeName;
        public override void Initialize(Driver driver)
        {
            if (Initialized) return;
            Initialized = false;
            gameObject.SetActive(true);
            employeeName.text = driver.Name.ToString();
            _driver = driver;
        }
        public override void SetContent() => _detailPage.SetDetails(_driver);
    }
}
