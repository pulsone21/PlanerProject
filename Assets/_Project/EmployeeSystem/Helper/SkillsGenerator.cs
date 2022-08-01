using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    public static class SkillsGenerator
    {

        public static List<Skill> GenerateSkills(int maxSkillLevel)
        {
            List<Skill> skills = new List<Skill>();
            skills[(int)SkillName.Adaptability] = new Skill(Random.Range(0, maxSkillLevel), SkillName.Adaptability);
            skills[(int)SkillName.Determination] = new Skill(Random.Range(0, maxSkillLevel), SkillName.Determination);
            skills[(int)SkillName.Leadership] = new Skill(Random.Range(0, maxSkillLevel), SkillName.Leadership);
            skills[(int)SkillName.Driving] = new Skill(Random.Range(0, maxSkillLevel), SkillName.Driving);
            skills[(int)SkillName.Law] = new Skill(Random.Range(0, maxSkillLevel), SkillName.Law);
            skills[(int)SkillName.Mechanic] = new Skill(Random.Range(0, maxSkillLevel), SkillName.Mechanic);
            skills[(int)SkillName.Negotiation] = new Skill(Random.Range(0, maxSkillLevel), SkillName.Negotiation);
            skills[(int)SkillName.Planing] = new Skill(Random.Range(0, maxSkillLevel), SkillName.Planing);
            return skills;
        }

    }
}
