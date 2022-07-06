using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleSystem
{
    [CreateAssetMenu(fileName = "Vehicle", menuName = "ScriptableObjects/VehicleSystem/Vehicle", order = 0)]
    public class Vehicle : BaseVehicle
    {
        public enum VehicleType { Van, Truck, TracktorUnit }
        [SerializeField] private VehicleType type;
        [SerializeField] private bool canHandleTrailer;
        [SerializeField] private List<Trailer.TrailerType> handleableTrailers;


        public List<Trailer.TrailerType> HandleableTrailers => handleableTrailers;
        public bool CanHandleTrailer => canHandleTrailer;
        public VehicleType Type => type;


    }
}
