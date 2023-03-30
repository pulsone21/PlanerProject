using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;
using System;
using DG.Tweening;
using Utilities;
using VehicleSystem;

namespace UISystem
{
    public class RouteListController : ListController
    {
        [SerializeField] private DispoListHandler DispoListHandler;
        private Tween tween;
        private List<RouteDestination> _destinations = new List<RouteDestination>();
        private List<RouteItemController> RICs = new List<RouteItemController>();
        public void SetRoute(List<RouteDestination> destinations)
        {
            _destinations = destinations;
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
        protected override void GenerateList()
        {
            if (_destinations.Count == 0)
            {
                GenerateDefaultText();
                return;
            }
            foreach (RouteDestination destination in _destinations) AddDestinationToList(destination);
        }
        internal Queue<RouteDestination> SaveRoute()
        {
            RICs = ListItemContainer.GetComponentsInChildren<RouteItemController>().ToList();
            Queue<RouteDestination> route = new Queue<RouteDestination>();
            if (EvaluateRoute())
            {
                foreach (RouteItemController ric in RICs) route.Enqueue(ric.Destination);
            }
            return route;
        }

        public void StartHighlight()
        {
            tween = transform.DOScale(new Vector3(0.9f, 0.9f, 1f), 0.6f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            RICs = ListItemContainer.GetComponentsInChildren<RouteItemController>().ToList();
            foreach (RouteItemController ric in RICs)
            {
                ric.DisableRaycast();
            }
        }

        public void StopHighlight()
        {
            tween.Rewind();
            tween.Kill();
            foreach (RouteItemController ric in RICs)
            {
                ric.EnableRaycast();
            }
        }
        private void AddDestinationToList(RouteDestination destination)
        {
            if (ListItemContainer.childCount == 1 && ListItemContainer.GetChild(0).name == "DefaultListItem(Clone)") ListItemContainer.ClearAllChildren();
            Transform item = Instantiate(ListItemPrefab).transform;
            item.SetParent(ListItemContainer);
            item.GetComponent<RouteItemController>().Initialize(destination);
        }
        public void DropOff(TransportContract contract)
        {
            DispoDetailPage.Instance.CurrentDriver.Route.AddContract(contract);
            AddDestinationToList(contract.StartDestination);
            AddDestinationToList(contract.TargetDestination);
        }
        public void ItemPositionChange()
        {
            Debug.Log("Item has Changed its Position in my List");
            RICs = ListItemContainer.GetComponentsInChildren<RouteItemController>().ToList();
            EvaluateRoute();
        }
        private bool EvaluateRoute()
        {
            RouteDestination[] dests = new RouteDestination[RICs.Count];
            for (int i = 0; i < RICs.Count; i++)
            {
                dests[i] = RICs[i].Destination;
            }
            Vehicle vehicle = DispoDetailPage.Instance.CurrentDriver.Vehicle ?? null;
            Trailer trailer = DispoDetailPage.Instance.CurrentDriver.Trailer ?? null;
            if (!RouteValidator.ValidateRoute(dests, vehicle, out string result, trailer))
            {
                Debug.LogWarning(result); // TODO Implement a correct Info way for the player
                return false;
            }
            return true;
        }

        internal void RemoveDestinations(RouteDestination destination)
        {
            DispoDetailPage.Instance.CurrentDriver.Route.RemoveContract(destination.Contract);
            _destinations = DispoDetailPage.Instance.CurrentDriver.Route.Destinations.ToList();
            ClearList();
            GenerateList();
            DispoListHandler.LoadContractList();
        }
    }
}
