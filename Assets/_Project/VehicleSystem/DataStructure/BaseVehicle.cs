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
        protected AnimationCurve priceCurve;
        protected float condition;
        protected string name;
        protected float capacity;
        protected bool canHandleCUBIC;
        protected bool hasForklift;
        protected bool hasCooling;
        protected bool hasCrane;
        protected int originalPrice;
        protected Image image;
        protected TimeStamp constructionYear;

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
            priceCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f), new Keyframe(0.2f, 0.5f), new Keyframe(0.35f, 0.35f), new Keyframe(1f, 0.1f) });
            condition = UnityEngine.Random.Range(0, 101);
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

        public float Condition => condition;

        public void ChangeCondition(float changeAmount)
        {
            condition += changeAmount;
            if (condition > 100) condition = 100;
            if (condition < 0) condition = 0;
        }

        public float GetCalculatedPrice()
        {
            float year = constructionYear.DifferenceToNowInYears();
            return priceCurve.Evaluate(year);
        }
    }
}
