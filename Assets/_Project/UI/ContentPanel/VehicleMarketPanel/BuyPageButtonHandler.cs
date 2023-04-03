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
        private TransportCompany player;
        private TableController table;
        private void Start()
        {
            player = PlayerCompanyController.Instance.Company;
            table = GetComponent<TableController>();
        }

        public void BuyVehicle()
        {
            foreach (TableRowController trc in table.SelectedRows)
            {
                Vehicle vehicle = trc.OriginRecord as Vehicle;
                Vehicle buyedVehicle = VehicleMarket.Instance.BuyVehicle(vehicle, player.VehicleFleet);
                if (buyedVehicle != null)
                {
                    table.RemoveRow(trc);
                    player.VehicleFleet.AddVehicle(buyedVehicle);
                    // TODO GIVE PLAYER FEEDBACK ON PURCHASE
                }
                else
                {
                    Debug.LogError("TODO - Error Handling for Enduser - Couldn't buy Vehicle");
                }
            }
        }
        public void BuyNewVehicle()
        {
            foreach (TableRowController trc in table.SelectedRows)
            {
                VehicleSO vehicleSO = trc.OriginRecord as VehicleSO;
                Vehicle vehicle = new(vehicleSO, true);
                Vehicle buyedVehicle = VehicleMarket.Instance.BuyVehicle(vehicle, player.VehicleFleet, true);
                if (buyedVehicle != null)
                {
                    player.VehicleFleet.AddVehicle(buyedVehicle);
                    // TODO GIVE PLAYER FEEDBACK ON PURCHASE
                }
                else
                {
                    Debug.LogError("TODO - Error Handling for Enduser - Couldn't buy Vehicle");
                }
            }
        }
        public void BuyTrailer()
        {
            foreach (TableRowController trc in table.SelectedRows)
            {
                Trailer trailer = trc.OriginRecord as Trailer;
                Trailer buyedTrailer = VehicleMarket.Instance.BuyVehicle(trailer, player.VehicleFleet);
                if (buyedTrailer != null)
                {
                    table.RemoveRow(trc);
                    player.VehicleFleet.AddVehicle(buyedTrailer);
                    // TODO GIVE PLAYER FEEDBACK ON PURCHASE
                }
                else
                {
                    Debug.LogError("TODO - Error Handling for Enduser - Couldn't buy Trailer");
                }
            }

        }

        public void BuyNewTrailer()
        {
            foreach (TableRowController trc in table.SelectedRows)
            {
                TrailerSO trailerSO = trc.OriginRecord as TrailerSO;
                Trailer trailer = new(trailerSO, true);
                Trailer buyedTrailer = VehicleMarket.Instance.BuyVehicle(trailer, player.VehicleFleet, true);
                if (buyedTrailer != null)
                {
                    player.VehicleFleet.AddVehicle(buyedTrailer);
                    // TODO GIVE PLAYER FEEDBACK ON PURCHASE
                }
                else
                {
                    Debug.LogError("TODO - Error Handling for Enduser - Couldn't buy Trailer");
                }
            }
        }
    }
}
