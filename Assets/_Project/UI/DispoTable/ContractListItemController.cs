using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;
using Planer;
using UnityEngine.UI;

namespace UISystem
{
    public class ContractListItemController : MonoBehaviour
    {
        private bool init = false;
        [SerializeField] private TextSetter ContractName, Start, Target, Type, DeliveryDate, LoadingDate;
        [SerializeField] private Image Cooling, Forklift, Crane;
        private void Awake() => gameObject.SetActive(init);
        public void Initlize(TransportContract contract)
        {
            if (init) return;
            init = true;
            ContractName.SetText(contract.ToString());
            Start.SetText("Start: " + contract.OriginCity.ToString());
            Target.SetText("Target:" + contract.DestinationCity.ToString());
            Type.SetText("Type: " + contract.TransportType.ToString());
            DeliveryDate.SetText("Delivery Date: " + contract.DeliveryDate.ToString());
            LoadingDate.SetText("Loading Date:" + contract.PickUpDate.ToString());
            Cooling.enabled = contract.Good.NeedsCooling;
            Crane.enabled = contract.Good.NeedsCrane;
            Forklift.enabled = contract.Good.NeedsForkLif;
        }
    }
}
