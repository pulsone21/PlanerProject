using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics;
using TMPro;
using System;
using UnityEngine.UI;
using DG.Tweening;
namespace UISystem
{
    public class IconButtonController : ToogleBtnVisualController, IExpandable
    {
        [SerializeField] private Color activeIconColor;
        [SerializeField] private Color passivIconColor;
        [SerializeField] private SVGImage Icon;
        [SerializeField] private GameObject label;
        private TextMeshProUGUI Label => label.GetComponent<TextMeshProUGUI>();
        private const int EXPANDED_WIDTH = 250;
        private const int SHRINKED_WIDTH = 75;
        [SerializeField, Range(0f, 1f)] private float IconFadeTime, labelDelayTime, labelFadeTime;
        [SerializeField] private bool Expandable, BackgroundFade;
        private bool IsExpanded = false;


        public override void SetBtnActive(bool force = false)
        {
            if (!force && IsActive) return;
            IsActive = true;
            Icon.DOColor(activeIconColor, IconFadeTime);
            if (BackgroundFade) FadeBackground(true);
            if (IsExpanded) label.GetComponent<TextMeshProUGUI>().color = activeIconColor;
        }
        public override void SetBtnPassive(bool force = false)
        {
            if (!force && !IsActive) return;
            IsActive = false;
            Icon.DOColor(passivIconColor, IconFadeTime);
            if (BackgroundFade) FadeBackground(false);
            if (IsExpanded) label.GetComponent<TextMeshProUGUI>().color = passivIconColor;
        }
        public void Expand(bool force = false)
        {
            if (!Expandable) return;
            if (!force) if (IsExpanded) return;
            IsExpanded = true;
            TextMeshProUGUI tmLabel = label.GetComponent<TextMeshProUGUI>();
            Color col = passivIconColor;
            if (IsActive) col = activeIconColor;
            Color startCol = col;
            startCol.a = 0f;
            tmLabel.color = startCol;
            label.SetActive(true);
            tmLabel.DOBlendableColor(col, labelFadeTime).SetDelay(labelDelayTime);
        }
        public void Shrink(bool force = false)
        {
            if (!Expandable) return;
            if (!force) if (!IsExpanded) return;
            TextMeshProUGUI tmLabel = label.GetComponent<TextMeshProUGUI>();
            tmLabel.DOFade(0f, labelFadeTime).OnComplete(() => label.SetActive(false));
            IsExpanded = false;
        }
    }
}
