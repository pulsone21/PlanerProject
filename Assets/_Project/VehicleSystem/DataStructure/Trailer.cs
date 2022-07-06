using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleSystem
{
    [CreateAssetMenu(fileName = "Trailer", menuName = "ScriptableObjects/VehicleSystem/Trailer", order = 0)]
    public class Trailer : BaseVehicle
    {
        public enum TrailerType { small, medium, large }
        [SerializeField] private TrailerType type;
        public TrailerType Type => type;

    }
}
