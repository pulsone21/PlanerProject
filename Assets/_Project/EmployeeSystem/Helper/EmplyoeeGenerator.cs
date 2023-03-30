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
        public static List<Canidate> GenerateCanidates(int amount)
        {
            List<Canidate> canidates = new List<Canidate>();

            for (int i = 0; i < amount; i++)
            {
                TimeStamp birthday = GenerateRndTimeStamp();
#if !UNITY_EDITOR
                int age = birthday.DifferenceToNowInYears();
                int maxSkillLevel = CalculateMaxSkillLevel(age);
#else
                int maxSkillLevel = 100;
#endif
                Skills skills = Skills.GenerateSkills(maxSkillLevel);
                Employee.EmployeeName name = GenerateEmployeeName();
                Canidate canidate = new Canidate(skills, name, birthday);
                canidates.Add(canidate);
            }
            return canidates;
        }
        public static List<Driver> GenerateDriver(int amount)
        {
            List<Driver> drivers = new List<Driver>();
            List<Canidate> canidates = GenerateCanidates(amount);
            foreach (Canidate canidate in canidates) drivers.Add(new(canidate));
            return drivers;
        }
        public static List<Accountant> GenerateAccountant(int amount)
        {
            List<Accountant> accountants = new List<Accountant>();
            List<Canidate> canidates = GenerateCanidates(amount);
            foreach (Canidate canidate in canidates) accountants.Add(new(canidate));
            return accountants;
        }
        public static List<Dispatcher> GenerateDispatcher(int amount)
        {
            List<Dispatcher> dispatchers = new List<Dispatcher>();
            List<Canidate> canidates = GenerateCanidates(amount);
            foreach (Canidate canidate in canidates) dispatchers.Add(new(canidate));
            return dispatchers;
        }

        private static int CalculateMaxSkillLevel(int age) => Mathf.FloorToInt(curve.Evaluate(age));

        private static TimeStamp GenerateRndTimeStamp()
        {
#if UNITY_EDITOR
            return new TimeStamp(0, 0, 8, 7, 1994);
#else
            TimeStamp timestamp = TimeStamp.GetRndBirthday();
            while (timestamp.DifferenceToNowInYears() > 69)
            {
                timestamp = TimeStamp.GetRndBirthday();
            }
            return timestamp;
#endif
        }


        private static Employee.EmployeeName GenerateEmployeeName()
        {
            HumanNames names = DataHandler.LoadFromJSON<HumanNames>("humanNames.json");
            return names.GetRndEmplyoeeName((HumanNames.Sex)Random.Range(0, 2));
        }
    }
}
