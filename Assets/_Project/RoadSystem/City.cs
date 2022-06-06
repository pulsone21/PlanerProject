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
        [SerializeField] private int _citizen;
        [SerializeField] private List<RoadNode> _connections;
        [SerializeField] private List<GoodCompany> _companies;

        public City(string name, int citizen)
        {
            _name = name;
            _citizen = citizen;
            _connections = new List<RoadNode>();
            _companies = new List<GoodCompany>();
        }

        public int Citizen => _citizen;
        public string Name => _name;

        public List<GoodCompany> Companies => _companies;

        public GoodCompany GetRndCompany() => Companies[Random.Range(0, Companies.Count)];
        public void AddConnection(RoadNode newConnection) { if (!_connections.Contains(newConnection)) _connections.Add(newConnection); }
        public void AddCompany(GoodCompany newCompany) { if (!Companies.Contains(newCompany)) Companies.Add(newCompany); }
        public void AddCompanies(List<GoodCompany> newCompanies) => Companies.AddRange(newCompanies);
        public void ClearConnection() => _connections.Clear();

        public bool HasCompanyWithCategory(GoodCategory category)
        {
            foreach (GoodCompany company in Companies)
            {
                if (company.GoodCategory == category) return true;
            }
            return false;
        }

        public void DeleteCompanies() => _companies = new List<GoodCompany>();

    }
}
