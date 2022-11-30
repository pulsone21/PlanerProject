using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeSystem;
using Sirenix.OdinInspector;

namespace FinanceSystem
{
    [System.Serializable]
    public class FinanceAccounting
    {
        [SerializeField] private Dictionary<Month, Dictionary<CostType, float>> expensesPerMonth;
        [SerializeField] private Dictionary<int, Dictionary<CostType, float>> expensesPerYear;
        [SerializeField] private Dictionary<Month, Dictionary<CostType, float>> incomePerMonth;
        [SerializeField] private Dictionary<int, Dictionary<CostType, float>> incomePerYear;
        public Dictionary<Month, Dictionary<CostType, float>> ExpensesPerMonth => expensesPerMonth;
        public Dictionary<int, Dictionary<CostType, float>> ExpensesPerYear => expensesPerYear;
        public Dictionary<Month, Dictionary<CostType, float>> IncomePerMonth => incomePerMonth;
        public Dictionary<int, Dictionary<CostType, float>> IncomePerYear => incomePerYear;
        private int currentMonth;
        public FinanceAccounting()
        {
            TimeStamp now = TimeManager.Instance.CurrentTimeStamp;
            currentMonth = now.Month;
            expensesPerMonth = new Dictionary<Month, Dictionary<CostType, float>>(){
                {Month.January,InitDictionary()},
                {Month.February,InitDictionary()},
                {Month.March,InitDictionary()},
                {Month.April,InitDictionary()},
                {Month.May,InitDictionary()},
                {Month.June,InitDictionary()},
                {Month.July,InitDictionary()},
                {Month.August,InitDictionary()},
                {Month.September,InitDictionary()},
                {Month.October,InitDictionary()},
                {Month.November,InitDictionary()},
                {Month.December,InitDictionary()}
            };
            incomePerMonth = new Dictionary<Month, Dictionary<CostType, float>>(){
                {Month.January,InitDictionary()},
                {Month.February,InitDictionary()},
                {Month.March,InitDictionary()},
                {Month.April,InitDictionary()},
                {Month.May,InitDictionary()},
                {Month.June,InitDictionary()},
                {Month.July,InitDictionary()},
                {Month.August,InitDictionary()},
                {Month.September,InitDictionary()},
                {Month.October,InitDictionary()},
                {Month.November,InitDictionary()},
                {Month.December,InitDictionary()}
            };
            expensesPerYear = new Dictionary<int, Dictionary<CostType, float>>(){
                {now.Year,InitDictionary()},
            };
            incomePerYear = new Dictionary<int, Dictionary<CostType, float>>(){
                {now.Year,InitDictionary()},
            };
        }
        private Dictionary<CostType, float> InitDictionary()
        {
            Dictionary<CostType, float> dict = new Dictionary<CostType, float>(){
                {CostType.Infrastructure, 0f},
                {CostType.Rent, 0f},
                {CostType.Wage, 0f},
                {CostType.Leasing, 0f},
                {CostType.Taxes, 0f},
                {CostType.Insurance, 0f},
                {CostType.Bonus, 0f}
            };
            return dict;
        }
        public void AddExpenses(float amount, CostType type)
        {
            TimeStamp now = TimeManager.Instance.CurrentTimeStamp;
            if (now.Month != currentMonth)
            {
                ResetDictionary(expensesPerMonth[(Month)now.Month]);
                currentMonth = now.Month;
            }
            expensesPerMonth[(Month)now.Month][type] += amount;
            expensesPerYear[now.Year][type] += amount;
        }

        public void AddIncome(float amount, CostType type)
        {
            TimeStamp now = TimeManager.Instance.CurrentTimeStamp;
            if (now.Month != currentMonth)
            {
                ResetDictionary(incomePerMonth[(Month)now.Month]);
                currentMonth = now.Month;
            }
            incomePerMonth[(Month)now.Month][type] += amount;
            incomePerYear[now.Year][type] += amount;
        }

        private void ResetDictionary(Dictionary<CostType, float> dictionary)
        {
            for (int i = 0; i < System.Enum.GetNames(typeof(CostType)).Length; i++)
            {
                dictionary[(CostType)i] = 0f;
            }
        }
    }
}
