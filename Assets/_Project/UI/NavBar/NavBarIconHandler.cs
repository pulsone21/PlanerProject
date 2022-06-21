using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace UISystem
{
    public class NavBarIconHandler : MonoBehaviour, IExpandable
    {
        [SerializeField] private GameObject Label;
        private TextMeshProUGUI tmLabel => Label.GetComponent<TextMeshProUGUI>();
        private bool IsExpanded = false;
        [SerializeField] private float labelFadeTime, labelDelayTime;

        public void Expand(bool force = false)
        {
            if (!force) if (IsExpanded) return;
            IsExpanded = true;
            Label.SetActive(true);
            Color targetCol = tmLabel.color;
            targetCol.a = 1f;
            tmLabel.DOBlendableColor(targetCol, labelFadeTime).SetDelay(labelDelayTime);
        }
        public void Shrink(bool force = false)
        {
            if (!force) if (!IsExpanded) return;
            IsExpanded = false;
            Color targetCol = tmLabel.color;
            targetCol.a = 0f;
            tmLabel.DOBlendableColor(targetCol, labelFadeTime).OnComplete(() => Label.SetActive(false));

        }
    }
}
