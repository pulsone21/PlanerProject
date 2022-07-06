namespace RoadSystem
{
    [System.Serializable]
    public class CityNr
    {
        public string Name;
        public int number;

        public CityNr(string name, int number)
        {
            Name = name;
            this.number = number;
        }
    }
}
