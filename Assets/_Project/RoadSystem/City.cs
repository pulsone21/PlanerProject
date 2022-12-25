using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanySystem;
using ContractSystem;
namespace RoadSystem
{
    [System.Serializable]
    public class City
    {
        [SerializeField] private string _name;
        [SerializeField] private bool isCaptial;
        [SerializeField] private int _citizen;
        public City(string name, int citizen)
        {
            _name = name;
            _citizen = citizen;
        }
        public bool IsCaptial => isCaptial;
        public int Citizen => _citizen;
        public string Name => _name;
        public override string ToString() => _name;
    }
}
