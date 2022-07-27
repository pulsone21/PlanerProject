using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;

namespace RoadSystem
{
    public class CityManager : MonoBehaviour
    {
        public static CityManager Instance;
        [SerializeField] private List<CityController> Cities;
        private List<City> cities = new List<City>();

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
    }
}