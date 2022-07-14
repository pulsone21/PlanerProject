using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanySystem;

namespace VehicleSystem
{
    public class VehicleFleet : Trader
    {
        private List<Vehicle> vehicles;
        private List<Trailer> trailers;
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
            throw new System.NotImplementedException();
        }

        public void AddMoney(float money)
        {
            throw new System.NotImplementedException();
        }

        public float GetMoney(float money)
        {
            throw new System.NotImplementedException();
        }
    }
}
