using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadSystem;
using UnityEngine.Events;
using System;

namespace UISystem
{
    public enum ModalType { Vertical, Horizontal }
    public class PopUpWindowManager : MonoBehaviour
    {
        public static PopUpWindowManager Instance;
        [SerializeField] private GameObject Container;
        [SerializeField] private List<Modal> Modals = new List<Modal>();
        private List<ModalController> activeModals = new List<ModalController>();
        private void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this;
            }
        }

        //?  POPUP Types -> Confirmation, Information, CityInformation
        //PopUp for Confirmation
        public static void ShowPopUp(ModalType type, string Header, string Content, UnityAction OnOkAction, UnityAction OnCancleAction, UnityAction OnAlternativeAction = null, Sprite image = null, string OkLabel = null, string CancleLabel = null, string alternativeLable = null)
        {
            Modal modal = Instance.GetModalByType(type);
            if (modal != default)
            {
                //? only set active if there is no other modal Open.
                if (Instance.activeModals.Count < 1) Instance.Container.SetActive(true);
                ModalController controller = Instantiate<GameObject>(modal.UI, Instance.Container.transform).GetComponent<ModalController>();
                Instance.activeModals.Add(controller);
                controller.ShowModal(Header, Content, OnOkAction, OnCancleAction, OnAlternativeAction, image, OkLabel, CancleLabel, alternativeLable);
            }
        }

        public static void ShowPopUp(ModalType type, string Header, string Content, UnityAction OnOkAction, string OkLabel = null)
        {
            Modal modal = Instance.GetModalByType(type);
            if (modal != default)
            {
                //? only set active if there is no other modal Open.
                if (Instance.activeModals.Count < 1) Instance.Container.SetActive(true);
                ModalController controller = Instantiate<GameObject>(modal.UI, Instance.Container.transform).GetComponent<ModalController>();
                Instance.activeModals.Add(controller);
                controller.ShowModal(Header, Content, OnOkAction, OkLabel);
            }
        }
        //PopUp for CityInformation
        public static void ShowPopUp(CityController cityController)
        {

        }

        private Modal GetModalByType(ModalType type)
        {
            foreach (Modal modal in Modals)
            {
                if (modal.ModalType == type) return modal;
            }
            Debug.LogError($"ModalType {type} not found in PopUpWindowManager List!");
            return default;
        }
        public static void OnClosedModal(ModalController modalController)
        {
            if (Instance.activeModals.Remove(modalController) && Instance.activeModals.Count < 1)
            {
                Instance.Container.SetActive(false);
            };
        }
    }
}
