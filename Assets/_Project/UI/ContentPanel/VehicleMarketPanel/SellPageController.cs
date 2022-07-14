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
        private PlayerCompanyController player;
        private void Start()
        {
            player = PlayerCompanyController.Instance;
        }
        public override void SetTableContent(string content)
        {
            if (content == "Vehicle")
            {
                List<ITableRow> rows = ExtractRows(player.Company.VehicleFleet.Vehicles);
                table.SetTableContent(rows);
            }
            else if (content == "Trailer")
            {
                List<ITableRow> rows = ExtractRows(player.Company.VehicleFleet.Vehicles);
                table.SetTableContent(rows);
            }

            Debug.LogError($"BuyPageController - SetTableContent - Content '${content}' is unknown");
        }

        private List<ITableRow> ExtractRows(Dictionary<Vehicle, int> vehicles)
        {
            List<ITableRow> rows = new List<ITableRow>();
            foreach (KeyValuePair<Vehicle, int> entry in vehicles)
            {
                for (int i = 0; i < entry.Value; i++)
                {
                    rows.Add(entry.Key);
                }
            }
            return rows;
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

        private List<ITableRow> ExtractRows(Dictionary<Trailer, int> trailers)
        {
            List<ITableRow> rows = new List<ITableRow>();
            foreach (KeyValuePair<Trailer, int> entry in trailers)
            {
                for (int i = 0; i < entry.Value; i++)
                {
                    rows.Add(entry.Key);
                }
            }
            return rows;
        }
    }
}
