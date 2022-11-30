using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TimeSystem;
using System;
using UISystem;
using Utilities;
namespace VehicleSystem
{
    public enum TrailerType { small, medium, large };
    public enum VehicleType { Van, Truck, TracktorUnit };
    public abstract class BaseVehicle : ITableRow
    {
        protected AnimationCurve priceCurve;
        [SerializeField] protected int condition;
        [SerializeField] protected string name;
        [SerializeField] protected float capacity;
        protected bool canHandleCUBIC;
        protected bool hasForklift;
        protected bool hasCooling;
        protected bool hasCrane;
        protected int basePrice;
        protected Image image;
        [SerializeField] protected TimeStamp constructionYear;
        protected const int forkLiftPrice = 1000;
        protected const int coolingPrice = 500;
        protected const int cranePrice = 2500;
        protected BaseVehicle(BaseVehicleSO baseVehicle, bool isNew = false)
        {
            this.name = baseVehicle.Name;
            this.capacity = baseVehicle.Capacity;
            this.canHandleCUBIC = baseVehicle.CanHandleCUBIC;
            this.hasForklift = baseVehicle.HasForklift;
            this.hasCooling = baseVehicle.HasCooling;
            this.hasCrane = baseVehicle.HasCrane;
            this.basePrice = baseVehicle.OriginalPrice;
            this.image = baseVehicle.Image;
            priceCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f), new Keyframe(0.2f, 0.8f), new Keyframe(0.35f, 0.5f), new Keyframe(1f, 0.25f) });

            if (!isNew)
            {
                constructionYear = TimeStamp.GetRndBirthday(50, 0);
                AnimationCurve conditionCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f), new Keyframe(0.2f, 0.5f), new Keyframe(0.35f, 0.35f), new Keyframe(1f, 0.1f) });
                float year = (float)constructionYear.DifferenceToNowInYears();
                float cond = conditionCurve.Evaluate((year + 0f).Normalized(0, 50)) * 100;
                condition = Mathf.FloorToInt(cond);
            }
            else
            {
                constructionYear = TimeManager.Instance.CurrentTimeStamp;
                condition = 100;
            }

        }
        public string Name => name;
        public bool CanHandleCUBIC => canHandleCUBIC;
        public float Capcity => capacity;
        public bool HasForklift => hasForklift;
        public bool HasCrane => hasCrane;
        public bool HasCooling => hasCooling;
        public int OriginalPrice => basePrice;
        public Image Image => image;
        public TimeStamp ConstructionYear => constructionYear;

        public float Condition => condition;

        public void ChangeCondition(float changeAmount)
        {
            condition += Mathf.FloorToInt(changeAmount);
            condition = Mathf.Clamp(condition, 0, 100);
        }

        public float GetCalculatedPrice()
        {
            float price = basePrice;
            float year = constructionYear.DifferenceToNowInYears();
            price -= basePrice * priceCurve.Evaluate(year.Normalized(50, 0));
            price -= price * (condition + 0f).Normalized(100, 0);
            if (hasForklift) price += forkLiftPrice;
            if (hasCooling) price += coolingPrice;
            if (hasCrane) price += cranePrice;
            return Mathf.FloorToInt(price);
        }
        public string ConditionAsString()
        {
            string outString = "";
            if (condition > 80)
            {
                outString = "Sehr Gut";
            }
            else if (condition > 65)
            {
                outString = "Gut";
            }
            else if (condition > 50)
            {
                outString = "Ausreichend";
            }
            else if (condition > 35)
            {
                outString = "Schlecht";
            }
            else
            {
                outString = "Sehr schlecht";
            }
            return outString + "/" + condition;
        }

        protected string Specialities()
        {
            string outString = "";
            if (hasForklift) outString += ", Forklift";
            if (hasCrane) outString += ", Crane";
            if (hasCooling) outString += ", Cooling";
            if (outString.Length == 0)
            {
                outString = "-";
            }
            else
            {
                outString = outString.Substring(2);
            }
            return outString;
        }

        public abstract string[] GetRowContent();
    }
}
