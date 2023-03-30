using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using RoadSystem;
using UnityEngine.UI;
namespace UISystem
{
    public class CityUICntroller : UIColorChanger, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private GameObject NamePlate;
        [SerializeField] private CityController _city;
        public Image circleBoarder, circleBackgorund, plateBorder, plateBackground;
        [SerializeField] private TextMeshProUGUI plateText;
        public City City => _city.City;
        public void SetCity(CityController city) => _city = city;
        public void SetName(string name) => NamePlate.GetComponentInChildren<TextMeshProUGUI>().text = name;
        public void OnPointerEnter(PointerEventData eventData) => NamePlate.SetActive(true);
        public void OnPointerExit(PointerEventData eventData) => NamePlate.SetActive(false);
        private void Awake() => NamePlate.SetActive(false);
        protected override void SetColorShema(Color borderColor, Color bgCol)
        {
            circleBoarder.color = borderColor;
            circleBackgorund.color = bgCol;
            plateBorder.color = borderColor;
            plateBackground.color = bgCol;
            plateText.color = borderColor;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            throw new NotImplementedException();
            //TODO open a free popUp with City OverView 
        }
    }
}
