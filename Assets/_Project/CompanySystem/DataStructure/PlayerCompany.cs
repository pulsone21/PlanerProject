using System.Collections;
using System.Collections.Generic;
using RoadSystem;
using UnityEngine;
using EmployeeSystem;
using MailSystem;
using VehicleSystem;
namespace CompanySystem
{
    public class PlayerCompany : TransportCompany
    {
        public readonly EmployeeManager EmployeeManager;
        public readonly MailManager MailManager;
        public readonly VehicleFleet VehicleFleet;

        public PlayerCompany(string name, City city, float startMoney) : base(name, city, startMoney)
        {
            this.EmployeeManager = new EmployeeManager();
            MailManager = new MailManager();
            VehicleFleet = new VehicleFleet(this);
        }
    }
}
