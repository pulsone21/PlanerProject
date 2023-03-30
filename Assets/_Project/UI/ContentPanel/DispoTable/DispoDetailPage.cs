using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EmployeeSystem;
using ContractSystem;
using Utilities;
using VehicleSystem;

namespace UISystem
{
    public class DispoDetailPage : MonoBehaviour
    {
        public static DispoDetailPage Instance;
        private void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this;
            }
        }
        [SerializeField] private GameObject defaultItem, RouteList, Details;
        [SerializeField] private TextMeshProUGUI DriverName, VehiclePlate, TrailerPlate;
        [SerializeField] private SkillTableController skillTableController;
        [SerializeField] private RouteListController routeListController;
        public Driver CurrentDriver { get; protected set; }
        private void OnEnable()
        {
            defaultItem.SetActive(true);
            routeListController.gameObject.SetActive(false);
            Details.SetActive(false);
        }
        private void OnDisable()
        {
            defaultItem.SetActive(true);
            routeListController.gameObject.SetActive(false);
            Details.SetActive(false);
        }
        public void SetDetails(Driver driver)
        {
            DriverName.text = driver.Name.ToString();
            skillTableController.SetEmployee(driver);
            VehiclePlate.text = driver.Vehicle != null ? driver.Vehicle.PlateText : "No vehicle assigned.";
            TrailerPlate.text = driver.Trailer != null ? driver.Trailer.PlateText : "No trailer assgined.";
            routeListController.SetRoute(driver.Route.Destinations.ToList());
            CurrentDriver = driver;
            defaultItem.SetActive(false);
            RouteList.SetActive(true);
            Details.SetActive(true);
        }
        public bool SetVehicle(Vehicle vehicle)
        {
            if (CurrentDriver.SetVehicle(vehicle))
            {
                VehiclePlate.text = CurrentDriver.Vehicle.PlateText;
                TrailerPlate.text = CurrentDriver.Trailer.PlateText ?? "No trailer assgined.";
                return true;
            }
            return false;
        }

        public bool SetTrailer(Trailer trailer)
        {
            if (CurrentDriver.SetVehicle(trailer))
            {
                VehiclePlate.text = CurrentDriver.Vehicle.PlateText;
                TrailerPlate.text = CurrentDriver.Trailer.PlateText ?? "No trailer assgined.";
                return true;
            }
            return false;
        }
        public void SaveRoute()
        {
            if (CurrentDriver == null) return;
            Queue<RouteDestination> route = routeListController.SaveRoute();
            if (route.Count > 0)
            {
                CurrentDriver.Route.RearrangeRoute(route);
                return;
            }
        }
    }
}