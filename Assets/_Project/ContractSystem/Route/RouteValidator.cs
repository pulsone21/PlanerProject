using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;

namespace ContractSystem
{
    public static class RouteValidator
    {
        public static bool ValidateRoute(RouteDestination[] destinations, Vehicle vehicle, out string result, Trailer trailer = null)
        {
            //TODO Implement that function
            bool orderCheck = CheckDestinationOrder(destinations);
            /*
                - check if every load dest is priror to its unload dest
                - we need to go through the complete route and look if we have enough space for everything in the Vehicle/trailer 
                - we need to check if can transport the types
                - //? maybe check if the driver has the correct certifications? -> maybe a learning curve to let the player this figure out by himself
                - this should not check if we hitting the delivery dates correctly -> that needs to be verified by player/dispatcher
            */

            result = $"Route Validation - OrderCheck:{orderCheck}";
            return orderCheck;
        }

        private static bool CheckDestinationOrder(RouteDestination[] destinations)
        {
            bool orderCheck = false;
            TransportContract currContract;
            for (int i = 0; i < destinations.Length; i++)
            {
                if (destinations[i].Direction == RouteDestination.LoadingDirection.Unload) continue;
                RouteDestination curDest = destinations[i];
                if (i == 0 && curDest.Direction == RouteDestination.LoadingDirection.Unload) return false;
                if (curDest.Direction == RouteDestination.LoadingDirection.Load)
                {
                    currContract = curDest.Contract;
                    orderCheck = FindUnloadDest(i);
                }
            }
            bool FindUnloadDest(int orignalIndex)
            {
                for (int i = 0; i < destinations.Length; i++)
                {
                    if (i == orignalIndex) continue;
                    RouteDestination curDest = destinations[i];
                    if (curDest.Contract == currContract && i > orignalIndex)
                    {
                        Debug.Log($"Route Validation - Loading Dest is befor Unloading Dest");
                        return true;
                    }
                }
                Debug.Log($"Route Validation - Loading Dest is after Unloading Dest! -> orignalIndex = {orignalIndex}");
                return false;
            }
            return orderCheck;
        }
    }
}
