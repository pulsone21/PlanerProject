using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;


namespace TooltipSystem
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string Header;
        [SerializeField, TextArea] private string Text;
        private float hoverDelay = 0.5f;
        private float currentDelay = 0f;
        private Sequence sequence;

        public void OnPointerEnter(PointerEventData eventData)
        {
            sequence = DOTween.Sequence();
            sequence.Append(DOTween.To(() => currentDelay, x => currentDelay = x, 1, hoverDelay).OnComplete(() => TooltipManager.ShowTooltip(Text, Header)));
            sequence.Play();
        }



        public void OnPointerExit(PointerEventData eventData)
        {
            sequence.Kill();
            currentDelay = 0;
            TooltipManager.HideTooltip();
        }
    }
}
