using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CompanySystem;

namespace ContractSystem
{
    public abstract class Contract
    {
        public readonly Company ContractGiver;
        private Company _contractReciever;
        public Company ContractReciever => _contractReciever;
        protected Contract(Company contractPartner1)
        {
            ContractGiver = contractPartner1;
        }

        private Action OnContractRecieverChange;
        public void RegisterOnContractRecieverChange(Action action) => OnContractRecieverChange += action;
        public void UnregisterOnContractRecieverChange(Action action) => OnContractRecieverChange -= action;


        public virtual void SetCompanyReceiver(Company company)
        {
            _contractReciever = company;
            OnContractRecieverChange?.Invoke();
        }
    }
}
