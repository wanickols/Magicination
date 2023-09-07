using UnityEngine;
using UnityEngine.UI;

namespace MGCNTN
{
    public class SkillHolder : MonoBehaviour
    {
        public Skill skill;
        [SerializeField] private Image icon;
        [SerializeField] private Image background;

        public SkillNode skillNode;

        private void Awake()
        {
            skillNode = GetComponent<SkillNode>();

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

        public bool isNode(SkillNode node) => skillNode == node;

        private void OnDestroy()
        {
            if (skill)
                skill.Data.changedStatus -= checkSkillVisibility;
        }

    }
}