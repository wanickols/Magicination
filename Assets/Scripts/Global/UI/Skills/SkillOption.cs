using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MGCNTN
{
    public class SkillOption : Option
    {

        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI energy;
        public Skill skill { get; private set; }

        public void changeOption(Skill skill)
        {
            this.skill = skill;

            if (skill != null)
                updateOption();
        }

        public override void clear()
        {
            base.clear();
            icon.sprite = null;
            skill = null;
        }

        private void updateOption()
        {
            updateText(skill.name);
            energy.text = "" + skill.Data.cost;
            icon.sprite = skill.Data.sprite;
        }

        public void hasEnergy(bool valid)
        {
            if (valid)
                energy.color = Color.white;
            else
                energy.color = Color.red;
        }
    }
}