using UnityEngine;
using TimeSystem;

namespace EmployeeSystem
{
    [RequireComponent(typeof(EmployeeController))]
    public class EmployeeStatsController : MonoBehaviour
    {
        private EmployeeController _controller;
        private Stress _stress => _controller.Employee.Stress;
        private Happines _happiness => _controller.Employee.Happines;
        private Loyalty _loyalty => _controller.Employee.Loyalty;
        private int oldStressValue;
        private int oldHappinessValue;
        private int oldLoyalValue;
        private void Start()
        {
            _controller = GetComponent<EmployeeController>();
            TimeManager.Instance.RegisterForTimeUpdate(_happiness.ContinualChange, TimeManager.SubscriptionType.Day);
            TimeManager.Instance.RegisterForTimeUpdate(_loyalty.ContinualChange, TimeManager.SubscriptionType.Month);
            TimeManager.Instance.RegisterForTimeUpdate(_stress.ContinualChange, TimeManager.SubscriptionType.Hour);
        }

        private void Update()
        {
            if (_stress.Value != oldStressValue)
            {
                oldStressValue = _stress.Value;
                int computedValue = Mathf.RoundToInt(oldStressValue / 100);
                _happiness.InfluenceRateOfChange(computedValue);
            }

            if (_happiness.Value != oldHappinessValue)
            {
                oldHappinessValue = _happiness.Value;
                int computedValue = Mathf.RoundToInt(oldHappinessValue / 100);
                _loyalty.InfluenceRateOfChange(computedValue);
            }

            if (_loyalty.Value != oldLoyalValue)
            {
                oldLoyalValue = _loyalty.Value;
                int computedValue = Mathf.RoundToInt(oldLoyalValue / 100);
                _happiness.InfluenceRateOfChange(computedValue);
            }
        }

        private void OnDestroy()
        {
            TimeManager.Instance.UnregisterForTimeUpdate(_happiness.ContinualChange, TimeManager.SubscriptionType.Day);
            TimeManager.Instance.UnregisterForTimeUpdate(_loyalty.ContinualChange, TimeManager.SubscriptionType.Month);
            TimeManager.Instance.UnregisterForTimeUpdate(_stress.ContinualChange, TimeManager.SubscriptionType.Hour);
        }


    }
}
