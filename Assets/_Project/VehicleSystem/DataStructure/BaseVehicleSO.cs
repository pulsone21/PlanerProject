using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UISystem;

namespace VehicleSystem
{
    public abstract class BaseVehicleSO : ScriptableObject, ITableRow
    {
        protected const int forkLiftPrice = 1000;
        protected const int coolingPrice = 500;
        protected const int cranePrice = 2500;
        public string Name;
        [Tooltip("In tonns")] public float Capacity;
        public bool CanHandleCUBIC;
        public bool HasForklift;
        public bool HasCooling;
        public bool HasCrane;
        public int OriginalPrice;
        public Image Image;
        public AnimationCurve Curve;
        public abstract string[] GetRowContent();
        protected virtual string Specialities()
        {
            string outString = "";
            if (HasForklift) outString += ", Forklift";
            if (HasCrane) outString += ", Crane";
            if (HasCooling) outString += ", Cooling";
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
        public virtual float GetCalculatedPrice() => Mathf.FloorToInt(GetBasePriceRaw());
        protected float GetBasePriceRaw()
        {
            float price = OriginalPrice;
            if (HasForklift) price += forkLiftPrice;
            if (HasCooling) price += coolingPrice;
            if (HasCrane) price += cranePrice;
            return price;
        }
    }
}
