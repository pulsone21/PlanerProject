using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;
using Planer;
using UnityEngine.UI;

namespace UISystem
{
    public class VehicleListItemController : MonoBehaviour
    {
        private bool init = false;
        [SerializeField] private TextSetter Plate, Capacity;
        [SerializeField] private Image Cubic, Trailer, Cooling, Forklift, Crane;
        private void Awake() => gameObject.SetActive(init);
        public void Initlize(Vehicle vehicle)
        {
            if (init) return;
            init = true;
            Plate.SetText(vehicle.PlateText);
            Capacity.SetText("Capcity: " + vehicle.Capcity.ToString());
            Cubic.enabled = vehicle.CanHandleCUBIC;
            Trailer.enabled = vehicle.CanHandleTrailer;
            Cooling.enabled = vehicle.HasCooling;
            Forklift.enabled = vehicle.HasForklift;
            Crane.enabled = vehicle.HasCrane;
        }
    }
}
