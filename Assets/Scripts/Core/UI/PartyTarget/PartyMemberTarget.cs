using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class PartyMemberTarget : MonoBehaviour
    {
        [SerializeField] private Image avatar;
        [SerializeField] private TextMeshProUGUI Name, HP, MP;


        public PartyMember member { get; private set; }

        public void setMember(PartyMember member)
        {
            this.member = member;
            updateValues();
        }

        public void updateValues()
        {
            avatar.sprite = member.MenuPortrait;
            Name.text = member.DisplayName;
            HP.text = $"{member.Stats.HP}/{member.Stats.MAXHP}";
            MP.text = $"{member.Stats.MP}/{member.Stats.MAXMP}";
        }
    }
}