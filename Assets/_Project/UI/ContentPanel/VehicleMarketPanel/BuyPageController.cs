using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;

namespace UISystem
{
    public class BuyPageController : TableContentController
    {
        public override void SetTableContent(string content)
        {
            Debug.Log(content);
            if (content == "Vehicle")
            {
                List<Vehicle> vehicles = VehicleMarket.Vehicles;
                List<ITableRow> rows = ExtractRows(vehicles);
                table.SetTableContent(rows);
                return;
            }
            else if (content == "NEWVehicle")
            {
                Debug.Log("setting NEW vehicle");
                List<VehicleSO> vehicleSOs = VehicleFactory.GetVehicles();
                List<ITableRow> rows = ExtractRows(vehicleSOs);
                table.SetTableContent(rows);
                return;
            }
            else if (content == "Trailer")
            {
                List<Trailer> trailers = VehicleMarket.Trailers;
                List<ITableRow> rows = ExtractRows(trailers);
                table.SetTableContent(rows);
                return;
            }
            else if (content == "NEWTrailer")
            {
                Debug.Log("setting NEW Trailer");
                List<TrailerSO> trialerSOs = VehicleFactory.GetTrailers();
                List<ITableRow> rows = ExtractRows(trialerSOs);
                table.SetTableContent(rows);
                return;
            }

            Debug.LogError($"BuyPageController - SetTableContent - Content '${content}' is unknown");
        }
        public void SetVehicleTable() => SetTableContent("Vehicle");
        public void SetNewVehicleTable() => SetTableContent("NEWVehicle");
        public void SetNewTrailerTable() => SetTableContent("NEWTrailer");
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
