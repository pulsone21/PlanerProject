using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChartSystem
{
    public class LineChartGenerator : ChartGenerator
    {
        private float yMaxValue = 0f;
        public LineChartGenerator(ChartController controller) : base(controller) { }

        public override void CreateChart(List<int> values, string yLabel, string xLabel)
        {
            yMaxValue = 0;
            foreach (int value in values) if (yMaxValue < value) yMaxValue = value;
            float chartWidth = controller.ChartWidth;
            float xOffSet = controller.XOffSet;
            float chartHeight = controller.ChartHeight;
            int separatorYCount = controller.SeparatorYCount;

            GameObject lastDot = null;
            float xIntervall = chartWidth / values.Count;
            for (int i = 0; i < values.Count; i++)
            {
                float xPosition = i * xIntervall + xOffSet;
                float yPosition = (values[i] / yMaxValue) * chartHeight;
                GameObject dot = CreateDot(xPosition, yPosition);

                if (lastDot != null) CreateConnection(lastDot.GetComponent<RectTransform>().anchoredPosition, dot.GetComponent<RectTransform>().anchoredPosition);
                lastDot = dot;
                CreateLabel(controller.LabelXPrefab, controller.LabelXArea.transform, xLabel + " " + i, new Vector2(xPosition, 5));
                CreateSeparatorLine(controller.VerticalLines, new Vector2(xPosition, 0f));
            }

            for (int i = 0; i <= separatorYCount; i++)
            {
                float normalizedIndex = i * 1f / separatorYCount;
                float yPosition = normalizedIndex * chartHeight;
                CreateLabel(controller.LabelYPrefab, controller.LabelYArea.transform, yLabel + " " + Mathf.RoundToInt(normalizedIndex * yMaxValue).ToString(), new Vector2(0, yPosition));
                CreateSeparatorLine(controller.HorizontalLines, new Vector2(-5f, yPosition));
            }
        }

        private GameObject CreateConnection(Vector3 positionDotA, Vector3 positionDotB)
        {
            GameObject go = new GameObject("DotConnection", typeof(Image));
            go.transform.SetParent(controller.ChartArea.transform, false);
            go.GetComponent<Image>().color = controller.LineColor;
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            Vector3 direction = (positionDotB - positionDotA).normalized;
            float distance = Vector2.Distance(positionDotA, positionDotB);
            rt.localEulerAngles = new Vector3(0, 0, Utilities.Utils.GetAngleFromVectorFloat(direction));
            rt.anchoredPosition = positionDotA + (direction * distance * .5f);
            rt.sizeDelta = new Vector2(distance, controller.LineWidth);
            return go;
        }
        protected override GameObject CreateDot(float xPosition, float yPosition)
        {
            GameObject go = new GameObject("Circle", typeof(Image));
            go.transform.SetParent(controller.ChartArea.transform, false);
            go.GetComponent<Image>().sprite = controller.CircleSprite;
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(xPosition, yPosition);
            rt.sizeDelta = new Vector2(11, 11);
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            return go;
        }
    }
}
