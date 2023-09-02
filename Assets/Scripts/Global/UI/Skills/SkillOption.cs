using UnityEngine;
using UnityEngine.UI;

namespace MGCNTN
{
    public class SkillOption : Option
    {

        [SerializeField] private Image icon;
        public Skill skill { get; private set; }

        public void changeOption(Skill skill)
        {
            this.skill = skill;

            if (skill != null)
                updateText();
        }

        public override void clear()
        {
            base.clear();
            icon.sprite = null;
            skill = null;
        }
    }
}