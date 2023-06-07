using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        partyMember = Party.ActiveMembers[0];
        memberName.text = partyMember.Name;
        partyMemberPortrait = partyMember.portrait;
        GetStats();
    }

    public void GetStats()
    {

        memberLevel.text = partyMember.Stats.LV.ToString();
        memberHP.text = $"{partyMember.Stats.HP}/{partyMember.Stats.MAXHP}";
        memberMP.text = $"{partyMember.Stats.MP}/{partyMember.Stats.MAXMP}";
        memberCurrEXP.text = partyMember.Stats.EXP.ToString();
        memberNextEXP.text = partyMember.Stats.NXTEXP.ToString();


    }
}
