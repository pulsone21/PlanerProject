using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TimeSystem;
using System;

namespace VehicleSystem
{
    public enum TrailerType { small, medium, large };
    public enum VehicleType { Van, Truck, TracktorUnit };
    public abstract class BaseVehicle
    {
        private AnimationCurve curve;
        [SerializeField] protected string name;
        [SerializeField, Tooltip("In tonns")] protected float capacity;
        [SerializeField] protected bool canHandleCUBIC;
        [SerializeField] protected bool hasForklift;
        [SerializeField] protected bool hasCooling;
        [SerializeField] protected bool hasCrane;
        [SerializeField] protected int originalPrice;
        [SerializeField] protected Image image;
        private TimeStamp constructionYear;

        protected BaseVehicle(BaseVehicleSO baseVehicle)
        {
            this.name = baseVehicle.Name;
            this.capacity = baseVehicle.Capacity;
            this.canHandleCUBIC = baseVehicle.CanHandleCUBIC;
            this.hasForklift = baseVehicle.HasForklift;
            this.hasCooling = baseVehicle.HasCooling;
            this.hasCrane = baseVehicle.HasCrane;
            this.originalPrice = baseVehicle.OriginalPrice;
            this.image = baseVehicle.Image;
            constructionYear = TimeStamp.GetRndBirthday(50, 0);
            curve = new AnimationCurve();
            curve.AddKey(0f, 1f);
            curve.AddKey(0.2f, 0.5f);
            curve.AddKey(0.35f, 0.35f);
            curve.AddKey(1f, 0.1f);
        }
        public string Name => name;
        public bool CanHandleCUBIC => canHandleCUBIC;
        public float Capcity => capacity;
        public bool HasForklift => hasForklift;
        public bool HasCrane => hasCrane;
        public bool HasCooling => hasCooling;
        public int OriginalPrice => originalPrice;
        public Image Image => image;
        public TimeStamp ConstructionYear => constructionYear;

        public float GetCalculatedPrice()
        {
            float year = constructionYear.DifferenceToNowInYears();
            return curve.Evaluate(year);
        }
    }
}
