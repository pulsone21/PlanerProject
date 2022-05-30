using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using ContractSystem;
using RoadSystem;

namespace CompanySystem
{
    public static class CompanyNameGenerator
    {
        public static string GenerateCompanyName(GoodCategory category, City city)
        {
            string name = "";
            CompanyNames companyNames = DataHandler.LoadFromJSON<CompanyNames>("companyNames.json");
            int rndNr = Random.Range(0, 101);

            //65% Chance Owner Name in it
            if (rndNr <= 65) name += companyNames.Names[Random.Range(0, companyNames.Names.Count - 1)];

            //50% Chance to have City Name in it
            if (rndNr >= 50) name += " " + city.Name;

            foreach (GoodCategoryNames cN in companyNames.CategoryNames)
            {
                if (cN.Category == category)
                {
                    bool categoryFound = false;
                    int tryNr = 0; // max ammount of trys to prevent infity loops
                    while (!categoryFound && tryNr < 3)
                    {
                        string testString = name + " " + cN.Names[Random.Range(0, cN.Names.Count - 1)];
                        if (testString.Split(" ").Length <= 3)
                        {
                            //this is to prevent extrem long combinations 
                            name = testString;
                            categoryFound = true;
                        }
                        tryNr++;
                    }
                }
            }
            //if name has not 3 parts, there is a (Three parts could be owner, city, goodCategory or )
            //50% Chance to have the legal form in it --> prevents to have to long names like Purvis Lab Equipment Association
            if (name.Split(" ").Length < 3)
            {
                if (rndNr <= 50) name += " " + companyNames.CompanyForms[Random.Range(0, companyNames.CompanyForms.Count - 1)];
            }
            return name.Trim();
        }
    }


}
