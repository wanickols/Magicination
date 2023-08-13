using Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class EquipmentMenu : MonoBehaviour
    {
        [Header("Party Member")]
        [SerializeField] private Image partyMemberFace;
        [SerializeField] private TextMeshProUGUI name;
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

        public void updateValues(PartyMember partyMember)
        {
            updateEquipmentInfo(partyMember);
            updatePartyMember(partyMember);
        }

        public void updatePartyMember(PartyMember member)
        {

            //Top Values
            partyMemberFace.sprite = member.MenuPortrait; //TODO FIX ME 
            name.text = member.DisplayName;
            Stats sts = member.Stats;
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

        public void updateEquipmentInfo(PartyMember member)
        {
            Equipment eqpmt = member.equipment;

            weaponName.text = eqpmt.getEquipped(EquipmentType.Weapon).name;
            headName.text = eqpmt.getEquipped(EquipmentType.Head).name;
            armsName.text = eqpmt.getEquipped(EquipmentType.Arms).name;
            chestName.text = eqpmt.getEquipped(EquipmentType.Chest).name;
            legsName.text = eqpmt.getEquipped(EquipmentType.Legs).name;
            accessoryName.text = eqpmt.getEquipped(EquipmentType.Accesesory).name;
        }
    }
}
