using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FinanceSystem.Testing
{
    public class Testing_CostManager : SerializedMonoBehaviour
    {
        [SerializeField, DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)] private Dictionary<CostType, float> monthlyCosts;
        public Dictionary<CostType, float> MonthlyCosts => monthlyCosts;
        [SerializeField, DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)] private Dictionary<CostType, float> yearlyCosts;
        public Dictionary<CostType, float> YearlyCosts => yearlyCosts;
        public void Awake()
        {
            monthlyCosts = new Dictionary<CostType, float>(){
                {CostType.Infrastructure, 0f},
                {CostType.Rent, 0f},
                {CostType.Salaray, 0f},
                {CostType.Leasing, 0f},
                {CostType.Taxes, 0f},
                {CostType.Insurance, 0f},
                {CostType.Bonus, 0f}
            };

            yearlyCosts = new Dictionary<CostType, float>(){
                {CostType.Infrastructure, 0f},
                {CostType.Rent, 0f},
                {CostType.Salaray, 0f},
                {CostType.Leasing, 0f},
                {CostType.Taxes, 0f},
                {CostType.Insurance, 0f},
                {CostType.Bonus, 0f}
            };
        }
        public void AddCosts(CostType type, float amount, CostTime time)
        {
            switch (time)
            {
                case CostTime.Monthly:
                    monthlyCosts[type] += amount;
                    break;
                case CostTime.Yearly:
                    yearlyCosts[type] += amount;
                    break;
                default: throw new System.NotImplementedException();
            }
        }
        public void RemoveCosts(CostType type, float amount, CostTime time)
        {
            switch (time)
            {
                case CostTime.Monthly:
                    monthlyCosts[type] = Mathf.Clamp(monthlyCosts[type] - amount, 0, float.MaxValue);
                    break;
                case CostTime.Yearly:
                    yearlyCosts[type] = Mathf.Clamp(yearlyCosts[type] - amount, 0, float.MaxValue);
                    break;
                default: throw new System.NotImplementedException();
            }

        }
    }
}
