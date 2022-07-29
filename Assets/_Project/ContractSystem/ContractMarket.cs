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
        }
        private void Start()
        {
            GenerateNewContracts(TimeManager.Instance.CurrentTimeStamp);
            TimeManager.Instance.RegisterForTimeUpdate(GenerateNewContracts, TimeManager.SubscriptionType.Month);
        }
        private void OnDestroy() => TimeManager.Instance.UnregisterForTimeUpdate(GenerateNewContracts, TimeManager.SubscriptionType.Month);
        private void GenerateNewContracts(TimeStamp timeStamp)
        {
            List<TransportContract> newContracts = ContractGenerator.GenerateContracts(UnityEngine.Random.Range(1, 50));
            contracts.AddRange(newContracts);
        }
        public static List<TransportContract> GetTransportContractsByTranportType(TransportType type)
        {
            Debug.Log(type);
            List<TransportContract> outList = new List<TransportContract>();
            foreach (TransportContract contract in Instance.contracts)
            {
                if (contract.TransportType == type)
                {
                    outList.Add(contract);
                }
            }
            Debug.Log("Working Contracts: " + outList.Count.ToString());
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
