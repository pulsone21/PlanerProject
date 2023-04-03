using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using TMPro;

namespace UISystem
{
    public class TableController : MonoBehaviour
    {
        [SerializeField] protected Transform rowContainer;
        [SerializeField] protected Color evenColor, oddColor, selectedColor, highlightedColor;
        [SerializeField] protected GameObject tableRowPrefab;
        [SerializeField] protected List<TableRowController> selectedRows = new List<TableRowController>();
        [SerializeField] protected List<TableRowController> rows = new List<TableRowController>();
        public List<TableRowController> SelectedRows => selectedRows;
        public Color EvenColor => evenColor;
        public Color OddColor => oddColor;
        public Color HighlightedColor => highlightedColor;
        public Color SelectedColor => selectedColor;
        public virtual void AddNewRow(ITableRow Content) => rows.Add(CreateTableRow(Content));
        public virtual void SetTableContent(List<ITableRow> rows)
        {
            this.rows.Clear();
            selectedRows.Clear();
            rowContainer.ClearAllChildren();
            foreach (ITableRow row in rows)
            {
                CreateTableRow(row);
            }
        }
        public virtual void RemoveRow(TableRowController trc, bool clearSelectedRow = false)
        {
            rows.Remove(trc);
            if (clearSelectedRow) selectedRows.Remove(trc);
            Destroy(trc.gameObject);
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
            go.transform.localScale = Vector3.one;
            go.name = "Row" + (go.transform.GetSiblingIndex() - 1).ToString();
            trc.SetContent(content, this);
            return trc;
        }
        public virtual void ChangeSelectedRow(TableRowController tableRowController)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("I should add all indecies between already selected row and new row");
                if (selectedRows.Count > 0)
                {
                    int lowestIndex = int.MaxValue;
                    foreach (TableRowController trc in selectedRows)
                    {
                        lowestIndex = lowestIndex > trc.transform.GetSiblingIndex() ? trc.transform.GetSiblingIndex() : lowestIndex;
                    }
                    int newIndex = tableRowController.transform.GetSiblingIndex();
                    selectedRows.Clear();
                    for (int i = lowestIndex; i < newIndex; i++)
                    {
                        TableRowController trc = rowContainer.GetChild(i).GetComponent<TableRowController>();
                        trc.Select();
                        selectedRows.Add(trc);
                    }
                }
                selectedRows.Add(tableRowController);
                return;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                Debug.Log("Should be additive");
                selectedRows.Add(tableRowController);
                return;
            }
            Debug.Log("Clearing Selections");
            if (selectedRows.Count > 0) DeselectAll();
            selectedRows.Add(tableRowController);
        }

        public void DeselectAll()
        {
            foreach (TableRowController row in selectedRows)
            {
                row.Deselect();
            }
            selectedRows.Clear();
        }
    }
}
