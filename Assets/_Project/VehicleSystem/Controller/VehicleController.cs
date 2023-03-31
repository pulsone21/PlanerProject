using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;
using System;
using EmployeeSystem;
using SLSystem;
using Pathfinding;
using CompanySystem;
using RoadSystem;
namespace VehicleSystem
{
    public class VehicleController : MonoBehaviour, IPersistenceData
    {
        // TODO Refactor completly, No Info save in this CLASS, this Class is responsible for driving, loading and unloading, the informations are stored in the classes them self.
        [SerializeField] private float _baseAvgDrivingSpeed = 70f;
        [SerializeField] private RouteDestination _currDest = null;
        [SerializeField, Range(0f, 0.01f)] private float _neededDistToWP = 0.0001f;
        [SerializeField] private float PathDistance;
        private bool Initialized = false;
        private float currentVehicleCapacity => Vehicle != null ? Vehicle.CurrentCapacity : 0f;
        private float currentTrailerCapacity => Trailer != null ? Trailer.CurrentCapacity : 0f;
        private Driver driver;
        public Driver Driver => driver;
        public Vehicle Vehicle => driver.Vehicle;
        public Trailer Trailer => driver.Trailer;
        public GameObject This => gameObject;
        private Path _currPath;
        private Vector3 _currWP;
        private bool logged = false;
        public bool IsDriving { get; protected set; }
        private void FixedUpdate()
        {
            if (!Initialized) return;
            if (!driver.CanDrive) return;
            if (driver.OnRoute)
            {
                if (Vector3.Distance(_currPath.currentWaypoint, transform.position) < _neededDistToWP)
                {
                    IsDriving = false;
                    Debug.Log("We have reached our WP");
                    // we have reached our target
                    if (_currPath.GetNextPosition(out Vector3 nextPos))
                    {
                        // we have still waypoints to reach
                        _currWP = nextPos;
                        Debug.Log("Found new WP to go to");
                    }
                    else
                    {
                        // we are at the end of the path and should load or un load things
                        Debug.Log("We have reached our destination, loading or unloading somehting");
                        _currDest.Act(this);
                        driver.OnRoute = false;
                    }
                }
                else
                {
                    // we need to drive
                    transform.position = Vector3.Lerp(transform.position, _currWP, Time.fixedDeltaTime * CalcMovement(CalculateDrivingSpeed(_baseAvgDrivingSpeed)));
                    IsDriving = true;
                }
            }
            else
            {
                IsDriving = false;
                if (driver.Route.HasDestination)
                {
                    Debug.Log("My Route has destinationes geting the next one");
                    _currDest = driver.Route.DequeueDestination();
                    _currPath = new AstarPath(transform.position, _currDest.DestinationCity.transform.position).Path;
                    PathDistance = _currPath.CalculateDistance();
                    driver.OnRoute = true;
                    logged = false;

                }
                else
                {
                    if (!logged)
                    {
                        Debug.LogWarning($"Driver - {driver.Name} has finished his route");
                        logged = true;
                    }
                    //?? Do nothing, destroy, report somewhere, drive home? 
                }
            }
        }

        private float CalculateDrivingSpeed(float baseAvgDrivingSpeed)
        {
            // TODO implement some calcualtion for traffic jam, vehicle condition, driving skill, road type (? not implemnted either)
            return baseAvgDrivingSpeed;
        }

        private float CalcMovement(float avgDrivingSpeed)
        {
            // based on an avaerage driving speed arround 70 km/h and current scale we would need to move the icon 0.78/h which is 0.00022f 
            float vectorDistancePerKM = 0.0111f;
            return ((vectorDistancePerKM * avgDrivingSpeed) / 60) / 60; // (distancePerHour)/minutes/seconds -> needs to be in seconds since we add this by deltaTime tick
        }

        public void Initialize(Driver driver)
        {
            if (Initialized) return;
            Initialized = true;
            this.driver = driver;
            if (CityManager.Instance.GetCityByName(PlayerCompanyController.Instance.Company.City.Name, out CityController city))
            {
                transform.position = city.transform.position;
            }
            driver.OnTrailerChange += TrailerChange;
            driver.OnVehilceChange += VehicleChange;
        }
        private void OnDestroy()
        {
            driver.OnTrailerChange -= TrailerChange;
            driver.OnVehilceChange -= VehicleChange;
        }
        private void TrailerChange()
        {

        }

        private void VehicleChange()
        {

        }

        public bool Load(TransportGood good, float amount)
        {
            if (!Initialized) return false;
            return LoadingAgent.Load(good, amount, Vehicle, Trailer);
        }

        public bool Unload(TransportGood good, float amount)
        {
            if (!Initialized) return false;
            return LoadingAgent.Unload(good, amount, Vehicle, Trailer);

        }
        public void Load(GameData gameData)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void Save(ref GameData gameData)
        {
            // TODO
            throw new NotImplementedException();
        }

        private void OnDrawGizmos()
        {
            if (!Initialized && !driver.OnRoute) return;
            Gizmos.color = Color.magenta;
            Vector3 oldPos = transform.position;
            foreach (Vector3 Pos in _currPath.Waypoints)
            {
                Gizmos.DrawLine(oldPos, Pos);
                oldPos = Pos;
            }
        }
    }
}
