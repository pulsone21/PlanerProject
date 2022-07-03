using System.Collections;
using System.Collections.Generic;
using RoadSystem;
using UnityEngine;
using EmployeeSystem;
using MailSystem;

namespace CompanySystem
{
    public class PlayerCompany : TransportCompany
    {
        public readonly EmployeeManager EmployeeManager;
        public readonly MailManager MailManager;



        public PlayerCompany(string name, City city, float startMoney) : base(name, city, startMoney)
        {
            this.EmployeeManager = new EmployeeManager();
            MailManager = new MailManager();
        }
    }
}
