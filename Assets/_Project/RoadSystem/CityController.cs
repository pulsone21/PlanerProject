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

        private List<GoodCompany> GenerateCompanies()
        {
            List<GoodCompany> newList = new List<GoodCompany>();
            int iterations = Mathf.CeilToInt(_city.Citizen * 0.01f);
            iterations = iterations >= 1 ? iterations : 1;
            int rndNr = Random.Range(0, 101);
            for (int i = 0; i < iterations; i++)
            {
                int amountOfCategories = System.Enum.GetNames(typeof(GoodCategory)).Length - 1;
                GoodCategory goodCategory = (GoodCategory)Random.Range(0, amountOfCategories);

                string companyName = CompanyNameGenerator.GenerateCompanyName(goodCategory, _city);

                TransportGood[] transportGoods = TransportGoodManager.Instance.GetRndListOfGoods(goodCategory).ToArray();

                GoodCompany newCompany = new GoodCompany(goodCategory, transportGoods, companyName, _city);

                newList.Add(newCompany);

            }
            return newList;
        }


        public void AddCity(City city)
        {
            if (_city == null) return;
            _city = city;
        }
    }
}
