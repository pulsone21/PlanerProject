using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FinanceSystem.Testing
{
    public class Tester : MonoBehaviour
    {
        public Testing_FinanceManager Manager;
        public float CostAmount;
        public CostType type;
        [Button("AddCosts")]
        private void AddCosts() => Manager.RemoveMoney(CostAmount, type);
        [Button("AddIncome")]
        private void AddIncome() => Manager.AddMoney(CostAmount, type);
        [Button("AddRecuringCosts")]
        private void AddRecuringCosts() => Manager.AddRecurringCosts(CostAmount, type, CostTime.Monthly);
    }
}
