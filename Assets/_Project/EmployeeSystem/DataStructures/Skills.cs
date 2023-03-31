using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [System.Serializable]
    public class Skills : IEnumerable<Skill>
    {
        public Skill Adaptability { get; protected set; }
        public Skill Determination { get; protected set; }
        public Skill Leadership { get; protected set; }
        public Skill Driving { get; protected set; }
        public Skill Law { get; protected set; }
        public Skill Mechanic { get; protected set; }
        public Skill Negotiation { get; protected set; }
        public Skill Planing { get; protected set; }
        private List<Skill> skills;
        public Skills(Skill adaptability,
                        Skill determination,
                        Skill driving,
                        Skill law,
                        Skill leadership,
                        Skill mechanic,
                        Skill negotiation,
                        Skill planing)
        {
            Adaptability = adaptability;
            Determination = determination;
            Driving = driving;
            Law = law;
            Leadership = leadership;
            Mechanic = mechanic;
            Negotiation = negotiation;
            Planing = planing;
            skills = new List<Skill>() { Adaptability, Determination, Leadership, Driving, Law, Mechanic, Negotiation, Planing };
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
                planing: new Skill(Random.Range(0, maxSkillLevel), "Planing", SkillType.Proficiency)
            );
            return skills;
        }

        public IEnumerator<Skill> GetEnumerator()
        {
            foreach (Skill value in skills)
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
