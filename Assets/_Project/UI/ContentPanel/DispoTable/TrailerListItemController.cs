using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;
using Planer;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UISystem
{
    public class TrailerListItemController : DragableItem
    {
        private bool init = false;
        [SerializeField] private TextSetter Plate, Capacity;
        [SerializeField] private Image Cubic, Cooling, Forklift, Crane;
        private DispoListHandler DispoListHandler;
        protected override void Awake()
        {
            base.Awake();
            gameObject.SetActive(init);
        }
        public Trailer Trailer { get; protected set; }
        public void Initlize(Trailer Trailer, DispoListHandler dlh)
        {
            if (init) return;
            init = true;
            this.Trailer = Trailer;
            DispoListHandler = dlh;
            Plate.SetText(Trailer.PlateText);
            Capacity.SetText("Capcity: " + Trailer.MaxCapacity.ToString());
            Cubic.enabled = Trailer.CanHandleCUBIC;
            Cooling.enabled = Trailer.HasCooling;
            Forklift.enabled = Trailer.HasForklift;
            Crane.enabled = Trailer.HasCrane;
            gameObject.SetActive(init);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            foreach (TrailerDropoffField obj in FindObjectsOfType<TrailerDropoffField>())
            {
                obj.StartHighlight();
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            foreach (TrailerDropoffField obj in FindObjectsOfType<TrailerDropoffField>())
            {
                obj.StopHighlight();
            }
            DispoListHandler.LoadTrailerList();
        }

        protected override bool ValidDropOff(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out TrailerDropoffField dropOf))
            {
                dropOf.DropOff(Trailer, Trailer.PlateText);
                Destroy(gameObject);
                return true;
            }
            return false;
        }
    }
}
