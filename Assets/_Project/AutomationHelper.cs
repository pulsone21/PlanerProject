using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CompanySystem;
using ContractSystem;
using RoadSystem;
using TMPro;

namespace Planer
{

    public class AutomationHelper : EditorWindow
    {
        string resultText;
        private const float COMPANY_RATIO = 0.000003f;
        public Object UIPrefab;
        public Transform UIContainer;

        [MenuItem("PlanerProject/AutomationHelper")]
        private static void ShowWindow()
        {
            var window = GetWindow<AutomationHelper>();
            window.titleContent = new GUIContent("AutomationHelper");
            window.Show();
        }

        private void OnGUI()
        {
            SerializedObject obj = new SerializedObject(this);
            EditorGUILayout.PropertyField(obj.FindProperty("UIPrefab"));
            EditorGUILayout.PropertyField(obj.FindProperty("UIContainer"));

            if (GUILayout.Button("Generate UI"))
            {
                GenerateUI();
            }

            if (GUILayout.Button("Clear Companies"))
            {
                ClearAllCompanies();
            }

            GUILayout.Space(25f);
            GUILayout.Label("Automation Feedback");
            GUILayout.Space(10f);
            GUILayout.TextArea(resultText);

            obj.ApplyModifiedProperties();
        }

        private void GenerateUI()
        {
            CityController[] cities = FindObjectsOfType<CityController>();
            foreach (CityController city in cities)
            {
                GameObject newUI = (GameObject)PrefabUtility.InstantiatePrefab(UIPrefab, UIContainer);
                newUI.name = $"CityUI_{city.name}";
                newUI.transform.position = city.transform.position;
                newUI.GetComponent<CityUICntroller>().SetName(city.name);
            }
        }

        private void GenerateCompanies()
        {
            int companyNr = 0;
            int citiesChanged = 0;
            CityController[] cityController = FindObjectsOfType<CityController>();

            foreach (CityController cC in cityController)
            {
                List<GoodCompany> companies = GenerateGoodCompanies(cC.City);
                cC.City.AddCompanies(companies);
                citiesChanged++;
                companyNr += companies.Count;
            }
            SetFeedbackText("Generated " + companyNr + " companies in " + citiesChanged + " cities!");
        }

        private void ClearAllCompanies()
        {
            CityController[] cityController = FindObjectsOfType<CityController>();

            foreach (CityController cC in cityController)
            {
                cC.City.DeleteCompanies();
            }
        }


        public List<GoodCompany> GenerateGoodCompanies(City city)
        {
            List<GoodCompany> newList = new List<GoodCompany>();
            int iterations = Mathf.CeilToInt(city.Citizen * COMPANY_RATIO);
            Debug.Log(city.Name + " has " + iterations + " iterations");
            iterations = iterations >= 1 ? iterations : 1;
            Debug.Log("Fixed Iterations " + iterations);
            for (int i = 0; i < iterations; i++)
            {
                newList.Add(GenerateGoodCompany(city));
            }
            return newList;
        }

        public GoodCompany GenerateGoodCompany(City city)
        {
            int amountOfCategories = System.Enum.GetNames(typeof(GoodCategory)).Length - 1;
            GoodCategory goodCategory = (GoodCategory)Random.Range(0, amountOfCategories);
            string companyName = CompanyNameGenerator.GenerateCompanyName(goodCategory, city);
            Debug.Log(companyName);
            TransportGoodManager TGM = FindObjectOfType<TransportGoodManager>();
            TransportGood[] transportGoods = TGM.GetRndListOfGoods(goodCategory).ToArray();
            GoodCompany newCompany = new GoodCompany(goodCategory, transportGoods, companyName, city);
            return newCompany;
        }

        private void ClearAutomationText() => resultText = "";
        private void SetFeedbackText(string text) => resultText = text;

        //Old Stuff
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
    }
}
