using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadSystem;
using SLSystem;
using Utilities;
namespace CompanySystem
{
    public class CompanyManager : MonoBehaviour, IPersistenceData
    {
        [SerializeField] private HashSet<GoodCompany> goodCompanies = new HashSet<GoodCompany>();
        public List<GoodCompany> GoodCompanies => goodCompanies.ToList();
        private string _className => GetType().Name;
        public GameObject This => gameObject;
        public void Load(GameData gameData)
        {
            if (gameData.Data.ContainsKey(_className))
            {
                PersistenceData persistenceData = JsonUtility.FromJson<PersistenceData>(gameData.Data[_className]);
                goodCompanies = persistenceData.GoodCompanies.ToHashSet();
            }
        }

        public void Save(ref GameData gameData)
        {
            gameData.Data[_className] = new PersistenceData(goodCompanies.ToList()).ToString();
        }

        public void AddGoodCompanies(HashSet<GoodCompany> companies)
        {
            foreach (GoodCompany company in companies)
            {
                goodCompanies.Add(company);
            }
        }

        // Helper Class which helps to persist data for SLSystem
        private class PersistenceData
        {
            public List<GoodCompany> GoodCompanies;
            public PersistenceData(List<GoodCompany> goodCompanies)
            {
                GoodCompanies = goodCompanies;
            }
            public override string ToString() => JsonUtility.ToJson(this);
        }


    }
}
