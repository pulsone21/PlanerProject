using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UISystem
{
    public class TableController : MonoBehaviour
    {
        [SerializeField] private Transform rowContainer;
        [SerializeField] private Color evenColor, oddColor, selectedColor, highlightedColor;
        [SerializeField] private GameObject tableRowPrefab;
        [SerializeField] private TableRowController selectedRow;
        [SerializeField] private List<TableRowController> rows;
        public TableRowController SelectedRow => selectedRow;
        public Color EvenColor => evenColor;
        public Color OddColor => oddColor;
        public Color HighlightedColor => highlightedColor;
        public Color SelectedColor => selectedColor;
        public void AddNewRow(ITableRow Content) => rows.Add(CreateTableRow(Content));
        public void SetTableContent(List<ITableRow> rows)
        {
            this.rows.Clear();
            selectedRow = null;
            rowContainer.ClearAllChildren();
            foreach (ITableRow row in rows)
            {
                AddNewRow(row);
            }
        }
        public void RemoveRow(int index)
        {
            GameObject row = rowContainer.GetChild(index).gameObject;
            if (row)
            {
                rows.Remove(row.GetComponent<TableRowController>());
                Destroy(row);
                RecalcutateBackgrounds();
            }
        }
        private void RecalcutateBackgrounds()
        {
            foreach (TableRowController row in rows)
            {

                row.CalcBaseColor();
            }
        }
        private TableRowController CreateTableRow(ITableRow content)
        {
            GameObject go = Instantiate(tableRowPrefab);
            TableRowController trc = go.GetComponent<TableRowController>();
            go.transform.SetParent(rowContainer);
            go.name = "Row" + (go.transform.GetSiblingIndex() - 1).ToString();
            trc.SetContent(content, this);
            return trc;
        }
        internal void ChangeSelectedRow(TableRowController tableRowController)
        {
            if (selectedRow != null) selectedRow.Deselect();
            selectedRow = tableRowController;
        }
    }
}
