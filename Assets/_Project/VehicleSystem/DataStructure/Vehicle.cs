using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleSystem
{
    public class Vehicle : BaseVehicle
    {
        [SerializeField] private VehicleType type;
        [SerializeField] private bool canHandleTrailer;
        [SerializeField] private List<TrailerType> handleableTrailers;

        public Vehicle(VehicleSO vehicleSO) : base(vehicleSO)
        {
            type = vehicleSO.Type;
            canHandleTrailer = vehicleSO.CanHandleTrailer;
            handleableTrailers = vehicleSO.HandleableTrailers;
        }

        public List<TrailerType> HandleableTrailers => handleableTrailers;
        public bool CanHandleTrailer => canHandleTrailer;
        public VehicleType Type => type;

    }
}
