using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UISystem
{
    [RequireComponent(typeof(HorizontalLayoutGroup), typeof(Image))]
    public class TableRowController : MonoBehaviour
    {
        private ITableRow originRecord;
        public ITableRow OriginRecord => originRecord;
        private TableController table;
        private Image image;

        private void Start()
        {
            HorizontalLayoutGroup hlg = GetComponent<HorizontalLayoutGroup>();
            hlg.childAlignment = TextAnchor.MiddleCenter;
            hlg.childForceExpandHeight = true;
            hlg.childForceExpandWidth = true;
            hlg.childControlHeight = true;
            hlg.childControlWidth = true;
            image = gameObject.GetComponent<Image>();
        }
        public void SetContent(ITableRow row, TableController table)
        {
            this.table = table;
            originRecord = row;
            SetBackground(transform.GetSiblingIndex());
            foreach (string value in row.GetRowContent())
            {
                CreateColumn(value);
            }
        }

        public void SetBackground(int SibIndex)
        {
            if (SibIndex % 2 == 0)
            {
                image.color = table.EvenColor;
                return;
            }
            image.color = table.OddColor;
        }

        private void CreateColumn(string content)
        {
            GameObject go = new GameObject();
            TextMeshProUGUI tmpro = go.AddComponent<TextMeshProUGUI>();
            tmpro.text = content;
            tmpro.alignment = TextAlignmentOptions.Center;
            tmpro.verticalAlignment = VerticalAlignmentOptions.Middle;
            tmpro.color = table.TextColor;
            go.transform.SetParent(transform);
            go.name = "Col" + (transform.childCount - 1).ToString();
        }

        public void Select()
        {
            image.color = table.HighlightedColor;
        }

        public void Deselect()
        {
            SetBackground(transform.GetSiblingIndex());
        }

        private void OnMouseUpAsButton()
        {
            table.ChangeSelectedRow(this);
        }


    }
}
