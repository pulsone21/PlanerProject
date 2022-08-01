using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleSystem
{
    [CreateAssetMenu(fileName = "Vehicle", menuName = "SO/VehicleSystem/Vehicle", order = 0)]
    public class VehicleSO : BaseVehicleSO
    {
        public VehicleType Type;
        public bool CanHandleTrailer;
        public List<TrailerType> HandleableTrailers;
    }
}
