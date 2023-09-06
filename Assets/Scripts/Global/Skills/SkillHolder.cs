using UnityEngine;
using UnityEngine.UI;

namespace MGCNTN
{
    public class SkillHolder : MonoBehaviour
    {
        public Skill skill;
        private Image background;
        private Image icon;

        public SkillNode skillNode;

        private void Awake()
        {
            skillNode = GetComponent<SkillNode>();
            background = GetComponent<Image>();
            icon = GetComponentInChildren<Image>();

            if (!skill)
                return;

            icon.sprite = skill.Data.sprite;
            checkSkillVisibility();
            skill.Data.changedStatus += checkSkillVisibility;
        }

        private void checkSkillVisibility()
        {
            switch (skill.Data.skillStatus)
            {
                case SkillStatus.unlocked:
                    background.color = new Color(175, 175, 175);
                    icon.color = Color.white;
                    icon.enabled = true;
                    break;
                case SkillStatus.locked:
                    background.color = new Color(175, 175, 175);
                    icon.color = Color.black;
                    icon.enabled = true;
                    break;
                case SkillStatus.hidden:
                    background.color = Color.black;
                    icon.enabled = false;
                    break;
            }
        }

        public bool isNode(SkillNode node) => skillNode == node;

        private void OnDestroy()
        {
            if (skill)
                skill.Data.changedStatus -= checkSkillVisibility;
        }

    }
}