using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadSystem;
using ContractSystem;

namespace CompanySystem
{
    public static class CompanyGenerator
    {
        private const float COMPANY_RATIO = 0.000003f;
        public static List<GoodCompany> GenerateGoodCompanies(City city)
        {
            List<GoodCompany> newList = new List<GoodCompany>();
            int iterations = Mathf.CeilToInt(city.Citizen * COMPANY_RATIO);
            iterations = iterations >= 1 ? iterations : 1;
            for (int i = 0; i < iterations; i++)
            {
                newList.Add(GenerateGoodCompany(city));
            }
            return newList;
        }

        public static GoodCompany GenerateGoodCompany(City city)
        {
            int amountOfCategories = System.Enum.GetNames(typeof(GoodCategory)).Length - 1;
            GoodCategory goodCategory = (GoodCategory)Random.Range(0, amountOfCategories);
            string companyName = CompanyNameGenerator.GenerateCompanyName(goodCategory, city);
            TransportGood[] transportGoods = TransportGoodManager.Instance.GetRndListOfGoods(goodCategory).ToArray();
            GoodCompany newCompany = new GoodCompany(goodCategory, transportGoods, companyName, city);
            return newCompany;
        }
    }
}
