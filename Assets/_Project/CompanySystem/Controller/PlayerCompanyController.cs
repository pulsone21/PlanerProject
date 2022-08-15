using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planer;
using RoadSystem;

namespace CompanySystem
{
    public class PlayerCompanyController : MonoBehaviour
    {
        public static PlayerCompanyController Instance;
        public static TransportCompany Company => Instance.company;
        [SerializeField] private TransportCompany company;
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

        private void Start()
        {
            PlayerSettings pS = PlayerSettings.LoadFromFile();
            CityManager.Instance.GetCityByName(pS.StartingCity, out City startingCity);
            company = new TransportCompany(pS.CompanyName, startingCity, pS.StartingMoney);
        }
    }

}