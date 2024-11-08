using Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class PartyMemberInfo : MonoBehaviour
    {
        private PartyMember partyMember;

        [SerializeField] private Image partyMemberPortrait;
        [SerializeField] private TextMeshProUGUI memberName;
        [SerializeField] private TextMeshProUGUI memberLevel;
        [SerializeField] private TextMeshProUGUI memberHP;
        [SerializeField] private TextMeshProUGUI memberMP;
        [SerializeField] private TextMeshProUGUI memberCurrEXP;
        [SerializeField] private TextMeshProUGUI memberNextEXP;

        // Start is called before the first frame update
        void Start()
        {
            int siblingIndex = this.gameObject.transform.GetSiblingIndex();
            partyMember = Party.ActiveMembers[siblingIndex];

            DisplayInfo();
        }

        public void DisplayInfo()
        {
            Stats stats = partyMember.Stats;
            memberName.text = partyMember.Name;
            partyMemberPortrait.sprite = partyMember.MenuPortrait;

            memberLevel.text = stats.LV.ToString();
            memberHP.text = $"{stats.HP}/{stats.MAXHP}";
            memberMP.text = $"{stats.MP}/{stats.MAXMP}";
            memberCurrEXP.text = stats.EXP.ToString();
            memberNextEXP.text = stats.NXTEXP.ToString();


        }
    }
}
