using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace UISystem
{
    public class VehicleMarketTableRowController : TableRowController
    {
        [SerializeField] private List<Color> colors = new List<Color>();
        public override void SetContent(ITableRow row, TableController table)
        {
            image = gameObject.GetComponent<Image>();
            this.table = table;
            originRecord = row;
            CalcBaseColor();
            SetBackground(baseColor);
            string[] contents = row.GetRowContent();
            for (int i = 0; i < contents.Length; i++)
            {
                TextMeshProUGUI tmpro = transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                if (i == 3)
                {
                    int condition = int.Parse(contents[i].Split("/")[1]);
                    tmpro.color = EvaluateConditionColor(condition);
                    tmpro.text = contents[i].Split("/")[0];
                    continue;
                }
                tmpro.text = contents[i];
            }
        }

        private Color EvaluateConditionColor(int condition)
        {
            Color col = colors[0];
            if (condition > 80)
            {
                col = colors[0];
            }
            else if (condition > 65)
            {
                col = colors[1];
            }
            else if (condition > 50)
            {
                col = colors[2];
            }
            else if (condition > 35)
            {
                col = colors[3];
            }
            else
            {
                col = colors[4];
            }
            return col;
        }
    }
}