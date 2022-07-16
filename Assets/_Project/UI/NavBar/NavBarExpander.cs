using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics;
using DG.Tweening;

namespace UISystem
{
    public class NavBarExpander : MonoBehaviour
    {
        private bool IsExpanded = false;
        [SerializeField] private NavBarController controller;
        [SerializeField] private AnimationCurve Curve;
        private const int POINTI_LEFT = 180;
        private const int POINTI_RIGHT = 0;
        [SerializeField] private RectTransform ownTransform;

        public void ToogleExpand()
        {
            IsExpanded = controller.ToggleExpand();
            StartCoroutine(RotateIcon(IsExpanded));
        }

        public void Expand()
        {
            if (IsExpanded) return;
            IsExpanded = true;
            StartCoroutine(RotateIcon(IsExpanded));
        }

        public void Shrink()
        {
            if (!IsExpanded) return;
            IsExpanded = false;
            StartCoroutine(RotateIcon(IsExpanded));
        }

        IEnumerator RotateIcon(bool isExpanded)
        {

            if (isExpanded)
            {
                for (float i = 1; i > 0; i -= Time.deltaTime)
                {
                    float nextRotation = (Curve.Evaluate(i) * (POINTI_RIGHT - POINTI_LEFT) + POINTI_LEFT);
                    ownTransform.rotation = Quaternion.Euler(0, nextRotation, 0);
                    yield return null;
                }
            }
            else
            {
                for (float i = 0; i < 1; i += Time.deltaTime)
                {
                    float nextRotation = (Curve.Evaluate(i) * (POINTI_RIGHT - POINTI_LEFT) + POINTI_LEFT);
                    ownTransform.rotation = Quaternion.Euler(0, nextRotation, 0);
                    yield return null;
                }
            }
        }
    }
}
