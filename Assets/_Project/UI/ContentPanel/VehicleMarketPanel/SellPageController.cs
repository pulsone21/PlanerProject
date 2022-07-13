using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem:
using CompanySystem;

namespace UISystem
{
    public class SellPageController : TableContentController
    {
        //TODO figure out where to store the vehicle from the company;

        private void Start()
        {
            market = GetComponent<VehicleMarket>();
        }
        public override void SetTableContent(string content)
        {
            if (content == "Vehicle")
            {
                List<ITableRow> rows = ExtractRows(market.Vehicles);
                table.SetTableContent(rows);
            }
            else if (content == "Trailer")
            {
                List<ITableRow> rows = ExtractRows(market.Trailers);
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
