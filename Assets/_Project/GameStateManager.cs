using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;
using CompanySystem;
using Utilities;
using RoadSystem;

namespace Planer
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance;
        [SerializeField] private PlayerCompany playerCompany;
        public PlayerCompany PlayerCompany => playerCompany;
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
            PlayerSettings settings = DataHandler.LoadFromJSON<PlayerSettings>("/GameSettings/playerSettings.json");
            if (CityManager.Instance.GetCityByName(settings.StartingCity, out City city))
            {
                playerCompany = new PlayerCompany(settings.CompanyName, city, settings.StartingMoney);
            }
        }



    }
}
