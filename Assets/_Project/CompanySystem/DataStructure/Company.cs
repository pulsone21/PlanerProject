using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadSystem;
using System;

namespace CompanySystem
{
    [System.Serializable]
    public abstract class Company
    {
        [SerializeField] private string name;
        public string Name => name;
        [SerializeField] private string _cityName;
        [SerializeField] protected List<Relationship> _relationships;
        public List<Relationship> Relationships => _relationships;
        public City City => GetCity();
        private City GetCity()
        {
            if (CityManager.Instance.GetCityByName(_cityName, out CityController city))
            {
                return city.City;
            }
            return default;
        }

        public void SETUP_SetCityName(string name)
        {
            _cityName = name;
        }
        protected Action OnRelationshipChange;
        protected Company(string name, string cityName)
        {
            this.name = name;
            _cityName = cityName;
            _relationships = new List<Relationship>();
        }
        public void RegisterOnRelationshipChange(Action action) => OnRelationshipChange += action;
        public void UnregisterOnRelationshipChange(Action action) => OnRelationshipChange -= action;
        public void RelationshipChange(int ammount, Company company)
        {
            bool foundRelation = false;
            foreach (Relationship relationship in _relationships)
            {
                if (relationship.Company == company)
                {
                    relationship.ChangeRelation(ammount);
                    foundRelation = true;
                    break;
                }
            }
            if (!foundRelation) _relationships.Add(new Relationship(company, ammount));
            OnRelationshipChange?.Invoke();
        }

        public Relationship GetRelationshipToPlayerCompany()
        {
            foreach (Relationship relationship in _relationships)
            {
                if (relationship.Company == PlayerCompanyController.Instance.Company) return relationship;
            }
            return default;
        }



    }
}
