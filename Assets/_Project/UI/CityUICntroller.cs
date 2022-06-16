using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using RoadSystem;

namespace UISystem
{
    public class CityUICntroller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private GameObject NamePlate;
        [SerializeField] private GameObject InfoPanel;
        [SerializeField] private City _city;
        public City City => _city;

        public void SetCity(City city) => _city = city;
        public void SetName(string name) => NamePlate.GetComponentInChildren<TextMeshProUGUI>().text = name;
        public void OnPointerEnter(PointerEventData eventData) => NamePlate.SetActive(true);
        public void OnPointerExit(PointerEventData eventData) => NamePlate.SetActive(false);
        public void OnPointerClick(PointerEventData eventData) => ShowInfoPanel();
        private void Awake() => NamePlate.SetActive(false);

        public void CloseInfoPanel()
        {
            InfoPanel.SetActive(false);
            NamePlate.SetActive(false);
        }

        private void ShowInfoPanel()
        {
            InfoPanel.SetActive(true);
            NamePlate.SetActive(true);
        }
    }
}
