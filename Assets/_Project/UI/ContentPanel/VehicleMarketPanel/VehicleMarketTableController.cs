using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;
using CompanySystem;

namespace UISystem
{
    public class VehicleMarketTableController : TableContentController
    {
        private readonly TransportCompany Player = PlayerCompanyController.Instance.Company;
        [SerializeField] private TableController TrailerTable;
        [SerializeField] private GameObject SellTrailerBtn, SellVehicleBtn, BuyVehicleBtn, BuyTrailerBtn, BuyNewVehicleBtn, BuyNewTrailerBtn;
        private bool showUsed = false;
        private bool buy = true;
        public override void SetTableContent(string content)
        {
            table.gameObject.SetActive(false);
            TrailerTable.gameObject.SetActive(false);

            if (buy)
            {
                if (content == "Vehicle")
                {
                    List<ITableRow> rows = new List<ITableRow>();
                    GameObject usedBtn = showUsed ? BuyVehicleBtn : BuyNewVehicleBtn;
                    if (showUsed)
                    {
                        List<Vehicle> vehicles = VehicleMarket.Vehicles;
                        rows = ExtractRows(vehicles);
                    }
                    else
                    {
                        List<VehicleSO> vehicleSOs = VehicleFactory.GetVehicles();
                        rows = ExtractRows(vehicleSOs);
                    }
                    table.SetTableContent(rows);
                    table.gameObject.SetActive(true);
                    SwitchButton(usedBtn);
                    return;
                }
                if (content == "Trailer")
                {
                    List<ITableRow> rows = new List<ITableRow>();
                    GameObject usedBtn = showUsed ? BuyTrailerBtn : BuyNewTrailerBtn;
                    if (showUsed)
                    {
                        List<Trailer> trailers = VehicleMarket.Trailers;
                        rows = ExtractRows(trailers);
                    }
                    else
                    {
                        List<TrailerSO> trialerSOs = VehicleFactory.GetTrailers();
                        rows = ExtractRows(trialerSOs);
                    }
                    TrailerTable.SetTableContent(rows);
                    TrailerTable.gameObject.SetActive(true);
                    SwitchButton(usedBtn);
                    return;
                }
            }
            if (content == "Vehicle")
            {
                List<ITableRow> rows = ExtractRows(Player.VehicleFleet.Vehicles);
                table.SetTableContent(rows);
                table.gameObject.SetActive(true);
                SwitchButton(SellVehicleBtn);
                return;
            }
            if (content == "Trailer")
            {
                List<ITableRow> rows = ExtractRows(Player.VehicleFleet.Trailers);
                TrailerTable.SetTableContent(rows);
                TrailerTable.gameObject.SetActive(true);
                SwitchButton(SellTrailerBtn);
                return;
            }
            Debug.LogError($"BuyPageController - SetTableContent - Content '${content}' is unknown");
        }
        private void SwitchButton(GameObject activeBtn)
        {
            SellTrailerBtn.SetActive(false);
            SellVehicleBtn.SetActive(false);
            BuyVehicleBtn.SetActive(false);
            BuyTrailerBtn.SetActive(false);
            BuyNewVehicleBtn.SetActive(false);
            BuyNewTrailerBtn.SetActive(false);
            activeBtn.SetActive(true);
        }
        public void SetVehicleTable() => SetTableContent("Vehicle");
        public void SetTrailerTable() => SetTableContent("Trailer");
        public void SetBuy(bool buying)
        {
            buy = buying;
            SetVehicleTable();
        }
        public void ShowUsed(bool show)
        {
            showUsed = show;
            SetVehicleTable();
        }
        private List<ITableRow> ExtractRows<T>(List<T> list) where T : ITableRow
        {
            List<ITableRow> rows = new List<ITableRow>();
            foreach (T entry in list)
            {
                rows.Add(entry);
            }
            return rows;
        }

        public void SellVehicle()
        {
            foreach (TableRowController trc in table.SelectedRows)
            {
                Vehicle vehicle = trc.OriginRecord as Vehicle;

                if (Player.VehicleFleet.RemoveVehicle(vehicle))
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
                if (Player.VehicleFleet.RemoveVehicle(trailer))
                {
                    table.RemoveRow(trc);
                }
                else
                {
                    Debug.LogError("TODO - Error Handling for Enduser - Couldn't sell Trailer");
                }
            }
        }

        public void BuyVehicle()
        {
            foreach (TableRowController trc in table.SelectedRows)
            {
                Vehicle vehicle = trc.OriginRecord as Vehicle;
                Vehicle buyedVehicle = VehicleMarket.Instance.BuyVehicle(vehicle, Player.VehicleFleet);
                if (buyedVehicle != null)
                {
                    table.RemoveRow(trc);
                    Player.VehicleFleet.AddVehicle(buyedVehicle);
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
                Vehicle buyedVehicle = VehicleMarket.Instance.BuyVehicle(vehicle, Player.VehicleFleet, true);
                if (buyedVehicle != null)
                {
                    Player.VehicleFleet.AddVehicle(buyedVehicle);
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
                Trailer buyedTrailer = VehicleMarket.Instance.BuyVehicle(trailer, Player.VehicleFleet);
                if (buyedTrailer != null)
                {
                    table.RemoveRow(trc);
                    Player.VehicleFleet.AddVehicle(buyedTrailer);
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
                Trailer buyedTrailer = VehicleMarket.Instance.BuyVehicle(trailer, Player.VehicleFleet, true);
                if (buyedTrailer != null)
                {
                    Player.VehicleFleet.AddVehicle(buyedTrailer);
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
