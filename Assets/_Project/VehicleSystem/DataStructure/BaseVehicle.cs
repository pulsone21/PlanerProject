using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VehicleSystem
{
    public class BaseVehicle : ScriptableObject
    {
        [SerializeField] private new string name;
        [SerializeField, Tooltip("In tonns")] private float capacity;
        [SerializeField] private bool canHandleCUBIC;
        [SerializeField] private bool hasForklift;
        [SerializeField] private bool hasCooling;
        [SerializeField] private bool hasCrane;
        [SerializeField] private int originalPrice;
        [SerializeField] private Image image;

        public string Name => name;
        public bool CanHandleCUBIC => canHandleCUBIC;
        public float Capcity => capacity;
        public bool HasForklift => hasForklift;
        public bool HasCrane => hasCrane;
        public bool HasCooling => hasCooling;
        public int OriginalPrice => originalPrice;
        public Image Image => image;
    }
}
