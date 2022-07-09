using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TimeSystem;

namespace VehicleSystem
{
    public class VehicleMarket : MonoBehaviour
    {
        private Dictionary<Vehicle, int> vehicles = new Dictionary<Vehicle, int>();
        private Dictionary<Trailer, int> trailers = new Dictionary<Trailer, int>();
        public Dictionary<Vehicle, int> Vehicles => vehicles;
        public Dictionary<Trailer, int> Trailers => trailers;

        private void Awake()
        {
            GenerateSalesList(null);
            TimeManager.Instance.RegisterForTimeUpdate(GenerateSalesList, TimeManager.SubscriptionType.Month);
        }

        private void GenerateSalesList(TimeStamp timeStamp)
        {
            GenerateItems(typeof(Vehicle));
            GenerateItems(typeof(Trailer));
        }

        private void GenerateItems(Type type)
        {
            if (type == typeof(Vehicle))
            {
                List<VehicleSO> vehicleSOs = VehicleManager.Instance.GetVehicles();
                foreach (VehicleSO vehicleSO in vehicleSOs)
                {
                    Vehicle vehicle = new Vehicle(vehicleSO);
                    vehicles[vehicle] += UnityEngine.Random.Range(0, 11);
                }
                return;
            }
            if (type == typeof(Trailer))
            {
                List<TrailerSO> trailerSOs = VehicleManager.Instance.GetTrailers();
                foreach (TrailerSO trailerSO in trailerSOs)
                {
                    Trailer trailer = new Trailer(trailerSO);
                    trailers[trailer] += UnityEngine.Random.Range(0, 11);
                }
                return;
            }
            Debug.LogError("VehicleMarket - GenerateItems() - Unknown Type: " + type.ToString());
        }

        public Vehicle BuyVehicle(Vehicle vehicle, Trader trader)
        {
            if (!vehicles.ContainsKey(vehicle) && vehicles[vehicle] < 1) return default;
            if (!trader.CanAfford(vehicle.OriginalPrice)) return default;
            trader.GetMoney(vehicle.GetCalculatedPrice());
            vehicles[vehicle] -= 1;
            return vehicle;
        }

        public Trailer BuyVehicle(Trailer trailer, Trader trader)
        {
            if (!trailers.ContainsKey(trailer) && trailers[trailer] < 1) return default;
            if (!trader.CanAfford(trailer.OriginalPrice)) return default;
            trader.GetMoney(trailer.GetCalculatedPrice());
            trailers[trailer] -= 1;
            return trailer;
        }
        public void Sellehicle(Vehicle vehicle, Trader trader)
        {
            trader.AddMoney(vehicle.GetCalculatedPrice());
            vehicles[vehicle]++;
        }
        public void Sellehicle(Trailer trailer, Trader trader)
        {
            trader.AddMoney(trailer.GetCalculatedPrice());
            trailers[trailer]++;
        }
    }
}
