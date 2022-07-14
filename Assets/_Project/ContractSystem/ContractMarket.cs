using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeSystem;
using System;

namespace ContractSystem
{
    public class ContractMarket : MonoBehaviour
    {
        public static ContractMarket Instance;
        private List<TransportContract> contracts = new List<TransportContract>();
        private void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this;
            }
        }
        private void Start() => TimeManager.Instance.RegisterForTimeUpdate(GenerateNewContracts, TimeManager.SubscriptionType.Month);
        private void OnDestroy() => TimeManager.Instance.UnregisterForTimeUpdate(GenerateNewContracts, TimeManager.SubscriptionType.Month);
        private void GenerateNewContracts(TimeStamp timeStamp)
        {
            List<TransportContract> newContracts = ContractGenerator.GenerateContracts(UnityEngine.Random.Range(1, 50));
            contracts.AddRange(newContracts);
        }
        public List<TransportContract> GetTransportContractsByTranportType(TransportType type)
        {
            List<TransportContract> outList = new List<TransportContract>();
            foreach (TransportContract contract in contracts)
            {
                if (contract.Good.transportType == type)
                {
                    outList.Add(contract);
                }
            }
            return outList;
        }
        public TransportContract ReceiveContract(TransportContract Contract)
        {
            foreach (TransportContract contract in contracts)
            {
                if (contract == Contract)
                {
                    TransportContract outContract = contract;
                    contracts.Remove(outContract);
                    return outContract;
                }
            }
            return default;
        }
    }
}
