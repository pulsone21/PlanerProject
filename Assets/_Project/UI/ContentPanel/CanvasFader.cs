using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UISystem
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasFader : SimpleFader
    {
        private CanvasGroup canvasGroup;
        private void Awake() => canvasGroup = GetComponent<CanvasGroup>();
        private void OnEnable() => FadeIn();

        private void OnDisable() => FadeOut();

        private void ActivateCanvasGroup()
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        public void SetActive() => gameObject.SetActive(true);
        public void SetInactive() => gameObject.SetActive(false);

        private void DeactivateCanvasGroup()
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
        public override void FadeIn()
        {
            ActivateCanvasGroup();
            canvasGroup.DOFade(1f, FadeDuration).SetDelay(FadeDelay);
        }
        public override void FadeOut()
        {
            DeactivateCanvasGroup();
            canvasGroup.DOFade(0f, FadeDuration).SetDelay(FadeDelay);
        }

    }
}
