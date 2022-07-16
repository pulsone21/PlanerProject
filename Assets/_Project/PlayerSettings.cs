using System;
using Utilities;
using UnityEngine;
namespace Planer
{
    [Serializable]
    public class PlayerSettings
    {
        public string CompanyName;
        public string StartingCity;
        public int StartingMoney;
        public PlayerSettings(string companyName, string startingCity, int startingMoney)
        {
            this.CompanyName = companyName;
            this.StartingCity = startingCity;
            this.StartingMoney = startingMoney;
        }
        public void SaveToFile()
        {
            DataHandler.SaveJSONToFile<PlayerSettings>(this, "/GameSettings/playerSettings.json", false);
        }

        public static PlayerSettings LoadFromFile()
        {
            return DataHandler.LoadFromJSON<PlayerSettings>("/GameSettings/playerSettings.json");
        }
    }

}