using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadSystem;
using System;

namespace CompanySystem
{
    public abstract class Company
    {
        [SerializeField] private string name;
        public string Name => name;
        [SerializeField] protected List<Relationship> _relationships;
        public List<Relationship> Relationships => _relationships;
        public readonly City City;
        protected Action OnRelationshipChange;
        protected Company(string name, City city)
        {
            this.name = name;
            City = city;
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
                if (relationship.Company == PlayerCompanyController.Company) return relationship;
            }
            return default;
        }



    }
}
