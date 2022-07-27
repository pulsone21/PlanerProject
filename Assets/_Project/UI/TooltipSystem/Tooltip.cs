using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
namespace TooltipSystem
{
    [ExecuteInEditMode()]
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Header;
        [SerializeField] private TextMeshProUGUI Text;
        [SerializeField] private LayoutElement layoutElement;
        [SerializeField] private int MaxLetterPerLine = 41;

        public float xOffset, yOffset;
        private void Awake() => gameObject.SetActive(false);

        internal void Show(string text, string header = "")
        {
            Header.text = header;
            if (header.Length < 1) Header.gameObject.SetActive(false);
            Text.text = text;
            CheckWrappLimit();
            gameObject.SetActive(true);
        }

        private void Update()
        {
            CalculatePosition();
        }

        private void CheckWrappLimit()
        {
            bool warpping = Header.text.Length > MaxLetterPerLine || Text.text.Length > MaxLetterPerLine;
            layoutElement.enabled = warpping;
        }

        private void CalculatePosition()
        {
            Vector3 mousePos = Input.mousePosition;
            float pivotX = mousePos.x / Screen.width;
            float pivotY = mousePos.y / Screen.height;
            pivotX = pivotX >= 0.5f ? 1 : 0;
            pivotY = pivotY >= 0.5f ? 1 : 0;
            float offX = xOffset * (pivotX == 1 ? 1 : -1);
            float offY = yOffset * (pivotY == 1 ? 1 : -1);
            transform.position = new Vector2(mousePos.x - offX, mousePos.y - offY);
            GetComponent<RectTransform>().pivot = new Vector2(pivotX, pivotY);
        }

        internal void Hide()
        {
            gameObject.SetActive(false);
            Header.gameObject.SetActive(true);
            Header.text = "Header";
            Text.text = "Text here";
        }

    }
}
