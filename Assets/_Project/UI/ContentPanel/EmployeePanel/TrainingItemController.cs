using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EmployeeSystem;
using UnityEngine.UI;
using DG.Tweening;
using TooltipSystem;

namespace UISystem
{
    public class TrainingItemController : ListItemController<SkillTraining>
    {
        [SerializeField] protected Graphic BackGround;
        [SerializeField, Range(0f, 1f)] protected float backgroundFadeTime, backgroundDelay;
        [SerializeField] private TextMeshProUGUI Title;
        [SerializeField] private TextMeshProUGUI ItemDescription;
        [SerializeField] private TooltipTrigger tooltipTrigger;
        public override void Initialize(SkillTraining item)
        {
            if (Initialized) return;
            this.item = item;
            Title.text = item.TitelID;
            string desc = $"Increasing {item.SkillName} for {item.SkillIncrease.ToString()}, takes {item.Duration} days to complete.";
            ItemDescription.text = desc;
            tooltipTrigger.Description = desc;
        }
        public override void SetContent() => TrainingCenter.SelectTraining(this);
        private bool currentState;
        public void SetActive(bool state)
        {
            if (state == currentState) return;
            currentState = state;
            FadeBackground(state);
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
                endCol.a = 10;
                BackGround.DOColor(endCol, backgroundFadeTime);
            }
        }
    }
}
