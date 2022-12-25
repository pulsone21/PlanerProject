using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadSystem;
using SLSystem;
using Utilities;
using Sirenix.OdinInspector;
using CompanySystem;
using ContractSystem;

namespace CompanySystem
{
    public class CompanyManager : SerializedMonoBehaviour, IPersistenceData
    {
        public static CompanyManager Instance;
        public Dictionary<string, List<GoodCompany>> _companyList = new Dictionary<string, List<GoodCompany>>();
        public Dictionary<string, List<GoodCompany>> CompanyList => _companyList;

        private string _className => GetType().Name;
        public GameObject This => gameObject;
        private const float COMPANY_RATIO = 0.000003f;
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        [Button("Generate Companies")]
        public void GenerateCompanies()
        {
            int companyNr = 0;
            int citiesChanged = 0;
            CityController[] cityController = FindObjectsOfType<CityController>();
            CompanyList.Clear();
            foreach (CityController cC in cityController)
            {
                List<GoodCompany> companies = GenerateGoodCompanies(cC.City);
                CompanyList[cC.City.Name] = companies;
                citiesChanged++;
                companyNr += companies.Count;
            }
            Debug.Log("Generated " + companyNr + " companies in " + citiesChanged + " cities!");
        }
        private List<GoodCompany> GenerateGoodCompanies(City city)
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
        private GoodCompany GenerateGoodCompany(City city)
        {
            int amountOfCategories = System.Enum.GetNames(typeof(GoodCategory)).Length - 1;
            GoodCategory goodCategory = (GoodCategory)Random.Range(0, amountOfCategories);
            string companyName = CompanyNameGenerator.GenerateCompanyName(goodCategory, city);
            TransportGoodManager TGM = FindObjectOfType<TransportGoodManager>();
            List<TransportGood> transportGoods = TGM.GetRndListOfGoods(goodCategory);
            GoodCompany newCompany = new GoodCompany(goodCategory, transportGoods, companyName, city.Name);
            return newCompany;
        }
        public List<GoodCompany> GetGoodCompaniesByCity(string CityName)
        {
            if (_companyList.ContainsKey(CityName))
            {
                return _companyList[CityName];
            }
            return default;
        }
        public List<GoodCompany> GetGoodCompaniesByCity(City City)
        {
            if (_companyList.ContainsKey(City.Name))
            {
                return _companyList[City.Name];
            }
            return default;
        }
        public void Load(GameData gameData)
        {
            if (gameData.Data.ContainsKey(_className))
            {
                PersistenceData persistenceData = JsonUtility.FromJson<PersistenceData>(gameData.Data[_className]);
                _companyList = persistenceData.ToCityCompanyDictionary();
            }
        }
        public void Save(ref GameData gameData)
        {
            gameData.Data[_className] = new PersistenceData(_companyList).ToString();
        }
        // Helper Class which helps to persist data for SLSystem
        private class PersistenceData
        {
            public List<CityCompanyItem> CompanyDictionary = new List<CityCompanyItem>();
            public PersistenceData(Dictionary<string, List<GoodCompany>> goodCompanies)
            {
                foreach (KeyValuePair<string, List<GoodCompany>> entry in goodCompanies)
                {
                    CompanyDictionary.Add(new CityCompanyItem(entry.Key, entry.Value));
                }
            }
            public Dictionary<string, List<GoodCompany>> ToCityCompanyDictionary()
            {
                Dictionary<string, List<GoodCompany>> dict = new Dictionary<string, List<GoodCompany>>();
                foreach (CityCompanyItem item in CompanyDictionary)
                {
                    dict[item.CityName] = item.GoodCompanies;
                }
                return dict;
            }
            public override string ToString() => JsonUtility.ToJson(this);
        }
        [System.Serializable]
        private class CityCompanyItem
        {
            public string CityName;
            public List<GoodCompany> GoodCompanies;

            public CityCompanyItem(string cityName, List<GoodCompany> goodCompanies)
            {
                CityName = cityName;
                GoodCompanies = goodCompanies;
            }
        }
    }
}
