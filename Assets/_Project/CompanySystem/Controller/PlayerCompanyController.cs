using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planer;
using RoadSystem;
using Sirenix.OdinInspector;
using SLSystem;

namespace CompanySystem
{
    public class PlayerCompanyController : SerializedMonoBehaviour, IPersistenceData
    {
        public static PlayerCompanyController Instance;
        [SerializeField] public TransportCompany company;
        private string _className;
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
            _className = this.GetType().Name;
        }
        public void Load(GameData gameData)
        {
            if (gameData.Data.ContainsKey(_className))
            {
                company = JsonUtility.FromJson<TransportCompany>(gameData.Data[_className]);
            }
        }
        public void Save(ref GameData gameData)
        {
            gameData.Data[_className] = JsonUtility.ToJson(company);
        }

        [Button("Serialize")]
        public void Serialize()
        {
            Debug.Log(JsonUtility.ToJson(company));
        }
    }
}