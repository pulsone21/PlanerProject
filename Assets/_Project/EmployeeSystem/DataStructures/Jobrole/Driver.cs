using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadSystem;
using ContractSystem;
using VehicleSystem;
using System;
using CompanySystem;

namespace EmployeeSystem
{
    [Serializable]
    public class Driver : Employee
    {
        [HideInInspector] public Vector3 CurrentPosition;
        private Route _route;
        private Vehicle _vehicle;
        private Trailer _trailer;
        public Driver(Canidate canidate) : base(canidate.Skills, canidate.Name, canidate.Birthday)
        {
#if !UNITY_EDITOR
            CurrentPosition = PlayerCompanyController.Instance.Company.City.Position;
#endif
            _route = new Route();
        }
        public Route Route => _route;
        public Vehicle Vehicle => _vehicle;
        public Trailer Trailer => _trailer;
        public bool OnRoute = false;
        public bool CanDrive => _vehicle != null;
        public Action OnVehilceChange, OnTrailerChange;
        public bool SetVehicle(Vehicle newVehicle)
        {
            if (_trailer != null)
            {
                Debug.LogWarning("Trailer needs to be removed befor vehicle");
                return false;
            }

            if (_vehicle == null)
            // case1 => we dont have a Vehicle Assignt
            {
                _vehicle = newVehicle;
                _vehicle.InUse = true;
                OnVehilceChange?.Invoke();
                return true;
            }
            if (_vehicle.IsLoaded)
            // case3 => vehicle assignt and loaded;
            {
                Debug.LogWarning("Vehicle is Loaded - Unload first");
                return false;
            }
            else
            // case2 => vehicle assignt but is empty
            {
                Vehicle oldVehicle = _vehicle;
                oldVehicle.InUse = false;
                _vehicle = newVehicle;
                _vehicle.InUse = true;
                OnVehilceChange?.Invoke();
                return true;
            }
        }
        public bool SetVehicle(Trailer newTrailer)
        {
            if (_vehicle == null) return false;
            if (!_vehicle.CanHandleTrailer(newTrailer.Type)) return false;
            if (_trailer == null)
            // case1 => we dont have a Trailer Assignt
            {
                _trailer = newTrailer;
                _trailer.InUse = true;
                OnTrailerChange?.Invoke();
                return true;
            }
            if (_trailer.IsLoaded)
            // case3 => trailer assignt and loaded;
            {
                Debug.LogWarning("Trailer is Loaded - Unload first");
                return false;
            }
            else
            // case2 => trailer assignt but is empty
            {
                Trailer oldTrailer = _trailer;
                oldTrailer.InUse = false;
                _trailer = newTrailer;
                _trailer.InUse = true;
                OnTrailerChange?.Invoke();
                return true;
            }
        }
        public bool RemoveVehicle()
        {
            if (_vehicle == null) return true;
            if (_trailer != null) return false;
            if (_vehicle.IsLoaded) return false;
            Vehicle oldVehicle = _vehicle;
            oldVehicle.InUse = false;
            _vehicle = null;
            OnVehilceChange?.Invoke();
            return true;
        }
        public bool RemoveTrailer()
        {
            if (_trailer == null) return true;
            if (_trailer.IsLoaded) return false;
            Trailer oldTrailer = _trailer;
            oldTrailer.InUse = false;
            _trailer = null;
            OnTrailerChange?.Invoke();
            return true;
        }
    }
}
