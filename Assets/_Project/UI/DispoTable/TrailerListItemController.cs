using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;
using Planer;
using UnityEngine.UI;

namespace UISystem
{
    public class TrailerListItemController : MonoBehaviour
    {

        private bool init = false;
        [SerializeField] private TextSetter Plate, Capacity;
        [SerializeField] private Image Cubic, Cooling, Forklift, Crane;
        private void Awake() => gameObject.SetActive(init);
        public void Initlize(Trailer Trailer)
        {
            if (init) return;
            init = true;
            Plate.SetText(Trailer.Type.ToString());
            Capacity.SetText("Capcity: " + Trailer.Capcity.ToString());
            Cubic.enabled = Trailer.CanHandleCUBIC;
            Cooling.enabled = Trailer.HasCooling;
            Forklift.enabled = Trailer.HasForklift;
            Crane.enabled = Trailer.HasCrane;
        }
    }
}
