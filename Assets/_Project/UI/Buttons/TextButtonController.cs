using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
namespace UISystem
{
    public class TextButtonController : ToogleBtnVisualController
    {

        [SerializeField] private Color activeTextColor;
        [SerializeField] private Color passivTextColor;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField, Range(0f, 1f)] private float LabelFadeTime, labelDelayTime;

        private void Awake()
        {
            IsActive = false;
        }

        public override void SetBtnActive(bool force = false)
        {
            if (!force && IsActive) return;
            IsActive = true;
            FadeBackground(true);
            label.DOColor(activeTextColor, LabelFadeTime).SetDelay(labelDelayTime);
        }

        public override void SetBtnPassive(bool force = false)
        {
            if (!force && !IsActive) return;
            IsActive = false;
            FadeBackground(false);
            label.DOColor(passivTextColor, LabelFadeTime).SetDelay(labelDelayTime);
        }
    }
}
