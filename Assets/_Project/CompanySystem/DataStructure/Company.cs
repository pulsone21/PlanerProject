using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadSystem;
using System;

namespace CompanySystem
{
    public abstract class Company
    {
        public readonly string Name;
        private List<Relationship> _relationships;
        public List<Relationship> Relationships => _relationships;
        public readonly City City;
        private Action OnRelationshipChange;
        protected Company(string name, City city)
        {
            Name = name;
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




    }
}
