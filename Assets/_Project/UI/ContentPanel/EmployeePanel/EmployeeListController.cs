using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;
using TMPro;
using CompanySystem;
namespace UISystem
{
    public class EmployeeListController : ListController
    {
        public enum sortMode { job, age, name }
        [SerializeField] private List<Employee> employees;
        protected override void GenerateList()
        {
            employees = PlayerCompanyController.Instance.Company.EmployeeManager.GetAllEmployees();
            if (employees.Count > 1)
            {
                foreach (Employee employee in employees)
                {
                    GameObject go = Instantiate(ListItemPrefab, Vector3.zero, Quaternion.identity);
                    go.transform.SetParent(ListItemContainer);
                    go.GetComponent<EmployeeListItemController>().Initialize(employee);
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
