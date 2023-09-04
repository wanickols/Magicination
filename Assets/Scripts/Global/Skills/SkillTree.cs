using System.Collections.Generic;

namespace MGCNTN
{
    public class SkillTree
    {
        private List<Skill> unlockedSkills;
        private List<Skill> lockedSkills;
        private List<Skill> hiddenSkills;

        public void Unlock(Skill skill)
        {
            if (!skill || unlockedSkills.Contains(skill) || (!lockedSkills.Contains(skill) && !hiddenSkills.Contains(skill)))
                return;

            unlockedSkills.Add(skill);
            if (!lockedSkills.Remove(skill))
                hiddenSkills.Remove(skill);

            skill.unlock();
        }

        //? Not sure yet
        public void discover(Skill skill)
        {
            if (!skill) return;

            lockedSkills.Add(skill);
            hiddenSkills.Remove(skill);
            skill.discover();
        }

        //??Not sure on this
        public void hide(Skill skill)
        {
            if (!skill) return;

            skill.hide();

            hiddenSkills.Add(skill);

            if (!unlockedSkills.Remove(skill))
                lockedSkills.Remove(skill);
        }
    }
}