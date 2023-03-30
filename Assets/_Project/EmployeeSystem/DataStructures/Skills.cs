using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    public class Skills : IEnumerable<EmployeeStat>
    {
        public Skill Adaptability { get; protected set; }
        public Skill Determination { get; protected set; }
        public Skill Driving { get; protected set; }
        public Skill Law { get; protected set; }
        public Skill Leadership { get; protected set; }
        public Skill Mechanic { get; protected set; }
        public Skill Negotiation { get; protected set; }
        public Skill Planing { get; protected set; }
        public Happines Happines { get; protected set; }
        public Loyalty Loyalty { get; protected set; }
        public Stress Stress { get; protected set; }
        private List<EmployeeStat> Stats;
        public Skills(Skill adaptability,
                        Skill determination,
                        Skill driving,
                        Happines happines,
                        Skill law,
                        Skill leadership,
                        Loyalty loyalty,
                        Skill mechanic,
                        Skill negotiation,
                        Skill planing,
                        Stress stress)
        {
            Adaptability = adaptability;
            Determination = determination;
            Driving = driving;
            Happines = happines;
            Law = law;
            Leadership = leadership;
            Loyalty = loyalty;
            Mechanic = mechanic;
            Negotiation = negotiation;
            Planing = planing;
            Stress = stress;
            Stats = new List<EmployeeStat>() { Adaptability, Determination, Driving, Happines, Law, Leadership, Loyalty, Mechanic, Negotiation, Planing, Stress };
        }
        public Skills(int maxSkillLevel) => GenerateSkills(maxSkillLevel);
        public static Skills GenerateSkills(int maxSkillLevel)
        {
            Skills skills = new(
                adaptability: new Skill(Random.Range(0, Mathf.Abs(maxSkillLevel)), "Adaptability", SkillType.Mental),
                determination: new Skill(Random.Range(0, Mathf.Abs(maxSkillLevel)), "Determination", SkillType.Mental),
                driving: new Skill(Random.Range(0, Mathf.Abs(maxSkillLevel)), "Driving", SkillType.Proficiency),
                law: new Skill(Random.Range(0, Mathf.Abs(maxSkillLevel)), "Law", SkillType.Proficiency),
                leadership: new Skill(Random.Range(0, Mathf.Abs(maxSkillLevel)), "Leadership", SkillType.Mental),
                mechanic: new Skill(Random.Range(0, maxSkillLevel), "Mechanic", SkillType.Proficiency),
                negotiation: new Skill(Random.Range(0, maxSkillLevel), "Negotiation", SkillType.Proficiency),
                planing: new Skill(Random.Range(0, maxSkillLevel), "Planing", SkillType.Proficiency),
                loyalty: new Loyalty(),
                happines: new Happines(),
                stress: new Stress()
            );
            return skills;
        }

        public IEnumerator<EmployeeStat> GetEnumerator()
        {
            foreach (EmployeeStat value in Stats)
            {
                yield return value;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
