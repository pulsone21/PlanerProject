using System.Collections;
using System.Collections.Generic;
using RoadSystem;
using UnityEngine;
using ContractSystem;
using System;

namespace CompanySystem
{
    [System.Serializable]
    public class TransportCompany : Company
    {
        private float money;
        private List<TransportContract> _transportContracts;
        public TransportCompany(string name, City city, float startMoney) : base(name, city)
        {
            _transportContracts = new List<TransportContract>();
            money = startMoney;
        }
        public bool AddNewTransportContract(TransportContract contract)
        {
            if (!CanAffordContract(contract.ContractPrice)) return false;
            _transportContracts.Add(contract);
            contract.SetCompanyReceiver(this);
            return true;
        }
        //leaves the flexibilty to adjust contract price based on company relationship
        private bool CanAffordContract(float contractPrice)
        {
            if (money > contractPrice)
            {
                money += contractPrice;
                return true;
            }
            return false;
        }

    }
}
