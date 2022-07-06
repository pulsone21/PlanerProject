using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UISystem
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasFader : SimpleFader
    {
        [SerializeField] private CanvasGroup canvasGroup;
        private void OnEnable() => FadeIn();

        private void OnDisable() => FadeOut();

        private void ActivateCanvasGroup()
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        private void DeactivateCanvasGroup()
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
        public override void FadeIn() => canvasGroup.DOFade(1f, FadeDuration).SetDelay(FadeDelay).OnComplete(ActivateCanvasGroup);
        public override void FadeOut() => canvasGroup.DOFade(0f, FadeDuration).SetDelay(FadeDelay).OnComplete(DeactivateCanvasGroup);

    }
}
