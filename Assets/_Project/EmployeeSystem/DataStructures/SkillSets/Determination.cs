namespace EmployeeSystem
{
    [System.Serializable]
    public class Determination : SkillSet
    {
        public Determination(int skill) : base(skill)
        {
            _type = SkillType.Mental;
        }
    }
}