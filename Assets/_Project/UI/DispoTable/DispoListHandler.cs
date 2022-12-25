using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanySystem;
using ContractSystem;
using VehicleSystem;

namespace UISystem
{
    public class DispoListHandler : MonoBehaviour
    {
        private TransportCompany PlayerCompany;
        protected Transform ContractContent, VehicleContent, TrailerContent;
        [SerializeField] private ContractListItemController ContractPrefab;
        [SerializeField] private VehicleListItemController VehiclePrefab;
        [SerializeField] private TrailerListItemController TrailerPrefab;

        private void Start()
        {
            PlayerCompany = PlayerCompanyController.Instance.company;
        }
        public void LoadList()
        {
            LoadList(PlayerCompany.GetOpenContracts());
            LoadList(PlayerCompany.VehicleFleet.GetFreeVehicles());
            LoadList(PlayerCompany.VehicleFleet.GetFreeTrailers());
        }
        private void LoadList(List<TransportContract> Contracts)
        {
            foreach (TransportContract contract in Contracts)
            {
                ContractListItemController clic = Instantiate(ContractPrefab);
                clic.transform.SetParent(ContractContent);
                clic.Initlize(contract);
            }
        }
        private void LoadList(List<Vehicle> Vehicles)
        {
            foreach (Vehicle vehicle in Vehicles)
            {
                VehicleListItemController vlic = Instantiate(VehiclePrefab);
                vlic.transform.SetParent(ContractContent);
                vlic.Initlize(vehicle);
            }
        }
        private void LoadList(List<Trailer> Trailers)
        {
            foreach (Trailer trailer in Trailers)
            {
                TrailerListItemController tlic = Instantiate(TrailerPrefab);
                tlic.transform.SetParent(ContractContent);
                tlic.Initlize(trailer);
            }
        }
    }
}
