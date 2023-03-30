using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanySystem;
using EmployeeSystem;
using VehicleSystem;
using Sirenix.OdinInspector;
using ContractSystem;
using Utilities;
using RoadSystem;


namespace Planer
{
    public class TestingController : MonoBehaviour
    {
        public VehicleController vehicleControllerUI;
        public Transform UIContainer;
        public TransportContract Contract;

        [Button("Spawn Vehicle")]
        private void SpawnVehicle()
        {
            Driver driver = PlayerCompanyController.Instance.Company.EmployeeManager.GetFreeDriver();
            driver.Route.AddContract(Contract);
            Vehicle vehicle = PlayerCompanyController.Instance.Company.VehicleFleet.GetFreeVehicles()[0];
            driver.SetVehicle(vehicle);
            CityManager.Instance.GetCityByName(PlayerCompanyController.Instance.Company.City.Name, out CityController city);
            VehicleController ui = Instantiate(vehicleControllerUI, UIContainer);
            ui.transform.position = city.transform.position;
            ui.Initialize(driver);
        }
    }
}
