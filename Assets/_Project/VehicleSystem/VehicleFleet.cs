using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanySystem;
using FinanceSystem;
using System;

namespace VehicleSystem
{
    [System.Serializable]
    public class VehicleFleet : ITrader
    {
        [SerializeField] private List<Vehicle> vehicles;
        [SerializeField] private List<Trailer> trailers;
        [SerializeField] private List<VehicleController> activeVehicles;
        public List<Vehicle> Vehicles => vehicles;
        public List<Trailer> Trailers => trailers;
        private TransportCompany company;
        public VehicleFleet(TransportCompany company)
        {
            vehicles = new List<Vehicle>();
            trailers = new List<Trailer>();
            this.company = company;
        }
        public float Money => company.FinanceManager.Money;
        public bool CanAfford(float cost) => company.FinanceManager.CanAfford(cost);
        public void AddMoney(float money) => company.FinanceManager.AddMoney(money, CostType.Infrastructure);
        public void RemoveMoney(float money) => company.FinanceManager.RemoveMoney(money, CostType.Infrastructure);
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
        public List<Trailer> GetFreeTrailers()
        {
            List<Trailer> outList = new List<Trailer>();
            foreach (Trailer trailer in trailers)
            {
                if (!trailer.InUse) outList.Add(trailer);
            }
            Debug.Log($"I should return 1 Trailer, i returning: {outList.Count}");
            return outList;
        }
        public List<Vehicle> GetFreeVehicles()
        {
            List<Vehicle> outList = new List<Vehicle>();
            foreach (Vehicle vehicle in vehicles)
            {
                if (!vehicle.InUse) outList.Add(vehicle);
            }
            Debug.Log($"I should return 1 Vehicle, i returning: {outList.Count}");
            return outList;
        }
    }
}
