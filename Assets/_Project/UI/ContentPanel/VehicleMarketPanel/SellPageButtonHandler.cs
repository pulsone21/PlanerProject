using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanySystem;
using VehicleSystem;

namespace UISystem
{
    [RequireComponent(typeof(TableController))]
    public class SellPageButtonHandler : MonoBehaviour
    {
        private PlayerCompany player;
        private TableController table;
        private void Start()
        {
            player = PlayerCompanyController.Company;
            table = GetComponent<TableController>();
        }

        public void SellVehicle()
        {
            Vehicle vehicle = table.SelectedRow.OriginRecord as Vehicle;

            if (player.VehicleFleet.RemoveVehicle(vehicle))
            {
                table.RemoveRow(table.SelectedRow.transform.GetSiblingIndex());
            }
            else
            {
                Debug.LogError("TODO - Error Handling for Enduser - Couldn't sell Vehicle");
            }
        }
        public void SellTrailer()
        {
            Trailer trailer = table.SelectedRow.OriginRecord as Trailer;
            if (player.VehicleFleet.RemoveVehicle(trailer))
            {
                table.RemoveRow(table.SelectedRow.transform.GetSiblingIndex());
            }
            else
            {
                Debug.LogError("TODO - Error Handling for Enduser - Couldn't sell Trailer");
            }
        }
    }
}
