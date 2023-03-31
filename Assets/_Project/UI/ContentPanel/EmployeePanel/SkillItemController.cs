using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TooltipSystem;
using TMPro;
using UnityEngine.UI;
using EmployeeSystem;
using Utilities;

namespace UISystem
{
    public class SkillItemController : MonoBehaviour
    {
        [SerializeField] private TooltipTrigger tooltip;
        [SerializeField] private Gradient colorGradient;
        [SerializeField] private Image ValueBackground;
        [SerializeField] private TextMeshProUGUI Label, Value;

        public void SetContent(Skill skill)
        {
            Label.text = skill.Name;
            Value.text = skill.Value.ToString();
            tooltip.Header = $"{skill.Value} / {skill.MaxValue}";
            ValueBackground.color = EvalColor(skill.Value, skill.MaxValue);
        }
        private Color EvalColor(int value, int maxValue)
        {
            float normalized = Utils.NormalizeBetween0And1(value, 0, maxValue);
            return colorGradient.Evaluate(normalized);
        }
    }
}
