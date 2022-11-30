using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;
using CompanySystem;
namespace RoadSystem
{
    public class CityManager : MonoBehaviour
    {
        private const float COMPANY_RATIO = 0.000003f;
        public static CityManager Instance;
        [SerializeField] private List<CityController> Cities;
        private List<City> cities = new List<City>();
        [SerializeField] private CityController CityControllerPrefab;

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
            foreach (CityController cC in Cities)
            {
                cities.Add(cC.City);
            }
        }

        private void Start()
        {
            ClearAllCompanies();
            GenerateCompanies();
        }

        public City GetRndCity() => cities[Random.Range(0, cities.Count)];

        public float GetDistance(City startCity, City destCity) => Vector2.Distance(GetCityControllerByCity(startCity).transform.position, GetCityControllerByCity(destCity).transform.position);

        private CityController GetCityControllerByCity(City city)
        {
            foreach (CityController cC in Cities)
            {
                if (cC.City == city) return cC;
            }
            return default;
        }
        public City GetRndCityByCategory(GoodCategory category)
        {
            List<City> possibleCities = new List<City>();
            foreach (City cit in cities)
            {
                if (cit.HasCompanyWithCategory(category)) possibleCities.Add(cit);
            }
            return possibleCities[Random.Range(0, possibleCities.Count)];
        }

        public bool GetCityByName(string Name, out City city)
        {
            foreach (City cit in cities)
            {
                if (cit.Name.ToLower() == Name.ToLower())
                {
                    city = cit;
                    return true;
                }
            }
            city = default;
            return false;
        }

        public List<City> GetAllCities() => cities;
        private void GenerateCompanies()
        {
            int companyNr = 0;
            int citiesChanged = 0;
            CityController[] cityController = FindObjectsOfType<CityController>();
            foreach (CityController cC in cityController)
            {
                List<GoodCompany> companies = GenerateGoodCompanies(cC.City);
                cC.City.AddCompanies(companies);
                citiesChanged++;
                companyNr += companies.Count;
            }
        }
        private void ClearAllCompanies()
        {
            CityController[] cityController = FindObjectsOfType<CityController>();
            foreach (CityController cC in cityController)
            {
                cC.City.DeleteCompanies();
            }
        }
        public List<GoodCompany> GenerateGoodCompanies(City city)
        {
            List<GoodCompany> newList = new List<GoodCompany>();
            int iterations = Mathf.CeilToInt(city.Citizen * COMPANY_RATIO);
            iterations = iterations >= 1 ? iterations : 1;
            for (int i = 0; i < iterations; i++)
            {
                newList.Add(GenerateGoodCompany(city));
            }
            return newList;
        }

        public GoodCompany GenerateGoodCompany(City city)
        {
            int amountOfCategories = System.Enum.GetNames(typeof(GoodCategory)).Length - 1;
            GoodCategory goodCategory = (GoodCategory)Random.Range(0, amountOfCategories);
            string companyName = CompanyNameGenerator.GenerateCompanyName(goodCategory, city);
            TransportGoodManager TGM = FindObjectOfType<TransportGoodManager>();
            List<TransportGood> transportGoods = TGM.GetRndListOfGoods(goodCategory);
            GoodCompany newCompany = new GoodCompany(goodCategory, transportGoods, companyName, city);
            return newCompany;
        }
    }

}