using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VehicleSystem
{
    public class Trailer : BaseVehicle
    {
        [SerializeField] private TrailerType type;

        public Trailer(TrailerSO TrailerSO) : base(TrailerSO)
        {
            type = TrailerSO.Type;
        }

        public TrailerType Type => type;
    }
}
