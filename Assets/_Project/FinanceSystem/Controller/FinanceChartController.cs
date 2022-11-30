using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanySystem;
using ChartSystem;
using TimeSystem;

namespace FinanceSystem
{
    public class FinanceChartController : MonoBehaviour
    {
        private FinanceManager manager;

        private void Start() => manager = PlayerCompanyController.Instance.company.FinanceManager;
        public void SetMonthlyOverallChart()
        {
            List<int> chart = new List<int>();
            foreach (KeyValuePair<Month, Dictionary<CostType, float>> entry in manager.FinanceAccounting.ExpensesPerMonth)
            {
                float sum = 0f;
                foreach (KeyValuePair<CostType, float> item in entry.Value)
                {
                    sum += item.Value;
                }
                chart.Add(Mathf.FloorToInt(sum));
            }
            ChartController.GenerateChart(chart, "Cost", "Month");
        }

        public void SetYearlyOverallChart()
        {
            List<int> chart = new List<int>();
            foreach (KeyValuePair<int, Dictionary<CostType, float>> entry in manager.FinanceAccounting.ExpensesPerYear)
            {
                float sum = 0f;
                foreach (KeyValuePair<CostType, float> item in entry.Value)
                {
                    sum += item.Value;
                }
                chart.Add(Mathf.FloorToInt(sum));
            }
            ChartController.GenerateChart(chart, "Cost", "Year");
        }

        public void SetMonthlyCategoryChart()
        {
            List<int> chart = new List<int>();
            foreach (KeyValuePair<TimeSystem.Month, Dictionary<CostType, float>> entry in manager.FinanceAccounting.ExpensesPerMonth)
            {
                float sum = 0f;
                foreach (KeyValuePair<CostType, float> item in entry.Value)
                {
                    sum += item.Value;
                }
                chart.Add(Mathf.FloorToInt(sum));
            }
            ChartController.GenerateChart(chart, "Cost", "Category");
        }

        public void SetYearlyCategoryChart()
        {

        }
    }
}
