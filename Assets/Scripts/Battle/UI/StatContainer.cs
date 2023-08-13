using TMPro;
using UnityEngine;

namespace Battle
{
    public class StatContainer : MonoBehaviour
    {

        [Header("Values")]
        [SerializeField] private TextMeshProUGUI memberName;
        [SerializeField] private TextMeshProUGUI memberHP, memberMP;

        [Header("Unchanging Text")]
        [SerializeField] private TextMeshProUGUI HPText;
        [SerializeField] private TextMeshProUGUI MPText;

        public void DisplayInfo(PartyMember partyMember)
        {
            Stats stats = partyMember.Stats;
            memberName.text = partyMember.DisplayName;

            //memberLevel.text = stats.LV.ToString();
            memberHP.text = $"{stats.HP}/{stats.MAXHP}";
            memberMP.text = $"{stats.MP}/{stats.MAXMP}";

            setColor(stats.HP, stats.MAXHP);
        }

        public void updateHealth(int hp, int maxHp)
        {
            memberHP.text = $"{hp}/{maxHp}";
            setColor(hp, maxHp);
        }

        public void updateMP(int mp, int maxMp)
        {
            memberHP.text = $"{mp}/{maxMp}";
        }


        private void setColor(int hp, int maxHp)
        {
            Color textColor = GetHpColor(hp, maxHp);
            HPText.color = textColor;
            MPText.color = textColor;
            memberName.color = textColor;
            memberHP.color = textColor;
            memberMP.color = textColor;
        }

        private Color GetHpColor(int hp, int maxHp)
        {
            float percentage = (float)hp / maxHp;

            if (percentage <= 0) // Dead
                return Color.red;
            else if (percentage < 0.3f) // Low health
                return Color.Lerp(Color.red, Color.yellow, percentage / 0.3f); // Gradually change from red to yellow as the percentage increases
            else // Normal health
                return Color.white;
        }
    }
}
