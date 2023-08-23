using Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class EquipMenu : MonoBehaviour
    {
        /// Public Parameters
        public PartyMember partyMember { get; private set; }


        /// Private Paremeters
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
        [SerializeField] private EquippableOption weaponName;
        [SerializeField] private EquippableOption headName;
        [SerializeField] private EquippableOption armsName;
        [SerializeField] private EquippableOption chestName;
        [SerializeField] private EquippableOption legsName;
        [SerializeField] private EquippableOption accessoryName;

        /// Public
        //Init
        public void initValues(PartyMember member)
        {
            partyMember = member;
            partyMember.equipment.changedEquipment += updateValues;
            updateValues();
        }

        public void updateValues()
        {
            updateEquipmentInfo();
            updatePartyMember();
        }

        /// Private

        //Stats
        private void updatePartyMember()
        {

            //Top Values
            partyMemberFace.sprite = partyMember.MenuPortrait; /// TODO FIX ME 
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

        //Equipment List
        private void updateEquipmentInfo()
        {
            Equipment eqpmt = partyMember.equipment;

            weaponName.changeOption(eqpmt.getEquipped(EquippableType.Weapon));
            headName.changeOption(eqpmt.getEquipped(EquippableType.Head));
            armsName.changeOption(eqpmt.getEquipped(EquippableType.Arms));
            chestName.changeOption(eqpmt.getEquipped(EquippableType.Chest));
            legsName.changeOption(eqpmt.getEquipped(EquippableType.Legs));
            accessoryName.changeOption(eqpmt.getEquipped(EquippableType.Accesesory));
        }

        //Destroy
        private void OnDestroy()
        {
            if (partyMember != null)
                partyMember.equipment.changedEquipment -= updateValues;
        }
    }
}
