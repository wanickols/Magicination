using TMPro;
using UnityEngine;

namespace Battle
{
    public class StatContainer : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI memberName;
        [SerializeField] private TextMeshProUGUI memberHP;
        [SerializeField] private TextMeshProUGUI memberMP;

        public void DisplayInfo(PartyMember partyMember)
        {
            Stats stats = partyMember.Stats;
            memberName.text = partyMember.Name;

            //memberLevel.text = stats.LV.ToString();
            memberHP.text = $"{stats.HP}/{stats.MAXHP}";
            memberMP.text = $"{stats.MP}/{stats.MAXMP}";
        }

        public void updateHealth(int hp, int maxHp)
        {
            memberHP.text = $"{hp}/{maxHp}";
        }

        public void updateMP(int mp, int maxMp)
        {
            memberHP.text = $"{mp}/{maxMp}";
        }

    }
}
