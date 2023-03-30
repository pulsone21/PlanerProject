using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using SLSystem;
using EmployeeSystem;
using ContractSystem;
using VehicleSystem;

namespace CompanySystem
{
    public class PlayerCompanyController : SerializedMonoBehaviour, IPersistenceData
    {
        public static PlayerCompanyController Instance;
        [Header("DebugTools")]
        [SerializeField] private TrailerSO testTrailer;
        [SerializeField] private VehicleSO testVehicle;
        [SerializeField] private bool DebugMode;
        [Space(2f)]
        [SerializeField] private TransportCompany _company;
        public TransportCompany Company => _company;
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

        private void Start()
        {
            if (DebugMode) SetUpTestCompany();
        }
        [Button("Genereate Test Company")]
        private void SetUpTestCompany()
        {
            _company = new TransportCompany("TestCompany", "Berlin", 9999999999f);
            List<Driver> drivers = EmplyoeeGenerator.GenerateDriver(5);
            foreach (Driver driver in drivers) _company.EmployeeManager.AddEmployeeToList(driver);
            _company.VehicleFleet.AddVehicle(new Vehicle(testVehicle));
            _company.VehicleFleet.AddVehicle(new Trailer(testTrailer));
        }

        public void Load(GameData gameData)
        {
            if (gameData.Data.ContainsKey(_className))
            {
                _company = JsonUtility.FromJson<TransportCompany>(gameData.Data[_className]);
            }
        }
        public void Save(ref GameData gameData)
        {
            gameData.Data[_className] = JsonUtility.ToJson(Company);
        }

        [Button("Serialize")]
        public void Serialize()
        {
            Debug.Log(JsonUtility.ToJson(Company));
        }

    }
}