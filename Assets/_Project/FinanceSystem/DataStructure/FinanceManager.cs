using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TimeSystem;

namespace FinanceSystem
{
    [Serializable]
    public class FinanceManager
    {
        [SerializeField] private float money;
        public float Money => money;
        [SerializeField] private CostManager costManager;
        [SerializeField] private FinanceAccounting financeAccounting;
        private Action OnMoneyChange;
        public FinanceManager(float startMoney)
        {
            money = startMoney;
            costManager = new CostManager();
            financeAccounting = new FinanceAccounting();
            TimeManager.Instance.RegisterForTimeUpdate(MonthlyPayment, TimeManager.SubscriptionType.Month);
            TimeManager.Instance.RegisterForTimeUpdate(YearlyPayments, TimeManager.SubscriptionType.Year);
        }
        public void RegisterOnMoneyChange(Action action) => OnMoneyChange += action;
        public void UnregisterOnMoneyChange(Action action) => OnMoneyChange -= action;
        private void MonthlyPayment()
        {
            Debug.Log("Monthly Payday");
            foreach (KeyValuePair<CostType, float> entry in costManager.MonthlyCosts)
            {
                if (entry.Value > 0) RemoveMoney(entry.Value, entry.Key);
            }
        }
        private void YearlyPayments()
        {
            foreach (KeyValuePair<CostType, float> entry in costManager.YearlyCosts)
            {
                if (entry.Value > 0) RemoveMoney(entry.Value, entry.Key);
            }
        }
        public void AddRecurringCosts(float amount, CostType type, CostTime time) => costManager.AddCosts(type, amount, time);
        public void RemoveRecurringCosts(float amount, CostType type, CostTime time) => costManager.RemoveCosts(type, amount, time);
        public void AddMoney(float amount, CostType type)
        {
            money += amount;
            financeAccounting.AddIncome(amount, type);
            OnMoneyChange?.Invoke();
        }
        public void RemoveMoney(float amount, CostType type)
        {
            money -= amount;
            financeAccounting.AddExpenses(amount, type);
            OnMoneyChange?.Invoke();
        }
        public bool CanAfford(float cost) => money - cost > 0;

    }
}
