using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompanySystem
{
    // TODO Figure out a way to increase or decrease relationship time based
    //? automatic decrease should not happen if you have done contracts for the company 
    public class Relationship
    {
        public readonly Company Company;
        private int _relation;
        public int Releation => _relation;
        public Relationship(Company company, int relationship)
        {
            Company = company;
            _relation = relationship;
        }
        public void ChangeRelation(int ammount)
        {
            _relation += ammount;
            if (_relation > 100) _relation = 100;
            if (_relation < -100) _relation = -100;
        }

        public override string ToString()
        {
            return Releation + "/100";
        }
    }
}
