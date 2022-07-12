using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleSystem
{
    public class VehicleFactory : MonoBehaviour
    {
        public static VehicleFactory Instance;
        private void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this;
            }
        }

        [SerializeField] private List<VehicleSO> Vehicles = new List<VehicleSO>();
        [SerializeField] private List<TrailerSO> Trailers = new List<TrailerSO>();
        [SerializeField] private GameObject vehicleController;

        public GameObject VehicleController => vehicleController;
        public List<VehicleSO> GetVehicles() => Vehicles;
        public List<TrailerSO> GetTrailers() => Trailers;


    }
}