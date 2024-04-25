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

        private void OnEnable() => initSkills();
        private void OnDisable() => clearSkills();

        public void setBattleData(BattleData data) => this.data = data;

        public void initSkills()
        {
            if (data == null)
                return;

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
                    options.Add(option);
                }
                else
                    t.gameObject.SetActive(false);

                i++;
            }
        }

        public Skill selectSkill(int index) => options[index].skill;

        ///Private Functions
        private void clearSkills()
        {
            foreach (Transform t in content.transform)
                t.gameObject.SetActive(false);
        }
    }
}