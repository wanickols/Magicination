using UnityEngine;
using UnityEngine.UI;

namespace MGCNTN
{
    public class SkillHolder : MonoBehaviour
    {
        [SerializeField] private Skill skill;

        public Skill Skill
        {
            get => skill;
            set
            {
                forgetSkill();
                skill = value;
                refresh();
            }
        }
        [SerializeField] private Image icon;
        [SerializeField] private Image background;

        public TreeNode skillNode;
        public bool isNode(TreeNode node) => skillNode == node;


        ///Private
        private void refresh()
        {
            if (skill != null)
            {
                icon.sprite = skill.Data.sprite;
                icon.enabled = true;
            }
            else
            {
                icon.sprite = null;
                icon.enabled = false;
            }


        }

        private void Awake()
        {
            skillNode = GetComponent<TreeNode>();

            if (!skill)
                return;

            checkSkillVisibility();
            skill.Data.changedStatus += checkSkillVisibility;
        }

        private void checkSkillVisibility()
        {
            switch (skill.Data.skillStatus)
            {
                case SkillStatus.unlocked:
                    icon.color = Color.white;
                    icon.enabled = true;
                    break;
                case SkillStatus.locked:
                    icon.color = Color.black;
                    icon.enabled = true;
                    break;
                case SkillStatus.hidden:
                    icon.color = Color.black;
                    icon.enabled = false;
                    break;
            }
        }

        private void forgetSkill()
        {
            if (skill)
                skill.Data.changedStatus -= checkSkillVisibility;
        }

        private void OnDestroy() => skill = null;


    }
}