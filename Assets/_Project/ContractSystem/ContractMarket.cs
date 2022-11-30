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
        [SerializeField] private List<TransportContract> contracts = new List<TransportContract>();
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
            GenerateNewContracts();
        }
        private void Start()
        {
            TimeManager.Instance.RegisterForTimeUpdate(GenerateNewContracts, TimeManager.SubscriptionType.Month);
        }
        private void GenerateNewContracts()
        {
            List<TransportContract> newContracts = TransportContractGenerator.GenerateContracts(UnityEngine.Random.Range(1, 50));
            contracts.AddRange(newContracts);
        }
        public static List<TransportContract> GetTransportContractsByTranportType(TransportType type)
        {

            List<TransportContract> outList = new List<TransportContract>();
            foreach (TransportContract contract in Instance.contracts)
            {
                if (contract.TransportType == type)
                {
                    outList.Add(contract);
                }
            }
            return outList;
        }
        public static TransportContract ReceiveContract(TransportContract Contract)
        {
            foreach (TransportContract contract in Instance.contracts)
            {
                if (contract == Contract)
                {
                    TransportContract outContract = contract;
                    Instance.contracts.Remove(outContract);
                    return outContract;
                }
            }
            return default;
        }
    }
}
