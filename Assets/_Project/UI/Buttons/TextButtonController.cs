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

        [SerializeField] private Color activeIconColor;
        [SerializeField] private Color passivIconColor;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField, Range(0f, 1f)] private float LabelFadeTime, labelDelayTime;

        public override void SetBtnActive()
        {
            base.SetBtnActive();
            if (IsActive) return;
            label.DOColor(activeIconColor, LabelFadeTime).SetDelay(labelDelayTime);
        }

        public override void SetBtnPassive()
        {
            base.SetBtnPassive();
            if (!IsActive) return;
            label.DOColor(passivIconColor, LabelFadeTime).SetDelay(labelDelayTime);
        }


    }
}
