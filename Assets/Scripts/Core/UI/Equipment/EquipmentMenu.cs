using Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class EquipmentMenu : MonoBehaviour
    {

        public PartyMember partyMember { get; private set; }

        [Header("Party Member")]
        [SerializeField] private Image partyMemberFace;
        [SerializeField] private TextMeshProUGUI displayName;
        [SerializeField] private TextMeshProUGUI lvlVal;
        [SerializeField] private TextMeshProUGUI HPVal;
        [SerializeField] private TextMeshProUGUI MPVal;
        [SerializeField] private TextMeshProUGUI attackVal;
        [SerializeField] private TextMeshProUGUI magicAtkVal;
        [SerializeField] private TextMeshProUGUI defenseVal;
        [SerializeField] private TextMeshProUGUI magicDefVal;
        [SerializeField] private TextMeshProUGUI speedVal;
        [SerializeField] private TextMeshProUGUI evasionVal;

        [Header("Equipment")]
        [SerializeField] private TextMeshProUGUI weaponName;
        [SerializeField] private TextMeshProUGUI headName;
        [SerializeField] private TextMeshProUGUI armsName;
        [SerializeField] private TextMeshProUGUI chestName;
        [SerializeField] private TextMeshProUGUI legsName;
        [SerializeField] private TextMeshProUGUI accessoryName;


        public void initValues(PartyMember member)
        {
            partyMember = member;
            updateValues();
        }

        public void updateValues()
        {
            updateEquipmentInfo();
            updatePartyMember();
        }

        public void updatePartyMember()
        {

            //Top Values
            partyMemberFace.sprite = partyMember.MenuPortrait; //TODO FIX ME 
            displayName.text = partyMember.DisplayName;
            Stats sts = partyMember.Stats;
            lvlVal.text = sts.LV.ToString();
            HPVal.text = $"{sts.HP}/{sts.MAXHP}";
            MPVal.text = $"{sts.MP}/{sts.MAXMP}";

            //Stats Values
            attackVal.text = sts.ATK.ToString();
            magicAtkVal.text = sts.MATK.ToString();
            defenseVal.text = sts.DEF.ToString();
            magicDefVal.text = sts.MDEF.ToString();
            speedVal.text = sts.SPD.ToString();
            evasionVal.text = sts.EVS.ToString();
        }

        public void updateEquipmentInfo()
        {
            Equipment eqpmt = partyMember.equipment;

            weaponName.text = eqpmt.getEquipped(EquippableType.Weapon).DisplayName;
            headName.text = eqpmt.getEquipped(EquippableType.Head).DisplayName;
            armsName.text = eqpmt.getEquipped(EquippableType.Arms).DisplayName;
            chestName.text = eqpmt.getEquipped(EquippableType.Chest).DisplayName;
            legsName.text = eqpmt.getEquipped(EquippableType.Legs).DisplayName;
            accessoryName.text = eqpmt.getEquipped(EquippableType.Accesesory).DisplayName;
        }
    }
}
