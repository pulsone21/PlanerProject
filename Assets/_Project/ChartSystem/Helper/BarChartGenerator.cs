using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChartSystem
{
    public class BarChartGenerator : ChartGenerator
    {
        private float yMaxValue = 0f;
        public BarChartGenerator(ChartController controller) : base(controller) { }
        public override void CreateChart(List<int> values, string yLabel, string xLabel)
        {
            yMaxValue = 0;
            float chartWidth = controller.ChartWidth;
            float xOffSet = controller.XOffSet;
            float chartHeight = controller.ChartHeight;
            int separatorYCount = controller.SeparatorYCount;
            foreach (int value in values) if (yMaxValue < value) yMaxValue = value;
            GameObject lastDot = null;
            List<Vector2> trendLineDots = new List<Vector2>();
            float xIntervall = chartWidth / values.Count;
            for (int i = 0; i < values.Count; i++)
            {
                float xPosition = i * xIntervall + xOffSet;
                float yPosition = (values[i] / yMaxValue) * chartHeight;
                GameObject dot = CreateDot(xPosition, yPosition);

                if (lastDot != null)
                {
                    trendLineDots.Add(CreateTrendlineDots(lastDot.GetComponent<RectTransform>(), dot.GetComponent<RectTransform>()));
                }
                else
                {
                    trendLineDots.Add(new Vector3(xPosition, yPosition, 0));
                };
                lastDot = dot;
                CreateLabel(controller.LabelXPrefab, controller.LabelXArea.transform, xLabel + " " + i, new Vector2(xPosition, 5));
                //CreateSeparatorLine(controller.VerticalLines, new Vector2(xPosition, 0f));
            }

            for (int i = 0; i <= separatorYCount; i++)
            {
                float normalizedIndex = i * 1f / separatorYCount;
                float yPosition = normalizedIndex * chartHeight;
                CreateLabel(controller.LabelYPrefab, controller.LabelYArea.transform, yLabel + " " + Mathf.RoundToInt(normalizedIndex * yMaxValue).ToString(), new Vector2(0, yPosition));
                CreateSeparatorLine(controller.HorizontalLines, new Vector2(-5f, yPosition));
            }

            for (int i = 0; i < trendLineDots.Count - 1; i++)
            {
                CreateTrendLine(trendLineDots[i], trendLineDots[i + 1]);
            }
        }
        private Vector2 CreateTrendlineDots(RectTransform positionDotB, RectTransform positionDotA)
        {
            float avgHeight = (positionDotA.sizeDelta.y + positionDotB.sizeDelta.y) / 2;
            return new Vector2(positionDotA.anchoredPosition.x, avgHeight);
        }


        private void CreateTrendLine(Vector3 positionDotA, Vector3 positionDotB)
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
        }

        protected override GameObject CreateDot(float xPosition, float height)
        {
            GameObject go = new GameObject("Bar", typeof(Image));
            go.transform.SetParent(controller.ChartArea.transform, false);
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(xPosition, 0);
            rt.sizeDelta = new Vector2(25, height);
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0.5f, 0);
            return go;
        }
    }
}
