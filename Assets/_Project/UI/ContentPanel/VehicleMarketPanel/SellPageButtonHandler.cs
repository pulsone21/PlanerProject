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
        private TransportCompany player;
        private TableController table;
        private void Start()
        {
            player = PlayerCompanyController.Instance.Company;
            table = GetComponent<TableController>();
        }

        public void SellVehicle()
        {
            foreach (TableRowController trc in table.SelectedRows)
            {
                Vehicle vehicle = trc.OriginRecord as Vehicle;

                if (player.VehicleFleet.RemoveVehicle(vehicle))
                {
                    table.RemoveRow(trc, true);
                }
                else
                {
                    Debug.LogError("TODO - Error Handling for Enduser - Couldn't sell Vehicle");
                }
            }
        }
        public void SellTrailer()
        {
            foreach (TableRowController trc in table.SelectedRows)
            {
                Trailer trailer = trc.OriginRecord as Trailer;
                if (player.VehicleFleet.RemoveVehicle(trailer))
                {
                    table.RemoveRow(trc);
                }
                else
                {
                    Debug.LogError("TODO - Error Handling for Enduser - Couldn't sell Trailer");
                }
            }
        }
    }
}
