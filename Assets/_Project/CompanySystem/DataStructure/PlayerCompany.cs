using System.Collections;
using System.Collections.Generic;
using RoadSystem;
using UnityEngine;
using EmployeeSystem;
using MailSystem;
using VehicleSystem;
namespace CompanySystem
{
    [System.Serializable]
    public class PlayerCompany : TransportCompany
    {
        [SerializeField] private EmployeeManager employeeManager;
        [SerializeField] private MailManager mailManager;
        [SerializeField] private VehicleFleet vehicleFleet;
        public EmployeeManager EmployeeManager => employeeManager;
        public MailManager MailManager => mailManager;
        public VehicleFleet VehicleFleet => vehicleFleet;
        public PlayerCompany(string name, City city, float startMoney) : base(name, city, startMoney)
        {
            this.employeeManager = new EmployeeManager();
            mailManager = new MailManager();
            vehicleFleet = new VehicleFleet(this);
        }
    }
}
