using UnityEngine;
using System.Collections.Generic;
using CompanySystem;
using ContractSystem;

namespace RoadSystem
{
    public class CityController : MonoBehaviour
    {
        [SerializeField] public List<RoadNode> _connections = new List<RoadNode>();
        public List<RoadNode> Connections => _connections;
        [SerializeField] private City _city;
        public City City => _city;
        public List<GoodCompany> Companies => CompanyManager.Instance.CompanyList[_city.Name];
        public GoodCompany GetRndCompany() => Companies[Random.Range(0, Companies.Count)];
        public bool HasCompanyWithCategory(GoodCategory category)
        {
            foreach (GoodCompany company in Companies)
            {
                if (company.GoodCategory == category) return true;
            }
            return false;
        }




        public void SETUP_AddCity(City city)
        {
            if (_city != null) return;
            _city = city;
        }
        public void SETUP_SetConnections(List<RoadNode> nodes) => _connections = nodes;
    }
}
