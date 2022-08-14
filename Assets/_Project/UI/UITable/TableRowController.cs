using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
namespace UISystem
{
    [RequireComponent(typeof(Image))]
    public class TableRowController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, Range(0f, 1f)] private float animationDuration;
        [SerializeField] private Image image;
        [SerializeField] private bool IsSelected = false;
        private ITableRow originRecord;
        public ITableRow OriginRecord => originRecord;
        [SerializeField] private TableController table;
        private Color baseColor;
        public void SetContent(ITableRow row, TableController table)
        {
            image = gameObject.GetComponent<Image>();
            this.table = table;
            originRecord = row;
            CalcBaseColor();
            SetBackground(baseColor);
            string[] contents = row.GetRowContent();
            for (int i = 0; i < contents.Length; i++)
            {
                transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = contents[i];
            }
        }
        private Color CalcBackground(int SibIndex)
        {
            if (table == null) return Color.white;
            Color col = table.OddColor;
            if (SibIndex % 2 == 0)
            {
                col = table.EvenColor;
            }
            return col;
        }
        public void CalcBaseColor() => baseColor = CalcBackground(transform.GetSiblingIndex());
        private void SetHighlight(bool state)
        {
            Color col = baseColor;
            if (table != null && state) col = table.HighlightedColor;
            SetBackground(col);
        }
        private void Select()
        {
            IsSelected = true;
            SetBackground(table.SelectedColor);
        }
        private void SetBackground(Color col) => image.DOColor(col, animationDuration);
        public void Deselect()
        {
            IsSelected = false;
            SetBackground(baseColor);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Clicked");
            Select();
            table.ChangeSelectedRow(this);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (IsSelected) return;
            SetHighlight(false);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsSelected) return;
            SetHighlight(true);
        }
    }
}
