using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CompanySystem;

namespace ContractSystem
{
    public abstract class Contract<T> : MonoBehaviour where T : Company
    {
        public Company ContractGiver { get; protected set; }
        public T ContractReciever { get; protected set; }
        private Action OnContractRecieverChange;
        public void RegisterOnContractRecieverChange(Action action) => OnContractRecieverChange += action;
        public void UnregisterOnContractRecieverChange(Action action) => OnContractRecieverChange -= action;
        public virtual void SetCompanyReceiver(T company)
        {
            ContractReciever = company;
            OnContractRecieverChange?.Invoke();
        }
    }
}
