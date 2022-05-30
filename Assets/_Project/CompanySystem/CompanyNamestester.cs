using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace CompanySystem
{
    public class CompanyNamestester : MonoBehaviour
    {
        public CompanyNames companyNames;
        public RoadSystem.City city;

        public void SaveJSONToFile()
        {
            DataHandler.SaveJSONToFile<CompanyNames>(companyNames, "companyNames.json");
        }

        public void LoadCompanyNamesFromFile()
        {
            companyNames = null;
            companyNames = DataHandler.LoadFromJSON<CompanyNames>("companyNames.json");
        }

    }
}
