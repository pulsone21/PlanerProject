using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;
using SLSystem;
namespace RoadSystem
{
    public class CityManager : MonoBehaviour
    {
        private const float COMPANY_RATIO = 0.000003f;
        public static CityManager Instance;
        [SerializeField] private List<CityController> Cities;
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
        }
        public CityController GetRndCity() => Cities[Random.Range(0, Cities.Count)];
        public float GetDistance(CityController startCity, CityController destCity) => Vector2.Distance(startCity.transform.position, destCity.transform.position);
        private CityController GetCityControllerByCity(City city)
        {
            foreach (CityController cC in Cities)
            {
                if (cC.City == city) return cC;
            }
            return default;
        }
        public CityController GetRndCityByCategory(GoodCategory category)
        {
            List<CityController> possibleCities = new List<CityController>();
            foreach (CityController cit in Cities)
            {
                if (cit.HasCompanyWithCategory(category)) possibleCities.Add(cit);
            }
            return possibleCities[Random.Range(0, possibleCities.Count)];
        }
        public bool GetCityByName(string Name, out CityController city)
        {
            foreach (CityController cC in Cities)
            {
                if (cC.City.Name.ToLower() == Name.ToLower())
                {
                    city = cC;
                    return true;
                }
            }
            city = default;
            return false;
        }
        public List<CityController> GetAllCities() => Cities;
    }
}