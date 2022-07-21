using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

namespace TooltipSystem
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Header;
        [SerializeField] private TextMeshProUGUI Text;

        private void Awake() => gameObject.SetActive(false);

        internal void Show(string text, string header = "")
        {
            CalculatePosition();
            Header.text = header;
            if (header.Length < 1) Header.gameObject.SetActive(false);
            Text.text = text;

            gameObject.SetActive(true);
        }

        private void CalculatePosition()
        {
            Vector2 mousePos = Event.current.mousePosition;

            float width = Screen.width / mousePos.x;
            float height = Screen.height / mousePos.y;

            GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
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
