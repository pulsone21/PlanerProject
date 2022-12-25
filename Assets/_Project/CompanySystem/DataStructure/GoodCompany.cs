using System.Collections;
using System.Collections.Generic;
using ContractSystem;
using UnityEngine;
using RoadSystem;


namespace CompanySystem
{
    [System.Serializable]
    public class GoodCompany : Company
    {
        [SerializeField] private GoodCategory _goodCategory;
        public GoodCategory GoodCategory => _goodCategory;
        [SerializeField] private List<TransportGood> _transportGoods;
        public TransportGood[] TransportGoods => _transportGoods.ToArray();
        public GoodCompany(GoodCategory goodCategory, List<TransportGood> transportGoods, string Name, string cityName) : base(Name, cityName)
        {
            _goodCategory = goodCategory;
            _transportGoods = transportGoods;
        }
    }
}
