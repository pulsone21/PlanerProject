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

        public List<City> Cities;
    }
}