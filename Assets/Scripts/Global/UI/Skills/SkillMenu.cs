using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{
    public class SkillMenu : MonoBehaviour
    {
        [SerializeField] private GameObject content; //content window

        private List<SkillOption> options = new List<SkillOption>();

        private BattleData data;
        private MemberBattleInfo currMember => data.currentActor.memberBattleInfo;

        public bool hasSkills()
        {
            if (data == null || currMember.activeSkills.Count <= 0)
                return false;

            return true;
        }

        public void setBattleData(BattleData data) => this.data = data;

        public void initSkills()
        {
            if (data == null)
            {
                clearSkills();
                return;
            }


            int i = 0;
            foreach (Transform t in content.transform)
            {
                SkillOption option = t.GetComponent<SkillOption>();

                if (option == null)
                    continue;

                if (i < currMember.activeSkills.Count)
                {
                    t.gameObject.SetActive(true);
                    option.changeOption(currMember.activeSkills.GetSkills()[i]);
                    option.hasEnergy(currMember.activeSkills.Get(i).Data.cost <= currMember.Stats.ENG);
                    options.Add(option);
                }
                else
                    t.gameObject.SetActive(false);

                i++;
            }
        }


        public Skill selectSkill(int index)
        {
            if (index >= 0 && index < options.Count)
            {
                if (options[index].skill.Data.cost > currMember.Stats.ENG)
                    return null;

                return options[index].skill;
            }



            return null;
        }

        ///Private Functions
        private void clearSkills()
        {
            foreach (Transform t in content.transform)
                t.gameObject.SetActive(false);
        }
    }
}