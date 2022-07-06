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
            IsActive = !IsActive;
            if (IsActive)
            {
                SetBtnActive();
                return;
            }
            SetBtnPassive();
        }

        public virtual void SetBtnActive()
        {
            if (IsActive) return;
            IsActive = true;
            FadeBackground(true);
        }
        public virtual void SetBtnPassive()
        {
            if (!IsActive) return;
            IsActive = false;
            FadeBackground(false);
        }

        protected void FadeBackground(bool FadeIn)
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
