using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VectorGraphics;
using System;
using DG.Tweening;


namespace UISystem
{
    public abstract class ToogleBtnVisualController : MonoBehaviour
    {
        [SerializeField] protected Graphic BackGround;
        [SerializeField, Range(0f, 1f)] protected float backgroundFadeTime, backgroundDelay;
        protected bool IsActive = false;

        public virtual void ToogleBtn()
        {
            if (IsActive)
            {
                SetBtnActive();
                return;
            }
            SetBtnPassive();
        }

        public virtual void SetBtnActive(bool force = false)
        {
            if (!force && IsActive) return;
            IsActive = true;
            FadeBackground(true);
        }
        public virtual void SetBtnPassive(bool force = false)
        {
            if (!force && !IsActive) return;
            IsActive = false;
            FadeBackground(false);
        }

        protected virtual void FadeBackground(bool FadeIn)
        {
            Color startCol = BackGround.color;

            if (FadeIn)
            {
                Color endCol = startCol;
                endCol.a = 100;
                BackGround.DOColor(endCol, backgroundFadeTime).SetDelay(backgroundDelay);
            }
            else
            {
                Color endCol = startCol;
                endCol.a = 0;
                BackGround.DOColor(endCol, backgroundFadeTime);
            }
        }
    }
}
