using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleSystem
{
    [CreateAssetMenu(fileName = "Trailer", menuName = "ScriptableObjects/VehicleSystem/Trailer", order = 0)]
    public class TrailerSO : BaseVehicleSO
    {
        public TrailerType Type;
    }
}
