using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace UISystem
{
    public class MapPanelController : SimpleFader
    {
        [SerializeField] private CameraController cameraController;
        [SerializeField] private Image myBackground;
        [SerializeField] private GameObject Environment;

        private void OnEnable() => ShowMapView();
        private void OnDisable() => CloseMapView();

        private void ShowMapView()
        {
            FadeOut();
            cameraController.enabled = true;
        }

        private void CloseMapView()
        {
            cameraController.enabled = false;
            FadeIn();
        }

        public override void FadeIn()
        {
            Color targetColor = myBackground.color;
            targetColor.a = 1f;
            myBackground.DOBlendableColor(targetColor, FadeDuration).SetDelay(FadeDelay).OnComplete(() => Environment.SetActive(false));
        }

        public override void FadeOut()
        {
            Color targetColor = myBackground.color;
            targetColor.a = 0f;
            myBackground.DOBlendableColor(targetColor, FadeDuration).SetDelay(FadeDelay).OnComplete(() => Environment.SetActive(true));
        }
    }
}
