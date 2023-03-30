using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;

namespace UISystem
{
    public class VehicleDropoffField : DropoffField<Vehicle>
    {
        public override void ClearValue()
        {
            if (currentValue == null) return;
            if (DispoDetailPage.Instance.CurrentDriver.RemoveVehicle())
            {
                currentValue = null;
                DisplayText.text = DefaultText;
            }
        }

        protected override void SaveValue() => DispoDetailPage.Instance.CurrentDriver.SetVehicle(currentValue);
    }
}
