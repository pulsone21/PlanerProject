using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utilities;

namespace ChartSystem
{
    public enum ChartType { LineChart, BarChart };
    public enum YAxisType { Dynamic, Logarithm };
    public class ChartController : MonoBehaviour
    {
        public static ChartController Instance;
        [Header("Prefabs and Visuals")]
        [SerializeField] private GameObject labelYPrefab;
        public GameObject LabelYPrefab => labelYPrefab;
        [SerializeField] private GameObject labelXPrefab;
        public GameObject LabelXPrefab => labelXPrefab;
        [SerializeField] private GameObject verticalLines;
        public GameObject VerticalLines => verticalLines;
        [SerializeField] private GameObject horizontalLines;
        public GameObject HorizontalLines => horizontalLines;
        [SerializeField] private Sprite circleSprite;
        public Sprite CircleSprite => circleSprite;


        [Space(5f), Header("Chart Areas")]
        [SerializeField] private GameObject labelXArea;
        public GameObject LabelXArea => labelXArea;
        [SerializeField] private GameObject labelYArea;
        public GameObject LabelYArea => labelYArea;
        [SerializeField] private GameObject chartArea;
        public GameObject ChartArea => chartArea;


        [Space(5f), Header("Apearence Settings")]
        [SerializeField] private float xOffSet;
        public float XOffSet => xOffSet;
        [SerializeField] private float yOffSet;
        public float YOffSet => yOffSet;
        [SerializeField] private Color lineColor;
        public Color LineColor => lineColor;
        [SerializeField] private float lineWidth = 3f;
        public float LineWidth => lineWidth;
        [SerializeField] private int separatorYCount = 10;
        public int SeparatorYCount => separatorYCount;

        [SerializeField] private ChartType chartType;
        public ChartType ChartType => chartType;
        private YAxisType yAxisType;
        public YAxisType YAxisType => yAxisType;
        private float chartHeight;
        public float ChartHeight => chartHeight;
        private float chartWidth;
        public float ChartWidth => chartWidth;

        private List<int> currentValues;
        private string currentXLabel, currentYLabel;
        private void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this;
            }

            yAxisType = YAxisType.Dynamic;
            Vector2 sizeDelta = chartArea.GetComponent<RectTransform>().sizeDelta;
            chartHeight = sizeDelta.y - yOffSet;
            chartWidth = sizeDelta.x - xOffSet;
        }

        // public static void GenerateChart(List<float> values, string yLabel, string xLabel, ChartType chartType, YAxisType YType)
        // {
        //     Instance.SetChartType(chartType);
        //     Instance.SetYType(YType);
        //     GenerateChart(values, yLabel, xLabel);
        // }

        //public static void GenerateChart(List<float> values, string yLabel, string xLabel) => Instance.CreateChart(values, yLabel, xLabel);

        public static void GenerateChart(List<int> values, string yLabel, string xLabel, ChartType chartType, YAxisType YType)
        {
            Instance.chartType = chartType;
            Instance.SetYType(YType);
            GenerateChart(values, yLabel, xLabel);
        }

        public static void GenerateChart(List<int> values, string yLabel, string xLabel)
        {
            Instance.currentValues = values;
            Instance.currentYLabel = yLabel;
            Instance.currentXLabel = xLabel;
            Instance.CreateChart(values, yLabel, xLabel);
        }

        public void SetChartType(ChartType chartType)
        {
            if (this.chartType == chartType) return;
            this.chartType = chartType;
            if (currentValues.Count > 0) CreateChart(currentValues, currentYLabel, currentXLabel);
        }

        public void SetLineChart() => SetChartType(ChartType.LineChart);
        public void SetBarChart() => SetChartType(ChartType.BarChart);
        public void SetYType(YAxisType yType)
        {
            if (yAxisType == yType) return;
            yAxisType = yType;
            if (currentValues.Count > 0) CreateChart(currentValues, currentYLabel, currentXLabel);
        }

        public void ClearOldChart()
        {
            chartArea.transform.ClearAllChildren();
            labelXArea.transform.ClearAllChildren();
            labelYArea.transform.ClearAllChildren();
        }

        private void CreateChart(List<int> values, string yLabel, string xLabel)
        {
            ClearOldChart();
            switch (chartType)
            {
                case ChartType.LineChart:
                    LineChartGenerator lineChartGenerator = new LineChartGenerator(this);
                    lineChartGenerator.CreateChart(values, yLabel, xLabel);
                    break;
                case ChartType.BarChart:
                    BarChartGenerator barChartGenerator = new BarChartGenerator(this);
                    barChartGenerator.CreateChart(values, yLabel, xLabel);
                    break;
            }
        }
    }
}