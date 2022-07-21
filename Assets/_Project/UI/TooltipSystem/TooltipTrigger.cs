using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TooltipSystem
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string Header;
        [SerializeField, TextArea] private string Text;

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipManager.ShowTooltip(Text, Header);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.HideTooltip();
        }
    }
}
