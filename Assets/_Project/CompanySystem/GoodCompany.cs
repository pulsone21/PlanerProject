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
        public readonly GoodCategory GoodCategory;
        public readonly TransportGood[] TransportGoods;

        public GoodCompany(GoodCategory goodCategory, TransportGood[] transportGoods, string Name, City city) : base(Name, city)
        {
            GoodCategory = goodCategory;
            TransportGoods = transportGoods;
        }
    }
}
