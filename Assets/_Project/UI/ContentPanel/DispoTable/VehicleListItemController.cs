using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;
using Planer;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace UISystem
{
    public class VehicleListItemController : DragableItem
    {
        private bool init = false;
        [SerializeField] private TextSetter Plate, Capacity;
        [SerializeField] private Image Cubic, Trailer, Cooling, Forklift, Crane;
        private DispoListHandler DispoListHandler;
        public Vehicle Vehicle { get; protected set; }
        protected override void Awake()
        {
            base.Awake();
            gameObject.SetActive(init);
        }
        public void Initlize(Vehicle vehicle, DispoListHandler dlh)
        {
            if (init) return;
            init = true;
            Vehicle = vehicle;
            DispoListHandler = dlh;
            Plate.SetText(vehicle.PlateText);
            Capacity.SetText("Capcity: " + vehicle.MaxCapacity.ToString());
            Cubic.enabled = vehicle.CanHandleCUBIC;
            Trailer.enabled = vehicle.CanHandleTrailer();
            Cooling.enabled = vehicle.HasCooling;
            Forklift.enabled = vehicle.HasForklift;
            Crane.enabled = vehicle.HasCrane;
            gameObject.SetActive(init);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            foreach (VehicleDropoffField obj in FindObjectsOfType<VehicleDropoffField>())
            {
                obj.StartHighlight();
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log(eventData.pointerCurrentRaycast.gameObject);
            foreach (VehicleDropoffField obj in FindObjectsOfType<VehicleDropoffField>())
            {
                obj.StopHighlight();
            }
            base.OnEndDrag(eventData);
            DispoListHandler.LoadVehicleList();
        }

        protected override bool ValidDropOff(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out VehicleDropoffField dropOf))
            {
                dropOf.DropOff(Vehicle, Vehicle.PlateText);
                Destroy(gameObject);
                return true;
            }
            return false;
        }
    }
}
