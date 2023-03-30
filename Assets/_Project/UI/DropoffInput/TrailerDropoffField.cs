using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;

namespace UISystem
{
    public class TrailerDropoffField : DropoffField<Trailer>
    {
        public override void ClearValue()
        {
            if (currentValue == null) return;
            if (DispoDetailPage.Instance.CurrentDriver.RemoveTrailer())
            {
                currentValue = null;
                DisplayText.text = DefaultText;
            }
        }

        protected override void SaveValue() => DispoDetailPage.Instance.CurrentDriver.SetVehicle(currentValue);
    }
}
