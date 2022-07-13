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
        [SerializeField] private Color evenColor, oddColor, highlightedColor;
        private List<TableRowController> rows;
        private TableRowController selectedRow;
        public TableRowController SelectedRow => selectedRow;
        public Color EvenColor => evenColor;
        public Color OddColor => oddColor;
        public Color HighlightedColor => highlightedColor;
        public void AddNewRow(ITableRow Content) => rows.Add(CreateTableRow(Content));
        public void SetTableContent(List<ITableRow> rows)
        {
            rows.Clear();
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
                Destroy(row);
            }
        }
        private TableRowController CreateTableRow(ITableRow content)
        {
            GameObject go = new GameObject();
            TableRowController trc = go.AddComponent<TableRowController>();
            go.transform.SetParent(rowContainer);
            go.name = "Row" + (go.transform.GetSiblingIndex() - 1).ToString();
            trc.SetContent(content, this);
            return trc;
        }

        internal void ChangeSelectedRow(TableRowController tableRowController)
        {
            if (selectedRow != null) selectedRow.Deselect();
            selectedRow = tableRowController;
            selectedRow.Select();
        }
    }
}
