using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VehicleSystem
{
    public class BaseVehicleSO : ScriptableObject
    {
        public string Name;
        [Tooltip("In tonns")] public float Capacity;
        public bool CanHandleCUBIC;
        public bool HasForklift;
        public bool HasCooling;
        public bool HasCrane;
        public int OriginalPrice;
        public Image Image;
        public AnimationCurve Curve;

    }
}
