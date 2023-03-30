using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;

namespace VehicleSystem
{
    public static class LoadingAgent
    {
        public static bool Load(TransportGood good, float amount, Vehicle vehicle, Trailer trailer = null)
        {
            Debug.Log($"Loading - {amount} of {good}");
            bool vehicleCheck = vehicle != null && vehicle.ValidateTransportGood(good);
            bool trailerCheck = trailer != null && trailer.ValidateTransportGood(good);

            bool case1 = vehicleCheck && vehicle.CurrentCapacity >= amount;
            bool case2 = trailerCheck && trailer.CurrentCapacity >= amount;
            bool case3 = vehicleCheck && trailerCheck && trailer.CurrentCapacity + vehicle.CurrentCapacity > amount;

            Debug.Log($"Case1 {case1}, Case2 {case2}, Case3 {case3}");

            if (case1)
            {
                vehicle.LoadTransportGood(good, amount, out float leftOver);
                Debug.Log("Case1 is true - good is stored in vehicle only");
                return true;
            }
            if (case2)
            {
                trailer.LoadTransportGood(good, amount, out float leftOver);
                Debug.Log("Case2 is true - good is stored in trailer only");
                return true;
            }
            if (case3)
            {
                vehicle.LoadTransportGood(good, amount, out float leftOver);
                trailer.LoadTransportGood(good, leftOver, out float trailerLeft);
                Debug.Log("Case3 is true - good is stored in vehicle and trailer");
                return true;
            }
            Debug.Log($"Cant handle the good or the amount - Vehicle {vehicleCheck}, Trailer {trailerCheck}, VehicleCap {vehicle.CurrentCapacity}, TrailerCap {trailer.CurrentCapacity}");
            return false;
        }

        public static bool Unload(TransportGood good, float amount, Vehicle vehicle, Trailer trailer = null)
        {
            Debug.Log($"I should Unload {good} _ {amount}");
            float vehicleLeft = 0f;
            float trailerLeft = 0f;
            bool vehicleCheck = vehicle != null && vehicle.CheckCargo(good, amount, out vehicleLeft);
            bool trailerCheck = false;
            if (vehicleLeft > 0f)
            {
                trailerCheck = trailer != null && trailer.CheckCargo(good, vehicleLeft, out trailerLeft);
            }
            Debug.Log($"Loading Checks: TrailerCheck: {trailerCheck} _ {trailerLeft}, VehicleCheck: {vehicleCheck} _ {vehicleLeft}");

            if ((trailerCheck && trailerLeft == 0) || (vehicleCheck && vehicleLeft == 0))
            {

                if (vehicleCheck) vehicle.UnloadTransportGood(good, amount - vehicleLeft);
                if (trailerCheck && vehicleLeft > 0) trailer.UnloadTransportGood(good, vehicleLeft);
                return true;
            }
            return false;
        }
    }
}
