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
        [SerializeField, Range(0f, 1f)] protected float animationDuration;
        [SerializeField] protected Image image;
        [SerializeField] protected bool IsSelected = false;
        protected ITableRow originRecord;
        public ITableRow OriginRecord => originRecord;
        [SerializeField] protected TableController table;
        protected Color baseColor;
        public virtual void SetContent(ITableRow row, TableController table)
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
        protected virtual Color CalcBackground(int SibIndex)
        {
            if (table == null) return Color.white;
            Color col = table.OddColor;
            if (SibIndex % 2 == 0)
            {
                col = table.EvenColor;
            }
            return col;
        }
        public virtual void CalcBaseColor(int index = int.MaxValue)
        {
            if (index == int.MaxValue) index = transform.GetSiblingIndex();
            baseColor = CalcBackground(index);
            SetBackground(baseColor);
        }
        protected virtual void SetHighlight(bool state)
        {
            Color col = baseColor;
            if (table != null && state) col = table.HighlightedColor;
            SetBackground(col);
        }
        protected virtual void Select()
        {
            IsSelected = true;
            SetBackground(table.SelectedColor);
        }
        protected virtual void SetBackground(Color col) => image.DOColor(col, animationDuration);
        public virtual void Deselect()
        {
            IsSelected = false;
            SetBackground(baseColor);
        }
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Clicked");
            Select();
            table.ChangeSelectedRow(this);
        }
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (IsSelected) return;
            SetHighlight(false);
        }
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (IsSelected) return;
            SetHighlight(true);
        }

        protected virtual void OnDestroy() => table.RecalcutateBackgrounds();

    }
}
