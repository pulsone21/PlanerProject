using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;
using CompanySystem;
using Planer;

namespace UISystem
{
    public class SellPageController : TableContentController
    {
        public override void SetTableContent(string content)
        {
            if (content == "Vehicle")
            {
                List<ITableRow> rows = ExtractRows(PlayerCompanyController.Company.VehicleFleet.Vehicles);
                table.SetTableContent(rows);
                return;
            }
            else if (content == "Trailer")
            {
                List<ITableRow> rows = ExtractRows(PlayerCompanyController.Company.VehicleFleet.Trailers);
                table.SetTableContent(rows);
                return;
            }

            Debug.LogError($"BuyPageController - SetTableContent - Content '${content}' is unknown");
        }
        public void SetVehicleTable() => SetTableContent("Vehicle");
        public void SetTrailerTable() => SetTableContent("Trailer");
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
