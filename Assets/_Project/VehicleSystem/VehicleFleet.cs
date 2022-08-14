using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanySystem;

namespace VehicleSystem
{
    [System.Serializable]
    public class VehicleFleet : ITrader
    {
        [SerializeField] private List<Vehicle> vehicles;
        [SerializeField] private List<Trailer> trailers;
        public List<Vehicle> Vehicles => vehicles;
        public List<Trailer> Trailers => trailers;
        public readonly PlayerCompany player;

        public VehicleFleet(PlayerCompany player)
        {
            this.vehicles = new List<Vehicle>();
            this.trailers = new List<Trailer>();
            this.player = player;
        }

        public float Money => player.Money;

        public bool CanAfford(float cost)
        {
            if (Money - cost > 0) return true;
            return false;
        }

        public void AddMoney(float money)
        {
            Debug.Log("Adding money: " + money);
        }

        public void RemoveMoney(float money)
        {
            Debug.Log($"Money: {Money} - Costs: {money} = {Money - money}");
        }

        public void AddVehicle(Vehicle vehicle) => vehicles.Add(vehicle);
        public void AddVehicle(Trailer trailer) => trailers.Add(trailer);

        public bool RemoveVehicle(Vehicle vehicle)
        {
            bool removed = vehicles.Remove(vehicle);
            if (removed)
            {
                AddMoney(vehicle.GetCalculatedPrice());
            }
            return removed;
        }
        public bool RemoveVehicle(Trailer trailer)
        {
            bool removed = trailers.Remove(trailer);
            if (removed)
            {
                AddMoney(trailer.GetCalculatedPrice());
            }
            return removed;
        }
    }
}
