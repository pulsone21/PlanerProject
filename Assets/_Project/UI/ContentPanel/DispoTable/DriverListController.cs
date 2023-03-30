using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;
using CompanySystem;

namespace UISystem
{
    public class DriverListController : ListController
    {

        public enum sortMode { job, age, name }
        [SerializeField] private List<Driver> _driver;
        protected override void GenerateList()
        {
            _driver = PlayerCompanyController.Instance.Company.EmployeeManager.Drivers;
            if (_driver.Count > 1)
            {
                foreach (Driver driver in _driver)
                {
                    GameObject go = Instantiate(ListItemPrefab, Vector3.zero, Quaternion.identity);
                    go.transform.SetParent(ListItemContainer);
                    go.GetComponent<DriverItemController>().Initialize(driver);
                }
            }
            else
            {
                GenerateDefaultText();
            }
            ListItemContainer.gameObject.SetActive(true);
        }
        public void FilterBy(FilterMode Modus)
        {
            throw new System.NotImplementedException("TODO figure out how to sort over the list and then display it again");
            //TODO figure out how to sort over the list and then display it again
        }

        public void SearchFor(string text)
        {
            throw new System.NotImplementedException("TODO Built out an basic search engine");
            //TODO Built out an basic search engine
        }
    }
}
