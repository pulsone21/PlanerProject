using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanySystem;
using RoadSystem;

namespace Planer
{
    public class PlayerCompanyController : MonoBehaviour
    {
        public static PlayerCompanyController Instance;
        public static PlayerCompany Company => Instance.company;
        private PlayerCompany company;
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
            PlayerSettings pS = PlayerSettings.LoadFromFile();
            CityManager.Instance.GetCityByName(pS.StartingCity, out City startingCity);
            company = new PlayerCompany(pS.CompanyName, startingCity, pS.StartingMoney);
        }
    }

}