using System.Collections.Generic;

namespace RoadSystem
{
    [System.Serializable]
    public class CityNrList
    {
        public CityNr[] cityNrs;

        public CityNrList(List<CityNr> cityNrs)
        {
            this.cityNrs = cityNrs.ToArray();
        }
    }
}
