using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ChartSystem
{
    public abstract class ChartGenerator
    {
        protected ChartController controller;
        protected ChartGenerator(ChartController controller) => this.controller = controller;
        public abstract void CreateChart(List<int> values, string yLabel, string xLabel);
        protected virtual GameObject CreateSeparatorLine(GameObject linePrefab, Vector2 anchoredPosition)
        {
            GameObject go = GameObject.Instantiate(linePrefab);
            go.transform.SetParent(controller.ChartArea.transform, false);
            go.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
            return go;
        }
        protected virtual GameObject CreateLabel(GameObject prefab, Transform parent, string lableText, Vector2 anchoredPosition)
        {
            GameObject go = GameObject.Instantiate(prefab);
            go.transform.SetParent(parent, false);
            go.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
            go.GetComponent<TextMeshProUGUI>().text = lableText;
            return go;
        }

        protected abstract GameObject CreateDot(float xPosition, float yPosition);
    }
}
