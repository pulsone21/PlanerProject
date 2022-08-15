using System.Collections;
using System.Collections.Generic;
using RoadSystem;
using UnityEngine;
using EmployeeSystem;
using MailSystem;
using VehicleSystem;
using FinanceSystem;
using ContractSystem;
namespace CompanySystem
{
    [System.Serializable]
    public class TransportCompany : Company
    {
        [SerializeField] private EmployeeManager employeeManager;
        [SerializeField] private MailManager mailManager;
        [SerializeField] private VehicleFleet vehicleFleet;
        [SerializeField] private FinanceManager fincanceManager;
        public EmployeeManager EmployeeManager => employeeManager;
        public MailManager MailManager => mailManager;
        public VehicleFleet VehicleFleet => vehicleFleet;
        public FinanceManager FinanceManager => fincanceManager;
        private List<TransportContract> _transportContracts;
        public TransportCompany(string name, City city, float startMoney) : base(name, city)
        {
            _transportContracts = new List<TransportContract>();
            employeeManager = new EmployeeManager();
            mailManager = new MailManager();
            vehicleFleet = new VehicleFleet(this);
            fincanceManager = new FinanceManager(startMoney);
        }
        public bool AddNewTransportContract(TransportContract contract)
        {
            _transportContracts.Add(contract);
            contract.SetCompanyReceiver(this);
            return true;
        }
    }
}
