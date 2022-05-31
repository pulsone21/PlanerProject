using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;
using CompanySystem;


namespace RoadSystem
{
    public class CityController : MonoBehaviour
    {
        [SerializeField] private City _city;
        public City City => _city;


        public void AddCity(City city)
        {
            if (_city == null) return;
            _city = city;
        }
    }
}
