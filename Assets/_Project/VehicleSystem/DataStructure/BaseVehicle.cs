using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TimeSystem;
using System;
using UISystem;
using Utilities;
using ContractSystem;

namespace VehicleSystem
{
    public enum TrailerType { small, medium, large };
    public enum VehicleType { Van, Truck, TracktorUnit };
    public abstract class BaseVehicle : ITableRow
    {
        protected AnimationCurve priceCurve;
        protected Image image;
        [SerializeField] protected int condition, basePrice;
        [SerializeField] protected string name;
        [SerializeField] protected float maxCapacity;
        [SerializeField] protected bool canHandleCUBIC, hasForklift, hasCooling, hasCrane;
        [SerializeField] protected TimeStamp constructionYear;
        public bool InUse = false;
        public readonly string PlateText;
        protected const int forkLiftPrice = 1000;
        protected const int coolingPrice = 500;
        protected const int cranePrice = 2500;
        public string Name => name;
        public bool CanHandleCUBIC => canHandleCUBIC;
        public float MaxCapacity => maxCapacity;
        public float CurrentCapacity => EvalCapacity();
        public bool HasForklift => hasForklift;
        public bool HasCrane => hasCrane;
        public bool HasCooling => hasCooling;
        public int OriginalPrice => basePrice;
        public Image Image => image;
        public TimeStamp ConstructionYear => constructionYear;
        public float Condition => condition;
        private Dictionary<TransportGood, float> storedGoods;
        public Dictionary<TransportGood, float> StoredGoods => storedGoods;
        public bool IsLoaded => CurrentCapacity < maxCapacity;
        protected BaseVehicle(BaseVehicleSO baseVehicle, bool isNew = false)
        {
            this.name = baseVehicle.Name;
            this.maxCapacity = baseVehicle.Capacity;
            this.canHandleCUBIC = baseVehicle.CanHandleCUBIC;
            this.hasForklift = baseVehicle.HasForklift;
            this.hasCooling = baseVehicle.HasCooling;
            this.hasCrane = baseVehicle.HasCrane;
            this.basePrice = baseVehicle.OriginalPrice;
            this.image = baseVehicle.Image;
            this.PlateText = VehicleFactory.GeneratePlateText();
            storedGoods = new Dictionary<TransportGood, float>();
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
#if !UNITY_EDITOR
                constructionYear = TimeManager.Instance.CurrentTimeStamp;
#else
                constructionYear = new TimeStamp(0, 0, 1, 1, 1994);
#endif
                condition = 100;
            }
        }
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
            //TODO make it translatable
            string outString = "";
            if (condition > 80) { outString = "Sehr Gut"; }
            else if (condition > 65) { outString = "Gut"; }
            else if (condition > 50) { outString = "Ausreichend"; }
            else if (condition > 35) { outString = "Schlecht"; }
            else { outString = "Sehr schlecht"; }
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
        public bool ValidateTransportGood(TransportGood transportGood)
        {
            // TODO There should also be a check for cross cubic materials.
            if (transportGood.transportType == TransportType.CUBIC && !CanHandleCUBIC)
            {
                Debug.LogWarning($"Vehicle can't handle CBUIC , {transportGood.transportType}");
                return false;
            }
            if (transportGood.NeedsCooling && !HasCooling)
            {
                Debug.LogWarning($"Vehicle can't cool, {transportGood.NeedsCooling}");
                return false;
            }
            if (transportGood.NeedsCrane && !HasCrane)
            {
                Debug.LogWarning($"Vehicle has no crane, {transportGood.NeedsCrane}");
                return false;
            }
            if (transportGood.NeedsForkLif && !HasForklift)
            {
                Debug.LogWarning($"Vehicle has no Forklift, {transportGood.NeedsForkLif}");
                return false;
            }
            return true;
        }
        public bool LoadTransportGood(TransportGood transportGood, float amount, out float leftOver)
        {
            leftOver = amount;
            if (!ValidateTransportGood(transportGood)) return false;
            float loadedAmount = CalculateLoadAmount(amount, out leftOver);
            if (loadedAmount == 0) return false;
            if (storedGoods.ContainsKey(transportGood)) { storedGoods[transportGood] += loadedAmount; }
            else { storedGoods.Add(transportGood, loadedAmount); }
            return true;
        }

        private float EvalCapacity()
        {
            float outNumber = maxCapacity;
            foreach (KeyValuePair<TransportGood, float> entry in storedGoods)
            {
                outNumber -= entry.Value;
            }
            return outNumber;
        }
        public bool CheckCargo(TransportGood transportGood, float amount, out float leftOver)
        {
            Debug.Log($"Checking Cargo: {amount}, {transportGood}");
            leftOver = amount;
            if (!storedGoods.ContainsKey(transportGood)) { Debug.Log($"TransportGood {transportGood} is not loaded"); return false; }
            leftOver = Math.Clamp(amount - storedGoods[transportGood], 0, float.MaxValue);
            Debug.Log($"Good is loaded and we {storedGoods[transportGood]} stored, we need {amount}, so leftOver is {leftOver}");
            return true;
        }
        public void UnloadTransportGood(TransportGood transportGood, float amount)
        {
            storedGoods[transportGood] += amount;
            if (storedGoods[transportGood] == 0f) storedGoods.Remove(transportGood);
        }
        private float CalculateLoadAmount(float amount, out float leftOver)
        {
            Debug.Log($"i should load {amount}");
            float tempLoad = CurrentCapacity - amount;
            if (tempLoad < 0)//we cant load everything
            {
                float loadAmount = amount + tempLoad;
                leftOver = -tempLoad;
                Debug.Log($"Cant load everything, loadAmount = {loadAmount}, LeftOver = {leftOver}");
                return loadAmount;
            }
            else // we have open space left;
            {
                leftOver = 0;
                Debug.Log($"We can load everything, LoadAmount {amount}, leftOver {leftOver}");
                return amount;
            }
        }

        public abstract Tuple<string, string> GetTooltipInfo();
    }
}
