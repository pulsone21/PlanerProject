using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeSystem;
using DG.Tweening;
using System;

namespace UISystem
{
    public class TimeUIController : UIController, IExpandable
    {
        [SerializeField] private RectTransform ownRect;
        [SerializeField] private CanvasGroup ActionBar;

        [SerializeField, Range(0f, 1f)] private float ExpandDuration, ExpandDelay, ShrinkDuration, ShrinkDelay;
        private bool IsExpanded = false;
        private const int EXPANDED_HIGHT = 95;
        private const int SHRINKED_HEIGHT = 45;

        public void ToogleExpand()
        {
            Debug.Log("toogling Expand");
            IsExpanded = !IsExpanded;
            if (IsExpanded)
            {
                Expand(true);
            }
            else
            {
                Shrink(true);
            }
        }

        public void Expand(bool force = false)
        {
            if (!force) if (IsExpanded) return;
            IsExpanded = true;
            ActionBar.gameObject.SetActive(true);
            ownRect.DOSizeDelta(new Vector2(ownRect.sizeDelta.x, EXPANDED_HIGHT), ExpandDuration).SetDelay(ExpandDelay).OnComplete(() => FadeCanvasGroup(true));
        }

        public void Shrink(bool force = false)
        {
            if (!force) if (!IsExpanded) return;
            IsExpanded = false;
            FadeCanvasGroup(false);
            ownRect.DOSizeDelta(new Vector2(ownRect.sizeDelta.x, SHRINKED_HEIGHT), ShrinkDuration).SetDelay(ShrinkDelay);
        }

        private void FadeCanvasGroup(bool FadeIn)
        {
            if (FadeIn)
            {
                ActionBar.DOFade(1f, 0.2f);
            }
            else
            {
                ActionBar.DOFade(0f, 0.2f).OnComplete(() => ActionBar.gameObject.SetActive(false));
            }
        }

        public void FastForwardDays(int days)
        {
            TimeStamp currStamp = TimeManager.Instance.CurrentTimeStamp;
            long tempMinutes = currStamp.InMinutes() + (days * TimeStamp.DAY_IN_MIN);
            TimeStamp tempStamp = TimeStamp.GetTimeStampFromTotalMinutes(tempMinutes);
            TimeStamp newTimeStamp = new TimeStamp(0, 8, tempStamp.Day, tempStamp.Month, tempStamp.Year, tempStamp.Season);
            TimeManager.Instance.FastForwardToTimestamp(newTimeStamp);
        }
    }
}
