using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;
using TimeSystem;

namespace VehicleSystem
{
    [RequireComponent(typeof(VehicleController))]
    public class VehicleConditionController : MonoBehaviour
    {
        private VehicleController controller;
        [SerializeField] private AnimationCurve curve;
        private void Start() => controller = GetComponent<VehicleController>();
        private void OnEnable() => TimeManager.Instance.RegisterForTimeUpdate(ChangeCondition, TimeManager.SubscriptionType.Day);
        private void OnDisable() => TimeManager.Instance.UnregisterForTimeUpdate(ChangeCondition, TimeManager.SubscriptionType.Day);
        private void ChangeCondition()
        {
            Employee driver = controller.Driver;
            float change = curve.Evaluate((driver.Driving.Value / 100));
            controller.Vehicle.ChangeCondition(change);
            Trailer trailer = controller.Trailer;
            if (trailer != null) trailer.ChangeCondition(change);
        }
    }
}
