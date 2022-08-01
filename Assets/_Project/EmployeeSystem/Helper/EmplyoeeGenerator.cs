using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using TimeSystem;

namespace EmployeeSystem
{
    public static class EmplyoeeGenerator
    {
        private static Keyframe[] frames = new Keyframe[4] { new Keyframe(19, 35), new Keyframe(28, 65), new Keyframe(40, 101), new Keyframe(60, 68) };
        private static AnimationCurve curve = new AnimationCurve(frames);
        public static List<Employee> GenerateEmployees(int amount)
        {
            List<Employee> employees = new List<Employee>();

            for (int i = 0; i < amount; i++)
            {
                TimeStamp birthday = GenerateRndTimeStamp();
                int maxSkillLevel = CalculateMaxSkillLevel(birthday.DifferenceToNowInYears());
                List<Skill> skills = SkillsGenerator.GenerateSkills(maxSkillLevel);
                Happines happy = new Happines(Random.Range(0, maxSkillLevel));
                Loyalty loyal = new Loyalty(Random.Range(0, maxSkillLevel));
                Stress stress = new Stress(Random.Range(0, maxSkillLevel));
                Employee.EmployeeName name = GenerateEmployeeName();
                Employee employee = new Employee(skills, happy, loyal, stress, name, birthday);
                employees.Add(employee);
            }
            return employees;
        }

        private static int CalculateMaxSkillLevel(int age)
        {
            int maxSkillLevel = Mathf.FloorToInt(curve.Evaluate(age));
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
