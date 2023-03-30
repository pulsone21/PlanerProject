using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanySystem;
using ContractSystem;
using VehicleSystem;
using Sirenix.OdinInspector;
using Utilities;

namespace UISystem
{
    public class DispoListHandler : MonoBehaviour
    {
        [SerializeField] private PlayerCompanyController PlayerCompany;
        [SerializeField] private Transform ContractContent, VehicleContent, TrailerContent;
        [SerializeField] private ContractListItemController ContractPrefab;
        [SerializeField] private VehicleListItemController VehiclePrefab;
        [SerializeField] private TrailerListItemController TrailerPrefab;
        [SerializeField] private TextSetter DefaultItem;
        private void OnEnable() => PlayerCompany = PlayerCompanyController.Instance;
        [Button("LoadList")]
        public void LoadList()
        {
            LoadList(PlayerCompany.Company.GetContractsByState(TransportContract.State.open));
            LoadList(PlayerCompany.Company.VehicleFleet.GetFreeVehicles());
            LoadList(PlayerCompany.Company.VehicleFleet.GetFreeTrailers());
        }
        public void LoadTrailerList() => LoadList(PlayerCompany.Company.VehicleFleet.GetFreeTrailers());
        public void LoadVehicleList() => LoadList(PlayerCompany.Company.VehicleFleet.GetFreeVehicles());
        public void LoadContractList() => LoadList(PlayerCompany.Company.GetContractsByState(TransportContract.State.open));
        private void LoadList(List<TransportContract> Contracts)
        {
            ContractContent.ClearAllChildren();
            if (Contracts.Count > 0)
            {
                foreach (TransportContract contract in Contracts)
                {
                    ContractListItemController clic = Instantiate(ContractPrefab);
                    clic.transform.SetParent(ContractContent);
                    clic.transform.localScale = Vector3.one;
                    clic.Initlize(contract, this);
                }
                return;
            }
            DisplayDefaultItem(ContractContent, "No open Contracts to show");
        }
        private void LoadList(List<Vehicle> Vehicles)
        {
            Debug.Log($"Trying to Display {Vehicles.Count} vehicle items");
            VehicleContent.ClearAllChildren();
            if (Vehicles.Count > 0)
            {
                foreach (Vehicle vehicle in Vehicles)
                {
                    VehicleListItemController vlic = Instantiate(VehiclePrefab);
                    vlic.transform.SetParent(VehicleContent);
                    vlic.transform.localScale = Vector3.one;
                    vlic.Initlize(vehicle, this);
                }
                return;
            }
            DisplayDefaultItem(VehicleContent, "No free vehicles to show");
        }
        private void LoadList(List<Trailer> Trailers)
        {
            TrailerContent.ClearAllChildren();
            if (Trailers.Count > 0)
            {
                foreach (Trailer trailer in Trailers)
                {
                    TrailerListItemController tlic = Instantiate(TrailerPrefab);
                    tlic.transform.SetParent(TrailerContent);
                    tlic.transform.localScale = Vector3.one;
                    tlic.Initlize(trailer, this);
                }
                return;
            }
            DisplayDefaultItem(TrailerContent, "No free trailer to show");
        }
        private void DisplayDefaultItem(Transform content, string text)
        {
            TextSetter defaultItem = Instantiate(DefaultItem);
            defaultItem.SetText(text);
            defaultItem.transform.SetParent(content);
            defaultItem.transform.localScale = Vector3.one;
        }
    }
}
