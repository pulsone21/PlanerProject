using UnityEngine;
using ContractSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TooltipSystem;

namespace UISystem
{
    [RequireComponent(typeof(TooltipTrigger))]
    public class ContractListItemController : DragableItem, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private bool init = false;
        private RectTransform myTransform;
        [SerializeField] private TextSetter ContractName, start, Type, DeliveryDate;
        [SerializeField] private Image Cooling, Forklift, Crane;
        private TransportContract contract;
        private DispoListHandler DispoListHandler;
        private TooltipTrigger _tooltip;
        protected override void Awake()
        {
            base.Awake();
            _tooltip = GetComponent<TooltipTrigger>();
        }
        public void Initlize(TransportContract contract, DispoListHandler dlh)
        {
            if (init) return;
            init = true;
            this.contract = contract;
            DispoListHandler = dlh;
            ContractName.SetText($"{contract.GoodAmmount.ToString()} {contract.Good.Unit} - {contract.Good.ToString()}");
            start.SetText("Start: " + contract.OriginCity.City.Name);
            Type.SetText("Type: " + contract.TransportType.ToString());
            DeliveryDate.SetText("Delivery: " + contract.DeliveryDate.ToDateString());
            Cooling.enabled = contract.Good.NeedsCooling;
            Crane.enabled = contract.Good.NeedsCrane;
            Forklift.enabled = contract.Good.NeedsForkLift;
            SetUpTooltip(contract);
        }
        private void SetUpTooltip(TransportContract contract)
        {
            string Header = "";
            string Description = "";

            Description += "Start: " + contract.OriginCity.City.Name + "\n";
            Description += "Loading: " + contract.PickUpDate.ToDateString() + "\n";
            Description += "Target: " + contract.DestinationCity.City.Name + "\n";
            Description += "Delivery: " + contract.DeliveryDate.ToDateString() + "\n";

            _tooltip.Header = Header;
            _tooltip.Description = Description;
        }
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            foreach (RouteListController obj in FindObjectsOfType<RouteListController>())
            {
                obj.StartHighlight();
            }
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            foreach (RouteListController obj in FindObjectsOfType<RouteListController>())
            {
                obj.StopHighlight();
            }
            base.OnEndDrag(eventData);
            DispoListHandler.LoadContractList();
        }
        protected override bool ValidDropOff(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out RouteListController RLC))
            {
                RLC.DropOff(contract);
                Destroy(gameObject);
                return true;
            }
            return false;
        }
    }
}
