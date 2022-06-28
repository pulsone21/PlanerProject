using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadSystem;
using DG.Tweening;

namespace UISystem
{
    public class DarkModeController : MonoBehaviour
    {
        private MapVisualController.MapMode currentState = MapVisualController.MapMode.day;
        [SerializeField] private MapVisualController mapVisualController;
        [SerializeField] private RectTransform ToogleSiwtch;
        [SerializeField] private Vector3 DarkModePos, LightModePos;
        [SerializeField] private float AnimationDuration, AnimationDelay;
        private Tween currentTween;



        public void ToogleMode()
        {
            Debug.Log("Henlo, toogle mode");
            switch (currentState)
            {
                case MapVisualController.MapMode.day:
                    SetDarkMode();
                    break;
                case MapVisualController.MapMode.night:
                    SetLightMode();
                    break;
            }
        }

        private void SetLightMode()
        {
            if (currentState == MapVisualController.MapMode.day) return;
            currentState = MapVisualController.MapMode.day;
            mapVisualController.SetMapMode(currentState);
            MoveToogleSiwtch(currentState);
        }

        private void MoveToogleSiwtch(MapVisualController.MapMode currentState)
        {
            if (currentTween.IsActive()) currentTween.Kill();
            switch (currentState)
            {
                case MapVisualController.MapMode.day:
                    currentTween = ToogleSiwtch.DOLocalMoveX(LightModePos.x, AnimationDuration).SetDelay(AnimationDelay);
                    break;
                case MapVisualController.MapMode.night:
                    currentTween = ToogleSiwtch.DOLocalMoveX(DarkModePos.x, AnimationDuration).SetDelay(AnimationDelay);
                    break;

            }
        }
        private void SetDarkMode()
        {
            if (currentState == MapVisualController.MapMode.night) return;
            currentState = MapVisualController.MapMode.night;
            mapVisualController.SetMapMode(currentState);
            MoveToogleSiwtch(currentState);
        }
    }
}
