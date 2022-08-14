using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanySystem;
using VehicleSystem;
namespace UISystem
{
    [RequireComponent(typeof(TableController))]
    public class BuyPageButtonHandler : MonoBehaviour
    {
        private PlayerCompany player;
        private TableController table;
        private void Start()
        {
            player = PlayerCompanyController.Company;
            table = GetComponent<TableController>();
        }

        public void BuyVehicle()
        {
            Vehicle vehicle = table.SelectedRow.OriginRecord as Vehicle;
            Vehicle buyedVehicle = VehicleMarket.Instance.BuyVehicle(vehicle, player.VehicleFleet);
            if (buyedVehicle != null)
            {
                table.RemoveRow(table.SelectedRow.transform.GetSiblingIndex());
                player.VehicleFleet.AddVehicle(buyedVehicle);
            }
            else
            {
                Debug.LogError("TODO - Error Handling for Enduser - Couldn't buy Vehicle");
            }
        }
        public void BuyTrailer()
        {
            Trailer trailer = table.SelectedRow.OriginRecord as Trailer;
            Trailer buyedTrailer = VehicleMarket.Instance.BuyVehicle(trailer, player.VehicleFleet);
            if (buyedTrailer != null)
            {
                table.RemoveRow(table.SelectedRow.transform.GetSiblingIndex());
                player.VehicleFleet.AddVehicle(buyedTrailer);
            }
            else
            {
                Debug.LogError("TODO - Error Handling for Enduser - Couldn't buy Trailer");
            }
        }
    }
}
