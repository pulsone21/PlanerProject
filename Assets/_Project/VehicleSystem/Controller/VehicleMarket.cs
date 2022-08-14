using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TimeSystem;

namespace VehicleSystem
{
    public class VehicleMarket : MonoBehaviour
    {
        public static VehicleMarket Instance;
        [SerializeField] private List<Vehicle> vehicles = new List<Vehicle>();
        [SerializeField] private List<Trailer> trailers = new List<Trailer>();
        public static List<Vehicle> Vehicles => Instance.vehicles;
        public static List<Trailer> Trailers => Instance.trailers;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        private void Start()
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
                List<VehicleSO> vehicleSOs = VehicleFactory.Instance.GetVehicles();
                foreach (VehicleSO vehicleSO in vehicleSOs)
                {
                    int amount = UnityEngine.Random.Range(0, 11);
                    for (int i = 0; i < amount; i++)
                    {
                        vehicles.Add(new Vehicle(vehicleSO));
                    }

                }
                return;
            }
            if (type == typeof(Trailer))
            {
                List<TrailerSO> trailerSOs = VehicleFactory.Instance.GetTrailers();
                foreach (TrailerSO trailerSO in trailerSOs)
                {
                    int amount = UnityEngine.Random.Range(0, 11);
                    for (int i = 0; i < amount; i++)
                    {
                        trailers.Add(new Trailer(trailerSO));
                    }
                }
                return;
            }
            Debug.LogError("VehicleMarket - GenerateItems() - Unknown Type: " + type.ToString());
        }

        public Vehicle BuyVehicle(Vehicle vehicle, ITrader trader)
        {
            if (!vehicles.Contains(vehicle)) return null;
            if (!trader.CanAfford(vehicle.OriginalPrice)) return null;
            trader.RemoveMoney(vehicle.GetCalculatedPrice());
            vehicles.Remove(vehicle);
            return vehicle;
        }

        public Trailer BuyVehicle(Trailer trailer, ITrader trader)
        {
            if (!trailers.Contains(trailer)) return null;
            if (!trader.CanAfford(trailer.OriginalPrice)) return null;
            trader.RemoveMoney(trailer.GetCalculatedPrice());
            trailers.Remove(trailer);
            return trailer;
        }
        public void Sellehicle(Vehicle vehicle, ITrader trader)
        {
            trader.AddMoney(vehicle.GetCalculatedPrice());
            vehicles.Add(vehicle);
        }
        public void Sellehicle(Trailer trailer, ITrader trader)
        {
            trader.AddMoney(trailer.GetCalculatedPrice());
            trailers.Add(trailer);
        }
    }
}
