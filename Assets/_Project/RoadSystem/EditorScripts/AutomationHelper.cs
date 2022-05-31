using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CompanySystem;
using ContractSystem;

namespace RoadSystem
{

    public class AutomationHelper : EditorWindow
    {
        string resultText;
        private const float COMPANY_RATIO = 0.000003f;

        [MenuItem("PlanerProject/AutomationHelper")]
        private static void ShowWindow()
        {
            var window = GetWindow<AutomationHelper>();
            window.titleContent = new GUIContent("AutomationHelper");
            window.Show();
        }

        private void OnGUI()
        {

            if (GUILayout.Button("Generate Companies"))
            {
                GenerateCompanies();
            }

            if (GUILayout.Button("Find empty cities"))
            {
                FindEmptyCities();
            }

            if (GUILayout.Button("Find empty controller"))
            {
                FindEmptyCityController();
            }

            if (GUILayout.Button("Calc Avarage"))
            {
                CalcAvarageCitizen();

            }


            GUILayout.Space(25f);
            GUILayout.Label("Automation Feedback");
            GUILayout.Space(10f);
            GUILayout.TextArea(resultText);
        }

        private void GenerateCompanies()
        {
            int companyNr = 0;
            int cities = 0;
            CityController[] cityController = FindObjectsOfType<CityController>();
            foreach (CityController cC in cityController)
            {
                List<GoodCompany> companies = GenerateGoodCompanies(cC.City);
                cC.City.AddCompanies(companies);
                cities++;
                companyNr += companies.Count;
            }
            SetFeedbackText("Generated " + companyNr + " companies in " + cities + " cities!");
        }

        private void FindEmptyCities()
        {
            List<City> cities = new List<City>();
            CityController[] cityController = FindObjectsOfType<CityController>();
            foreach (CityController cC in cityController)
            {
                if (cC.City.Citizen < 1) cities.Add(cC.City);
            }
            SetFeedbackText("found " + cities.Count + " Cities with no citizien, " + cities.ToString());
        }

        private void FindEmptyCityController()
        {
            CityController[] cityController = FindObjectsOfType<CityController>();
            foreach (CityController cC in cityController)
            {
                if (cC.City == null)
                {
                    Debug.Log("FoundEmpty City on " + cC.name);
                }
            }
        }
        private void CalcAvarageCitizen()
        {
            int maxCitizien = 0;
            CityController[] cityController = FindObjectsOfType<CityController>();
            foreach (CityController cC in cityController)
            {
                maxCitizien += cC.City.Citizen;
            }
            int avarage = maxCitizien / cityController.Length;
            SetFeedbackText("Avarge Citizen: " + avarage + " on " + cityController.Length + " Cities");
        }

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
            TransportGoodManager TGM = FindObjectOfType<TransportGoodManager>();
            TransportGood[] transportGoods = TGM.GetRndListOfGoods(goodCategory).ToArray();
            GoodCompany newCompany = new GoodCompany(goodCategory, transportGoods, companyName, city);
            return newCompany;
        }

        private void ClearAutomationText() => resultText = "";
        private void SetFeedbackText(string text) => resultText = text;
    }
}
