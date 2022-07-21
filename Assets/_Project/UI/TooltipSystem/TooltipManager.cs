using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooltipSystem
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance;
        [SerializeField] private Tooltip tooltip;
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
        }
        public static void ShowTooltip(string Text, string Header = "") => Instance.tooltip.Show(Text, Header);
        public static void HideTooltip() => Instance.tooltip.Hide();
    }
}
