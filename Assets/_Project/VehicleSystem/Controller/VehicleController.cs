using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;
using System;
using EmployeeSystem;

namespace VehicleSystem
{
    public class VehicleController : MonoBehaviour
    {
        private bool Initialized = false;
        [SerializeField] private Vehicle vehicle;
        [SerializeField] private Trailer trailer;
        private float currentVehicleCapacity;
        private float currentTrailerCapacity;
        private Dictionary<TransportGood, float> goodsInVehicle;
        private Dictionary<TransportGood, float> goodsInTrailer;
        private Employee driver;
        private void Awake()
        {
            vehicle = null;
            trailer = null;
            currentTrailerCapacity = 0;
            currentVehicleCapacity = 0;
            goodsInTrailer = new Dictionary<TransportGood, float>();
            goodsInVehicle = new Dictionary<TransportGood, float>();
        }
        public Employee Driver => driver;
        public Vehicle Vehicle => vehicle;
        public Trailer Trailer => trailer;
        public bool Initialize(Vehicle Vehicle, Employee driver)
        {
            if (vehicle != null) return false;
            currentVehicleCapacity = Vehicle.Capcity;
            vehicle = Vehicle;
            this.driver = driver;
            Initialized = true;
            return true;
        }

        public bool AttachTrailer(Trailer Trailer)
        {
            if (!Initialized) return false;
            if (vehicle == null || !vehicle.CanHandleTrailer) return false;
            if (!vehicle.HandleableTrailers.Contains(Trailer.Type)) return false;
            currentTrailerCapacity = Trailer.Capcity;
            trailer = Trailer;
            return true;
        }

        public bool DetachTrailer(out Trailer Trailer)
        {
            Trailer = default;
            if (!Initialized) return false;
            if (trailer == null) return false;
            currentTrailerCapacity = 0;
            Trailer = trailer;
            trailer = null;
            return true;
        }

        public bool LoadVehicle(TransportGood transportGood, float amount, out float leftOver)
        {
            leftOver = amount;
            if (!Initialized) return false;
            return LoadTransportGood(vehicle, transportGood, amount, out leftOver);
        }

        public bool UnloadVehicle(TransportGood transportGood, float amount)
        {
            if (!Initialized) return false;
            if (!goodsInVehicle.ContainsKey(transportGood)) return false;
            goodsInVehicle[transportGood] -= amount;
            if (goodsInVehicle[transportGood] < 0) goodsInVehicle.Remove(transportGood);
            return true;
        }
        public bool UnLoadTrailer(TransportGood transportGood, float amount)
        {
            if (!Initialized) return false;
            if (trailer == null) return false;
            if (!goodsInTrailer.ContainsKey(transportGood)) return false;
            goodsInTrailer[transportGood] -= amount;
            if (goodsInTrailer[transportGood] < 0) goodsInTrailer.Remove(transportGood);
            return true;
        }


        public bool LoadTrailer(TransportGood transportGood, float amount, out float leftOver)
        {
            leftOver = amount;
            if (!Initialized) return false;
            if (trailer == null) return false;
            return LoadTransportGood(trailer, transportGood, amount, out leftOver);
        }

        private bool LoadTransportGood(BaseVehicle baseVehicle, TransportGood transportGood, float amount, out float leftOver)
        {
            leftOver = amount;
            if (!ValidateTransportGoodForVehicle(baseVehicle, transportGood)) return false;
            float loadedAmount = CalculateLoadAmount(baseVehicle, amount, out leftOver);
            if (loadedAmount == 0) return false;
            if (baseVehicle.GetType() == typeof(Vehicle))
            {
                goodsInVehicle.Add(transportGood, loadedAmount);
                currentVehicleCapacity -= loadedAmount;
            }
            else
            {
                goodsInTrailer.Add(transportGood, loadedAmount);
                currentTrailerCapacity -= loadedAmount;
            }
            return true;
        }

        private float CalculateLoadAmount(BaseVehicle baseVehicle, float amount, out float leftOver)
        {
            leftOver = amount;
            if (baseVehicle.GetType() == typeof(Vehicle))
            {
                float tempLoad = currentVehicleCapacity - amount;
                if (tempLoad < 0)//we cant load everything
                {
                    float loadAmount = amount + tempLoad;
                    currentVehicleCapacity -= loadAmount;
                    leftOver = -tempLoad;
                    return loadAmount;
                }
                else // we have open space left;
                {
                    currentVehicleCapacity -= tempLoad;
                    leftOver = 0;
                    return tempLoad;
                }
            }
            if (baseVehicle.GetType() == typeof(Trailer))
            {
                float tempLoad = currentTrailerCapacity - amount;
                if (tempLoad < 0)//we cant load everything
                {
                    float loadAmount = amount + tempLoad;
                    currentTrailerCapacity -= loadAmount;
                    leftOver = -tempLoad;
                    return loadAmount;
                }
                else // we have open space left;
                {
                    currentTrailerCapacity -= tempLoad;
                    leftOver = 0;
                    return tempLoad;
                }
            }
            Debug.LogError("VehicleController - CalcLoadAmount - BaseVehicleType Unknown!");
            return 0;
        }

        private bool ValidateTransportGoodForVehicle(BaseVehicle baseVehicle, TransportGood transportGood)
        {
            if (transportGood.NeedsCooling && !baseVehicle.HasCooling) return false;
            if (transportGood.NeedsCrane && !baseVehicle.HasCrane) return false;
            if (transportGood.NeedsForkLif && !baseVehicle.HasForklift) return false;
            if (transportGood.transportType == TransportType.CUBIC && !baseVehicle.CanHandleCUBIC) return false;
            return true;
        }
    }
}
