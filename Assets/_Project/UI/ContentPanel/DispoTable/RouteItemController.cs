using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;
using TMPro;
using UnityEngine.UI;

namespace UISystem
{
    [RequireComponent(typeof(CanvasGroup), typeof(ReordableListItem))]
    public class RouteItemController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI cityName, contractName, deleviryDate;
        [SerializeField] private GameObject UpArrow, DownArrow;
        public RouteDestination Destination { get; protected set; }
        private void Awake() => _canvasGroup = GetComponent<CanvasGroup>();
        public void Initialize(RouteDestination destination)
        {
            ReordableListItem rli = GetComponent<ReordableListItem>();
            rli.Initialize();
            rli.OnPositionChange += GetComponentInParent<RouteListController>().ItemPositionChange;
            cityName.text = destination.DestinationCity.name;
            contractName.text = destination.Contract.ToString();
            Destination = destination;
            switch (destination.Direction)
            {
                case RouteDestination.LoadingDirection.Load:
                    deleviryDate.text = "";
                    UpArrow.SetActive(false);
                    DownArrow.SetActive(true);
                    break;
                case RouteDestination.LoadingDirection.Unload:
                    deleviryDate.text = destination.Contract.DeliveryDate.ToDateString();
                    UpArrow.SetActive(true);
                    DownArrow.SetActive(false);
                    break;
                default:
                    break;
            }
        }
        public void ClearDestination() => GetComponentInParent<RouteListController>().RemoveDestinations(Destination);
        public void DisableRaycast() => _canvasGroup.blocksRaycasts = false;
        public void EnableRaycast() => _canvasGroup.blocksRaycasts = true;
    }
}
