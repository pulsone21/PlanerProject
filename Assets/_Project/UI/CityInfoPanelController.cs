using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UISystem
{
    public class CityInfoPanelController : MonoBehaviour
    {
        [SerializeField] private CityUICntroller _controller;
        [SerializeField] private TextMeshProUGUI _citizen;
        [SerializeField] private TextMeshProUGUI _amountCompanies;
        [SerializeField] private TextMeshProUGUI _name;

        private void OnEnable()
        {
            _name.text = _controller.City.Name;
            _citizen.text = _controller.City.Citizen.ToString();
            _amountCompanies.text = _controller.City.Companies.Count.ToString();

        }
    }
}
