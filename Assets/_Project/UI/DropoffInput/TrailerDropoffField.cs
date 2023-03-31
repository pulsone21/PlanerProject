using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;
using TooltipSystem;
using System;

namespace UISystem
{
    [RequireComponent(typeof(TooltipTrigger))]
    public class TrailerDropoffField : DropoffField<Trailer>
    {
        private TooltipTrigger tooltip;
        private void Awake()
        {
            tooltip = GetComponent<TooltipTrigger>();
            tooltip.Header = "Drag & Drop Trailer to the Field";
        }
        public override void ClearValue()
        {
            if (currentValue == null) return;
            if (DispoDetailPage.Instance.CurrentDriver.RemoveTrailer())
            {
                currentValue = null;
                DisplayText.text = DefaultText;
            }
        }

        protected override void SaveValue()
        {
            UpdateToolTip();
            DispoDetailPage.Instance.CurrentDriver.SetVehicle(currentValue);
        }

        private void UpdateToolTip()
        {
            Tuple<string, string> infos = currentValue.GetTooltipInfo();
            tooltip.Header = infos.Item1;
            tooltip.Description = infos.Item2;
        }
    }
}
