using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadSystem;

namespace CompanySystem
{
    public class CompanyManager : MonoBehaviour
    {
        [SerializeField] private List<GoodCompany> goodCompanies = new List<GoodCompany>();
        public List<GoodCompany> GoodCompanies => goodCompanies;
        void Start()
        {
            List<City> cities = CityManager.Instance.GetAllCities();
            foreach (City city in cities)
            {
                goodCompanies.AddRange(city.Companies);
            }
        }


    }
}
