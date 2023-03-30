using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeSystem;
using System;
using SLSystem;
using Sirenix.OdinInspector;

namespace ContractSystem
{
    public class ContractMarket : MonoBehaviour, IPersistenceData
    {
        public static ContractMarket Instance;
        [SerializeField] private List<TransportContract> contracts = new List<TransportContract>();
        private string _className => this.GetType().ToString();
        public GameObject This => gameObject;
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
        private void Start() => TimeManager.Instance.RegisterForTimeUpdate(GenerateNewContracts, TimeManager.SubscriptionType.Month);

        private void GenerateNewContracts()
        {
            List<RawContractDetails> newContracts = TransportContractGenerator.GenerateContracts(UnityEngine.Random.Range(1, 50));
            foreach (RawContractDetails details in newContracts)
            {
                TransportContract newContract = new GameObject(details.ContractName).AddComponent<TransportContract>();
                newContract.transform.SetParent(transform);
                newContract.Initialize(details);
                contracts.Add(newContract);
            }
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
        public void DeleteContract(TransportContract contract)
        {
            GameObject contractGO = contracts.Find((a) => a == contract).gameObject;
            contracts.Remove(contract);
            Destroy(contractGO);
        }
        public void Load(GameData gameData)
        {
            if (gameData.Data.ContainsKey(_className))
            {
                contracts = JsonUtility.FromJson<List<TransportContract>>(gameData.Data[_className]);
            }
        }
        public void Save(ref GameData gameData)
        {
            gameData.Data[_className] = JsonUtility.ToJson(contracts);
        }

        [Button("GenerateContract")]
        public void GenerateContract()
        {
            List<RawContractDetails> newContracts = TransportContractGenerator.GenerateContracts(1);
            TransportContract newContract = new GameObject(newContracts[0].ContractName).AddComponent<TransportContract>();
            newContract.transform.SetParent(transform);
            newContract.Initialize(newContracts[0]);
            contracts.Add(newContract);
        }


        [Button("Serilize")]
        private void Serializable()
        {
            foreach (TransportContract item in contracts)
            {
                Debug.Log(JsonUtility.ToJson(item));
            }
            //TODO gets not serialized
            Debug.Log(JsonUtility.ToJson(contracts.ToArray()));
        }
    }
}