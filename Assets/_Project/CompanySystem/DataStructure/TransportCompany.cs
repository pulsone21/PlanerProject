using System.Collections;
using System.Collections.Generic;
using RoadSystem;
using UnityEngine;
using EmployeeSystem;
using MailSystem;
using VehicleSystem;
using FinanceSystem;
using ContractSystem;
using System;

namespace CompanySystem
{
    [System.Serializable]
    public class TransportCompany : Company
    {
        [SerializeField] private List<TransportContract> _transportContracts;
        [SerializeField] private EmployeeManager employeeManager;
        [SerializeField] private MailManager mailManager;
        [SerializeField] private VehicleFleet vehicleFleet;
        [SerializeField] private FinanceManager fincanceManager;
        public EmployeeManager EmployeeManager => employeeManager;
        public MailManager MailManager => mailManager;
        public VehicleFleet VehicleFleet => vehicleFleet;
        public FinanceManager FinanceManager => fincanceManager;
        public TransportCompany(string name, string cityName, float startMoney) : base(name, cityName)
        {
            _transportContracts = new List<TransportContract>();
            employeeManager = new EmployeeManager();
            mailManager = new MailManager();
            vehicleFleet = new VehicleFleet(this);
            fincanceManager = new FinanceManager(startMoney);
        }

        public TransportCompany(
            string name, string cityName, List<TransportContract> contracts,
            EmployeeManager EmployeeManager, VehicleFleet VehicleFleet, FinanceManager FinanceManager,
            MailManager MailManager) : base(name, cityName)
        {
            _transportContracts = contracts;
            employeeManager = EmployeeManager;
            vehicleFleet = VehicleFleet;
            mailManager = MailManager;
            fincanceManager = FinanceManager;
        }

        public bool AddNewTransportContract(TransportContract contract)
        {
            _transportContracts.Add(contract);
            contract.SetCompanyReceiver(this);
            contract.OnClose += () => _transportContracts.Remove(contract);
            return true;
        }

        public List<TransportContract> GetContractsByState(TransportContract.State state)
        {
            List<TransportContract> outContracts = new List<TransportContract>();
            foreach (TransportContract contract in _transportContracts)
            {
                if (contract.CurrentState == state)
                {
                    outContracts.Add(contract);
                }
            }
            return outContracts;
        }
    }
}
