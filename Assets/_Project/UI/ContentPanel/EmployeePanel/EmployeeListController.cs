using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;
using TMPro;
namespace UISystem
{
    public class EmployeeListController : ListController
    {
        public enum sortMode { job, age, name }

        [SerializeField] private List<Employee> employees;
        [SerializeField] private EmployeeDetailController employeeDetailController;

        protected override void GenerateList()
        {
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
                GameObject go = Instantiate(defaultItemPrefab);
                go.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "You currently don't have any employees hired.";
                go.transform.SetParent(ListItemContainer);
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
