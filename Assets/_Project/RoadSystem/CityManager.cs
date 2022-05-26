using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RoadSystem
{
    public class CityManager : MonoBehaviour
    {
        public static CityManager Instance;

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

        public City GetRndCity() => Cities[Random.Range(0,Cities.Count)];

        private List<City> Cities;

        public int GetDistance(City startCity, City destCity)
        {
            throw new System.NotImplementedException();
        }
    }
}