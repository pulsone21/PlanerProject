using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace UISystem
{
    public class NewVehicleTableController : TableController
    {
        [SerializeField] private GameObject trailerRowPrefab;
        private bool takeTrailerPrefab = false;
        public override void SetTableContent(List<ITableRow> rows)
        {
            this.rows.Clear();
            selectedRow = null;
            rowContainer.ClearAllChildren();
            takeTrailerPrefab = rows[0].GetRowContent().Length < 6;
            foreach (ITableRow row in rows)
            {
                AddNewRow(row);
            }
        }
        public override void AddNewRow(ITableRow Content)
        {
            GameObject prefab = takeTrailerPrefab ? trailerRowPrefab : null;
            rows.Add(CreateTableRow(Content, prefab));
        }

    }
}
