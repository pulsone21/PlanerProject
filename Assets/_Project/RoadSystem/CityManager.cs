using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;

namespace RoadSystem
{
    public class CityManager : MonoBehaviour
    {
        public static CityManager Instance;
        private List<CityController> Cities;

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

        public City GetRndCity() => Cities[Random.Range(0, Cities.Count)].City;

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
            foreach (CityController cityController in Cities)
            {
                if (cityController.City.HasCompanyWithCategory(category)) possibleCities.Add(cityController.City);
            }
            return possibleCities[Random.Range(0, possibleCities.Count)];
        }
    }
}