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
        [SerializeField] protected Transform rowContainer;
        [SerializeField] protected Color evenColor, oddColor, selectedColor, highlightedColor;
        [SerializeField] protected GameObject tableRowPrefab;
        [SerializeField] protected TableRowController selectedRow;
        [SerializeField] protected List<TableRowController> rows;
        public TableRowController SelectedRow => selectedRow;
        public Color EvenColor => evenColor;
        public Color OddColor => oddColor;
        public Color HighlightedColor => highlightedColor;
        public Color SelectedColor => selectedColor;
        public virtual void AddNewRow(ITableRow Content) => rows.Add(CreateTableRow(Content));
        public virtual void SetTableContent(List<ITableRow> rows)
        {
            this.rows.Clear();
            selectedRow = null;
            rowContainer.ClearAllChildren();
            foreach (ITableRow row in rows)
            {
                AddNewRow(row);
            }
        }
        public virtual void RemoveRow(int index)
        {
            GameObject row = rowContainer.GetChild(index).gameObject;
            if (row)
            {
                rows.Remove(row.GetComponent<TableRowController>());
                Destroy(row);
            }
        }
        public virtual void RecalcutateBackgrounds()
        {
            for (int i = 0; i < rows.Count; i++)
            {
                rows[i].CalcBaseColor(i);
            }
        }
        protected virtual TableRowController CreateTableRow(ITableRow content, GameObject Prefab = null)
        {
            GameObject go = Instantiate(Prefab ?? tableRowPrefab);
            TableRowController trc = go.GetComponent<TableRowController>();
            go.transform.SetParent(rowContainer);
            go.name = "Row" + (go.transform.GetSiblingIndex() - 1).ToString();
            trc.SetContent(content, this);
            return trc;
        }
        public virtual void ChangeSelectedRow(TableRowController tableRowController)
        {
            if (selectedRow != null) selectedRow.Deselect();
            selectedRow = tableRowController;
        }
    }
}
