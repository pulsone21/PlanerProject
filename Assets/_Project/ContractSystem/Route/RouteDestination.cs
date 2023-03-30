using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadSystem;
using System;
using VehicleSystem;

namespace ContractSystem
{
    [System.Serializable]
    public class RouteDestination
    {
        public enum LoadingDirection { Load, Unload }
        [SerializeField] private CityController _destinationCity;
        [SerializeField] private LoadingDirection _direction;
        [SerializeField] private TransportGood _good;
        [SerializeField] private int _amount;
        public TransportContract Contract { get; protected set; }
        public CityController DestinationCity => _destinationCity;
        public LoadingDirection Direction => _direction;
        public TransportGood Good => _good;
        public int Amount => _amount;
        public RouteDestination(CityController destinationCity, LoadingDirection direction, TransportContract contract)
        {
            _destinationCity = destinationCity;
            _direction = direction;
            _good = contract.Good;
            _amount = contract.GoodAmmount;
            Contract = contract;
        }
        public void Act(VehicleController vehicle)
        {

            switch (Direction)
            {
                case LoadingDirection.Load:
                    Load(vehicle);
                    Contract.SetState(TransportContract.State.inTransit);
                    break;
                case LoadingDirection.Unload:
                    Unload(vehicle);
                    Contract.SetState(TransportContract.State.delivered);
                    break;
            }
        }

        private void Unload(VehicleController vehicle)
        {
            if (vehicle.Unload(Good, Amount)) return;
            Debug.LogWarning($"ERROR on RouteDest.UnLoad() for Vehicle: {vehicle.Vehicle.PlateText}  - Couldn't UnLoad Goods for some reason.");
        }

        private void Load(VehicleController vehicle)
        {
            //? This needs to be able to delete or add cargo to the vehicle/trailer composition
            if (vehicle.Load(Good, Amount)) return;
            Debug.LogWarning($"ERROR on RouteDest.Load() for Vehicle: {vehicle.Vehicle.PlateText}  - Couldn't Load Goods for some reason.");
            // ? What should i do if something went wrong?
        }

        public override string ToString() => $"{_direction} Destination of Contract {Contract}";
    }
}
