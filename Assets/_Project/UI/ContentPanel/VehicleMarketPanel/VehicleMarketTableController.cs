using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;
using CompanySystem;

namespace UISystem
{
    public class VehicleMarketTableController : TableContentController
    {
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
                List<ITableRow> rows = ExtractRows(PlayerCompanyController.Instance.Company.VehicleFleet.Vehicles);
                table.SetTableContent(rows);
                table.gameObject.SetActive(true);
                SwitchButton(SellVehicleBtn);
                return;
            }
            if (content == "Trailer")
            {
                List<ITableRow> rows = ExtractRows(PlayerCompanyController.Instance.Company.VehicleFleet.Trailers);
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

    }
}
