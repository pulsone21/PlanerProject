using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using TimeSystem;

namespace EmployeeSystem
{
    public static class EmplyoeeGenerator
    {
        public static List<Employee> GenerateEmployees(int amount)
        {
            List<Employee> employees = new List<Employee>();

            for (int i = 0; i < amount; i++)
            {
                TimeStamp birthday = GenerateRndTimeStamp();
                int maxSkillLevel = CalculateMaxSkillLevel(birthday.DifferenceToNowInYears());
                Law lawSkill = new Law(Random.Range(0, maxSkillLevel));
                Adaptability adapt = new Adaptability(Random.Range(0, maxSkillLevel));
                Determination determ = new Determination(Random.Range(0, maxSkillLevel));
                Driving driv = new Driving(Random.Range(0, maxSkillLevel));
                Happines happy = new Happines(Random.Range(0, maxSkillLevel));
                Leadership leader = new Leadership(Random.Range(0, maxSkillLevel));
                Loyalty loyal = new Loyalty(Random.Range(0, maxSkillLevel));
                Mechanic mech = new Mechanic(Random.Range(0, maxSkillLevel));
                Negotiation nego = new Negotiation(Random.Range(0, maxSkillLevel));
                Planing plan = new Planing(Random.Range(0, maxSkillLevel));
                Stress stress = new Stress(Random.Range(0, maxSkillLevel));
                Employee.EmployeeName name = GenerateEmployeeName();
                Employee employee = new Employee(adapt, determ, driv, happy, lawSkill, leader, loyal, mech, nego, plan, stress, name, birthday);
                employees.Add(employee);
            }
            return employees;
        }

        private static int CalculateMaxSkillLevel(int age)
        {
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(19, 35);
            curve.AddKey(28, 65);
            curve.AddKey(40, 101);
            curve.AddKey(60, 80);
            int maxSkillLevel = Mathf.FloorToInt(curve.Evaluate(age));
            Debug.Log("Max Skill Level: " + maxSkillLevel);
            return maxSkillLevel;
        }

        private static TimeStamp GenerateRndTimeStamp()
        {
            TimeStamp timestamp = TimeStamp.GetRndBirthday();
            while (timestamp.DifferenceToNowInYears() > 69)
            {
                timestamp = TimeStamp.GetRndBirthday();
            }
            return timestamp;
        }


        private static Employee.EmployeeName GenerateEmployeeName()
        {
            HumanNames names = DataHandler.LoadFromJSON<HumanNames>("humanNames.json");
            return names.GetRndEmplyoeeName((HumanNames.Sex)Random.Range(0, 2));
        }
    }
}
