using Utilities;

namespace Planer
{
    public class PlayerSettings
    {
        public readonly string CompanyName;
        public readonly string StartingCity;
        public readonly int StartingMoney;
        public PlayerSettings(string companyName, string startingCity, int startingMoney)
        {
            this.CompanyName = companyName;
            this.StartingCity = startingCity;
            this.StartingMoney = startingMoney;
            SaveToFile();
        }
        private void SaveToFile()
        {
            DataHandler.SaveJSONToFile<PlayerSettings>(this, "/GameSettings/playerSettings.json", false);
        }
    }

}