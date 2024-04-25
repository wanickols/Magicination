
using System.Collections.Generic;

namespace MGCNTN
{
    public class SkillsCollection
    {
        public int Count => skillList.Count;

        private const int MaxSkillsAllowed = 8;
        private List<Skill> skillList = new List<Skill>(new Skill[MaxSkillsAllowed]);

        public Skill Get(int index)
        {

            if (skillList.Count > index && index > -1)
                return skillList[index];

            return null;
        }

        public List<Skill> GetSkills() => skillList;

        public void Add(int index, Skill skill) => skillList[index] = skill;

        public void Remove(int index) => skillList.RemoveAt(index);

        public void Clear()
        {
            for (int i = 0; i < skillList.Count; i++)
                skillList[i] = null;
        }
    }
}