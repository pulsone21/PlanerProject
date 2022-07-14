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
            }
            else if (content == "Trailer")
            {
                List<ITableRow> rows = ExtractRows(PlayerCompanyController.Company.VehicleFleet.Trailers);
                table.SetTableContent(rows);
            }

            Debug.LogError($"BuyPageController - SetTableContent - Content '${content}' is unknown");
        }
        private List<ITableRow> ExtractRows(List<Vehicle> vehicles)
        {
            List<ITableRow> rows = new List<ITableRow>();
            foreach (Vehicle vehicle in vehicles)
            {
                rows.Add(vehicle);
            }
            return rows;
        }
        private List<ITableRow> ExtractRows(List<Trailer> trailers)
        {
            List<ITableRow> rows = new List<ITableRow>();
            foreach (Trailer trailer in trailers)
            {
                rows.Add(trailer);
            }
            return rows;
        }
    }
}
