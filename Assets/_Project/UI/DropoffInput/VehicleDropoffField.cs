using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;
using TooltipSystem;
using System;

namespace UISystem
{
    [RequireComponent(typeof(TooltipTrigger))]
    public class VehicleDropoffField : DropoffField<Vehicle>
    {
        private TooltipTrigger tooltip;
        private void Awake()
        {
            tooltip = GetComponent<TooltipTrigger>();
            tooltip.Header = "Drag & Drop Vehicle to the Field";
        }
        public override void ClearValue()
        {
            if (currentValue == null) return;
            if (DispoDetailPage.Instance.CurrentDriver.RemoveVehicle())
            {
                currentValue = null;
                DisplayText.text = DefaultText;
                foreach (VehicleController vc in FindObjectsOfType<VehicleController>())
                {
                    if (vc.Driver == DispoDetailPage.Instance.CurrentDriver)
                    {
                        Destroy(vc.gameObject);
                        break;
                    }
                }
            }
        }
        protected override void SaveValue()
        {
            UpdateToolTip();
            DispoDetailPage.Instance.SetVehicle(currentValue);
        }

        private void UpdateToolTip()
        {
            Tuple<string, string> infos = currentValue.GetTooltipInfo();
            tooltip.Header = infos.Item1;
            tooltip.Description = infos.Item2;
        }
    }
}
